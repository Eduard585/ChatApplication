using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;

namespace DAL.FriendsData
{
    class FriendsDataProvider : IFriendsDataProvider
    {
        private const string GetUsersFriendsCmd = "spGetUsersFriends";

        public Friend GetUserFriends(long userId)//Status codes: 1-Pending, 2-Accepted, 3-Declined,4-Blocked
        {
            
        }
    }
}
