using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Views_Renamer.UI
{
    public partial class MainInterface : Form
    {
        private static Mainform ARform = null;
        private static STform STform = null;
        //private static Mainform maininterface = null;
        public MainInterface()
        {
            InitializeComponent();
        }

        private void arc_Click(object sender, EventArgs e)
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
                    this.Show();
                    ARform = null;
                };

                this.Hide();
                ARform.Show();
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Error",ex.Message);
            }
        }

        private void str_Click(object sender, EventArgs e)
        {
            try
            {
                // If already open, just bring it to front
                if (STform != null && !STform.IsDisposed)
                {
                    STform.BringToFront();
                    return;
                }

                STform = new STform();

                // When ARform closes, re-show this interface
                STform.FormClosed += (s, args) =>
                {
                    this.Show();
                    STform = null;
                };

                this.Hide();
                STform.Show();
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Error", ex.Message);
            }
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

        private void elec_Click(object sender, EventArgs e)
        {

        }

        private void hvac_Click(object sender, EventArgs e)
        {

        }

        private void ff_Click(object sender, EventArgs e)
        {

        }

        private void pb_Click(object sender, EventArgs e)
        {

        }
    }
}
