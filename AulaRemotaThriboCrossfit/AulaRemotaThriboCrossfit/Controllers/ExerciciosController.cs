using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AulaRemotaThriboCrossfit.Models;
using AulaRemotaThriboCrossfit.Data.Interface;

namespace AulaRemotaThriboCrossfit.Controllers
{
    public class ExerciciosController : Controller
    {
        private readonly IExercicioRepository _exercicioRepository;
        private readonly IFirebaseStorageRepository _firebaseStorageRepository;

        public ExerciciosController(IExercicioRepository exercicioRepository, IFirebaseStorageRepository firebaseStorageRepository)
        {
            _exercicioRepository = exercicioRepository;
            _firebaseStorageRepository = firebaseStorageRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _exercicioRepository.Get()); ;
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercicio = await _exercicioRepository.GetById(id);
            if (exercicio == null)
            {
                return NotFound();
            }

            return View(exercicio);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Uid,Nome,Equipamento,Video,TipoExercicio")] ExercicioDTO exercicioDTO)
        {
            var exercicio = new Exercicio();
            if (ModelState.IsValid)
            {
                var stream = exercicioDTO.Video.OpenReadStream();
                var urlVideo = await _firebaseStorageRepository.SaveFileAsync(stream);

                exercicio.Equipamento = exercicioDTO.Equipamento;
                exercicio.Nome = exercicioDTO.Nome;
                exercicio.VideoURL = urlVideo;
                exercicio.TipoExercicio = exercicioDTO.TipoExercicio;

                await _exercicioRepository.Create(exercicio);

                return RedirectToAction(nameof(Index));
            }
            return View(exercicio);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercicio = await _exercicioRepository.GetById(id);
            if (exercicio == null)
            {
                return NotFound();
            }
            return View(exercicio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Uid,Nome,Equipamento,VideoURL")] Exercicio exercicio)
        {
            if (id != exercicio.Uid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _exercicioRepository.Update(exercicio.Uid, exercicio);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await ExercicioExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(exercicio);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercicio = await _exercicioRepository.GetById(id);
            if (exercicio == null)
            {
                return NotFound();
            }

            return View(exercicio);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var exercicio = await _exercicioRepository.GetById(id);
            await _exercicioRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ExercicioExists(string id)
        {
            return await _exercicioRepository.GetById(id) != null;
        }
    }
}
