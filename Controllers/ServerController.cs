using desafio_fullstack.Domain;
using desafio_fullstack.Repository;
using desafio_fullstack.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace desafio_fullstack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServerController : ControllerBase
    {
        private readonly ISynchronizeService _synchronizeService;

        public ServerController(ISynchronizeService synchronizeService)
        {
            _synchronizeService = synchronizeService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<LeaderBoard>>> Get()
        {
            try
            {
                List<LeaderBoard> leaderBoard = new List<LeaderBoard>();
                
                var listLeaderBoardUpdate = await _synchronizeService.SynchronizeData();

                leaderBoard.AddRange(listLeaderBoardUpdate);
                return Ok(leaderBoard.OrderByDescending(x => x.Balance).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Synchronize")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ExecuteSynchronizeData()
        {
            try
            {
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
