using AulaRemotaThriboCrossfit.Data.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AulaRemotaThriboCrossfit.Controllers
{
    public class VerTreinoController : Controller
    {
        private readonly ITreinoRepository _treinoRepository;

        public VerTreinoController(ITreinoRepository treinoRepository)
        {
            _treinoRepository = treinoRepository;
        }

        public async Task<IActionResult> Index(string id)
        {
            if (id != null)
            {
                var treino = await _treinoRepository.GetById(id);

                if (treino != null)
                {
                    return View(treino);
                }
            }
            return View("TreinoNaoDisponivel");
        }
    }
}
