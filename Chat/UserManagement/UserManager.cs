﻿using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
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

        public SaveUserResult CreateUser(User user)
        {
            long userId = 0;
            UserFilter filter = new UserFilter()
            {
                Email = user.Email,
                Login = user.Login
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
            userId = _userDataProvider.SaveUser(user);
            if (userId == 0)
            {
                return new SaveUserResult()
                {
                    Error = "Unable to save user",
                    IsNew = false,
                    Success = false,
                    UserId = 0
                };
            }
            return new SaveUserResult()
            {
                Error = null,
                IsNew = true,
                Success = true,
                UserId = userId
            };
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
