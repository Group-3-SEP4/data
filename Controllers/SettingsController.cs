using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebService.DAO;
using WebService.Models.Shared;

namespace WebService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class SettingsController : Controller
    {
        private EnviormentContext _context;
        private DbRepository _Repo;

        public SettingsController(EnviormentContext context)
        {
            _context = context;
            _Repo = new DbRepository(_context);
        }

        [HttpGet]
        public IEnumerable<Settings> GetSettings()
        {
            return _Repo.GetSettings();
        }
    }
}