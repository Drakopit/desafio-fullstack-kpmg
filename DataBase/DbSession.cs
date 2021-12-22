using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace desafio_fullstack.DataBase
{
    public sealed class DbSession : IDisposable
    {
        private Guid id;
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }

        public DbSession(IConfiguration configuration)
        {
            id = Guid.NewGuid();
            Connection = new SqlConnection(configuration.GetConnectionString("Conexao"));
            Connection.Open();
        }

        public void Dispose() => Connection?.Dispose();
    }
}
