using Microsoft.AspNetCore.Mvc;
using WebService.Repository;
using WebService.Repository.Context;

namespace WebService.Controllers
{
    /// <summary>
    /// This is the controller for settings which holds get and post methods.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    
    public class SettingsController : Controller {
        private readonly IDbRepository _repo;

        public SettingsController(IDbRepository repo) {
            _repo = repo;
        }
        
        /// <summary>
        /// This a method to get all the settings. You send the device id to request settings for that device.
        /// </summary>
        /// <param name="deviceEUI"></param>
        /// <returns>Returns settings that you requested</returns>
        [HttpGet]
        public Settings GetSettings([FromQuery(Name = "deviceEUI")] string deviceEUI) {
            return _repo.GetSettings(deviceEUI);
            
        }
        
        /// <summary>
        /// This a method to post the changed settings. Returns the posted settings afterwards
        /// </summary>
        /// <param name="settings"></param>
        /// <returns>Returns a settings entity that you sent, to confirm that it was updated</returns>
        [HttpPost]
        public Settings PostSettings(Settings settings) {
            return _repo.PostSettings(settings);
        }
    }
}