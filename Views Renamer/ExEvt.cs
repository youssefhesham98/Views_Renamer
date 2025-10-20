using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Views_Renamer.UI;

namespace Views_Renamer
{
    public class ExEvt : IExternalEventHandler
    {
        public Request request { get; set; }
        public void Execute(UIApplication app)
        {
            switch (request)
            {
                case Request.Collect:
                    RvtUtils.Collector(ExCmd.doc, Mainform.selection,Mainform.selected_);
                    break;
                case Request.Rename:
                    RvtUtils.Renamer(ExCmd.doc,Mainform.elevations_,Mainform.sections_,Mainform.threeds,Mainform.newvalue);
                    break;
                case Request.STCollector:
                    RvtUtils.STCollector(ExCmd.doc, STform.selected_);
                    break;
                case Request.STrenamer:
                    RvtUtils.Renamer(ExCmd.doc, STform.elevations_, STform.sections_, STform.threeds, STform.newvalue);
                    break;
                case Request.ELCollector:
                    RvtUtils.ELCollector(ExCmd.doc, ELform.selected_);
                    break;
                case Request.ELrenamer:
                    RvtUtils.Renamer(ExCmd.doc, ELform.elevations_, ELform.sections_, ELform.threeds, ELform.newvalue);
                    break;
                case Request.FFCollector:
                    RvtUtils.MECollector(ExCmd.doc, FFform.selected_);
                    break;
                case Request.FFrenamer:
                    RvtUtils.Renamer(ExCmd.doc, FFform.elevations_, FFform.sections_, FFform.threeds, FFform.newvalue);
                    break;
                case Request.HVACCollector:
                    RvtUtils.MECollector(ExCmd.doc, hvacform.selected_);
                    break;
                case Request.HVACrenamer:
                    RvtUtils.Renamer(ExCmd.doc, hvacform.elevations_, hvacform.sections_, hvacform.threeds, hvacform.newvalue);
                    break;
                case Request.PLCollector:
                    RvtUtils.PLCollector(ExCmd.doc, PLform.selected_);
                    break;
                case Request.PLrenamer:
                    RvtUtils.Renamer(ExCmd.doc, PLform.elevations_, PLform.sections_, PLform.threeds, PLform.newvalue);
                    break;
                case Request.INFRACollector:
                    RvtUtils.INFRFACollector(ExCmd.doc, INFRAform.selected_);
                    break;
                case Request.INFRArenamer:
                    RvtUtils.Renamer(ExCmd.doc, INFRAform.elevations_, INFRAform.sections_, INFRAform.threeds, INFRAform.newvalue);
                    break;
            }
        }

        public string GetName()
        {
            return "EDECS Toolkit";
        }
        public enum Request
        {
            Collect,
            Rename,
            STCollector,
            STrenamer,
            ELCollector,
            ELrenamer,
            FFCollector,
            FFrenamer,
            HVACCollector,
            HVACrenamer,
            PLCollector,
            PLrenamer,
            INFRACollector,
            INFRArenamer
        }

    }
}
