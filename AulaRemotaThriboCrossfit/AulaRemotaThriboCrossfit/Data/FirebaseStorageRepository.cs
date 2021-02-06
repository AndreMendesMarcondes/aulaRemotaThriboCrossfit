using AulaRemotaThriboCrossfit.Data.Interface;
using Firebase.Storage;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AulaRemotaThriboCrossfit.Data
{
    public class FirebaseStorageRepository : IFirebaseStorageRepository
    {
        public async Task<string> SaveFileAsync(Stream img)
        {
            var task = new FirebaseStorage("aularemotathribocrossfit.appspot.com")
             .Child(Guid.NewGuid().ToString())
             .PutAsync(img);

            var downloadUrl = await task;
            return downloadUrl;
        }
    }
}
