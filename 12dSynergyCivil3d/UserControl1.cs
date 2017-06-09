using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScriptTest;


namespace _12dSynergyCivil3d
{
    
    public partial class UserControl1 : UserControl
    {
        
        const string NO_DATA = "Please add some files";

        public UserControl1()
        {
            InitializeComponent();
        }

        //public ScriptTest()
        //{
        //    ScriptTest();
        //}

        private void button1_Click(object sender, EventArgs e)
        {

            dataGridView1.Rows.Clear();

            string[] row1 = { "true", "not checked out", "110274CC101", "A", "Issued for CC", "DJH", "NW", "", "", "13/3/15" };
            string[] row2 = { "true", "not checked out", "110274CC102", "A", "Issued for CC", "DJH", "NW", "", "", "13/3/15" };
            string[] row3 = { "true", "not checked out", "110274CC103", "A", "Issued for CC", "DJH", "NW", "", "", "13/3/15" };
            string[] row4 = { "true", "not checked out", "110274CC104", "A", "Issued for CC", "DJH", "NW", "", "", "13/3/15" };
            string[] row5 = { "true", "not checked out", "110274CC105", "2", "Issued for CC", "DJH", "NW", "", "", "13/3/15" };
            string[] row6 = { "true", "not checked out", "110274CC106", "1", "Issued for CC", "DJH", "NW", "", "", "13/3/15" };

            object[] rows = new object[] { row1, row2, row3, row4, row5, row6 };

        foreach (string[] rowArray in rows)
            {
                dataGridView1.Rows.Add(rowArray);
            }
        }

        private void button2_Click(object sender, EventArgs e) //Edit
        {
            //Desc infoupdate = new Desc();
            //infoupdate.Show();
            var rowcount = dataGridView1.RowCount;
            if (rowcount == 0)
            {
                MessageBox.Show(NO_DATA, "No Data", MessageBoxButtons.OK);
                return;
            }

            DialogResult dialogresult = MessageBox.Show("This will edit the currently selected files to take the information provided. Do you want to continue?", "Edit Files", MessageBoxButtons.YesNo);

            if (dialogresult == DialogResult.Yes)
            {

                

                for (int i = 0; i <= (rowcount - 1); i++)
                {
                    //MessageBox.Show(dataGridView1.Rows[i].Cells[0].Value.ToString());
                    if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "true")
                    {
                        if(txtDesc.Text != "")
                        {
                            dataGridView1.Rows[i].Cells[4].Value = txtDesc.Text;
                        }
                        
                        if(txtDesign.Text != "")
                        {
                            dataGridView1.Rows[i].Cells[5].Value = txtDesign.Text;
                        }
                        
                        if(txtDraft.Text != "")
                        {
                            dataGridView1.Rows[i].Cells[6].Value = txtDraft.Text;
                        }
                        
                        if(txtCheck.Text != "")
                        {
                            dataGridView1.Rows[i].Cells[7].Value = txtCheck.Text;
                        }
                        if (txtApprove.Text != "")
                        {
                            dataGridView1.Rows[i].Cells[8].Value = txtApprove.Text;
                        }
                        
                        if(txtDate.Text != "")
                        {
                            dataGridView1.Rows[i].Cells[9].Value = txtDate.Text;
                        }
                        

                    }
                    //controlpanel.dataGridView1.Rows[i].Cells[4].Value = txtDesc.Text;


                }

                dataGridView1.Update();

            }

        }

        private void button4_Click(object sender, EventArgs e) //Rev Up
        {
            var rowcount = dataGridView1.RowCount;

            if (rowcount == 0)
            {
                MessageBox.Show(NO_DATA, "No Data", MessageBoxButtons.OK);
                return;
            }

            DialogResult dialogresult = MessageBox.Show("This will rev up the currently selected files to the next revision and add the information provided. Changes are not committed until files checked in. Do you want to continue?", "Rev Up Files", MessageBoxButtons.YesNo);

            if (dialogresult == DialogResult.Yes)
            {
                

                //MessageBox.Show(rowcount.ToString());

                //Console.WriteLine("row count: " + rowcount.ToString());

                for (int i = 0; i <= (rowcount - 1); i++)
                {
                    //MessageBox.Show(dataGridView1.Rows[i].Cells[0].Value.ToString());
                    if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "true")
                    {
                        dataGridView1.Rows[i].Cells[3].Value = getNextRevision(dataGridView1.Rows[i].Cells[3].Value.ToString());
                        dataGridView1.Rows[i].Cells[4].Value = txtDesc.Text;                        
                        dataGridView1.Rows[i].Cells[5].Value = txtDesign.Text;
                        dataGridView1.Rows[i].Cells[6].Value = txtDraft.Text;
                        dataGridView1.Rows[i].Cells[7].Value = txtCheck.Text;
                        dataGridView1.Rows[i].Cells[8].Value = txtApprove.Text;
                        dataGridView1.Rows[i].Cells[9].Value = txtDate.Text;

                    }
                    //controlpanel.dataGridView1.Rows[i].Cells[4].Value = txtDesc.Text;


                }

                dataGridView1.Update();

            }
        }

        private void button5_Click(object sender, EventArgs e) //check all
        {
            var rowcount = dataGridView1.RowCount;

            if (rowcount == 0)
            {
                MessageBox.Show(NO_DATA, "No Data", MessageBoxButtons.OK);
                return;
            }

            for (int i = 0; i <= (rowcount - 1); i++)
            {
                
                    dataGridView1.Rows[i].Cells[0].Value = "true";
                
                //controlpanel.dataGridView1.Rows[i].Cells[4].Value = txtDesc.Text;


            }

            dataGridView1.Update();

        }

        private void button6_Click(object sender, EventArgs e) //uncheck all
        {
            var rowcount = dataGridView1.RowCount;

            if (rowcount == 0)
            {
                MessageBox.Show(NO_DATA, "No Data", MessageBoxButtons.OK);
                return;
            }

            for (int i = 0; i <= (rowcount - 1); i++)
            {

                dataGridView1.Rows[i].Cells[0].Value = "False";

                //controlpanel.dataGridView1.Rows[i].Cells[4].Value = txtDesc.Text;


            }

            dataGridView1.Update();
        }

        public String getNextRevision(String curRev)
        {
            String revString = "";
            String revListAlpha = "ABCDEFGHKLMNPQRSTUVWXYZ";

            int n;
            bool isNumeric = int.TryParse(curRev, out n);

            if(isNumeric == true)
            {
                revString = (n + 1).ToString();
            } else
            {
                int index = revListAlpha.IndexOf(curRev);

                revString = revListAlpha.Substring(index + 1, 1);

            }

            return revString;
        }

        private void button3_Click(object sender, EventArgs e) //check in
        {
            var rowcount = dataGridView1.RowCount;

            if (rowcount == 0)
            {
                MessageBox.Show(NO_DATA, "No Data", MessageBoxButtons.OK);
                return;
            }
        
            DialogResult dialogresult = MessageBox.Show("Your files will be checked in and the changes committed. After this point any changes to be undone need to be completed inside Synergy. Do you want to continue?", "Check In Files", MessageBoxButtons.YesNo);

            if(dialogresult == DialogResult.Yes)
            {

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }

        //private void radioButton2_CheckedChanged(object sender, EventArgs e)
        //{
        //    radioButton1.Checked = false;
        //    radioButton2.Checked = true;
        //}

        //private void radioButton1_CheckedChanged(object sender, EventArgs e)
        //{
        //    radioButton2.Checked = false;
        //    radioButton1.Checked = true;
        //}

        private void radioButton2_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
            radioButton2.Checked = true;
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            radioButton2.Checked = false;
        }

        private void button8_Click(object sender, EventArgs e) //convert number to letter
        {
            var rowcount = dataGridView1.RowCount;

            if (rowcount == 0)
            {
                MessageBox.Show(NO_DATA, "No Data", MessageBoxButtons.OK);
                return;
            }

            int n;
            
            for (int i = 0; i <= (rowcount - 1); i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "true")
                {
                    if(int.TryParse(dataGridView1.Rows[i].Cells[3].Value.ToString(), out n) == true)
                    {
                        dataGridView1.Rows[i].Cells[3].Value = "A";
                    } else
                    {
                        MessageBox.Show("Drawing " + dataGridView1.Rows[i].Cells[2].Value.ToString() + " already has a lettered revision and will be skipped", "Lettered Revision", MessageBoxButtons.OK);
                    }
                    
                }
            }

            dataGridView1.Update();
        }

       
    }
}
