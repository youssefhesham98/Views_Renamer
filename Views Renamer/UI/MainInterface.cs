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
        private static ELform Elform = null;
        private static FFform FFform = null;
        private static PLform PLform = null;
        private static hvacform hvacform = null;
        private static INFRAform infraform = null;
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
            try
            {
                // If already open, just bring it to front
                if (Elform != null && !Elform.IsDisposed)
                {
                    ARform.BringToFront();
                    return;
                }

                Elform = new ELform();

                // When ARform closes, re-show this interface
                Elform.FormClosed += (s, args) =>
                {
                    this.Show();
                    Elform = null;
                };

                this.Hide();
                Elform.Show();
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Error", ex.Message);
            }
        }

        private void hvac_Click(object sender, EventArgs e)
        {
            try
            {
                // If already open, just bring it to front
                if (hvacform != null && !hvacform.IsDisposed)
                {
                    hvacform.BringToFront();
                    return;
                }

                hvacform = new hvacform();

                // When ARform closes, re-show this interface
                hvacform.FormClosed += (s, args) =>
                {
                    this.Show();
                    hvacform = null;
                };

                this.Hide();
                hvacform.Show();
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Error", ex.Message);
            }
        }

        private void ff_Click(object sender, EventArgs e)
        {
            try
            {
                // If already open, just bring it to front
                if (FFform != null && !FFform.IsDisposed)
                {
                    ARform.BringToFront();
                    return;
                }

                FFform = new FFform();

                // When ARform closes, re-show this interface
                FFform.FormClosed += (s, args) =>
                {
                    this.Show();
                    FFform = null;
                };

                this.Hide();
                FFform.Show();
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Error", ex.Message);
            }
        }

        private void pb_Click(object sender, EventArgs e)
        {
            try
            {
                // If already open, just bring it to front
                if (PLform != null && !PLform.IsDisposed)
                {
                    PLform.BringToFront();
                    return;
                }

                PLform = new PLform();

                // When ARform closes, re-show this interface
                PLform.FormClosed += (s, args) =>
                {
                    this.Show();
                    PLform = null;
                };

                this.Hide();
                PLform.Show();
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Error", ex.Message);
            }
        }

        private void infra_Click(object sender, EventArgs e)
        {
            try
            {
                // If already open, just bring it to front
                if (infraform != null && !infraform.IsDisposed)
                {
                    infraform.BringToFront();
                    return;
                }

                infraform = new INFRAform();

                // When ARform closes, re-show this interface
                infraform.FormClosed += (s, args) =>
                {
                    this.Show();
                    infraform = null;
                };

                this.Hide();
                infraform.Show();
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Error", ex.Message);
            }
        }

        private void lnkd_Click(object sender, EventArgs e)
        {
            string url = @"https://www.linkedin.com/in/youssef-hesham/";

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

        private void cls_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
