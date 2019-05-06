using System;
using System.Collections.Generic;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Filtering
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class Filtering : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document document = uidoc.Document;
            ElementCategoryFilter filter = new ElementCategoryFilter(BuiltInCategory.OST_Walls);
            FilteredElementCollector collector = new FilteredElementCollector(document);

            //use WhereElementIsNotElementType() to find all wall instances only(not include wall type).
            IList<Element> walls = collector.WherePasses(filter).WhereElementIsNotElementType().ToElements();

            string prompt = "The walls in the current document are:\n";
            foreach (Element e in walls)
            {
                prompt += e.Name + "\n";
            }

            TaskDialog.Show("WPF", prompt);

            return Result.Succeeded;
        }
    }
}
