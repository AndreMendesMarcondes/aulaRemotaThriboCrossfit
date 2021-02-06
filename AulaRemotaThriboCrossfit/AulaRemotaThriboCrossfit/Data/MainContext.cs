using Google.Cloud.Firestore;

namespace AulaRemotaThriboCrossfit.Data
{
    public class MainContext
    {
        protected FirestoreDb _db;

        public MainContext()
        {
            _db = FirestoreDb.Create("aularemotathribocrossfit");
        }
    }
}
