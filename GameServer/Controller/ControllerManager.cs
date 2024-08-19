using System.Reflection;
using Common;
using GameServer.Servers;


namespace GameServer.Controller
{
    class ControllerManager
    {
        readonly Dictionary<RequestCode, BaseController> controllerDict = new();
        readonly Server server;

        public ControllerManager(Server server)
        {
            InitController();
            this.server = server;
        }

        /// <summary>
        /// Initialize controller.
        /// </summary>
        void InitController()
        {
            DefaultController defaultController = new();
            controllerDict.Add(defaultController.RequestCode, defaultController);
            controllerDict.Add(RequestCode.User, new UserController());
            controllerDict.Add(RequestCode.Room, new RoomController());
        }

        /// <summary>
        /// Handling requests from clients.
        /// </summary>
        /// <param name="requestCode">Code of request</param>
        /// <param name="actionCode">Code of action</param>
        /// <param name="data">Data from clients</param>
        /// <param name="client">Client instance</param>
        public void HandleRequest(RequestCode requestCode, ActionCode actionCode, string data, Client client)
        {
            //Get the corresponding controller from the request code.
            bool isGet = controllerDict.TryGetValue(requestCode, out BaseController? controller);
            if (!isGet || controller == null)
            {
                Console.WriteLine("Request code is " + requestCode + ". But controller can't be obtained .");
                return;
            }

            //Get the corresponding action code from the ActionCode enum.
            string? action = Enum.GetName(typeof(ActionCode), actionCode);
            if (action == null)
            {
                Console.WriteLine("ActionCode doesn't exist.");
                return;
            }

            //Get the corresponding method from the action.
            MethodInfo? method = controller.GetType().GetMethod(action);
            if (method == null)
            {
                Console.WriteLine(action + "has no corresponding processing method in" + controller.GetType());
                return;
            }

            //Parameters.
            object[] parameters = { data, client, server };
            //Method invoking.
            object? o = method.Invoke(controller, parameters);
            //Type conversion.
            string? oString = o as string;
            if (o == null || String.IsNullOrEmpty(oString)) return;
            //Send response.
            Server.SendResponse(client, actionCode, oString);
        }
    }
}
