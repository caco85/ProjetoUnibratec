﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PI_INFOX.Models.Basicas;

namespace PI_INFOX.Areas.Aluno.Controllers
{
    public class UsuarioController : Controller
    {
        private dbInfoXContext db = new dbInfoXContext();

        //
        // GET: /Usuario/

        public ActionResult Index()
        {
            var userid = (Session["usuarioLogadoID"]);
            var id = Convert.ToInt32(userid);
            basUsuario basusuario = db.Usuario.Find(id);
            if (basusuario == null)
            {
                return HttpNotFound();
            }
            var usuarios = db.Usuario.Include(b => b.Disciplinas);
            return View(basusuario);
                     
                   
        }
         
        
        //
        // GET: /Usuario/Details/5

        public ActionResult Details(int id = 0)
        {
            basUsuario basusuario = db.Usuario.Find(id);
            if (basusuario == null)
            {
                return HttpNotFound();
            }
            return View(basusuario);
        }

        //
        // GET: /Usuario/Create

        public ActionResult Create()
        {
            ViewBag.DisciplinaID = new SelectList(db.Disciplina, "DisciplinaID", "nomeDisciplina");
            return View();
        }

        //
        // POST: /Usuario/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(basUsuario basusuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuario.Add(basusuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DisciplinaID = new SelectList(db.Disciplina, "DisciplinaID", "nomeDisciplina", basusuario.DisciplinaID);
            return View(basusuario);
        }

        //
        // GET: /Usuario/Edit/5

        public ActionResult Edit(int id = 0)
        {
            basUsuario basusuario = db.Usuario.Find(id);
            if (basusuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.DisciplinaID = new SelectList(db.Disciplina, "DisciplinaID", "nomeDisciplina", basusuario.DisciplinaID);
            return View(basusuario);
        }

        //
        // POST: /Usuario/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(basUsuario basusuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(basusuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DisciplinaID = new SelectList(db.Disciplina, "DisciplinaID", "nomeDisciplina", basusuario.DisciplinaID);
            return View(basusuario);
        }

        //
        // GET: /Usuario/Delete/5

        public ActionResult Delete(int id = 0)
        {
            basUsuario basusuario = db.Usuario.Find(id);
            if (basusuario == null)
            {
                return HttpNotFound();
            }
            return View(basusuario);
        }

        //
        // POST: /Usuario/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            basUsuario basusuario = db.Usuario.Find(id);
            db.Usuario.Remove(basusuario);
            db.SaveChanges();
            Session.Abandon();
            return RedirectToAction("Index", "Home", new { Area = "" });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}