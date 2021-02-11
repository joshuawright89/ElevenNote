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


    }
}
