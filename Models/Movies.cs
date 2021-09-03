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
    public class Movies
    {
        public int Id { get; set; }
        public string Imagen { get; set; }
        [NotMapped]
        [DisplayName("upload file")]
        public IFormFile ImagenFile { get; set; }
        public string Titulo { get; set; }
        [DisplayFormat(DataFormatString ="{0:YYY-MM-dd}",ApplyFormatInEditMode =true)]
        public DateTime Fecha_Creacion { get; set; }

        [Range(1, 5)]
        public int Calificacion { get; set; }

        public int PersAsocId { get; set; }

        [ForeignKey("PersAsocId")]
        public Characters character{ get; set; }

       

        
        public int GeneroAsocId { get; set; }
        [ForeignKey("GeneroAsocId")]
        public  Genres genres { get; set; }

      
    }
}
