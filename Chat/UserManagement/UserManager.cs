using System;
using System.Collections.Generic;
using Chat.ViewModels;
using DAL.Models;
using DAL.UserData;

namespace Chat.UserManagement
{
    internal class UserManager : IUserManager
    {
        private readonly UserDataProvider _userDataProvider;
        private readonly List<IRegistrationHandler> _registrationHandlers;

        public UserManager()//TODO add configuration and instance creating
        {
            _userDataProvider = new UserDataProvider();
        }
        public LoginResult Login(string login, string password)
        {
            var filter = new UserFilter();
            if (login.Contains('@'))
            {
                filter.Email = login;
            }
            else
            {
                filter.Login = login;
            }

            var users = _userDataProvider.GetUsersByFilter(filter);
            if (users.Count == 0)
            {
                return new LoginResult()
                {
                    Error = "User does not exists",
                    Success = false,
                    UserId = 0
                };
            }
            if (users[0].IsBlocked)
            {
                return new LoginResult()
                {
                    Error = "User is blocked",
                    Success = false,
                    UserId = 0
                };
            }

            if (!CheckUserPassword(password,users[0]))
            {
                return new LoginResult()
                {
                    Error = "Invalid password",
                    Success = false,
                    UserId = 0
                };
            }
            return new LoginResult()
            {
                Error = null,
                Success = true,
                UserId = users[0].Id,
                UserName = users[0].Login
            };
        }

        public User GetUserById(long id)
        {
            throw new NotImplementedException();
        }

        public List<User> GetUsersByFilter(UserFilter filter)
        {
            throw new NotImplementedException();
        }

        public SaveUserResult SaveUser(User user)
        {
            throw new NotImplementedException();
        }

        public SaveUserResult CreateUser(RestUserRegistrationInfo userInfo)
        {
            long userId = 0;
            UserFilter filter = new UserFilter()
            {
                Email = userInfo.Email,
                Login = userInfo.Login
            };
            if (_userDataProvider.GetUsersByFilterCountOR(filter) > 0)
            {
                return new SaveUserResult()
                {
                    Error = "User already exists",
                    IsNew = false,
                    Success = false,
                    UserId = 0
                };
            }

            User user = CreateUserModel(userInfo);
            userId = _userDataProvider.SaveUser(user);
            if (userId != 0)
            {
                return new SaveUserResult()
                {
                    Error = "",
                    IsNew = true,
                    Success = true,
                    UserId = userId
                };
            }
            return new SaveUserResult()
            {
                Error = "Unable to save user",
                IsNew = false,
                Success = false,
                UserId = 0
            };
        }


        private User CreateUserModel(RestUserRegistrationInfo userInfo)
        {
            var salt = PasswordUtil.CreateSalt(15);
            var hash = PasswordUtil.GenerateSaltedHash(userInfo.Password, salt);


            User user = new User()
            {
                Email = userInfo.Email,
                Login = userInfo.Login,
                Salt = Convert.ToBase64String(salt),
                PasswordHash = Convert.ToBase64String(hash),
                UpdDate = DateTime.Now
            };

            return user;
        }

        private bool CheckUserPassword(string password,User user)
        {
            var salt = Convert.FromBase64String(user.Salt);
            var hash = Convert.FromBase64String(user.PasswordHash);

            var newHash = PasswordUtil.GenerateSaltedHash(password, salt);
            return PasswordUtil.CompareByteArrays(hash, newHash);
        }

        public void RegisterUser(long userId)//TODO add handler if needed
        {
            if (_registrationHandlers != null)
            {
                foreach (var rgHandler in _registrationHandlers)
                {
                    rgHandler.Handle(userId);
                }
            }

        }


    }
}
