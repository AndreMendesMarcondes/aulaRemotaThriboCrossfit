using Google.Cloud.Firestore;
using System;

namespace AulaRemotaThriboCrossfit.Models
{
    [FirestoreData]
    public class ExercicioDescricao
    {
        public ExercicioDescricao()
        {
            Uid = Guid.NewGuid().ToString();
        }

        [FirestoreDocumentId]
        public string Uid { get; set; }
        [FirestoreProperty]
        public string Descricao { get; set; }
        [FirestoreProperty]
        public virtual Exercicio Exercicio { get; set; }        
    }
}
