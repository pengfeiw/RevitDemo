using System;
using System.Collections.Generic;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System.Windows.Media.Imaging;

namespace Ribbon
{
    public class MyRibbon : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
            //throw new NotImplementedException();
        }

        public Result OnStartup(UIControlledApplication application)
        {
            //创建一个新的tab，并添加panel。
            string tabName = "MyRibbonTab";
            application.CreateRibbonTab(tabName);

            //create pushButton
            PushButtonData button1 = new PushButtonData("Button1", "Button One", "HelloWorld.dll", "HelloWorld.HelloWorld");
            PushButtonData button2 = new PushButtonData("Button2", "Button Two", "HelloWorld.dll", "HelloWorld.HelloRevit");

            //create ribbon panel
            RibbonPanel myPanel = application.CreateRibbonPanel(tabName, "MyFirstPanel");

            List<RibbonItem> buttons = new List<RibbonItem>();
            buttons.AddRange(myPanel.AddStackedItems(button1, button2));
            return Result.Succeeded;

            //在附加模块tab里，添加一个panel
            RibbonPanel panel2 = application.CreateRibbonPanel("New Ribbon Panel");
            //AddRadioGroup(panel2);
            //panel2.AddSeparator();
            //AddPushButton(panel2);
            //AddSplitButton(panel2);
            //AddStackedButtons(panel2);
            //AddSlideOut(panel2);

            return Result.Succeeded;
        }


        private void AddRadioGroup(RibbonPanel panel)
        {
            // add radio button group
            RadioButtonGroupData radioData = new RadioButtonGroupData("radioGroup");
            RadioButtonGroup radioButtonGroup = panel.AddItem(radioData) as RadioButtonGroup;

            // create toggle buttons and add to radio button group
            ToggleButtonData tb1 = new ToggleButtonData("toggleButton1", "Red");
            tb1.ToolTip = "Red Option";
            tb1.LargeImage = new BitmapImage(new Uri(@"D:\Sample\HelloWorld\bin\Debug\Red.bmp"));
            ToggleButtonData tb2 = new ToggleButtonData("toggleButton2", "Green");
            tb2.ToolTip = "Green Option";
            tb2.LargeImage = new BitmapImage(new Uri(@"D:\Sample\HelloWorld\bin\Debug\Green.bmp"));
            ToggleButtonData tb3 = new ToggleButtonData("toggleButton3", "Blue");
            tb3.ToolTip = "Blue Option";
            tb3.LargeImage = new BitmapImage(new Uri(@"D:\Sample\HelloWorld\bin\Debug\Blue.bmp"));
            radioButtonGroup.AddItem(tb1);
            radioButtonGroup.AddItem(tb2);
            radioButtonGroup.AddItem(tb3);
        }

        private void AddPushButton(RibbonPanel panel)
        {
            PushButton pushButton = panel.AddItem(new PushButtonData("HelloWorld",
                "HelloWorld", @"D:\Sample\HelloWorld\bin\Debug\HelloWorld.dll", "HelloWorld.CsHelloWorld")) as PushButton;

            // Set ToolTip and contextual help
            pushButton.ToolTip = "Say Hello World";
            ContextualHelp contextHelp = new ContextualHelp(ContextualHelpType.Url,
                "http://www.autodesk.com");
            pushButton.SetContextualHelp(contextHelp);

            // Set the large image shown on button
            pushButton.LargeImage =
                new BitmapImage(new Uri(@"D:\Sample\HelloWorld\bin\Debug\39-Globe_32x32.png"));
        }

        private void AddSplitButton(RibbonPanel panel)
        {
            string assembly = @"D:\Sample\HelloWorld\bin\Debug\Hello.dll";

            // create push buttons for split button drop down
            PushButtonData bOne = new PushButtonData("ButtonNameA", "Option One",
             assembly, "Hello.HelloOne");
            bOne.LargeImage =
                    new BitmapImage(new Uri(@"D:\Sample\HelloWorld\bin\Debug\One.bmp"));

            PushButtonData bTwo = new PushButtonData("ButtonNameB", "Option Two",
                    assembly, "Hello.HelloTwo");
            bTwo.LargeImage =
             new BitmapImage(new Uri(@"D:\Sample\HelloWorld\bin\Debug\Two.bmp"));

            PushButtonData bThree = new PushButtonData("ButtonNameC", "Option Three",
             assembly, "Hello.HelloThree");
            bThree.LargeImage =
                    new BitmapImage(new Uri(@"D:\Sample\HelloWorld\bin\Debug\Three.bmp"));

            SplitButtonData sb1 = new SplitButtonData("splitButton1", "Split");
            SplitButton sb = panel.AddItem(sb1) as SplitButton;
            sb.AddPushButton(bOne);
            sb.AddPushButton(bTwo);
            sb.AddPushButton(bThree);
        }

        private void AddStackedButtons(RibbonPanel panel)
        {
            ComboBoxData cbData = new ComboBoxData("comboBox");

            TextBoxData textData = new TextBoxData("Text Box");
            textData.Image =
                    new BitmapImage(new Uri(@"D:\Sample\HelloWorld\bin\Debug\39-Globe_16x16.png"));
            textData.Name = "Text Box";
            textData.ToolTip = "Enter some text here";
            textData.LongDescription = "This is text that will appear next to the image"
                    + "when the user hovers the mouse over the control";
            textData.ToolTipImage =
                    new BitmapImage(new Uri(@"D:\Sample\HelloWorld\bin\Debug\39-Globe_32x32.png"));

            IList<RibbonItem> stackedItems = panel.AddStackedItems(textData, cbData);
            if (stackedItems.Count > 1)
            {
                TextBox tBox = stackedItems[0] as TextBox;
                if (tBox != null)
                {
                    tBox.PromptText = "Enter a comment";
                    tBox.ShowImageAsButton = true;
                    tBox.ToolTip = "Enter some text";
                    // Register event handler ProcessText
                    tBox.EnterPressed +=
            new EventHandler<Autodesk.Revit.UI.Events.TextBoxEnterPressedEventArgs>(ProcessText);
                }

                ComboBox cBox = stackedItems[1] as ComboBox;
                if (cBox != null)
                {
                    cBox.ItemText = "ComboBox";
                    cBox.ToolTip = "Select an Option";
                    cBox.LongDescription = "Select a number or letter";

                    ComboBoxMemberData cboxMemDataA = new ComboBoxMemberData("A", "Option A");
                    cboxMemDataA.Image =
                            new BitmapImage(new Uri(@"D:\Sample\HelloWorld\bin\Debug\A.bmp"));
                    cboxMemDataA.GroupName = "Letters";
                    cBox.AddItem(cboxMemDataA);

                    ComboBoxMemberData cboxMemDataB = new ComboBoxMemberData("B", "Option B");
                    cboxMemDataB.Image =
                            new BitmapImage(new Uri(@"D:\Sample\HelloWorld\bin\Debug\B.bmp"));
                    cboxMemDataB.GroupName = "Letters";
                    cBox.AddItem(cboxMemDataB);

                    ComboBoxMemberData cboxMemData = new ComboBoxMemberData("One", "Option 1");
                    cboxMemData.Image =
                            new BitmapImage(new Uri(@"D:\Sample\HelloWorld\bin\Debug\One.bmp"));
                    cboxMemData.GroupName = "Numbers";
                    cBox.AddItem(cboxMemData);
                    ComboBoxMemberData cboxMemData2 = new ComboBoxMemberData("Two", "Option 2");
                    cboxMemData2.Image =
                            new BitmapImage(new Uri(@"D:\Sample\HelloWorld\bin\Debug\Two.bmp"));
                    cboxMemData2.GroupName = "Numbers";
                    cBox.AddItem(cboxMemData2);
                    ComboBoxMemberData cboxMemData3 = new ComboBoxMemberData("Three", "Option 3");
                    cboxMemData3.Image =
                            new BitmapImage(new Uri(@"D:\Sample\HelloWorld\bin\Debug\Three.bmp"));
                    cboxMemData3.GroupName = "Numbers";
                    cBox.AddItem(cboxMemData3);
                }
            }
        }

        private static void AddSlideOut(RibbonPanel panel)
        {
            string assembly = @"D:\Sample\HelloWorld\bin\Debug\Hello.dll";

            panel.AddSlideOut();

            // create some controls for the slide out
            PushButtonData b1 = new PushButtonData("ButtonName1", "Button 1",
                    assembly, "Hello.HelloButton");
            b1.LargeImage =
                    new BitmapImage(new Uri(@"D:\Sample\HelloWorld\bin\Debug\39-Globe_32x32.png"));
            PushButtonData b2 = new PushButtonData("ButtonName2", "Button 2",
                    assembly, "Hello.HelloTwo");
            b2.LargeImage =
                    new BitmapImage(new Uri(@"D:\Sample\HelloWorld\bin\Debug\39-Globe_16x16.png"));

            // items added after call to AddSlideOut() are added to slide-out automatically
            panel.AddItem(b1);
            panel.AddItem(b2);
        }

        void ProcessText(object sender, Autodesk.Revit.UI.Events.TextBoxEnterPressedEventArgs args)
        {
            // cast sender as TextBox to retrieve text value
            TextBox textBox = sender as TextBox;
            string strText = textBox.Value as string;
        }
    }


}
