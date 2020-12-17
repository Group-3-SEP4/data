using Microsoft.AspNetCore.Mvc;
using WebService.Repository;
using WebService.Repository.Context;

namespace WebService.Controllers
{
    /// <summary>
    /// This is the controller for getting or posting data related to rooms.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RoomsController : Controller {
        private readonly IDbRepository _repo;

        public RoomsController(IDbRepository repo) {
            _repo = repo;
        }
        
        /// <summary>
        /// This is a method to initialize the room for a newly added device, will return true. If the device was already added it will send false
        /// </summary>
        /// <param name="deviceEUI"></param>
        /// <returns>Returns either a true if it was initialyzed or false if it is already there</returns>
        [HttpPost]
        public bool InitRoom([FromQuery(Name = "deviceEUI")] string deviceEUI) {
            return _repo.InitRoom(deviceEUI);
        }
        
        /// <summary>
        /// This is a method to get the room entity with a specific deviceID.
        /// </summary>
        /// <param name="deviceEUI"></param>
        /// <returns>Returns an entity of room that was requested</returns>
        [HttpGet]
        public Room GetRoom([FromQuery(Name = "deviceEUI")] string deviceEUI) {
            return _repo.GetRoom(deviceEUI);
        }
        
        /// <summary>
        /// This method is for updating the name of the room. THe whole room has to be passed
        /// </summary>
        /// <param name="room"></param>
        /// <returns>The updated entity of the room</returns>
        [HttpPut]
        public Room UpdateRoom(Room room) {
            return _repo.UpdateRoom(room);
        }
    }
}