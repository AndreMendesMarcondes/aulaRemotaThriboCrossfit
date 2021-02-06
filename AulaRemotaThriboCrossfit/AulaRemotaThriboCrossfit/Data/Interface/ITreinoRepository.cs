using AulaRemotaThriboCrossfit.Models;
using System.Linq;
using System.Threading.Tasks;

namespace AulaRemotaThriboCrossfit.Data.Interface
{
    public interface ITreinoRepository
    {
        public Task Create(Treino entity);
        public Task<Treino> GetById(string uid);
        Task<IQueryable<Treino>> Get();
        Task Update(string uid, Treino entity);
        Task Delete(string uid);
    }
}
