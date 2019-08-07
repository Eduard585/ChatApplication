using DAL.Models;
using System.Collections.Generic;

namespace Chat.FriendsManagement
{
    interface IFriendsManager
    {
        List<Friend> GetFriends(long id);
    }
}
