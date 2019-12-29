using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Models;
using DAL.FriendsData;
using DAL.Enums;

namespace Chat.FriendsManagement
{
    public class FriendsManager : IFriendsManager
    {
        private readonly FriendsDataProvider _friendsDataProvider;

        public FriendsManager()
        {
            _friendsDataProvider = new FriendsDataProvider();
        }

        public void AddFriend(long userId1, long userId2)
        {
            throw new NotImplementedException();
        }

        public List<Friend> GetFriends(long id)
        {
            return _friendsDataProvider.GetUserFriends(id,2);
        }

        public void SaveFriendStatus(long userId1,long userId2,UserRelationStatus status)
        {
            _friendsDataProvider.SaveFriendStatus(userId1, userId2, status);
        }
       
    }
}
