using desafio_fullstack.Domain;
using desafio_fullstack.Repository;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using ServiceStack.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desafio_fullstack.Service
{
    public class SynchronizeService : ISynchronizeService
    {
        private readonly IGameResultRepository _gameResultRepository;
        private readonly ILeaderBoardRepository _leaderBoardRepository;
        private readonly IDistributedCache _distributedCache;

        public SynchronizeService(IGameResultRepository gameResultRepository,
                                  ILeaderBoardRepository leaderBoardRepository,
                                  IDistributedCache distributedCache)
        {
            _gameResultRepository = gameResultRepository;
            _leaderBoardRepository = leaderBoardRepository;
            _distributedCache = distributedCache;
        }

        public async Task SynchronizeData()
        {
            // Preciso ver como fazer para carregar a lista final com os dados somados
            var listLeaderBoard = new List<LeaderBoard>();
            var listRedis = LoadRedisData();
            var listDataBase = await LoadDataBaseData();
            //listLeaderBoard.AddRange(listRedis);
            //listLeaderBoard.AddRange(listDataBase);
        }

        private async Task<IEnumerable<LeaderBoard>> LoadDataBaseData()
        {
            return await _leaderBoardRepository.GetAll();
        }

        private IEnumerable<GameResult> LoadRedisData()
        {
            string cacheKey = "gameResults";
            string serializeGameResults;
            var gameResults = new List<GameResult>();

            var redisGameResults = _distributedCache.Get(cacheKey);

            if (redisGameResults != null)
            {
                serializeGameResults = Encoding.UTF8.GetString(redisGameResults);
                gameResults = JsonConvert.DeserializeObject<List<GameResult>>(serializeGameResults);
            }

            //using (var redisClient = new RedisClient())
            //{
            //    var ola = redisClient.SearchKeys(nameof(GameResult.Id));
            //    var teste = redisClient.Get<GameResult>(nameof(GameResult.Id));
            //    // gameResultList.AddRange(teste);
            //    redisClient.FlushAll();
            //}

            return gameResults;
        }

        public async Task SaveRedisData(GameResult gameResult)
        {
            string cacheKey = "gameresults";
            string serializedGameResult = JsonConvert.SerializeObject(gameResult);
            var gameresultByte = Encoding.UTF8.GetBytes(serializedGameResult);
            var options = new DistributedCacheEntryOptions();

            _distributedCache.SetAsync(cacheKey, gameresultByte, options);
            //using (var redisClient = new RedisClient())
            //{
            //    redisClient.Set<GameResult>(gameResult.Id.ToString(), gameResult);
            //}
        }
    }
}
