using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AulaRemotaThriboCrossfit.Models
{
    [FirestoreData]
    public class User
    {
        [FirestoreDocumentId]
        public string Uid { get; set; }
        [FirestoreProperty]
        public string Nome { get; set; }
        [FirestoreProperty]
        public string Senha { get; set; }
    }
}
