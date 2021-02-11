using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models
{
    public class NoteDetail   //Here we write the NoteDetail model, which will allow us to view all the properties of a specific Note  (((4.07)))
    {

        public int NoteId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        [Display(Name ="Created:")]
        public DateTimeOffset CreatedUtc { get; set; }

        [Display(Name ="Modified:")]
        public DateTimeOffset? ModifedUtc { get; set; }

        //"Now we can add other endpoints that deal with specific properties, like a GetNoteById method"  (((4.07)))



    }
}
