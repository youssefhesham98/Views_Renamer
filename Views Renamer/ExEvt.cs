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
            }
        }

        public string GetName()
        {
            return "EDECS Toolkit";
        }
        public enum Request
        {
            Collect,
            Rename
        }
    }
}
