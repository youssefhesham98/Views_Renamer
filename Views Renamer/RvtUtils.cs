using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.DB.IFC;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using Views_Renamer.UI;
using Form = System.Windows.Forms.Form;
using ListBox = System.Windows.Forms.ListBox;
using View = Autodesk.Revit.DB.View;

namespace Views_Renamer
{
    public class RvtUtils
    {
        /// <summary>
        /// Collect all Elevation, Section and 3D views in the document and categorize them based on the "View Category" parameter.
        /// </summary>
        /// <param name="doc"></param>
        public static void CollectRestViews(Document doc)
        {
            #region trenascation
            //using (Transaction tns = new Transaction(doc, "collector"))
            //{
            //    tns.Start();
            //}
            //tns.Commit();
            #endregion
            var restViews = new FilteredElementCollector(doc)
                   .OfClass(typeof(View))
                   .Cast<View>()
                   .Where(v => !v.IsTemplate)
                   .Where(v =>
                       v.ViewType == ViewType.Elevation ||
                       v.ViewType == ViewType.Section ||
                       v.ViewType == ViewType.ThreeD)
                   .ToList();

            foreach (var view in restViews)
            {
                if (view.ViewType == ViewType.Elevation)
                {
                    Parameter param = view.LookupParameter("View Category");
                    if (param == null || !param.HasValue) continue;
                    string paramValue = param.AsString();
                    if (string.IsNullOrEmpty(paramValue)) continue;
                    //Match only values from our list
                    if (Data.ViewCategories.Contains(paramValue))
                    {
                        if (!Data.elevdic.ContainsKey(paramValue))
                            Data.elevdic[paramValue] = new List<View>();
                            Data.elevdic[paramValue].Add(view);
                            Data.elevations[view.Name] = view;
                    }
                }
                else if (view.ViewType == ViewType.Section)
                {
                    Parameter param = view.LookupParameter("View Category");
                    if (param == null || !param.HasValue) continue;
                    string paramValue = param.AsString();
                    if (string.IsNullOrEmpty(paramValue)) continue;
                    //Match only values from our list
                    if (Data.ViewCategories.Contains(paramValue))
                    {
                        if (!Data.secdic.ContainsKey(paramValue))
                            Data.secdic[paramValue] = new List<View>();
                            Data.secdic[paramValue].Add(view);
                            Data.sections[view.Name] = view;
                    }   
                }
                else if (view.ViewType == ViewType.ThreeD)
                {
                    Parameter param = view.LookupParameter("View Category");
                    if (param == null || !param.HasValue) continue;
                    string paramValue = param.AsString();
                    if (string.IsNullOrEmpty(paramValue)) continue;
                    //Match only values from our list
                    if (Data.ViewCategories.Contains(paramValue))
                    {
                        if (!Data.threeddic.ContainsKey(paramValue))
                            Data.threeddic[paramValue] = new List<View>();
                            Data.threeddic[paramValue].Add(view);
                            Data.threed[view.Name] = view;
                    }
                }
            }
        }
        /// <summary>
        /// switcher method to populate list boxes based on selected category
        /// </summary>
        /// <param name="selected"></param>
        /// <param name="elevations"></param>
        /// <param name="sections"></param>
        /// <param name="threed"></param>
        public static void Switcher(string selected, ListBox elevations, ListBox sections, ListBox threed)
        {
            if (Data.elevdic.TryGetValue(selected, out var elevs))
            {
                foreach (var view in elevs)
                {
                    elevations.Items.Add(view.Name.ToString());
                }
            }
            if (Data.secdic.TryGetValue(selected, out var secs))
            {
                foreach (var view in secs)
                {
                    sections.Items.Add(view.Name.ToString());
                }
            }
            if (Data.threeddic.TryGetValue(selected, out var threeds))
            {
                foreach (var view in threeds)
                {

                    threed.Items.Add(view.Name.ToString());
                }
            }
        }
        /// <summary>
        /// rename selected view from any of the 3 list boxes
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="elevations"></param>
        /// <param name="sections"></param>
        /// <param name="threed"></param>
        /// <param name="newValue"></param>
        public static void Renamer(Document doc, ListBox elevations,ListBox sections, ListBox threed, string newValue)
        {
            using (Transaction tns = new Transaction(doc, "Renamer"))
            {
                tns.Start();
                // Check all 3 list boxes
                if (elevations.SelectedIndex != -1)
                {
                    View view = Data.elevations[elevations.SelectedItem.ToString()];
                    view.Name = newValue;
                    elevations.Items[elevations.SelectedIndex] = newValue;
                }
                else if (sections.SelectedIndex != -1)
                {
                    View view = Data.sections[sections.SelectedItem.ToString()];
                    view.Name = newValue;
                    sections.Items[sections.SelectedIndex] = newValue;
                }
                else if (threed.SelectedIndex != -1)
                {
                    View view = Data.threed[threed.SelectedItem.ToString()];
                    view.Name = newValue;
                    threed.Items[threed.SelectedIndex] = newValue;
                }
                else
                {
                    TaskDialog.Show("Error", "Please select an item from one of the lists.");
                }
                tns.Commit();
            }
        }
        /// <summary>
        /// collector method to rename floor and ceiling plans based on selected categories from the form
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="ParammValue"></param>
        /// <param name="selection"></param>
        public static void Collector(Document doc, string ParammValue, List<string> selection)
        {
            using (Transaction tns = new Transaction(doc, "Renamer"))
            {
                tns.Start();
                // Collect all views except templates
                var allViews = new FilteredElementCollector(doc)
                    .OfClass(typeof(View))
                    .Cast<View>()
                    .Where(v => !v.IsTemplate)
                    .Where(v =>
                        v.ViewType == ViewType.FloorPlan ||
                        v.ViewType == ViewType.CeilingPlan) 
                    .OrderBy(v => v.GenLevel.Elevation)
                    .ToList();

                // Collect all levels in the project
                var allLevels = new FilteredElementCollector(doc)
                    .OfClass(typeof(Level))
                    .Cast<Level>()
                    .OrderBy(l => l.Elevation)
                    .ToList();
                var levelIds = new HashSet<ElementId>(allLevels.Select(l => l.Id));

                var viewsByCategory = new Dictionary<string, List<View>>();
                var floorplans = new Dictionary<string, List<View>>();
                var ceilingplans = new Dictionary<string, List<View>>();
                StringBuilder sb = new StringBuilder();

                try
                {
                    foreach (var view in allViews)
                    {
                        Level lvl = null;
                        if (view is ViewPlan vp && vp.GenLevel != null)
                        {
                            lvl = doc.GetElement(vp.GenLevel.Id) as Level;
                        }
                        if (lvl != null && levelIds.Contains(lvl.Id))
                        {
                            if (view.ViewType == ViewType.FloorPlan)
                            {
                                Parameter param = view.LookupParameter("View Category");
                                if (param == null || !param.HasValue) continue;
                                string paramValue = param.AsString();
                                if (string.IsNullOrEmpty(paramValue)) continue;
                                //Match only values from our list
                                if (Data.ViewCategories.Contains(paramValue))
                                {
                                    if (!floorplans.ContainsKey(paramValue))
                                        floorplans[paramValue] = new List<View>();
                                        floorplans[paramValue].Add(view);
                                }
                            }
                            else if (view.ViewType == ViewType.CeilingPlan)
                            {
                                Parameter param = view.LookupParameter("View Category");
                                if (param == null || !param.HasValue) continue;
                                string paramValue = param.AsString();
                                if (string.IsNullOrEmpty(paramValue)) continue;
                                //Match only values from our list
                                if (Data.ViewCategories.Contains(paramValue))
                                {
                                    if (!ceilingplans.ContainsKey(paramValue))
                                        ceilingplans[paramValue] = new List<View>();
                                        ceilingplans[paramValue].Add(view);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    TaskDialog.Show("Error", ex.Message);
                }
                foreach (var select in selection)
                {
                    if (floorplans.TryGetValue(select, out var plans))
                    {
                        if (Data.prefixMap.TryGetValue(select, out string prefix))
                        {
                            int k = 0;
                            foreach (var view in plans)
                            {
                                var levelName = view.GenLevel != null ? view.GenLevel.Name : "XX";
                                var name = $"{prefix}_{k + 1:00}_{levelName}_PLN_XX";
                                view.Name = name;
                                k++;
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else if (ceilingplans.TryGetValue(select, out var cplans))
                    {
                        {
                            if (Data.prefixMap.TryGetValue(select, out string prefix))
                            {
                                int k = 0;
                                foreach (var view in cplans)
                                {
                                    var levelName = view.GenLevel != null ? view.GenLevel.Name : "XX";
                                    var name = $"{prefix}_{k + 1:00}_{levelName}_PLN_XX";
                                    view.Name = name;
                                    k++;
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    //foreach (var select in selection)
                    //{
                    //    if (ceilingplans.TryGetValue(select, out var cplans))
                    //    {
                    //        if (Data.prefixMap.TryGetValue(select, out string prefix))
                    //        {
                    //            int k = 0;
                    //            foreach (var view in cplans)
                    //            {
                    //                var name = $"{prefix}_{k + 1:00}_{Data.Levels[k]}_PLN_XX";
                    //                view.Name = name;
                    //                k++;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            continue;
                    //        }
                    //    }
                    //}
                }
                tns.Commit();
                #region Print
                //foreach (var category in floorplans)
                //{
                //    if (category.Key == ParammValue)
                //    {
                //        List<View> viewsInCategory = floorplans[ParammValue];
                //        foreach (var view in viewsInCategory)
                //        { 
                //            sb.AppendLine($"Category: {category.Key}, View Name: {view.Name}");
                //        }
                //    }
                //    else
                //    {
                //        continue;
                //    }
                //}

                //foreach (var category in ceilingplans)
                //{
                //    if (category.Key == ParammValue)
                //    {
                //        List<View> viewsInCategory = ceilingplans[ParammValue];
                //        foreach (var view in viewsInCategory)
                //        {
                //            sb.AppendLine($"Category: {category.Key}, View Name: {view.Name}");
                //        }
                //    }
                //    else
                //    {
                //        continue;
                //    }
                //}
                #endregion
                #region Old
                //if (select == "00_Work In Progress")
                //{
                //    int k = 0;
                //    foreach (var view in plans)
                //    {
                //        var name = $"WIP_{k + 1:00}_{Data.Levels[k]}_PLN_XX";
                //        view.Name = name;
                //        k++;
                //    }
                //}
                //else if (select == "01_Blockwork Plans")
                //{
                //    int k = 0;
                //    foreach (var view in plans)
                //    {
                //        var name = $"BW_{k + 1:00}_{Data.Levels[k]}_PLN_XX";
                //        view.Name = name;
                //        k++;
                //    }
                //}
                //else if (select == "02_Flooring")
                //{
                //    int k = 0;
                //    foreach (var view in plans)
                //    {
                //        var name = $"fl_{k + 1:00}_{Data.Levels[k]}_PLN_XX";
                //        view.Name = name;
                //        k++;
                //    }
                //}
                //else if (select == "03_Ceiling")
                //{
                //    continue;
                //}
                //else if (select == "04_Furniture")
                //{
                //    int k = 0;
                //    foreach (var view in plans)
                //    {
                //        var name = $"FN_{k + 1:00}_{Data.Levels[k]}_PLN_XX";
                //        view.Name = name;
                //        k++;
                //    }
                //}
                //else if (select == "05_Elevations")
                //{
                //    continue;
                //}
                //else if (select == "06_Interior Design")
                //{
                //    int k = 0;
                //    foreach (var view in plans)
                //    {
                //        var name = $"ID_{k + 1:00}_{Data.Levels[k]}_PLN_XX";
                //        view.Name = name;
                //        k++;
                //    }
                //}
                //else if (select == "07_Sttairs Details")
                //{
                //    int k = 0;
                //    foreach (var view in plans)
                //    {
                //        var name = $"SD_{k + 1:00}_{Data.Levels[k]}_PLN_XX";
                //        view.Name = name;
                //        k++;
                //    }
                //}
                //else if (select == "08_Doors and Windows")
                //{
                //    int k = 0;
                //    foreach (var view in plans)
                //    {
                //        var name = $"DW_{k + 1:00}_{Data.Levels[k]}_PLN_XX";
                //        view.Name = name;
                //        k++;
                //    }
                //}
                //else if (select == "09_General Details")
                //{
                //    int k = 0;
                //    foreach (var view in plans)
                //    {
                //        var name = $"GD_{k + 1:00}_{Data.Levels[k]}_PLN_XX";
                //        view.Name = name;
                //        k++;
                //    }
                //}
                //else if (select == "10_Landscapr")
                //{
                //    int k = 0;
                //    foreach (var view in plans)
                //    {
                //        var name = $"LS_{k + 1:00}_{Data.Levels[k]}_PLN_XX";
                //        view.Name = name;
                //        k++;
                //    }
                //}
                //else if (select == "11_Life Safety")
                //{
                //    int k = 0;
                //    foreach (var view in plans)
                //    {
                //        var name = $"LF_{k + 1:00}_{Data.Levels[k]}_PLN_XX";
                //        view.Name = name;
                //        k++;
                //    }
                //}
                //else
                //{
                //    TaskDialog.Show("Error", "No matching category found.");
                //}
                #endregion
            }
        }
        public static void STCollector(Document doc, List<string> selection)
        {
            using (Transaction tns = new Transaction(doc, "Renamer"))
            {
                tns.Start();
                // Collect all views except templates
                var allViews = new FilteredElementCollector(doc)
                    .OfClass(typeof(View))
                    .Cast<View>()
                    .Where(v => !v.IsTemplate)
                    .Where(v =>
                        v.ViewType == ViewType.EngineeringPlan ||
                        v.ViewType == ViewType.CeilingPlan)
                    .OrderBy(v => v.GenLevel.Elevation)
                    .ToList();

                // Collect all levels in the project
                var allLevels = new FilteredElementCollector(doc)
                    .OfClass(typeof(Level))
                    .Cast<Level>()
                    .OrderBy(l => l.Elevation)
                    .ToList();
                var levelIds = new HashSet<ElementId>(allLevels.Select(l => l.Id));

                var viewsByCategory = new Dictionary<string, List<View>>();
                var floorplans = new Dictionary<string, List<View>>();
                var ceilingplans = new Dictionary<string, List<View>>();
                try
                {
                    foreach (var view in allViews)
                    {
                        Level lvl = null;
                        if (view is ViewPlan vp && vp.GenLevel != null)
                        {
                            lvl = doc.GetElement(vp.GenLevel.Id) as Level;
                        }
                        if (lvl != null && levelIds.Contains(lvl.Id))
                        {
                            if (view.ViewType == ViewType.EngineeringPlan)
                            {
                                Parameter param = view.LookupParameter("View Category");
                                if (param == null || !param.HasValue) continue;
                                string paramValue = param.AsString();
                                if (string.IsNullOrEmpty(paramValue)) continue;
                                //Match only values from our list
                                if (Data.STViewCategories.Contains(paramValue))
                                {
                                    if (!floorplans.ContainsKey(paramValue))
                                        floorplans[paramValue] = new List<View>();
                                        floorplans[paramValue].Add(view);
                                }
                            }
                            else if (view.ViewType == ViewType.CeilingPlan)
                            {
                                Parameter param = view.LookupParameter("View Category");
                                if (param == null || !param.HasValue) continue;
                                string paramValue = param.AsString();
                                if (string.IsNullOrEmpty(paramValue)) continue;
                                //Match only values from our list
                                if (Data.STViewCategories.Contains(paramValue))
                                {
                                    if (!ceilingplans.ContainsKey(paramValue))
                                        ceilingplans[paramValue] = new List<View>();
                                        ceilingplans[paramValue].Add(view);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    TaskDialog.Show("Error", ex.Message);
                }
                foreach (var select in selection)
                {
                    if (floorplans.TryGetValue(select, out var plans))
                    {
                        if (Data.STprefixMap.TryGetValue(select, out string prefix))
                        {
                            int k = 0;
                            foreach (var view in plans)
                            {
                                var levelName = view.GenLevel != null ? view.GenLevel.Name : "XX";
                                var name = $"{prefix}_{k + 1:00}_{levelName}_PLN_XX";
                                view.Name = name;
                                k++;
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else if (ceilingplans.TryGetValue(select, out var cplans))
                    {
                        {
                            if (Data.STprefixMap.TryGetValue(select, out string prefix))
                            {
                                int k = 0;
                                foreach (var view in cplans)
                                {
                                    var levelName = view.GenLevel != null ? view.GenLevel.Name : "XX";
                                    var name = $"{prefix}_{k + 1:00}_{levelName}_PLN_XX";
                                    view.Name = name;
                                    k++;
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                }
                tns.Commit();
            }
        }
        /// <summary>
        /// separate collector method to categorize views based on their types
        /// </summary>
        /// <param name="doc"></param>
        public static void SeperateCollector(Document doc)
        {
            // Collect all views in the project (excluding templates)
            var allViews = new FilteredElementCollector(doc)
                .OfClass(typeof(View))
                .Cast<View>()
                .Where(v => !v.IsTemplate)
                .ToList();

            // Filter by view type
            var floorPlans = allViews
                .Where(v => v.ViewType == ViewType.FloorPlan)
                .ToList();

            var ceilingPlans = allViews
                .Where(v => v.ViewType == ViewType.CeilingPlan)
                .ToList();

            var elevations = allViews
                .Where(v => v.ViewType == ViewType.Elevation)
                .ToList();

            var sections = allViews
                .Where(v => v.ViewType == ViewType.Section)
                .ToList();

            var threeDViews = allViews
                .Where(v => v.ViewType == ViewType.ThreeD)
                .ToList();
        }
        /// <summary>
        /// Windows form runner
        /// </summary>
        /// <param name="ARform"></param>
        /// <param name="main"></param>
        public static void FormRunner(Form ARform , Form main)
        {
            try
            {
                // If already open, just bring it to front
                if (ARform != null && !ARform.IsDisposed)
                {
                    ARform.BringToFront();
                    return;
                }

                ARform = new Mainform();

                // When ARform closes, re-show this interface
                ARform.FormClosed += (s, args) =>
                {
                    main.Show();
                    ARform = null;
                };

                 main.Hide();
                ARform.Show();
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Error", ex.Message);
            }
        }
    }
}
