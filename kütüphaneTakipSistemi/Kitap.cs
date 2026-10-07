using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kütüphaneTakipSistemi
{
    public class Kitap
    {
        public int Id { get; set; }
        public string KitapAdi { get; set; }
        public string Yazar { get; set; }
        public string YayinEvi { get; set; }
        public int Yil { get; set; }

        public Kitap() { }

        public Kitap(string kitapAdi, string yazar, string yayinEvi, int yil)
        {
            KitapAdi = kitapAdi;
            Yazar = yazar;
            YayinEvi = yayinEvi;
            Yil = yil;
        }
    }

}
