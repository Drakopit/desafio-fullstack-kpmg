using desafio_fullstack.Domain;
using desafio_fullstack.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace desafio_fullstack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServerController : ControllerBase
    {
        private readonly IGameResultRepository _gameResultRepository;
        private readonly ILeaderBoardRepository _leaderBoardRepository;

        public ServerController(IGameResultRepository gameResultRepository,
                                ILeaderBoardRepository leaderBoardRepository)
        {
            _gameResultRepository = gameResultRepository;
            _leaderBoardRepository = leaderBoardRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeaderBoard>>> Get()
        {
            List<LeaderBoard> list = new List<LeaderBoard>();
            list.Add(new LeaderBoard(0, 250, DateTime.Now));
            list.Add(new LeaderBoard(1, 320, DateTime.Now));
            list.Add(new LeaderBoard(2, 120, DateTime.Now));
            list.Add(new LeaderBoard(3, 530, DateTime.Now));
            return list.OrderByDescending(x => x.Balance).ToList();
        }

        [HttpPost]
        public async Task<ActionResult<GameResult>> Post([FromBody] GameResult gameResult)
        {
            return gameResult;
        }
    }
}
