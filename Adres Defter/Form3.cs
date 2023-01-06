using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.Collections;

namespace Adres_Defter
{
    public partial class Form3 : Form
    {
        private static SqlConnection baglanti = new SqlConnection("Data Source=.;Initial Catalog=deneme_db;Integrated Security=True");
        
        public Form3()
        {
            InitializeComponent();
        }



        private void Form3_Load(object sender, EventArgs e)
        {

        }

       
    }
}
