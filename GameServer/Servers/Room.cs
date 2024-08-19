namespace GameServer.Servers
{
    enum RoomState
    {
        WaitingJoin,
        WaitingBattle,
        Battle,
        End,
    }

    class Room
    {
        List<Client> rooms = new();
        RoomState state = RoomState.WaitingJoin;

        public void AddClient(Client client)
        {
            rooms.Add(client);
        }
    }
}
