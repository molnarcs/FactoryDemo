using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FactoryDemo.Models
{
    public class Termek
    {
        public int Azonosito { get; set; }
        public string Nev { get; set; }
        public int? KategoriaAzonosito { get; set; }
        public int? Ar { get; set; }
    }
}