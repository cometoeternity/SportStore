using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace SportStore.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage ="Введите наименнование товара!")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Введите описание товара")]
        public string Discription { get; set; }

        [Required]
        [Range(0.01,double.MaxValue,ErrorMessage ="Введите положительное значение цены")]
        public decimal Price { get; set; }

        [Required(ErrorMessage ="Введите категорию товара")]
        public string Category { get; set; }
    }
}
