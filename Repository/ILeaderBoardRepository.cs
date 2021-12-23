using desafio_fullstack.Domain;
using System.Threading.Tasks;

namespace desafio_fullstack.Repository
{
    public interface ILeaderBoardRepository : IRepository<LeaderBoard>
    {
        Task Update(long playerId);
    }
}
