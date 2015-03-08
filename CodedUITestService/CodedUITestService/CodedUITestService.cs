using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Microsoft.VisualStudio.TestTools.UITest.Input;
using Microsoft.VisualStudio.TestTools.UITesting;
using System.Web.Helpers;

namespace CodedUITestService
{
    [CodedUITest]
    public class CodedUITestService
    {
        public CodedUITestService()
        {
        }

        [TestMethod]
        public void Boostrap()
        {
            var response = HandleCommand(Settings.Command);
        }

        public string HandleCommand(string commandString)
        {
            dynamic command = Json.Decode(commandString);   
            switch((string)command.name) {
                case "find":
                    return HandleFind(command);
                case "tap":
                    return HandleTap(command);
                default:
                    return "{\"result\":\"false\"}";
            }
        }

        public string HandleFind(dynamic command)
        {
            var control = new UITestControl();
            control.TechnologyName = "UIA";
            control.SearchProperties[command.args.strategy] = command.args.value;

            if (control.Exists)
            {
                return "{\"result\":\"true\"}";
            }
            else
            {
                return "{\"result\":\"false\"}";
            }
        }

        public string HandleTap(dynamic command)
        {
            var control = new UITestControl();
            control.TechnologyName = "UIA";
            control.SearchProperties[command.args.strategy] = command.args.value;
            Rectangle rect = control.BoundingRectangle;
            Point centerPoint = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2); 
            Gesture.Tap(centerPoint);
            return "{\"result\":\"true\"}";
        }

    }
}
