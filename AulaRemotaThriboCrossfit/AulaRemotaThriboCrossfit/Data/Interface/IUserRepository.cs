using AulaRemotaThriboCrossfit.Models;
using System.Threading.Tasks;

namespace AulaRemotaThriboCrossfit.Data.Interface
{
    public interface IUsuarioRepository
    {
        Task<User> Get(string username, string password);
    }
}
