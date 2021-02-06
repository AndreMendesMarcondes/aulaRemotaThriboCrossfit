using Google.Cloud.Firestore;
using System;

namespace AulaRemotaThriboCrossfit.Models
{
    [FirestoreData]
    public class Exercicio : BaseDomain
    {
        public Exercicio()
        {
            Uid = Guid.NewGuid().ToString();
        }
        [FirestoreDocumentId]
        public string Uid { get; set; }
        [FirestoreProperty]
        public string Nome { get; set; }
        [FirestoreProperty]
        public string Equipamento { get; set; }
        [FirestoreProperty]
        public string VideoURL { get; set; }
    }
}
