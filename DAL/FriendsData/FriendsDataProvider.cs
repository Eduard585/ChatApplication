using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using ChatAppUtils.Configuration;
using DAL.Models;
using ChatAppUtils;
using DAL.Enums;

namespace DAL.FriendsData
{
    public class FriendsDataProvider : IFriendsDataProvider
    {
        private readonly string _connectionString;

        private const string GetUsersFriendsCmd = "spGetUsersFriends";
        private const string SaveFriendStatusCmd = "spSaveFriendStatus";

        public FriendsDataProvider()
        {
            _connectionString = ConfigurationAdapter.GetConnectionString("DefaultConnection");
        }

        public List<Friend> GetUserFriends(long userId,int statusId)//Status codes: 1-Pending, 2-Accepted, 3-Declined,4-Blocked
        {
            var result = new List<Friend>();

            using(var connection = new SqlConnection(_connectionString))
            using(var cmd = connection.CreateCommand())
            {
                connection.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = GetUsersFriendsCmd;
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@StatusId", statusId);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(ReadFriend(reader));
                }
                return result;
            }
        }

        public void SaveFriendStatus(long userId1,long userId2,UserRelationStatus status)
        {            
            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                connection.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = GetUsersFriendsCmd;
                cmd.Parameters.AddWithValue("@userId1", userId1);
                cmd.Parameters.AddWithValue("@userId2", userId2);
                cmd.Parameters.AddWithValue("@status", (int)status);
                cmd.ExecuteNonQuery();
            }
        }

        private Friend ReadFriend(SqlDataReader reader)
        {
            var friend = new Friend();
            int ord = 0;
            friend.Id = reader.GetInt64Inc(ref ord);
            friend.Login = reader.GetStringInc(ref ord);
            return friend;
        }
    }
}
