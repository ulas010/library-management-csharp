using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kütüphaneTakipSistemi
{
    public class Odunc
    {
        public int Id { get; set; }
        public Uye Uye { get; set; }
        public Kitap Kitap { get; set; }
        public DateTime AlisTarihi { get; set; }
        public DateTime? IadeTarihi { get; set; }

        public Odunc() { }

        public Odunc(Uye uye, Kitap kitap)
        {
            Uye = uye;
            Kitap = kitap;
        }

        public Odunc(Uye uye, Kitap kitap, DateTime alis, DateTime? iade)
        {
            Uye = uye;
            Kitap = kitap;
            AlisTarihi = alis;
            IadeTarihi = iade;
        }
    }

}
