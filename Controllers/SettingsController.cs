using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebService.DAO;
using WebService.DAO.Context;
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
        private readonly IDBRepository _repo;

        public SettingsController(IDBRepository repo)
        {
            _repo = repo;
        }
        /// <summary>
        /// This a method to get all the settings
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Settings> GetSettings()
        {
            return _repo.GetSettings();
        }
    }
}