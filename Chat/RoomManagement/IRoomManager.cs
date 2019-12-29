using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.RoomManagement
{
    public interface IRoomManager
    {
        List<Room> GetRooms(long? userId, long? roomId);       
        long SaveRoom(Room room, List<long> userIdList);
    }
}
