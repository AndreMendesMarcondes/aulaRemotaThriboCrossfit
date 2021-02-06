using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;

namespace AulaRemotaThriboCrossfit.Models
{
    [FirestoreData]
    public class Treino
    {
        public Treino()
        {
            Uid = Guid.NewGuid().ToString();
            Exercicios = new List<ExercicioDescricao>();
        }
        [FirestoreDocumentId]
        public string Uid { get; set; }
        [FirestoreProperty]
        public DateTime Dia { get; set; }
        [FirestoreProperty]
        public List<ExercicioDescricao> Exercicios { get; set; }
    }
}
