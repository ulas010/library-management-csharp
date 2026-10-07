using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kütüphaneTakipSistemi
{
    public class Uye
    {
        public int Id { get; set; }
        public string AdSoyad { get; set; }
        public string Telefon { get; set; }

        public Uye() { }

        public Uye(string adSoyad, string telefon)
        {
            AdSoyad = adSoyad;
            Telefon = telefon;
        }
    }

}
