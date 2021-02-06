using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AulaRemotaThriboCrossfit.Models;
using AulaRemotaThriboCrossfit.Data.Interface;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;
using System;

namespace AulaRemotaThriboCrossfit.Controllers
{
    [Authorize]
    public class TreinoController : Controller
    {
        private List<Exercicio> ListaExercicios
        {
            get
            {
                if (TempData["ListaDeExercicios"] == null)
                {
                    CarregarListaExercicios().Wait();
                    return (List<Exercicio>)TempData["ListaDeExercicios"];
                }
                else
                {
                    return (List<Exercicio>)TempData["ListaDeExercicios"];
                }
            }
        }

        private readonly ITreinoRepository _treinoRepository;
        private readonly IExercicioRepository _exercicioRepository;

        public TreinoController(ITreinoRepository treinoRepository, IExercicioRepository exercicioRepository)
        {
            _treinoRepository = treinoRepository;
            _exercicioRepository = exercicioRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _treinoRepository.Get()); ;
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treino = await _treinoRepository.GetById(id);
            if (treino == null)
            {
                return NotFound();
            }

            return View(treino);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection collection)
        {
            if (ModelState.IsValid)
            {
                int maxValue = GetMaxValue(collection);
                var exercicioDescricaos = new List<ExercicioDescricao>();

                for (int i = 0; i <= maxValue; i++)
                {
                    if (!string.IsNullOrEmpty(collection[$"selectExercicios{i}"]))
                    {
                        var exercicioDescricao = new ExercicioDescricao();
                        exercicioDescricao.Descricao = collection[$"descricaoExercicios{i}"];
                        exercicioDescricao.Exercicio = ListaExercicios.FirstOrDefault(c => c.Uid == collection[$"selectExercicios{i}"]);
                        exercicioDescricaos.Add(exercicioDescricao);
                    }
                }

                if (exercicioDescricaos.Any())
                {
                    var treino = new Treino();
                    treino.Dia = Convert.ToDateTime(collection["Dia"]).ToUniversalTime();
                    treino.Exercicios = exercicioDescricaos;
                    await _treinoRepository.Create(treino);
                }
            }
            return View();
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treino = await _treinoRepository.GetById(id);
            if (treino == null)
            {
                return NotFound();
            }
            return View(treino);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Uid,Dia")] Treino treino)
        {
            if (id != treino.Uid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _treinoRepository.Update(treino.Uid, treino);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await TreinoExists(id))
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
            return View(treino);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treino = await _treinoRepository.GetById(id);
            if (treino == null)
            {
                return NotFound();
            }

            return View(treino);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var treino = await _treinoRepository.GetById(id);
            await _treinoRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> TreinoExists(string id)
        {
            return await _treinoRepository.GetById(id) != null;
        }

        private async Task CarregarListaExercicios()
        {
            var listaExercicios = await _exercicioRepository.Get();
            TempData["ListaDeExercicios"] = listaExercicios;
        }

        [HttpPost]
        public IActionResult _renderPartialSelectExercicios(string tipoExercicio, string equipamento)
        {
            var listaFiltrada = new List<Exercicio>();

            if (!string.IsNullOrEmpty(tipoExercicio) && !string.IsNullOrEmpty(equipamento))
                listaFiltrada = ListaExercicios.Where(c => c.TipoExercicio == tipoExercicio && c.Equipamento == equipamento).ToList();

            ViewBag.ListaDeExercicios = listaFiltrada;

            if (listaFiltrada.Any())
                return PartialView(listaFiltrada);
            else
                return null;
        }

        [HttpPost]
        public IActionResult _NovoExercicioTreino(int count)
        {
            return PartialView(count);
        }

        private int GetMaxValue(IFormCollection collection)
        {
            int max = 0;
            foreach (var item in collection.Keys)
            {
                int numero = 0;
                Int32.TryParse(Regex.Match(item, @"\d+").Value, out numero);

                if (numero > max)
                    max = numero;
            }

            return max;
        }
    }
}
