using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using ChatAppUtils;
using ChatAppUtils.Configuration;
using DAL.Models;

namespace DAL.RoomsData
{
    public class RoomDataProvider : IRoomDataProvider
    {
        private readonly string _connectionString;

        private const string SaveRoomCmd = "spSaveRoom";
        private const string GetRoomCmd = "spGetRooms";
        private const string AddUsersToRoomCmd = "spAddUsersToRoom";

        public RoomDataProvider()
        {
            _connectionString = ConfigurationAdapter.GetConnectionString("DefaultConnection");
        }       

        public List<Room> GetRooms(long? userId, long? roomId)
        {
            var result = new List<Room>();

            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                connection.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = GetRoomCmd;
                cmd.Parameters.AddWithValue("@userId", userId);                
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(ReadRoom(reader));
                }
                return result;
            }
        }

        public long SaveRoom(Room room)
        {             
            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                connection.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = SaveRoomCmd;
                cmd.Parameters.AddWithValue("@id", room.Id);
                cmd.Parameters.AddWithValue("@name", room.Name);
                cmd.Parameters.AddWithValue("@isActive", room.IsActive);
                var roomId = cmd.ExecuteScalar();
                return Convert.ToInt64(roomId);
            }
        }

        public void AddUsersToRoom(long roomId,List<long> userIdList)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                connection.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = AddUsersToRoomCmd;
                cmd.Parameters.AddWithValue("@roomId", roomId);
                cmd.Parameters.Add(TableUtil.CreateTableLong("@userList", userIdList));
                cmd.ExecuteNonQuery();
            }
        }

        private Room ReadRoom(SqlDataReader reader)
        {
            var room = new Room();
            int ord = 0;
            room.Id = reader.GetInt64Inc(ref ord);
            room.Name = reader.GetStringInc(ref ord);
            room.CreateDate = reader.GetDateTimeInc(ref ord);
            room.IsActive = reader.GetBooleanInc(ref ord);
            return room;
        }
    }
}
