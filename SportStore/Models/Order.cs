﻿using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SportStore.Models
{
    public class Order
    {
        [BindNever]
        public int OrderID { get; set; }

        [BindNever]
        public ICollection<CartLine> Lines { get; set; }

        [Required(ErrorMessage = "Пожалуйста введите имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Пожалуйста введите первую строку адреса")]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }

        [Required(ErrorMessage = "Пожалуйста введите название города")]
        public string City { get; set; }

        [Required(ErrorMessage = "Пожалуйста введите название области")]
        public string State { get; set; }

        [Required(ErrorMessage = "Пожалуйста введите почтовый индекс")]
        public string Zip { get; set; }

        [Required(ErrorMessage = "Пожалуйста введите название страны доставки")]
        public string Country { get; set; }
        public bool GiftWrap { get; set; }
    }
}
