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
    public partial class Ture : Form
    {

        Class1 Class = new Class1(); 
        enum RowState
        {
            Existet,
            New,
            Modified,
            ModifiedNew,
            Deleted

        }

        int selectRow;
        public Ture()
        {
            InitializeComponent();
        }

        private void Ture_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefresDataGrid(dataGridView1);
        }
        private void CreateColumns()
        {

            dataGridView1.Columns.Add("Страна", "Страна");
            dataGridView1.Columns.Add("Проживание", "Проживание");
            dataGridView1.Columns.Add("Питание", "Питание");
            dataGridView1.Columns.Add("IsNew", String.Empty);
        }
        private void ReadSingleRow(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetString(0), record.GetString(1), record.GetString(2), RowState.ModifiedNew);

        }
        private void RefresDataGrid(DataGridView dgv)
        {
            dgv.Rows.Clear();

            string queryString = $"select *from Ture";
            SqlCommand command = new SqlCommand(queryString, Class.getConnection());

            Class.openConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgv, reader);

            }
            reader.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefresDataGrid(dataGridView1);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectRow];

                textBox2.Text = row.Cells[0].Value.ToString();
                textBox3.Text = row.Cells[1].Value.ToString();
                textBox4.Text = row.Cells[1].Value.ToString();

            }

        }
        private void Search(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string search = $"select * from Ture where concat(Страна, Проживание, Питание) like '%" + textBox1.Text + "%'";
            SqlCommand com = new SqlCommand(search, Class.getConnection());

           Class.openConnection();

            SqlDataReader reader = com.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgv, reader);
            }
            reader.Close();


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView1);
        }
        private void deleteRow()
        {
            int index = dataGridView1.CurrentCell.RowIndex;

            dataGridView1.Rows[index].Visible = false;

            if (dataGridView1.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView1.Rows[index].Cells[3].Value = RowState.Deleted;
                return;
            }
            dataGridView1.Rows[index].Cells[3].Value = RowState.Deleted;
        }
        private void Update()
        {
            Class.openConnection();

            for (int index = 0; index < dataGridView1.Rows.Count; index++)
            {
                var rowState = (RowState)dataGridView1.Rows[index].Cells[2].Value;

                if (rowState == RowState.Existet)
                    continue;

                if (rowState == RowState.Deleted)
                {
                    var Name = Convert.ToString(dataGridView1.Rows[index].Cells[0].Value);
                    var deleteQuery = $"delete from Ture where Страна = {Name}";

                    var command = new SqlCommand(deleteQuery, Class.getConnection());
                    command.ExecuteNonQuery();
                }

                if (rowState == RowState.Modified)
                {

                    var fam = dataGridView1.Rows[index].Cells[0].Value.ToString();
                    var name = dataGridView1.Rows[index].Cells[1].Value.ToString();
                    var cam = dataGridView1.Rows[index].Cells[2].Value.ToString();

                    var changeQuery = $"update Ture set Питание = '{name}', Проживание = '{cam}', where Страны = '{fam}'";

                    var command = new SqlCommand(changeQuery, Class.getConnection());
                    command.ExecuteNonQuery();
                }


            }

            Class.closeConnection();


        }

        private void Change()
        {
            var selectedRowIndex = dataGridView1.CurrentCell.RowIndex;

            var brig = textBox2.Text;
            var job = textBox3.Text;
            var dog = textBox4.Text;

            if (dataGridView1.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {

                dataGridView1.Rows[selectedRowIndex].SetValues(brig, job, dog);
                dataGridView1.Rows[selectedRowIndex].Cells[3].Value = RowState.Modified;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Change();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Update();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            deleteRow();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dobav newBrigad = new dobav();
            newBrigad.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
