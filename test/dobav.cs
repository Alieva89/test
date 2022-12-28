using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test
{


    public partial class dobav : Form
    {

        Class1 Class = new Class1();
        public dobav()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dobav newBrigad = new dobav();
            newBrigad.Show();
            Class.openConnection();

            var one = textBox1.Text;
            var two = textBox2.Text;
            var three = textBox3.Text; 

            var addQuare = $"insert into Ture(Страны, Питание, Проживание) values ('{one}','{two}', '{three}')";

            var command = new SqlCommand(addQuare, Class.getConnection());
            command.ExecuteNonQuery();

            MessageBox.Show("Тур добавлен");
            Class.closeConnection();
        }
    }
}
