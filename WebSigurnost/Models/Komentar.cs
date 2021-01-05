using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebSigurnost.Models
{
    [Table("Komentar")]
    public class Komentar
    {
        public int KomentarId { get; set; }
        [Required(ErrorMessage ="Unesite autora")]
        [StringLength(30,ErrorMessage ="Max 30 karaktera")]
        public string Autor { get; set; }
        [Required(ErrorMessage = "Unesite naslov komentara")]
        [Display(Name="Naslov komentara")]
        [StringLength(50, ErrorMessage = "Max 30 karaktera,a min 5 karaktera",MinimumLength =5)]
        public string Naslov { get; set; }
        [Required(ErrorMessage = "Unesite sadrzaj komentara")]
        [StringLength(200, ErrorMessage = "Max 200 karaktera,a min 5 karaktera", MinimumLength = 5)]
        [Display(Name = "Sadrzaj komentara")]
        public string Sadrzaj { get; set; }
    }
}
