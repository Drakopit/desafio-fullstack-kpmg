using Dapper;
using desafio_fullstack.DataBase;
using desafio_fullstack.Domain;
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
            try
            {
                // Load those that are already in the database
                string sql = $"select `{nameof(LeaderBoard.Id)}`, `{nameof(LeaderBoard.PlayerId)}`, `{nameof(LeaderBoard.Balance)}`, " +
                    $"`{nameof(LeaderBoard.LastUpdateDate)}` from `servidor`.`{nameof(LeaderBoard)}`;";

                return await _session.Connection.QueryAsync<LeaderBoard>(sql, null, _session.Transaction);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<LeaderBoard> GetById(long id)
        {
            throw new NotImplementedException();
        }

        public async Task Save(long playerId)
        {
            // TODO: Quando não há registro, é criado.
            string sql = $"insert into `servidor`.`LeaderBoard` (`PlayerId`, `Balance`, `LastUpdateDate`) " +
                $"(select `PlayerId`, SUM(`win`) as `Balance`, {DateTime.Now} as `LastUpdateDate` " +
                $"from `servidor`.`gameresult` where `PlayerId` = {playerId} Group by `PlayerId`);";
        }

        public async Task<bool> CheckIfExist(long playerId)
        {
            string sql = $"select * from `servidor`.`gameresult` where `PlayerId` = {playerId}";

            var result = await _session.Connection.QueryAsync<GameResult>(sql, null, _session.Transaction);
            return result == null ? false : true;
        } 

        public async Task UpdateBalanceLeaderBoard(long playerId)
        {
            if (await CheckIfExist(playerId) == false)
                await Save(playerId);
            else
                await Update(playerId);
        }

        public async Task Update(long playerId)
        {
            string sql = $"SET @player = 1; " +
                $"SET SQL_SAFE_UPDATES = 0; " +
                $"update `world`.`LeaderBoard`, " +
                $"(select `PlayerId`, SUM(`PlayerId`) as `Balance`, {DateTime.Now} as `LastUpdateDate` " +
                $"from `Servidor`.`GameResult` where `PlayerId` = {playerId} Group by `PlayerId`) as leaderBoardTemp " +
                $"set `LeaderBoard`.`Balance` = leaderBoardTemp.`Balance` " +
                $"and `LeaderBoard`.`LastUpdateDate` = leaderBoardTemp.`LastUpdateDate` " +
                $"where `LeaderBoard`.`PlayerId` = {playerId}; " +
                $"SET SQL_SAFE_UPDATES = 1; ";

            await _session.Connection.QueryAsync<LeaderBoard>(sql, null, _session.Transaction);

            throw new NotImplementedException();
        }

        public async Task Delete(LeaderBoard entity)
        {
            throw new NotImplementedException();
        }

        public async Task Update(LeaderBoard entity)
        {
            throw new NotImplementedException();
        }

        public async Task Save(LeaderBoard entity)
        {
            throw new NotImplementedException();
        }
    }
}
