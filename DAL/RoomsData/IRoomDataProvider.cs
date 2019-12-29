using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.RoomsData
{
    public interface IRoomDataProvider
    {
        long SaveRoom(Room room);
        List<Room> GetRooms(long? userId,long? roomId);
        void AddUsersToRoom(long roomId, List<long> userIdList);

    }
}
