using Dapper;
using desafio_fullstack.DataBase;
using desafio_fullstack.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace desafio_fullstack.Repository
{
    public class GameResultRepository : IGameResultRepository
    {
        private readonly DbSession _session;

        public GameResultRepository(DbSession session)
        {
            _session = session;
        }

        public async Task<IEnumerable<GameResult>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<GameResult> GetById(long id)
        {
            throw new NotImplementedException();
        }

        public async Task Save(GameResult entity)
        {
            string sql = $"INSERT INTO {nameof(entity)} VALUES({entity.PlayerId}, " +
                $"{entity.GameId}, {entity.Win}, {entity.Timestamp})";

            await _session.Connection.ExecuteAsync(sql, null, _session.Transaction);
        }

        public async Task Update(GameResult entity)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(GameResult entity)
        {
            throw new NotImplementedException();
        }
    }
}
