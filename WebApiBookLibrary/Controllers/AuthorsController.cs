using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiBookLibrary.Context;
using WebApiBookLibrary.Entities;
using WebApiBookLibrary.Helpers;

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


        //Return first of the table with this url https://localhost:44329/api/Authors/First
        [HttpGet("First")]
        //Return first of the table with this url https://localhost:44329/First
        [HttpGet("/First")]
        //0. public ActionResult<Author> GetFirstAuthor()
        //1. Applicated async method
        public async Task <ActionResult<Author>> GetFirstAuthor()
        {

            //0. return context.Authors.FirstOrDefault();
            return await context.Authors.FirstOrDefaultAsync();
        }


        //Bring all authors
        //https://localhost:44329/api/Authors
        [HttpGet("List")]
        [HttpGet("/List")]
        [HttpGet]
        [ServiceFilter(typeof(MyActionFilter))
            ]
        public ActionResult<IEnumerable<Author>> Get()
        {
            //Test the filter of exeption
            //throw new NotImplementedException();
            //return all author with their books
            //return context.Authors.Include(x => x.Books).ToList();
            //return all author
            return context.Authors.ToList();
        }

        //Bring all author speific
        //1. you can call this method through the name GetAuthor return new CreatedAtRouteResult("GetAuthor", new { id = author.Id},author);
        //1. https://localhost:44329/api/Authors/1
        //1. [HttpGet("{id}",Name ="GetAuthor")]
        //2. Bring author by id and ask a second variable to show the endpoint
        //2. https://localhost:44329/api/Authors/2/juan
        //2. [HttpGet("{id}/{param}")]
        //3. Bring author by id and ask a second variable to show the endpoint but the second is no mandatory
        //3. [HttpGet("{id}/{param?}")]
        //4. is the second variable is empty put value gavilan en the variable param
        //5. [HttpGet("{id}/{param=gavilan}")]
        //1. public ActionResult<Author> Get(int id)
        //0. public ActionResult<Author> Get(int id,string param)
        //5. return actions inherated and type of data
        //5. public IActionResult Get(int id,string param)
        //6. Use Binder to get data through the url
        //6. https://localhost:44329/api/Authors/3/?param=word
        [HttpGet("{id}")]
        //5. public Author Get(int id, string param)
        public Author Get(int id,[BindRequired] string param)
        {

            var author = context.Authors.Include(x => x.Books).FirstOrDefault(x => x.Id == id);

            //return simple
            //var author = context.Authors.FirstOrDefault(x => x.Id == id);

            //0.if (author == null)
            //0. {
            //0.    return NotFound();
            //0.}

            //5. return Ok(author);
            return author;
        }

        //Add new Author and it return the author calling the method Get through GetAuthor and sending the name of the object created
        //https://localhost:44329/api/Authors
        //{"Name":"Douglas Loaiza"}
        [HttpPost]
        public ActionResult Post([FromBody] Author author)
        {
            //To revalidate the model
            ///TryValidateModel(author);
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

