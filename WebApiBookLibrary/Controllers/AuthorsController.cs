using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiBookLibrary.Context;
using WebApiBookLibrary.Entities;

namespace WebApiBookLibrary.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {

        public readonly ApplicationDBContext context;

        public AuthorsController(ApplicationDBContext context)
        {
            this.context = context;
        }


        //Bring all authors
        //https://localhost:44329/api/Authors
        [HttpGet]
        public ActionResult<IEnumerable<Author>> Get()
        {
            //return all author with their books
            return context.Authors.Include(x => x.Books).ToList();
            //return all author
            //return context.Authors.ToList();
        }

        //Bring all author speific
        //you can call this method through the name GetAuthor return new CreatedAtRouteResult("GetAuthor", new { id = author.Id},author);
        //https://localhost:44329/api/Authors/1
        [HttpGet("{id}",Name ="GetAuthor")]
        public ActionResult<Author> Get(int id)
        {

            var author = context.Authors.Include(x => x.Books).FirstOrDefault(x => x.Id == id);

            //return simple
            //var author = context.Authors.FirstOrDefault(x => x.Id == id);

            if (author == null)
            {
                return NotFound();
            }

            return author;

        }

        //Add new Author and it return the author calling the method Get through GetAuthor and sending the name of the object created
        //https://localhost:44329/api/Authors
        //{"Name":"Douglas Loaiza"}
        [HttpPost]
        public ActionResult Post([FromBody] Author author)
        {
            context.Authors.Add(author);
            context.SaveChanges();
            return new CreatedAtRouteResult("GetAuthor", new { id = author.Id},author);
        }


        //Update a record
        //https://localhost:44329/api/Authors/1
        //{"id":1,"name":"Douglas Loaiza Sierra"}
        [HttpPut("{id}")]
        public ActionResult Put(int id,[FromBody] Author value)
        {
            if(id != value.Id)
            {
                return BadRequest();
            }
            
            context.Entry(value).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }




        //Delete a record
        //https://localhost:44329/api/Authors/1
        [HttpDelete("{id}")]
        public ActionResult <Author>Delete(int id)
        {
            var author = context.Authors.FirstOrDefault(x => x.Id == id);

            if (author == null)
            {
                return NotFound();
            }

            context.Authors.Remove(author);
            context.SaveChanges();
            return author;
        }
    }
}

