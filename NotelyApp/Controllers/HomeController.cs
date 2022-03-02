using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotelyApp.Repository;
using NotelyApp.Models;

namespace NotelyApp.Controllers
{
    
       
    public class HomeController : Controller
    {
        private readonly INotelyRepository _noteRepository;
        public HomeController(INotelyRepository notelyRepository)
        {
            _noteRepository = notelyRepository;
        }
        public IActionResult Index()
        {
            var notes = _noteRepository.GetAllNotes().Where(n => n.IsDeleted == false);

            return View(notes);
        }
        public IActionResult NoteDetail(Guid Id)
        {
            var notes = _noteRepository.FindNoteById(Id);

            return View(notes);
        }
        [HttpGet]
        public IActionResult NoteEditor(Guid Id = default)
        {
            if(Id != Guid.Empty)
            {
                var notes = _noteRepository.FindNoteById(Id);
                return View(notes);
            }
            return View();
        }

        public IActionResult NoteEditor(NoteModel noteModel)
        {
            var date = DateTime.Now;

            if (noteModel != null && noteModel.Id == Guid.Empty) {
                noteModel.Id = Guid.NewGuid();
                noteModel.CreatedDate = date;
                noteModel.LastModifiedDate = date;
                _noteRepository.SaveNote(noteModel);
            }
            else
            {
                var notes = _noteRepository.FindNoteById(noteModel.Id);
                notes.LastModifiedDate = date;
                notes.Subject = noteModel.Subject;
                notes.Detail = noteModel.Detail;
            }
            return RedirectToAction("Index");
        }
        public IActionResult DeleteNote(Guid Id)
        {
            var note = _noteRepository.FindNoteById(Id);
            note.IsDeleted = true;
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
