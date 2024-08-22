namespace GameServer.Servers
{
    enum RoomState
    {
        WaitingJoin,
        WaitingBattle,
        Battle,
        End,
    }

    public class Room
    {
        List<Client> rooms = new();
        RoomState state = RoomState.WaitingJoin;
        readonly Server server;

        public Room(Server server)
        {
            this.server = server;
        }
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
            client.Room = this;
        }

        /// <summary>
        /// Get rooms' data
        /// </summary>
        public string GetHouseOwnerData()
        {
            return rooms[0].GetUserData();
        }

        public void Close(Client client)
        {
            if (client == rooms[0])
            {
                server.RemoveRoom(this);
            }
            else rooms.Remove(client);
        }

        public int GetId()
        {
            if (rooms.Count > 0)
            {
                return rooms[0].GetUserId();
            }
            return -1;
        }
    }
}
