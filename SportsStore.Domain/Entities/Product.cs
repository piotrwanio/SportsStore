using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SportsStore.Domain.Entities
{
    public class Product
    {
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "ID")]
        public int ProductID { get; set; }

        [Display(Name="Imię")]
        [Required(ErrorMessage = "Proszę podać nazwę produktu.")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Opis")]
        [Required(ErrorMessage = "Proszę podać opis.")]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Proszę podać dodatnią cenę.")]
        [Display(Name = "Cena")]
        public decimal Price { get; set; }

        [Display(Name = "Kategoria")]
        [Required(ErrorMessage = "Proszę określić kategorię.")]
        public string Category { get; set; }
    }
}
