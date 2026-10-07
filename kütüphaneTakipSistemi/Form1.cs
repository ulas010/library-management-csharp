using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace kütüphaneTakipSistemi

{
    public partial class Form1 : Form
    {
            public Form1()
            {
                InitializeComponent();
            }
           public serviskatmani servis = new serviskatmani();

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = servis.KitapListele();
            if (dataGridView1.Columns["Id"] != null)
            {
                dataGridView1.Columns["Id"].Visible = false;
            }
        }
        private void btnEkle_Click(object sender, EventArgs e)
        {
            string kitapAdi = txtKitapAdi.Text.Trim();
            string yazar = txtYazar.Text.Trim();
            string yayinEvi = txtYayinevi.Text.Trim();
            string yilText = txtYil.Text.Trim();

            if (string.IsNullOrWhiteSpace(kitapAdi) || string.IsNullOrWhiteSpace(yazar))
            {
                MessageBox.Show("Kitap Adı ve Yazar alanları zorunludur.");
                return;
            }

            if (!int.TryParse(yilText, out int yil))
            {
                MessageBox.Show("Yıl sayısal bir değer olmalıdır.");
                return;
            }

          
            Kitap k = new Kitap
            {
                KitapAdi = kitapAdi,
                Yazar = yazar,
                YayinEvi = yayinEvi,
                Yil = yil
            };

            
            servis.KitapEkle(k);

            MessageBox.Show("Kitap başarıyla eklendi!");
            dataGridView1.DataSource = servis.KitapListele();
        }

       

        private void btnSorgula_Click(object sender, EventArgs e)
        {
            string kitapAdi = txtKitapAdi.Text;
            dataGridView1.DataSource = servis.KitapSorgula(kitapAdi);
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            List<int> silinecekKitapIdler = new List<int>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                
                if (row.Cells["SEC"].Value != null && (bool)row.Cells["SEC"].Value == true)
                {
                    
                    Kitap kitap = row.DataBoundItem as Kitap;
                    if (kitap != null)
                    {
                        silinecekKitapIdler.Add(kitap.Id);
                    }
                }
            }

            if (silinecekKitapIdler.Count == 0)
            {
                MessageBox.Show("Lütfen silinecek kitapları seçin.");
                return;
            }

            // Silme işlemi
            servis.KitapSil(silinecekKitapIdler);

            MessageBox.Show("Seçilen kitap(lar) başarıyla silindi.");

            
            dataGridView1.DataSource = servis.KitapListele();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            // Step 1: load the checked book into the text boxes so it can be edited.
            if (btnGuncelle.Tag == null)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["SEC"].Value != null && (bool)row.Cells["SEC"].Value == true)
                    {
                        Kitap kitap = row.DataBoundItem as Kitap;
                        if (kitap != null)
                        {
                            txtKitapAdi.Text = kitap.KitapAdi;
                            txtYazar.Text = kitap.Yazar;
                            txtYayinevi.Text = kitap.YayinEvi;
                            txtYil.Text = kitap.Yil.ToString();

                            btnGuncelle.Tag = kitap.Id;
                            MessageBox.Show("Bilgileri düzenleyip tekrar Güncelle'ye basın.");
                            return;
                        }
                    }
                }

                MessageBox.Show("Lütfen güncellenecek kitabı seçin.");
                return;
            }

            // Step 2: save the edited values.
            if (!int.TryParse(txtYil.Text.Trim(), out int yil))
            {
                MessageBox.Show("Yıl sayısal bir değer olmalıdır.");
                return;
            }

            Kitap guncellenenKitap = new Kitap
            {
                Id = Convert.ToInt32(btnGuncelle.Tag),
                KitapAdi = txtKitapAdi.Text.Trim(),
                Yazar = txtYazar.Text.Trim(),
                YayinEvi = txtYayinevi.Text.Trim(),
                Yil = yil
            };

            servis.KitapGuncelle(guncellenenKitap);
            btnGuncelle.Tag = null;

            MessageBox.Show("Kitap başarıyla güncellendi!");
            dataGridView1.DataSource = servis.KitapListele();
        }

    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >=0 ) 
            {
                DataGridViewRow secilenSatir = dataGridView1.Rows[e.RowIndex];

                    txtKitapAdi.Text = secilenSatir.Cells["KitapAdi"].Value.ToString();
                    txtYazar.Text = secilenSatir.Cells["Yazar"].Value.ToString();
                    txtYayinevi.Text = secilenSatir.Cells["YayinEvi"].Value.ToString();
                    txtYil.Text = secilenSatir.Cells["Yil"].Value.ToString();
                
            }

        }
    }
    }






