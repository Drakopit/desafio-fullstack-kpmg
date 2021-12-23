using desafio_fullstack.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace desafio_fullstack.Service
{
    public interface ISynchronizeService
    {
        Task SynchronizeData();
        Task SaveRedisData(GameResult gameResult);
    }
}
