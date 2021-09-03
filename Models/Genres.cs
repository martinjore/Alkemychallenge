using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Alkemy.Models
{
    public class Genres
    {
        [Key]
        public int Id { get; set; }
        [Column]
        public string Descripcion { get; set; }
        [Column]
        [DisplayName("Imagen")]
        public string Imagen { get; set; }
        [NotMapped]
        [DisplayName("upload file")]
        public IFormFile ImagenFile { get; set; }
    }
}
