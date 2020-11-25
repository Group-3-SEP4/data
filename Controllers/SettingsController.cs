using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebService.DAO.Repository;
using WebService.Models;


namespace WebService.Controllers
{
    /// <summary>
    /// This is the controller for settings which holds get and post methods.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    
    public class SettingsController : Controller
    {
        private readonly IDbRepository _repo;

        public SettingsController(IDbRepository repo)
        {
            _repo = repo;
        }
        /// <summary>
        /// This a method to get all the settings
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Settings GetSettings()
        {
            return _repo.GetSettings();
        }
        
        /// <summary>
        /// This a method to post the changed settings. Returns the posted settings afterwards
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Settings PostSettings(Settings settings)
        {
            return _repo.PostSettings(settings);
        }
    }
}