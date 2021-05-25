using BeerCup.DataAccess;
using BeerCup.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerCup.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BattleController : ControllerBase
    {
        private readonly IRepository<Battle> battleRepository;

        public BattleController(IRepository<Battle> battleRepository)
        {
            this.battleRepository = battleRepository;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<Battle> GetAllBattles() => this.battleRepository.GetAll();

        [HttpGet]
        [Route("battleId")]
        public Battle GetBattleById(int battleId) => this.battleRepository.GetById(battleId);
    }
}
