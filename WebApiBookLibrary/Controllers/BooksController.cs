using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiBookLibrary.Context;
using WebApiBookLibrary.Entities;
using WebApiBookLibrary.Services;

namespace WebApiBookLibrary.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        //Services test
        private readonly IClaseB claseB;

        public BooksController(ApplicationDBContext context, IClaseB claseB)
        {
            this.context = context;
            //test services
            this.claseB = claseB;
        }

        //Return all the books
        //https://localhost:44329/api/Books
        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get()
        {
            //Test services
            //claseB.todosomething();
            return context.Books.Include(x => x.author).ToList();
            // Return the list of book 
            //return context.Books.ToList();
        }

        //return the information of the book 
        [HttpGet("{id}", Name = "GetBook")]
        public ActionResult<Book> Get(int id)
        {
            var book = context.Books.Include(x=>x.author).FirstOrDefault(x => x.Id == id);

            if (book == null)
            {
                return NotFound();
            }
            return book;

        }

        //Add new book
        ////https://localhost:44329/api/Books
        ///el authorid has to be valid in the table author
        //{	"Title":"Javascript","AuthorId":"2"}
    [HttpPost]
        public ActionResult Post([FromBody] Book book)
        {
            context.Books.Add(book);
            context.SaveChanges();
            return new CreatedAtRouteResult("GetBook", new { id = book.Id }, book);
        }

        //Delete a record
        //https://localhost:44329/api/Bookss/1
        [HttpDelete("{id}")]
        public ActionResult<Book> Delete(int id)
        {
            var book = context.Books.FirstOrDefault(x => x.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            context.Books.Remove(book);
            context.SaveChanges();
            return book;
        }

    }
}
