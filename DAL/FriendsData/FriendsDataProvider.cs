using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using ChatAppUtils.Configuration;
using DAL.Models;
using ChatAppUtils;

namespace DAL.FriendsData
{
    public class FriendsDataProvider : IFriendsDataProvider
    {
        private readonly string _connectionString;

        private const string GetUsersFriendsCmd = "spGetUsersFriends";

        public FriendsDataProvider()
        {
            _connectionString = ConfigurationAdapter.GetConnectionString("DefaultConnection");
        }

        public List<Friend> GetUserFriends(long userId)//Status codes: 1-Pending, 2-Accepted, 3-Declined,4-Blocked
        {
            var result = new List<Friend>();

            using(var connection = new SqlConnection(_connectionString))
            using(var cmd = connection.CreateCommand())
            {
                connection.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = GetUsersFriendsCmd;
                cmd.Parameters.AddWithValue("@UserId", userId);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(ReadFriend(reader));
                }
                return result;
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
