using System.IO;
using System.Threading.Tasks;

namespace AulaRemotaThriboCrossfit.Data.Interface
{
    public interface IFirebaseStorageRepository
    {
        Task<string> SaveFileAsync(Stream img);
    }
}
