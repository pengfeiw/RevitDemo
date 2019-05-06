using System;
using System.Collections.Generic;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Events;

namespace DialogBoxShowingEvent
{
    //Test:关闭Revit会弹出保存项目对话框，在该对话框弹出之前，会引发事件。
    public class Application_DialogBoxShowing : IExternalApplication
    {
        // Implement the OnStartup method to register events when Revit starts.
        public Result OnStartup(UIControlledApplication application)
        {
            // Register related events
            application.DialogBoxShowing +=
    new EventHandler<Autodesk.Revit.UI.Events.DialogBoxShowingEventArgs>(AppDialogShowing);
            return Result.Succeeded;
        }

        // Implement this method to unregister the subscribed events when Revit exits.
        public Result OnShutdown(UIControlledApplication application)
        {
            // unregister events
            application.DialogBoxShowing -=
    new EventHandler<Autodesk.Revit.UI.Events.DialogBoxShowingEventArgs>(AppDialogShowing);
            return Result.Succeeded;
        }

        // The DialogBoxShowing event handler, which allow you to 
        // do some work before the dialog shows
        void AppDialogShowing(object sender, DialogBoxShowingEventArgs args)
        {
            // Get the help id of the showing dialog
            string dialogId = args.DialogId;

            // return if the dialog has no DialogId (such as with a Task Dialog)
            if (dialogId == "")
                return;

            // Show the prompt message and allow the user to close the dialog directly.
            TaskDialog taskDialog = new TaskDialog("Revit");
            taskDialog.MainContent = "A Revit dialog is about to be opened.\n" +
                "The DialogId of this dialog is " + dialogId + "\n" +
                "Press 'Cancel' to immediately dismiss the dialog";
            taskDialog.CommonButtons = TaskDialogCommonButtons.Ok |
                                         TaskDialogCommonButtons.Cancel;
            TaskDialogResult result = taskDialog.Show();
            if (TaskDialogResult.Cancel == result)
            {
                // dismiss the Revit dialog 
                args.OverrideResult(1);
            }
        }
    }
}
