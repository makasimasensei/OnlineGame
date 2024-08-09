using System.Reflection;
using Common;
using GameServer.Servers;


namespace GameServer.Controller
{
    internal class ControllerManager
    {
        Dictionary<RequestCode, BaseController> controllerDict = new Dictionary<RequestCode, BaseController>();
        Server server;

        public ControllerManager(Server server)
        {
            InitController();
            this.server = server;
        }

        void InitController()
        {
            DefaultController defaultController = new();
            controllerDict.Add(defaultController.RequestCode, defaultController);
        }

        public void HandleRequest(RequestCode requestCode, ActionCode actionCode, string data, Client client)
        {
            bool isGet = controllerDict.TryGetValue(requestCode, out BaseController? controller);
            if (!isGet || controller == null)
            {
                Console.WriteLine("Can not get controller where requestcode is " + requestCode);
                return;
            }
            string? methodName = Enum.GetName(typeof(ActionCode), actionCode);
            if (methodName == null)
            {
                Console.WriteLine("ActionCode is null.");
                return;
            }
            MethodInfo? mi = controller.GetType().GetMethod(methodName);
            if (mi == null)
            {
                Console.WriteLine(methodName + "has no corresponding processing method in" + controller.GetType());
                return;
            }
            object[] parameters = { data, client, server };
            object? o = mi.Invoke(controller, parameters);
            string? oString = o as string;
            if (o == null || String.IsNullOrEmpty(oString))
            {
                return;
            }
            server.SendResponse(client, actionCode, oString);
        }
    }
}
