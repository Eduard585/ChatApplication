using DAL.Models;
using System.Collections.Generic;

namespace Chat.FriendsManagement
{
    interface IFriendsManager
    {
        List<Friend> GetFriends(long id);
        void AddFriend(long userId1, long userId2);
    }
}
