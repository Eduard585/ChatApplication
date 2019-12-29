using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Models;
using DAL.RoomsData;

namespace Chat.RoomManagement
{
    public class RoomManager : IRoomManager
    {
        private readonly RoomDataProvider _roomDataProvider;

        public RoomManager()
        {
            _roomDataProvider = new RoomDataProvider();
        }
        public List<Room> GetRooms(long? userId, long? roomId)
        {
            return _roomDataProvider.GetRooms(userId, roomId);
        }

        public long SaveRoom(Room room,List<long> userIdList)
        {
            var roomId = _roomDataProvider.SaveRoom(room);
            _roomDataProvider.AddUsersToRoom(roomId, userIdList);//TODO: make atomic transaction
            return roomId;
        }
    }
}
