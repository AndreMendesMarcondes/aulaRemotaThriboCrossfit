using AulaRemotaThriboCrossfit.Data.Interface;
using AulaRemotaThriboCrossfit.Models;
using Google.Cloud.Firestore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AulaRemotaThriboCrossfit.Data.Imp
{
    public class ExercicioRepository : MainContext, IExercicioRepository
    {
        CollectionReference _collection;

        public ExercicioRepository()
        {
            _collection = _db.Collection("Exercicios");
        }

        public async Task Create(Exercicio entity)
        {
            await _collection.Document(entity.Uid).SetAsync(entity);
        }

        public async Task<Exercicio> GetById(string uid)
        {
            var snapshot = await _collection.Document(uid).GetSnapshotAsync();

            if (snapshot.Exists)
            {
                var exercicio = snapshot.ConvertTo<Exercicio>();
                return exercicio;
            }
            return null;
        }

        public async Task<IQueryable<Exercicio>> Get()
        {
            var myList = new List<Exercicio>();
            var snapshot = await _collection.GetSnapshotAsync();

            myList.AddRange(from item in snapshot.Documents
                            select item.ConvertTo<Exercicio>());

            return myList.AsQueryable();
        }

        public async Task Update(string uid, Exercicio entity)
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
