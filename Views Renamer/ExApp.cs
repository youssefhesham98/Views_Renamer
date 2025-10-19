using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using System.Reflection;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO;

namespace Views_Renamer
{
    public class ExApp : IExternalApplication
    {
        public UIControlledApplication uicapp { get; set; }
        public Result OnShutdown(UIControlledApplication uicapp)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication uicapp)
        {
            //this.uicapp = uicapp;
            //CreateBushButton();
            //return Result.Succeeded;

            LicenseCheck check = new LicenseCheck();
            var begin = check.Check();
            if (begin)
            {
                try
                {
                    this.uicapp = uicapp;
                    CreateBushButton();
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                    TaskDialog.Show("Error", message);
                }
            }
            return Result.Succeeded;
        }
        private void CreateBushButton()
        {
            try
            {
                var tab_name = "EDECS TOOLKIT";
                var pnl_name = "Documentation";
                var btn_name = "Views Renamer";
                try
                {
                    uicapp.CreateRibbonTab(tab_name);
                }
                catch (Exception ex) { /*TaskDialog.Show("Failed", ex.Message.ToString());*/ }
                List<RibbonPanel> panels = uicapp.GetRibbonPanels(tab_name);
                RibbonPanel panel = panels.FirstOrDefault(p => p.Name == pnl_name);
                if (panel == null)
                {
                    panel = uicapp.CreateRibbonPanel(tab_name, pnl_name);
                }
                Assembly assembly = Assembly.GetExecutingAssembly();
                // @"Directory\XXXX.dll"
                PushButtonData pd_data = new PushButtonData(btn_name, btn_name, assembly.Location, "Views_Renamer.ExCmd");
                PushButton pb = panel.AddItem(pd_data) as PushButton;
                if (pb != null)
                {
                    pb.ToolTip = "Views renamer according to EDECS manual.";
                    //pb.LargeImage = new BitmapImage(new Uri($@"{Path.GetDirectoryName(assembly.Location)}\pb.png"));
                    //Stream stream = assembly.GetManifestResourceStream("Naming_Convention_Tester.bin.Resources.pb.png");
                    //PngBitmapDecoder decoder = new PngBitmapDecoder(stream , BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                    //pb.LargeImage = decoder.Frames[0];
                    pb.LargeImage = GetImageSource("Views_Renamer.bin.Resources.viewsrenamer24.png");
                }
            }
            catch (Exception ex) { /*TaskDialog.Show("Failed", ex.Message.ToString());*/ }
        }
        private ImageSource GetImageSource(string ImageFullname)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(ImageFullname);
            PngBitmapDecoder decoder = new PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            // use the extension related to the image extension like PngBitmapDecoder for PNG Image
            return decoder.Frames[0];
        }
    }
}
