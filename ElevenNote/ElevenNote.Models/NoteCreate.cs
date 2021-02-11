using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models
{
    public class NoteCreate   //"Think about the things we'll need to capture when we create a note and save it to the database. We need to create a Title, Content, and DateCreated. Will we provide an id though?...no.  The id will be created after the POST request happens, after we fill out a form with the two properties above... Our .Service and .Data layer[s] will work together to take are of that."
    {
        [Required]
        [MinLength(3, ErrorMessage = "Please enter at least three (3) characters.")]
        [MaxLength(2000, ErrorMessage = "Do not exceed 2,000 characters for this note's title.")]
        public string Title { get; set; }
        [MaxLength(8000, ErrorMessage = "Do not exceed 8,000 characters.")]
        public string Content { get; set; }

    }
}
