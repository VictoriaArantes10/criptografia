using Aluno.Context;
using Aluno.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Aluno.Controllers
{
    public class AlunoController : Controller
    {
        private Contexto _contexto = new Contexto();

        // GET: Aluno
        public ActionResult Index()
        {
            var alunos = _contexto.Alunos.ToList();

            foreach(AlunoModel aluno in alunos)
            {
                if(!String.IsNullOrEmpty(aluno.Nome))
                    aluno.Nome = Criptografia.Decrypt(aluno.Nome);
            }

            return View(alunos);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(AlunoModel aluno)
        {
            if (ModelState.IsValid)
            {
                aluno.Nome = Criptografia.Encrypt(aluno.Nome);
                _contexto.Alunos.Add(aluno);
                _contexto.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult Details (int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlunoModel aluno = _contexto.Alunos.Find(id);
            if(aluno == null)
            {
                return HttpNotFound();
            }

            aluno.Nome = Criptografia.Decrypt(aluno.Nome);
            return View(aluno);
        }

        [HttpGet]
        public ActionResult Delete (int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlunoModel aluno = _contexto.Alunos.Find(id);
            if(aluno == null)
            {
                return HttpNotFound();
            }

            aluno.Nome = Criptografia.Decrypt(aluno.Nome);
            return View(aluno);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AlunoModel aluno = _contexto.Alunos.Find(id);
            _contexto.Alunos.Remove(aluno);
            _contexto.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlunoModel aluno = _contexto.Alunos.Find(id);
            if (aluno == null)
            {
                return HttpNotFound();
            }

            aluno.Nome = Criptografia.Decrypt(aluno.Nome);
            return View(aluno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit (AlunoModel aluno)
        {
            if (ModelState.IsValid)
            {
                aluno.Nome = Criptografia.Encrypt(aluno.Nome);
                _contexto.Entry(aluno).State = EntityState.Modified;
                _contexto.SaveChanges();
                return RedirectToAction("Index");
            }

            aluno.Nome = Criptografia.Decrypt(aluno.Nome);
            return View(aluno);
        }
    }
}