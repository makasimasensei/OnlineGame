namespace GameServer.Servers
{
    class Room
    {
        enum RoomState
        {
            WaitingJoin,
            WaitingBattle,
            Battle,
            End,
        }

        List<Client> rooms  = new();
        RoomState state = RoomState.WaitingJoin;
    }
}
