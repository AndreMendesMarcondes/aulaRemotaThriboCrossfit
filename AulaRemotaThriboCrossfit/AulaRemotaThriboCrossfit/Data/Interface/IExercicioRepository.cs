using AulaRemotaThriboCrossfit.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AulaRemotaThriboCrossfit.Data.Interface
{
    public interface IExercicioRepository
    {
        public Task Create(Exercicio entity);
        public Task<Exercicio> GetById(string uid);
        Task<IEnumerable<Exercicio>> Get();
        Task Update(string uid, Exercicio entity);
        Task Delete(string uid);
    }
}
