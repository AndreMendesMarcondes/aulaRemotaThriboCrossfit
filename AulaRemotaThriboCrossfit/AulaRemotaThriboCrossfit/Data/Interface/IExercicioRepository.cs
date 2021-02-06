using AulaRemotaThriboCrossfit.Models;
using System.Linq;
using System.Threading.Tasks;

namespace AulaRemotaThriboCrossfit.Data.Interface
{
    public interface IExercicioRepository
    {
        public Task Create(Exercicio entity);
        public Task<Exercicio> GetById(string uid);
        Task<IQueryable<Exercicio>> Get();
    }
}
