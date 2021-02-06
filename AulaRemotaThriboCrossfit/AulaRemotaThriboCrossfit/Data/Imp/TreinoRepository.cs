using AulaRemotaThriboCrossfit.Data.Interface;
using AulaRemotaThriboCrossfit.Models;
using Google.Cloud.Firestore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AulaRemotaThriboCrossfit.Data.Imp
{
    public class TreinoRepository : MainContext, ITreinoRepository
    {
        CollectionReference _collection;

        public TreinoRepository()
        {
            _collection = _db.Collection("Treinos");
        }

        public async Task Create(Treino entity)
        {
            await _collection.Document(entity.Uid).SetAsync(entity);
        }

        public async Task<Treino> GetById(string uid)
        {
            var snapshot = await _collection.Document(uid).GetSnapshotAsync();

            if (snapshot.Exists)
            {
                var exercicio = snapshot.ConvertTo<Treino>();
                return exercicio;
            }
            return null;
        }

        public async Task<IQueryable<Treino>> Get()
        {
            var myList = new List<Treino>();
            var snapshot = await _collection.GetSnapshotAsync();

            myList.AddRange(from item in snapshot.Documents
                            select item.ConvertTo<Treino>());

            return myList.AsQueryable();
        }

        public async Task Update(string uid, Treino entity)
        {
            var document = _collection.Document(uid);
            await document.SetAsync(entity);
        }
        public async Task Delete(string uid)
        {
            await _collection.Document(uid).DeleteAsync();
        }
    }
}
