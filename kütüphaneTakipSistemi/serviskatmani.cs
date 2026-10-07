using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace kütüphaneTakipSistemi
{
    // Data access layer (SQL Server). All queries use parameters.
    public class serviskatmani
    {
        // The connection string is read from the LIBRARY_DB_CONNECTION environment variable
        // so that no password is stored in the source code. Without it, local Windows authentication is used.
        public static string connectionString =
            Environment.GetEnvironmentVariable("LIBRARY_DB_CONNECTION")
            ?? "Data Source=localhost;Initial Catalog=KutuphaneDB;Integrated Security=True;";

        private static Kitap KitapOku(SqlDataReader okuyucu)
        {
            return new Kitap
            {
                Id = Convert.ToInt32(okuyucu["Id"]),
                KitapAdi = okuyucu["KitapAdi"].ToString(),
                Yazar = okuyucu["Yazar"].ToString(),
                YayinEvi = okuyucu["YayinEvi"].ToString(),
                Yil = Convert.ToInt32(okuyucu["Yil"])
            };
        }

        public List<Kitap> KitapListele()
        {
            List<Kitap> kitapListesi = new List<Kitap>();

            using (SqlConnection baglanti = new SqlConnection(connectionString))
            using (SqlCommand komut = new SqlCommand("SELECT * FROM Kitaplar", baglanti))
            {
                baglanti.Open();
                using (SqlDataReader okuyucu = komut.ExecuteReader())
                {
                    while (okuyucu.Read())
                    {
                        kitapListesi.Add(KitapOku(okuyucu));
                    }
                }
            }

            return kitapListesi;
        }

        public void KitapEkle(Kitap k)
        {
            string sorgu = "INSERT INTO Kitaplar (KitapAdi, Yazar, YayinEvi, Yil) " +
                           "VALUES (@KitapAdi, @Yazar, @YayinEvi, @Yil)";

            using (SqlConnection baglanti = new SqlConnection(connectionString))
            using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
            {
                komut.Parameters.AddWithValue("@KitapAdi", k.KitapAdi);
                komut.Parameters.AddWithValue("@Yazar", k.Yazar);
                komut.Parameters.AddWithValue("@YayinEvi", k.YayinEvi ?? "");
                komut.Parameters.AddWithValue("@Yil", k.Yil);

                baglanti.Open();
                komut.ExecuteNonQuery();
            }
        }

        public List<Kitap> KitapSorgula(string kitapAdi)
        {
            List<Kitap> kitapListesi = new List<Kitap>();

            using (SqlConnection baglanti = new SqlConnection(connectionString))
            using (SqlCommand komut = new SqlCommand(
                "SELECT * FROM Kitaplar WHERE KitapAdi LIKE @Arama", baglanti))
            {
                komut.Parameters.AddWithValue("@Arama", "%" + kitapAdi + "%");

                baglanti.Open();
                using (SqlDataReader okuyucu = komut.ExecuteReader())
                {
                    while (okuyucu.Read())
                    {
                        kitapListesi.Add(KitapOku(okuyucu));
                    }
                }
            }

            return kitapListesi;
        }

        public void KitapSil(List<int> list)
        {
            if (list == null || list.Count == 0) return;

            // One parameter per id: @id0, @id1, ...
            string[] parametreAdlari = list.Select((id, i) => "@id" + i).ToArray();
            string sorgu = "DELETE FROM Kitaplar WHERE Id IN (" + string.Join(",", parametreAdlari) + ")";

            using (SqlConnection baglanti = new SqlConnection(connectionString))
            using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
            {
                for (int i = 0; i < list.Count; i++)
                {
                    komut.Parameters.AddWithValue(parametreAdlari[i], list[i]);
                }

                baglanti.Open();
                komut.ExecuteNonQuery();
            }
        }

        public void KitapGuncelle(Kitap kitap)
        {
            string sorgu = "UPDATE Kitaplar SET KitapAdi = @KitapAdi, Yazar = @Yazar, " +
                           "YayinEvi = @YayinEvi, Yil = @Yil WHERE Id = @Id";

            using (SqlConnection baglanti = new SqlConnection(connectionString))
            using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
            {
                komut.Parameters.AddWithValue("@KitapAdi", kitap.KitapAdi);
                komut.Parameters.AddWithValue("@Yazar", kitap.Yazar);
                komut.Parameters.AddWithValue("@YayinEvi", kitap.YayinEvi ?? "");
                komut.Parameters.AddWithValue("@Yil", kitap.Yil);
                komut.Parameters.AddWithValue("@Id", kitap.Id);

                baglanti.Open();
                komut.ExecuteNonQuery();
            }
        }
    }
}
