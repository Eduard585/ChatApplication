using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.RoomManagement;
using Chat.ViewModels;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers
{
    [Route("api/room")]
    [EnableCors("MyPolicy")]
    [ApiController]
    [Authorize]
    public class RoomController : ControllerBase
    {
        public readonly RoomManager _roomManager;
        public RoomController()
        {
            _roomManager = new RoomManager();
        }

        [HttpPost("save")]
        public IActionResult SaveRoom([FromBody]RestRoom restRoom)
        {
            var room = GetRoom(restRoom);
            var userIdList = restRoom.Users;
            var roomId = _roomManager.SaveRoom(room,userIdList);
            return Ok(roomId);
        }

        [HttpGet("{userId}/{roomId}")]
        public IActionResult GetRooms([FromRoute]long userId,long roomId)
        {
            var rooms = _roomManager.GetRooms(userId, roomId);
            return Ok(rooms);
        }

        private Room GetRoom(RestRoom restRoom)
        {
            var room = new Room();
            room.Id = restRoom.Id;
            room.Name = restRoom.Name;
            room.CreateDate = restRoom.CreateDate;
            room.IsActive = restRoom.IsActive;
            return room;
        }
    }
}