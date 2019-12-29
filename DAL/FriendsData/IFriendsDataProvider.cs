using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.FriendsData
{
    interface IFriendsDataProvider
    {
        List<Models.Friend> GetUserFriends(long userId,int statusId);       
    }
}
