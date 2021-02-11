using ElevenNote.Data;
using ElevenNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services
{
    /// <summary>
    /// "The service layer is how our application interacts with the database.  In this section we will create the NoteService that will push and pull notes from the database."
    /// </summary>

    public class NoteService    //(((4.02)))  *from above* "...create the NoteService that will push and pull notes from the database."
    {
        private readonly Guid _userId;

        public NoteService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateNote(NoteCreate model)   //"This will create an instance of Note." (((4.02)))
        {
            var entity =
                new Note()
                {
                    OwnerId = _userId,
                    Title = model.Title,
                    Content = model.Content,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Notes.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<NoteListItem> GetNotes()    //"This method will allow us to see all the notes that belong to a specific user." (((4.02)))
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Notes
                    .Where(e => e.OwnerId == _userId)
                    .Select(
                        e =>
                        new NoteListItem
                        {
                            NoteId = e.NoteId,
                            Title = e.Title,
                            CreatedUtc = e.CreatedUtc
                        }
                        );
                return query.ToArray();
            }
        }

        //(((4.08)))
        public NoteDetail GetNoteById(int id)  //this is our GetNoteById SERVICE method
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Notes.Single(e => e.NoteId == id && e.OwnerId == _userId);
                return
                    new NoteDetail
                    {
                        NoteId = entity.NoteId,
                        Title = entity.Title,
                        Content = entity.Content,
                        CreatedUtc = entity.CreatedUtc,
                        ModifedUtc = entity.ModifiedUtc
                    };
            }
        }

        //(((4.10)))
        public bool UpdateNote(NoteEdit model)   //...returns a bool based on if the NoteId is in the database AND the note belongs to a specific _userId:"  This is our SERVICE method for NoteEdit model (located in .Models)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Notes
                    .Single(e => e.NoteId == model.NoteId && e.OwnerId == _userId);  //should be one note with a matching NoteId AND created by the same user

                entity.Title = model.Title;
                entity.Content = model.Content;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }


        //(((4.12)))
        public bool DeleteNote(int noteId)  //this is our service method for Delete method in our controller
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                ctx
                    .Notes
                    .Single(e => e.NoteId == noteId && e.OwnerId == _userId);

                ctx.Notes.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }


    }
}
