using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.FriendsManagement;
using Chat.Identity;
using DAL.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers
{
    [Route("api/friends")]
    [EnableCors("MyPolicy")]
    [ApiController]
    [Authorize]
    public class FriendsController : ControllerBase
    {
        public readonly FriendsManager _friendsManager;
        public readonly UserIdentity _userIdentity;
        public FriendsController()
        {
            _friendsManager = new FriendsManager();
            _userIdentity = new UserIdentity();
        }
        [HttpGet("my")]
        public IActionResult GetMyFriends()
        {
            var userId = _userIdentity.GetUserId(User);
            var friends = _friendsManager.GetFriends(userId);
            return Ok(friends);
        }

        [HttpPost("save-status/{userId}/{status}")]
        public IActionResult SaveFriendStatus([FromRoute] long userId,int status)
        {
            var currentUserId = _userIdentity.GetUserId(User);
            _friendsManager.SaveFriendStatus(currentUserId, userId, (UserRelationStatus)status);
            return NoContent();
        }
    }
}