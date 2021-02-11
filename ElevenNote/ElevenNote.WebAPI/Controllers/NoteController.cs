using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ElevenNote.WebAPI.Controllers
{
    [Authorize]
    public class NoteController : ApiController  //"Inside the controller, we're going to add a method that creates a NoteService."  (((4.03)))
    {
        private NoteService CreateNoteService()  //Creates a NoteService   (((4.03)))
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var noteService = new NoteService(userId);
            return noteService;
        }

        public IHttpActionResult Get()   //"Now let's add a Get All method." (((4.03)))
        {
            NoteService noteService = CreateNoteService();
            var notes = noteService.GetNotes();
            return Ok(notes);
        }

        public IHttpActionResult Post(NoteCreate note)   //"And a Post(NoteCreate note) method:"  (((4.03)))
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateNoteService();

            if (!service.CreateNote(note))
                return InternalServerError();

            return Ok();
        }
        

        //(((4.08)))
        //"Next we'll make our GetById [controller] method."
        public IHttpActionResult Get(int id)  //refer to NoteService for the corresponding service method
        {
            NoteService noteService = CreateNoteService();
            var note = noteService.GetNoteById(id);  //<--- here we call the service method
            return Ok(note);
        }

    }
}
