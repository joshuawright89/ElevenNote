using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models
{
    /// <summary>
    /// (((4.10)))  NoteEdit *model*
    /// </summary>
    public class NoteEdit  //look in NoteService for corresponding service method UpdateNote.cs
    {
        public int NoteId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }


    }
}
