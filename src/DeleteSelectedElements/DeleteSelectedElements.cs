using System;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System.Collections.Generic;

namespace DeleteSelectedElements
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class DeleteSelectedElements : IExternalCommand
    {

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //try
            //{
            //    UIDocument uidoc = commandData.Application.ActiveUIDocument;
            //    Document doc = uidoc.Document;

            //    //delete selected elements.
            //    ICollection<ElementId> ids = doc.Delete(uidoc.Selection.GetElementIds());
            //    TaskDialog taskDialog = new TaskDialog("Revit");
            //    taskDialog.MainContent = ("Click Yes to return Succeeded. Selected members will be deleted.\n" +
            //"Click No to return Failed.  Selected members will not be deleted.\n" +
            //"Click Cancel to return Cancelled.  Selected members will not be deleted.");
            //    TaskDialogCommonButtons buttons = TaskDialogCommonButtons.Yes | TaskDialogCommonButtons.No | TaskDialogCommonButtons.Cancel;
            //    taskDialog.CommonButtons = buttons;
            //    TaskDialogResult dialogRes = taskDialog.Show();

            //    if (dialogRes == TaskDialogResult.Yes)
            //    {
            //        return Result.Succeeded;
            //    }
            //    else if(dialogRes == TaskDialogResult.No)
            //    {
            //        foreach (ElementId id in ids)
            //        {
            //            elements.Insert(doc.GetElement(id));
            //        }
            //        message = "Failed to delete selection.";
            //        return Autodesk.Revit.UI.Result.Failed;
            //    }
            //    else
            //    {
            //        return Autodesk.Revit.UI.Result.Cancelled;
            //    }
            //}
            //catch (Exception e)
            //{

            //    message = "Unexpected Exception thrown.";
            //    return Result.Failed;
            //}
            //return Result.Succeeded;


            try
            {
                Document doc = commandData.Application.ActiveUIDocument.Document;
                UIDocument uidoc = commandData.Application.ActiveUIDocument;
                // Delete selected elements

                using (Transaction trans = new Transaction(doc, "Delete selected elements"))
                {
                    trans.Start();
                    ICollection<Autodesk.Revit.DB.ElementId> ids =
                   doc.Delete(uidoc.Selection.GetElementIds());

                    TaskDialog taskDialog = new TaskDialog("Revit");
                    taskDialog.MainContent =
                        ("Click Yes to return Succeeded. Selected members will be deleted.\n" +
                        "Click No to return Failed.  Selected members will not be deleted.\n" +
                        "Click Cancel to return Cancelled.  Selected members will not be deleted.");
                    TaskDialogCommonButtons buttons = TaskDialogCommonButtons.Yes |
                        TaskDialogCommonButtons.No | TaskDialogCommonButtons.Cancel;
                    taskDialog.CommonButtons = buttons;
                    TaskDialogResult taskDialogResult = taskDialog.Show();

                    if (taskDialogResult == TaskDialogResult.Yes)
                    {
                        trans.Commit();
                        return Autodesk.Revit.UI.Result.Succeeded;
                    }
                    else if (taskDialogResult == TaskDialogResult.No)
                    {
                        //ICollection<ElementId> selectedElementIds = uidoc.Selection.GetElementIds();
                        foreach (ElementId id in ids)
                        {
                            elements.Insert(doc.GetElement(id));
                        }
                        message = "Failed to delete selection.";
                        return Autodesk.Revit.UI.Result.Failed;
                    }
                    else
                    {
                        return Autodesk.Revit.UI.Result.Cancelled;
                    }
                }
            }
            catch
            {
                message = "Unexpected Exception thrown.";
                return Autodesk.Revit.UI.Result.Failed;
            }
        }
    }
}
