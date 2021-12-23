using desafio_fullstack.Domain;
using desafio_fullstack.Repository;
using desafio_fullstack.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly ISynchronizeService _synchronizeService;

        public ServerController(IGameResultRepository gameResultRepository,
                                ILeaderBoardRepository leaderBoardRepository,
                                ISynchronizeService synchronizeService)
        {
            _gameResultRepository = gameResultRepository;
            _leaderBoardRepository = leaderBoardRepository;
            _synchronizeService = synchronizeService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<LeaderBoard>>> Get()
        {
            List<LeaderBoard> leaderBoard = new List<LeaderBoard>();

            //using (var redisClient = new RedisClient())
            //{
            //    var redisResult = redisClient.GetAll<LeaderBoard>();
            //    leaderBoard.AddRange(redisResult);
            //}

            await _synchronizeService.SynchronizeData();

            var dataBaseResult = _leaderBoardRepository.GetAll();

            try
            {
                // Mock
                leaderBoard.Add(new LeaderBoard(0, 250, DateTime.Now));
                leaderBoard.Add(new LeaderBoard(1, 320, DateTime.Now));
                leaderBoard.Add(new LeaderBoard(2, 120, DateTime.Now));
                leaderBoard.Add(new LeaderBoard(3, 530, DateTime.Now));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(leaderBoard.OrderByDescending(x => x.Balance).ToList());
        }

        [HttpPost("Synchronize")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ExecuteSynchronizeData()
        {
            // Sincroniza os dados
            try
            {
                // Em andamento
                await _synchronizeService.SynchronizeData();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(GameResult gameResult)
        {
            try
            {
                await _synchronizeService.SaveRedisData(gameResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction("Post", gameResult);
        }
    }
}
