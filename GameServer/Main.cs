using GameServer.Servers;

Server server = new("127.0.0.1", 6000);
server.Start();
Console.ReadKey();
