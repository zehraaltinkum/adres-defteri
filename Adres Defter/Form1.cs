using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.Collections;
namespace Adres_Defter
{
    public partial class Form1 : Form
    {
        private static SqlConnection baglanti = new SqlConnection("Data Source=.;Initial Catalog=deneme_db;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            veri_cek();
        }
        void veri_cek()
        {

            if (baglanti.State == ConnectionState.Closed)
            {

                baglanti.Open();

            }



            SqlCommand komut = new SqlCommand("Select adi,soyadi,telefon,email,adres,grup,aciklama from adresdefteri", baglanti);

            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();

            da.Fill(dt);
            dataGridView1.DataSource = dt;

            baglanti.Close();

            dataGridView1.Columns["adi"].HeaderText = "ADI";
            dataGridView1.Columns["adi"].Width = 100;

            dataGridView1.Columns["soyadi"].HeaderText = "SOYADI";
            dataGridView1.Columns["soyadi"].Width = 100;

            dataGridView1.Columns["telefon"].HeaderText = "TELEFON";
            dataGridView1.Columns["telefon"].Width = 250;

            dataGridView1.Columns["email"].HeaderText = "E-MAİL";
            dataGridView1.Columns["email"].Width = 200;


            dataGridView1.Columns["adres"].HeaderText = "ADRES";
            dataGridView1.Columns["adres"].Width = 200;

            dataGridView1.Columns["grup"].HeaderText = "GRUP";
            dataGridView1.Columns["grup"].Width = 200;


            dataGridView1.Columns["aciklama"].HeaderText = "AÇIKLAMA";
            dataGridView1.Columns["aciklama"].Width = 250;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
            txt_adi.Clear();
            txt_soyadi.Clear();
            txt_telefon.Clear();
            txt_adres.Clear();
            txt_email.Clear();
            txt_not.Clear();
            comboBox2.Text = "";
            comboBox2.SelectedIndex = -1;
            txt_adi.Focus();

        }

        private void button2_Click(object sender, EventArgs e)
        
        {

            
           
                if (txt_adi.Text != "" || txt_email.Text != "" || txt_telefon.Text != "")
                {
                    if (baglanti.State == ConnectionState.Closed)
                    {
                        baglanti.Open();
                    }

                    SqlCommand komut = new SqlCommand("insert into adresdefteri (adi,soyadi,telefon,email,adres,grup,aciklama) values(@adi,@soyadi,@telefon,@email,@adres,@grup,@aciklama)", baglanti);

                    komut.Parameters.AddWithValue("@adi", txt_adi.Text);

                    komut.Parameters.AddWithValue("@soyadi", txt_soyadi.Text);

                    komut.Parameters.AddWithValue("@telefon", txt_telefon.Text);

                    komut.Parameters.AddWithValue("@email", txt_email.Text);

                    komut.Parameters.AddWithValue("@adres", txt_adres.Text);

                    komut.Parameters.AddWithValue("@grup", comboBox2.Text);

                    komut.Parameters.AddWithValue("@aciklama", txt_not.Text);
                   
                    komut.ExecuteNonQuery();

                    baglanti.Close();

                    MessageBox.Show("Kayıt İşlemi Tamamlanmıştır.");
                }

                else
                {
                    MessageBox.Show("Eksik Veya Yanlış  Girilmiş Kayıt Bilgileri Var.Lütfen Kutuları Kontrol Ediniz.");
                }

                veri_cek();

            }

       

        private void button3_Click(object sender, EventArgs e)
        {

            if (txt_adi.Text != "" || txt_email.Text != "" || txt_not.Text != "" || txt_telefon.Text != "")
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }

                SqlCommand komut = new SqlCommand("Update adresdefteri set adi_soyadi=adi=@adi,soyadi=@soyadi,telefon=@telefon,email=@email,adres=@adres,grup=@grup,aciklama=@aciklama ", baglanti);

                komut.Parameters.AddWithValue("@adi", txt_adi.Text);

                komut.Parameters.AddWithValue("@soyadi", txt_soyadi.Text);

                komut.Parameters.AddWithValue("@telefon", txt_telefon.Text);

                komut.Parameters.AddWithValue("@email", txt_email.Text);
                komut.Parameters.AddWithValue("@adres", txt_adres.Text);
                komut.Parameters.AddWithValue("@grup", comboBox2.Text);

                komut.Parameters.AddWithValue("@aciklama", txt_not.Text);


                komut.ExecuteNonQuery();


                baglanti.Close();

                MessageBox.Show("Güncelleme İşlemi Tamamlanmıştır.");

            }

            else
            {

                MessageBox.Show("Güncelleme İşlemi Yalnızca Var Olan Bir Kayıt İçin Yapılabilir.");

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            PrintDialog yazdir = new PrintDialog();
            yazdir.Document = printDocument1;
            yazdir.UseEXDialog = true;
            if (yazdir.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {



            DialogResult secenek = MessageBox.Show("Kaydı veritabanından kalıcı olarak  silmek istediğinizden emin misiniz ?", "Veri Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (secenek == DialogResult.Yes)
            {

                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }

                SqlCommand komut = new SqlCommand("Delete from  adresdefteri ", baglanti);

                

                komut.ExecuteNonQuery();

                baglanti.Close();

                MessageBox.Show("Silme İşlemi Tamamlanmıştır.");
            }

        }
       
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            { veri_cek(); }
            try
            {



                DateTime Bugun = DateTime.Now;

                string format = "MMM ddd d HH:mm yyyy";

                Font font = new Font("Arial", 16);
                SolidBrush firca = new SolidBrush(Color.Black);
                e.Graphics.DrawString("Tarih:" + Bugun.ToString(format), font, firca, 60, 25);
                font = new Font("Arial", 20, FontStyle.Bold);



                e.Graphics.DrawString("ADRES DEFTERİ", font, firca, 350, 70);
                e.Graphics.DrawString("-------------------------", font, firca, 350, 100);
                font = new Font("Arial", 16, FontStyle.Bold);


                e.Graphics.DrawString("AD", font, firca, 40, 170);
               
                e.Graphics.DrawString("SOYAD", font, firca, 120, 170);

                e.Graphics.DrawString("TELEFON", font, firca, 250, 170);
                
                e.Graphics.DrawString("E-MAİL", font, firca, 420, 170);
                e.Graphics.DrawString("ADRES", font, firca, 520, 170);
                e.Graphics.DrawString("GRUP", font, firca, 620, 170);


                e.Graphics.DrawString("AÇIKLAMA", font, firca,710, 170);

                int i = 0;
                int y = 210;

                while (i <= dataGridView1.Rows.Count - 2)
                {
                    font = new Font("Arial", 16);
                    e.Graphics.DrawString(dataGridView1.Rows[i].Cells[1].Value.ToString(), font, firca, 40, y);
                    e.Graphics.DrawString(dataGridView1.Rows[i].Cells[2].Value.ToString(), font, firca, 120, y);
                    e.Graphics.DrawString(dataGridView1.Rows[i].Cells[3].Value.ToString(), font, firca, 250, y);
                    e.Graphics.DrawString(dataGridView1.Rows[i].Cells[4].Value.ToString(), font, firca, 420, y);
                    e.Graphics.DrawString(dataGridView1.Rows[i].Cells[5].Value.ToString(), font, firca, 520, y);
                    e.Graphics.DrawString(dataGridView1.Rows[i].Cells[6].Value.ToString(), font, firca, 620, y);
                    e.Graphics.DrawString(dataGridView1.Rows[i].Cells[7].Value.ToString(), font, firca, 710, y);


                    y = y + 40;
                    i = i + 1;

                }

            }
            catch (Exception)
            {


            }



        }

        private void button6_Click(object sender, EventArgs e)
        {  veri_cek();

            {

                printPreviewDialog1.ShowDialog();
            }
            
        }
        private void button7_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * from adresdefteri where adi like '%" + txtSearch.Text + "%'", baglanti);
            
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataSet dt = new DataSet();

            da.Fill(dt);
            dataGridView1.DataSource = dt.Tables[0];

            baglanti.Close();
        }

     

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

           comboBox2.Text = dataGridView1.SelectedCells[5].Value.ToString();

            txt_adi.Text = dataGridView1.SelectedCells[0].Value.ToString();
            txt_soyadi.Text = dataGridView1.SelectedCells[1].Value.ToString();
            txt_telefon.Text = dataGridView1.SelectedCells[2].Value.ToString();
            txt_adres.Text = dataGridView1.SelectedCells[4].Value.ToString();
            txt_email.Text = dataGridView1.SelectedCells[3].Value.ToString();
            txt_not.Text = dataGridView1.SelectedCells[6].Value.ToString();
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form3 crys = new Form3();
            crys.ShowDialog();
        }

       
       

        
       
    }

          

        

}

      

     

       


