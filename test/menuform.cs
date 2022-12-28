using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test
{
    public partial class menuform: Form
    {
        private Button button1;

       /* public menuform()
        {
            InitializeComponent();
        }

       */

        private void menuformLoad (object sender, EventArgs e)
        {

        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            Ture form1 = new Ture();
            this.Hide();
            this.Close();
            form1.ShowDialog();
            this.Show();
        }

        /*private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // menuform
            // 
            this.ClientSize = new System.Drawing.Size(851, 450);
            this.Name = "menuform";
            this.ResumeLayout(false);

        }
        */
    }
}
