using desafio_fullstack.Domain;
using desafio_fullstack.Repository;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace desafio_fullstack.Service
{
    public class SynchronizeService : ISynchronizeService
    {
        private readonly IGameResultRepository _gameResultRepository;
        private readonly ILeaderBoardRepository _leaderBoardRepository;

        public SynchronizeService(IGameResultRepository gameResultRepository,
                                  ILeaderBoardRepository leaderBoardRepository)
        {
            _gameResultRepository = gameResultRepository;
            _leaderBoardRepository = leaderBoardRepository;
        }

        public async Task<IEnumerable<LeaderBoard>> SynchronizeData()
        {
            var listLeaderBoard = new List<LeaderBoard>();

            RefreshLeaderBoardByRedisData();
            var leaderBoardList = await _leaderBoardRepository.GetAll();
            listLeaderBoard.AddRange(leaderBoardList);
            
            return listLeaderBoard;
        }

        private void RefreshLeaderBoardByRedisData()
        {            
            using (var redisClient = new RedisClient())
            {
                var gameResultsList = redisClient.GetAllKeys();
                for (var x = 0; x < gameResultsList.Count; x ++)
                {
                    var gameResultTemp = redisClient.Get<GameResult>(gameResultsList[x]);
                    _gameResultRepository.Save(gameResultTemp);
                    _leaderBoardRepository.Update(gameResultTemp.Id);
                }
                //redisClient.FlushAll();
            }
        }

        public async Task SaveRedisData(GameResult gameResult)
        {
            Guid guid = Guid.NewGuid();
            string cacheKey = $"gameresults_{guid}";

            using (var redisClient = new RedisClient())
            {
                redisClient.Set<GameResult>(cacheKey, gameResult);
            }
        }
    }
}
