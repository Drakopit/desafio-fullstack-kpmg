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

        public async Task<IEnumerable<LeaderBoard>> GetAll()
        {
            string sql = "";

            return await _session.Connection.QueryAsync<LeaderBoard>(sql, null, _session.Transaction);
        }

        public async Task<LeaderBoard> GetById(long id)
        {
            string sql = "";
            
            return await _session.Connection.QueryFirstAsync<LeaderBoard>(sql, null, _session.Transaction);
        }

        public async Task Save(LeaderBoard entity)
        {
            throw new NotImplementedException();
        }

        public async Task Update(LeaderBoard entity)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(LeaderBoard entity)
        {
            throw new NotImplementedException();
        }
    }
}
