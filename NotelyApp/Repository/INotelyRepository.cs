using NotelyApp.Models;
using System;
using System.Collections.Generic;

namespace NotelyApp.Repository
{
    public interface INotelyRepository
    {
        void DeleteNote(NoteModel noteModel);
        NoteModel FindNoteById(Guid Id);
        IEnumerable<NoteModel> GetAllNotes();
        void SaveNote(NoteModel noteModel);
    }
}