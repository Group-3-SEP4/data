using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebService.DAO;
using WebService.Models.Shared;

namespace WebService.Controllers
{
    /// <summary>
    /// This is the controller for settings which holds get and post methods.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    
    public class SettingsController : Controller
    {
        private EnviormentContext _context;
        private DbRepository _Repo;

        public SettingsController(EnviormentContext context)
        {
            _context = context;
            _Repo = new DbRepository(_context);
        }
        /// <summary>
        /// This a method to get all the settings
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Settings> GetSettings()
        {
            return _Repo.GetSettings();
        }
    }
}