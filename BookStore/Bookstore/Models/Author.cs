using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models
{
    public class Author
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]                          //The Name cannot be empty, if empty throw validation exception
        public string Name { get; set; }

    }
}