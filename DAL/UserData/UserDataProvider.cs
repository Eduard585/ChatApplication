using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ChatAppUtils.Configuration;
using DAL.Models;
using ChatAppUtils;

namespace DAL.UserData
{
    public class UserDataProvider:IUserDataProvider
    {
        private readonly string _connectionString;

        private const string SaveUserCmd = "spSaveUser";
        private const string GetUserByFilterCmd = "spGetUsersByFilter";
        private const string GetUserByFilterCountCmd = "spGetUsersByFilterCount";
        private const string GetUserByFilterCountORCmd = "spGetUsersByFilterCountOR";
        private const string CheckUserPassrodCmd = "spCheckUserPassword";
        public UserDataProvider()
        {
            _connectionString = ConfigurationAdapter.GetConnectionString("DefaultConnection");
        }

        
        public User GetUserById(long userId)
        {
            throw new NotImplementedException();
        }

        public List<User> GetUsersByFilter(UserFilter filter)
        {
            var result = new List<User>();
            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = GetUserByFilterCmd;
                cmd.Parameters.AddWithValue("@Id", filter.Id);
                cmd.Parameters.AddWithValue("@Login", filter.Login);
                cmd.Parameters.AddWithValue("@Email", filter.Email);
                cmd.Parameters.AddWithValue("@IsBlocked", filter.IsBlocked);                

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {                   
                    result.Add(ReadUser(reader));
                }
                return result;
            }
        }

        public long GetUsersByFilterCount(UserFilter filter)
        {           
            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = GetUserByFilterCountCmd;               
                cmd.Parameters.AddWithValue("@Id", filter.Id);
                cmd.Parameters.AddWithValue("@Login", filter.Login);
                cmd.Parameters.AddWithValue("@Email", filter.Email);
                cmd.Parameters.AddWithValue("@IsBlocked", filter.IsBlocked);

                var reader = cmd.ExecuteScalar();
                return Convert.ToInt64(reader);                                
            }
        }

        public long GetUsersByFilterCountOR(UserFilter filter)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = GetUserByFilterCountORCmd;                
                cmd.Parameters.AddWithValue("@Id", filter.Id);
                cmd.Parameters.AddWithValue("@Login", filter.Login);
                cmd.Parameters.AddWithValue("@Email", filter.Email);                

                var reader = cmd.ExecuteScalar();
                return Convert.ToInt64(reader);
            }
        }

        public long SaveUser(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = SaveUserCmd;
                cmd.Parameters.AddWithValue("@Id", user.Id);
                cmd.Parameters.AddWithValue("@Login", user.Login);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@IsBlocked", user.IsBlocked);
                cmd.Parameters.AddWithValue("@UpdDate", user.UpdDate);
                var executeScalar = cmd.ExecuteScalar();
                return Convert.ToInt64(executeScalar);
            }
        }

        public bool CheckUserPassword(string login, string password)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = CheckUserPassrodCmd;               
                cmd.Parameters.AddWithValue("@login", login);                
                cmd.Parameters.AddWithValue("@password", password);
                
                var executeScalar = cmd.ExecuteScalar();
                return Convert.ToBoolean(executeScalar);
            }
        }

        private User ReadUser(SqlDataReader reader)
        {
            var user = new User();
            int ord = 0;
            user.Id = reader.GetInt64Inc(ref ord);
            user.Login = reader.GetStringInc(ref ord);
            user.Email = reader.GetStringInc(ref ord);
            user.IsBlocked = reader.GetBooleanInc(ref ord);
            user.UpdDate = reader.GetDateTimeInc(ref ord);
            return user;
        }
    }
}
