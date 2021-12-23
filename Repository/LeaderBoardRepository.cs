using Dapper;
using desafio_fullstack.DataBase;
using desafio_fullstack.Domain;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace desafio_fullstack.Repository
{
    public class LeaderBoardRepository : ILeaderBoardRepository
    {
        private readonly DbSession _session;

        public LeaderBoardRepository(DbSession session)
        {
            _session = session;
        }

        // TODO: melhorar esse código
        public async Task<IEnumerable<LeaderBoard>> GetAll()
        {
            // Load those that are already in the database
            string sql = $"select `{nameof(LeaderBoard.PlayerId)}`, `{nameof(LeaderBoard.Balance)}`, " +
                $"`{nameof(LeaderBoard.LastUpdateDate)}` from `servidor`.`{nameof(LeaderBoard)}`;";

            return await _session.Connection.QueryAsync<LeaderBoard>(sql, null, _session.Transaction);
        }

        public async Task<LeaderBoard> GetById(long id)
        {
            string sql = "";
            
            return await _session.Connection.QueryFirstAsync<LeaderBoard>(sql, null, _session.Transaction);
        }

        public async Task Save(LeaderBoard entity)
        {
            // TODO: Quando não há registro, é criado.
            string sql = $"insert into `servidor`.`LeaderBoard` (`PlayerId`, `Balance`, `LastUpdateDate`) " +
                $"(select `PlayerId`, SUM(`win`) as `Balance`, {DateTime.Now} as `LastUpdateDate` " +
                $"from `world`.`teste` where `PlayerId` = {entity.PlayerId} Group by `PlayerId`);";
        }

        public async Task Update(LeaderBoard entity)
        {
            string sql = $"select `{nameof(GameResult.PlayerId)}`, SUM(`{nameof(GameResult.Win)}`) " +
            $"as Balance from `servidor`.`{nameof(GameResult)}` Group by `{nameof(GameResult.PlayerId)}`;";

            await _session.Connection.QueryAsync<LeaderBoard>(sql, null, _session.Transaction);

            throw new NotImplementedException();
        }

        public async Task Delete(LeaderBoard entity)
        {
            throw new NotImplementedException();
        }
    }
}
