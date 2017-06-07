using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _12dSynergyCivil3d
{
    public partial class Desc : Form
    {
        public Desc()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserControl1 controlpanel = new UserControl1();

            //UserControl1 frm = MdiParent;

            DataGridView dt = (DataGridView)controlpanel.Controls["dataGridView1"];

            //var rowcount = controlpanel.dataGridView1.RowCount;
            var rowcount = dt.RowCount;

            MessageBox.Show(rowcount.ToString());

            Console.WriteLine("row count: " + rowcount.ToString());

            for (int i = 0; i <= (rowcount - 1); i++)
            {
                //controlpanel.dataGridView1.Rows[i].Cells[4].Value = txtDesc.Text;
                dt.Rows[i].Cells[4].Value = txtDesc.Text;

            }

            //controlpanel.dataGridView1.Update();
            dt.Update();

            Close();
        }
    }
}
