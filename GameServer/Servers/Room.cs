using System.Text;

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
            if (rooms.Count >= 2)
            {
                state = RoomState.WaitingBattle;
            }
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

        public String GetRoomData()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Client client in rooms)
            {
                sb.Append(client.GetUserData()+"|");
            }
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length-1, 1);
            }
            return sb.ToString();
        }
    }
}
