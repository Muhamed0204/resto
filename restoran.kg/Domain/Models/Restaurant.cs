using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
   
        public class Restaurant
        {
            public int Id { get; set; }
            public string Название { get; set; }
            public string Адрес { get; set; }
            public string Телефон { get; set; }
            public string ЧасыРаботы { get; set; }
        }
    }

