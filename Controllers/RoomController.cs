using Microsoft.AspNetCore.Mvc;
using WebService.DAO.Repository;
using WebService.Models;

namespace WebService.Controllers
{
    /// <summary>
    /// This is the controller for getting or posting data related to rooms.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RoomController : Controller
    {
        
        private readonly IDbRepository _repo;

        public RoomController(IDbRepository repo) {
            _repo = repo;
        }
        
        /// <summary>
        /// This is a method to initialize the room for a newly added device, will return true. If the device was already added it will send false
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns>Returns either a true if it was initialyzed or false if it is already there</returns>
        [HttpPost]
        public bool InitRoom([FromQuery(Name = "deviceId")] string deviceId) {
            return _repo.InitRoom(deviceId);
        }
        
        /// <summary>
        /// This is a method to get the room entity with a specific deviceID.
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns>Returns an entity of room that was requested</returns>
        [HttpGet]
        public Room GetRoom([FromQuery(Name = "deviceId")] string deviceId) {
            return _repo.GetRoom(deviceId);
        }
    }
}