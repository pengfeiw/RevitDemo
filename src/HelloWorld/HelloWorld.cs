using System;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;

namespace HelloWorld
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class HelloWorld : IExternalCommand
    {
        public Autodesk.Revit.UI.Result Execute(ExternalCommandData revit, ref string message, ElementSet elements)
        {
            //Document doc = revit.Application.ActiveUIDocument.Document;
            
            //Autodesk.Revit.UI.ExternalCommandData
            //Autodesk.Revit.DB.ElementSet

            TaskDialog.Show("Revit", "Hello World!");
            message = "Test message.";
            return Result.Succeeded;
        }
    }

    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class HelloRevit : IExternalCommand
    {
        public Autodesk.Revit.UI.Result Execute(ExternalCommandData revit, ref string message, ElementSet elements)
        {
            //Document doc = revit.Application.ActiveUIDocument.Document;

            //Autodesk.Revit.UI.ExternalCommandData
            //Autodesk.Revit.DB.ElementSet

            TaskDialog.Show("Revit", "Hello Revit!");
            message = "Test message.";
            return Result.Succeeded;
        }
    }
}
