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
    public class Characters
    {

        [Key]
        public int Id { get; set; }
        [Column]
        [DisplayName("Foto")]
        public string Imagen { get; set; }
        [NotMapped]
        [DisplayName("upload file")]
        public IFormFile ImagenFile { get; set; }

        [Column]
        public string Nombre { get; set; }

        [Column]
        public int Edad { get; set; }

        [Column]
        public decimal Peso { get; set; }

        [Column]
        public string Historia { get; set; }
    }
}
