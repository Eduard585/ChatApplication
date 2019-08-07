using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Models;
using DAL.FriendsData;

namespace Chat.FriendsManagement
{
    public class FriendsManager : IFriendsManager
    {
        private readonly FriendsDataProvider _friendsDataProvider;

        public FriendsManager()
        {
            _friendsDataProvider = new FriendsDataProvider();
        }
        public List<Friend> GetFriends(long id)
        {
            return _friendsDataProvider.GetUserFriends(id);
        }
       
    }
}
