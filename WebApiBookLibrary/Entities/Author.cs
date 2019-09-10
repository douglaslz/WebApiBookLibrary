using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiBookLibrary.Entities
{
    public class Author
    {
        public int Id { get; set; }
        //Validation by Attribute
        //have to be post https://localhost:44329/api/Authors
        //{"Name":"douglas","Age":23,"Creditcard":453242,"Url":"kjhkjh"}
        [Required]
        [StringLength(10,MinimumLength =1,ErrorMessage ="The field must be at least 1 character or maximo 10")]
        public string Name { get; set; }
        [Range (18,120)]
        public int Age { get; set; }
        [CreditCard]
        public int Creditcard { get; set; }
        [Url]
        public string  Url { get; set; }
        public List<Book> Books { get; set; }
    }
}
