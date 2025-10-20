using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Views_Renamer.ExEvt;
using Color = System.Drawing.Color;
using Form = System.Windows.Forms.Form;
using TextBox = System.Windows.Forms.TextBox;

namespace Views_Renamer.UI
{
    public partial class INFRAform : Form
    {
        //public static List<View> ele;
        //public static List<View> sec;
        //public static List<View> three_d;
        public List<string> _viewsByCategory;
        public static List<string> selected_;
        public static string selection;
        public static string newvalue;
        public static ListBox elevations_;
        public static ListBox sections_;
        public static ListBox threeds;
        private bool _suppressEvents = false;
        public INFRAform()
        {
            InitializeComponent();
            InitializePlaceholders();
            // Hook events
            newname.Enter += TextBox_Enter;
            newname.Leave += TextBox_Leave;
            //elevations.MouseDown += listBox_MouseDown;
            //sections.MouseDown += listBox_MouseDown;
            //threed.MouseDown += listBox_MouseDown;
            Data.elevdic.Clear();
            Data.secdic.Clear();
            Data.threeddic.Clear();
            elevations.Items.Clear();
            sections.Items.Clear();
            threed.Items.Clear();
            RvtUtils.PLCollectRestViews(ExCmd.doc);
            _viewsByCategory = Data.INFRAViewCategories;
            elevations_ = elevations;
            sections_ = sections;
            threeds = threed;
        }
        // EVENTS
        private void TextBox_GlobalKeyDown(object sender, KeyEventArgs e)
        {

        }
        private void InitializePlaceholders()
        {
            SetPlaceholder(newname, "Enter new name");
        }
        private void SetPlaceholder(TextBox tb, string placeholder)
        {
            if (string.IsNullOrEmpty(tb.Text) || tb.Text == tb.Tag as string)
            {
                tb.ForeColor = Color.SeaGreen;
                tb.Font = new Font(tb.Font, FontStyle.Italic);
                tb.Text = placeholder;
                tb.Tag = placeholder; // remember the placeholder text
            }
        }
        private void RemovePlaceholder(TextBox tb)
        {
            if (tb.Text == tb.Tag as string)
            {
                tb.Text = "";
                tb.ForeColor = Color.FromArgb(0, 101, 96);
            }
        }
        private void TextBox_Enter(object sender, EventArgs e)
        {
            RemovePlaceholder((TextBox)sender);
        }
        private void TextBox_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (string.IsNullOrEmpty(tb.Text))
            {
                SetPlaceholder(tb, tb.Tag as string);
            }
        }
        // EVENTS
        private void HandleExclusiveSelection(ListBox activeList, ListBox[] others)
        {
            try
            {
                _suppressEvents = true;

                // Clear selection in the other list boxes
                foreach (var lb in others)
                    lb.ClearSelected();

                // Ensure we only allow one selection in the active one
                if (activeList.SelectedIndices.Count > 1)
                {
                    int lastIndex = activeList.SelectedIndices[activeList.SelectedIndices.Count - 1];
                    activeList.ClearSelected();
                    activeList.SelectedIndex = lastIndex;
                }
            }
            finally
            {
                _suppressEvents = false;
            }
        }
        private void listBox_MouseDown(object sender, MouseEventArgs e)
        {
            var lb = sender as ListBox;
            int index = lb.IndexFromPoint(e.Location);

            // If user clicked the same selected item -> deselect it manually
            if (index == lb.SelectedIndex)
            {
                lb.ClearSelected();
            }
        }
        private void INFRAform_Load(object sender, EventArgs e)
        {
            if (_viewsByCategory == null) return;
            foreach (var cat in _viewsByCategory)
                cattegories.Items.Add(cat);
        }
        private void edecs_Click(object sender, EventArgs e)
        {
            string url = @"https://www.edecs.com/";

            if (!string.IsNullOrEmpty(url))
            {
                try
                {
                    // Open the URL in the default browser
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = url,
                        UseShellExecute = true // Required to use the default browser
                    });

                }
                catch (Exception ex)
                {
                    TaskDialog.Show("Error", $"Error opening the URL: {ex.Message}");
                }
            }
            else
            {
                TaskDialog.Show("Error", "No URL entered.");
            }
        }
        private void cattegories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cattegories.SelectedItem == null) return;
            string selected = this.cattegories.SelectedItem.ToString();
            elevations.Items.Clear();
            sections.Items.Clear();
            threed.Items.Clear();

            switch (selected)
            {
                case "00_Work in Progress":
                    RvtUtils.Switcher(selected, elevations, sections, threed);
                    break;
                case "01_General":
                    RvtUtils.Switcher(selected, elevations, sections, threed);
                    break;
                case "02_Storm Water":
                    RvtUtils.Switcher(selected, elevations, sections, threed);
                    break;
                case "03_Swege":
                    RvtUtils.Switcher(selected, elevations, sections, threed);
                    break;
                case "04_Water Supply":
                    RvtUtils.Switcher(selected, elevations, sections, threed);
                    break;
                case "05_Firefighting":
                    RvtUtils.Switcher(selected, elevations, sections, threed);
                    break;
                case "06_Irrigation":
                    RvtUtils.Switcher(selected, elevations, sections, threed);
                    break;
                case "07_CCTV":
                    RvtUtils.Switcher(selected, elevations, sections, threed);
                    break;
                case "08_Public Address":
                    RvtUtils.Switcher(selected, elevations, sections, threed);
                    break;
                case "09_LV Network":
                    RvtUtils.Switcher(selected, elevations, sections, threed);
                    break;
                case "10_Fire Alarm":
                    RvtUtils.Switcher(selected, elevations, sections, threed);
                    break;
                case "11_Low Current":
                    RvtUtils.Switcher(selected, elevations, sections, threed);
                    break;
                case "12_MV Network":
                    RvtUtils.Switcher(selected, elevations, sections, threed);
                    break;
                case "13_Ductbanks":
                    RvtUtils.Switcher(selected, elevations, sections, threed);
                    break;
            }
        }
        private void elevations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            HandleExclusiveSelection(elevations, new[] { sections, threed });
        }
        private void sections_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            HandleExclusiveSelection(sections, new[] { elevations, threed });
        }
        private void threed_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            HandleExclusiveSelection(threed, new[] { elevations, sections });
        }
        private void select_Click(object sender, EventArgs e)
        {
            //if (viewcategories.SelectedItem == null) return;
            //string selected = viewcategories.SelectedItem.ToString();
            var selection_ = cattegories.SelectedItems.Cast<string>().ToList();
            //selection = selected;
            selected_ = selection_;
            ExCmd.exevt.request = Request.INFRACollector;
            ExCmd.exevthan.Raise();
        }
        private void renamer_Click(object sender, EventArgs e)
        {
            string newValue = newname.Text.Trim();
            newvalue = newValue;

            if (string.IsNullOrEmpty(newValue))
            {
                TaskDialog.Show("Error", "Please enter a new value in the text box.");
                return;
            }
            ExCmd.exevt.request = Request.INFRArenamer;
            ExCmd.exevthan.Raise();
        }
    }
}
