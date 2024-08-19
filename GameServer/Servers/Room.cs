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

        public bool IsWaitingJoin()
        {
            return state == RoomState.WaitingJoin;
        }

        /// <summary>
        /// Add rooms.
        /// </summary>
        /// <param name="client"></param>
        public void AddClient(Client client)
        {
            rooms.Add(client);
        }

        /// <summary>
        /// Get rooms' data
        /// </summary>
        public string GetHouseOwnerData()
        {
            return rooms[0].GetUserData();
        }
    }
}
