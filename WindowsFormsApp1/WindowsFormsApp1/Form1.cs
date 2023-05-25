using MetroFramework.Forms;
using MetroFramework.Fonts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Data.OleDb;
using Excel;
using System.Data.SqlClient;
using System.Globalization;


namespace WindowsFormsApp1
{
    public partial class Form1 : MetroForm
    {
        SqlConnection con;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        OleDbDataAdapter adaper = new OleDbDataAdapter();
        double kkal, weight, activity, Metabolism, proteins, fats, carbohydrate;
        int height, age;
        int id = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            DataView dv = new DataView(dt);
            dv.RowFilter = string.Format("Продукт LIKE '%{0}%'", textBox5.Text);
            dataGridView1.DataSource = dv;
        }//пошук продуктів

        private void Form1_Load(object sender, EventArgs e)
        { 
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='c:\users\влад\documents\visual studio 2017\Projects\WindowsFormsApp1\WindowsFormsApp1\DtBs.mdf';Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter("SELECT [Продукт], [Б], [Ж], [В], [Ккал] FROM [Table]", con);
            
            sda.Fill(dt);

            DataView dv = new DataView(dt);
            dataGridView1.DataSource = dv;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Rows[id].Selected = false;
           


            using (var sr = new StreamReader(@"C:\Users\Влад\Documents\Visual Studio 2017\Projects\WindowsFormsApp1\WindowsFormsApp1\Information\17.txt", Encoding.Default))
            {
                var str = sr.ReadToEnd();
                textBox6.Text = str.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int m = dataGridView3.Rows.Add();
            dataGridView3.Rows[m].Cells[0].Value = dateTimePicker1.Value.ToShortDateString();
            dataGridView3.Rows[m].Cells[1].Value = numericUpDown1.Value.ToString();
            dataGridView3.Rows[m].Cells[2].Value = numericUpDown2.Value.ToString();
            dataGridView3.Rows[m].Cells[3].Value = numericUpDown3.Value.ToString();
            dataGridView3.Rows[m].Cells[4].Value = numericUpDown4.Value.ToString();
            dataGridView3.Rows[m].Cells[5].Value = numericUpDown5.Value.ToString();
            dataGridView3.Rows[m].Cells[6].Value = numericUpDown6.Value.ToString();
            dataGridView3.Rows[m].Cells[7].Value = numericUpDown7.Value.ToString();
            dataGridView3.Rows[m].Cells[8].Value = numericUpDown8.Value.ToString();
            dataGridView3.Rows[m].Cells[9].Value = numericUpDown9.Value.ToString();
            dataGridView3.Rows[m].Cells[10].Value = numericUpDown10.Value.ToString();
            dataGridView3.Rows[m].Cells[11].Value = numericUpDown11.Value.ToString();
            dataGridView3.Rows[m].Cells[12].Value = numericUpDown12.Value.ToString();
        }//антропометрия

        private void button5_Click(object sender, EventArgs e)
        {
            
            double sump = 0;
            double sumf = 0;
            double sumc = 0;
            double sumk = 0;
            

            if (textBox7.Text == "")
            {
                MessageBox.Show("Вкажіть вагу вибраного продукту", "Помилка");
            }
            else
            {
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    if ((bool)item.Selected == true)
                    {
                        id = item.Index;
                        int i = dataGridView2.Rows.Add();
                        dataGridView2.Rows[i].Cells[0].Value = item.Cells[1].Value.ToString();
                        dataGridView2.Rows[i].Cells[1].Value = Convert.ToDouble(textBox7.Text);
                        dataGridView2.Rows[i].Cells[2].Value = (Convert.ToDouble(item.Cells[2].Value) * (Convert.ToDouble(textBox7.Text) / 100));
                        dataGridView2.Rows[i].Cells[3].Value = (Convert.ToDouble(item.Cells[3].Value) * (Convert.ToDouble(textBox7.Text) / 100));
                        dataGridView2.Rows[i].Cells[4].Value = (Convert.ToDouble(item.Cells[4].Value) * (Convert.ToDouble(textBox7.Text) / 100));
                        dataGridView2.Rows[i].Cells[5].Value = (Convert.ToDouble(item.Cells[5].Value) * (Convert.ToDouble(textBox7.Text) / 100));

                        break;
                    }
                }
            }

            for (int i = 0; i < dataGridView2.Rows.Count; ++i)
            {
                sump += Convert.ToDouble(dataGridView2.Rows[i].Cells[2].Value);
                sumf += Convert.ToDouble(dataGridView2.Rows[i].Cells[3].Value);
                sumc += Convert.ToDouble(dataGridView2.Rows[i].Cells[4].Value);
                sumk += Convert.ToDouble(dataGridView2.Rows[i].Cells[5].Value);
            }
            label33.Text = sump.ToString();
            label34.Text = sumf.ToString();
            label35.Text = sumc.ToString();
            label36.Text = sumk.ToString();

            dataGridView1.Rows[id].Selected = false;
            textBox7.Clear();
        }

        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            int numSel = dataGridView1.SelectedRows.Count;
            if (numSel > 1)
            {
                int i = dataGridView1.SelectedRows[0].Index;
                foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                {
                    r.Selected = false;
                }

                dataGridView1.Rows[i].Selected = true;

            }
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            int CheckedIDg = 0;
            int CheckedIDt = 0;
            int CheckedIDp = 0;

            if (radioButton1.Checked)
            { 
                 CheckedIDg = 1;
            }
            else if (radioButton2.Checked)
            {
                CheckedIDg = 2;
            }


            if (radioButton3.Checked)
            {
                CheckedIDp = 1;
            }
            else if (radioButton4.Checked)
            {
                CheckedIDp = 2;
            }
            else if (radioButton5.Checked)
            {
                CheckedIDp = 3;
            }


            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    CheckedIDt = 1;
                    break;
                case 1:
                    CheckedIDt = 2;
                    break;
                case 2:
                    CheckedIDt = 3;
                    break;
                case 3:
                    CheckedIDt = 4;
                    break;
                case 4:
                    CheckedIDt = 5;
                    break;
            }

                con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='c:\users\влад\documents\visual studio 2017\Projects\WindowsFormsApp1\WindowsFormsApp1\DtBs.mdf';Integrated Security=True");
            
                SqlCommand cmd = new SqlCommand(@"INSERT INTO [User] VALUES(N'"+textBox1.Text+ "', '" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "', '" + CheckedIDg + "', '" + CheckedIDt + "', '" + CheckedIDp + "')", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
           
        }

        private void metroTabPage2_Click(object sender, EventArgs e)
        {

        }

        public void DataCalculation()
        {
            age = Convert.ToInt32(textBox2.Text);
            height = Convert.ToInt32(textBox3.Text);
            weight = Convert.ToDouble(textBox4.Text);
            switch (this.comboBox1.SelectedIndex)
            {
                case 0:
                    this.activity = 1.2;
                    break;
                case 1:
                    this.activity = 1.375;
                    break;
                case 2:
                    this.activity = 1.55;
                    break;
                case 3:
                    this.activity = 1.73;
                    break;
                case 4:
                    this.activity = 1.9;
                    break;
            }

            if (radioButton1.Checked) //чоловік
            {

                Metabolism = (88.362 + (13.397 * weight) + (4.799 * height) - (5.677 * age)) * this.activity;
            }
            else if (radioButton2.Checked) // жінка
            {
                Metabolism = (447.593 + (9.247 * weight) + (3.098 * height) - (4.33 * age)) * this.activity;
            }
        }
        public void PFC(double pCoefficient, double fCoeffisient, double cCoefficient)
        {
            proteins = (Metabolism * pCoefficient) / 4;
            fats = (Metabolism * fCoeffisient) / 9;
            carbohydrate = (Metabolism * cCoefficient) / 4;
            label10.Text = Convert.ToString(Math.Ceiling(proteins));
            label37.Text = Convert.ToString(Math.Ceiling(proteins));
            label11.Text = Convert.ToString(Math.Ceiling(fats));
            label38.Text = Convert.ToString(Math.Ceiling(fats));
            label12.Text = Convert.ToString(Math.Ceiling(carbohydrate));
            label39.Text = Convert.ToString(Math.Ceiling(carbohydrate));
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e) // підтримка форми
        {
            DataCalculation();
            kkal = Math.Ceiling(Metabolism);
            label9.Text = Convert.ToString(kkal);
            label40.Text = Convert.ToString(kkal);

            if (radioButton1.Checked)
            {
                PFC(0.35, 0.2, 0.45);
            }
            else if(radioButton2.Checked)
            {
                PFC(0.35, 0.25, 0.40);
            }      
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e) // набір маси
        {
            DataCalculation();
            Metabolism *= 1.15;
            kkal = Math.Ceiling(Metabolism);
            label9.Text = Convert.ToString(kkal);
            label40.Text = Convert.ToString(kkal);

            if (radioButton1.Checked)
            {
                PFC(0.30, 0.20, 0.5);
            }
            else if (radioButton2.Checked)
            {
                PFC(0.33, 0.22, 0.45);
            }
        }
        private void radioButton5_CheckedChanged(object sender, EventArgs e)// схуднення
        {
            DataCalculation();
            Metabolism *= 0.85;
            kkal = Math.Ceiling(Metabolism);
            label9.Text = Convert.ToString(kkal);
            label40.Text = Convert.ToString(kkal);

            if (radioButton1.Checked)
            {
                PFC(0.4, 0.25, 0.35);
            }
            else if (radioButton2.Checked)
            {
                PFC(0.4, 0.25, 0.35);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var sr = new StreamReader(@"C:\Users\Влад\Documents\Visual Studio 2017\Projects\WindowsFormsApp1\WindowsFormsApp1\Information\1.txt", Encoding.Default))
            {
                var str = sr.ReadToEnd();
                textBox9.Text = str.ToString();
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var sr = new StreamReader(@"C:\Users\Влад\Documents\Visual Studio 2017\Projects\WindowsFormsApp1\WindowsFormsApp1\Information\2.txt", Encoding.Default))
            {
                var str = sr.ReadToEnd();
                textBox9.Text = str.ToString();
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var sr = new StreamReader(@"C:\Users\Влад\Documents\Visual Studio 2017\Projects\WindowsFormsApp1\WindowsFormsApp1\Information\3.txt", Encoding.Default))
            {
                var str = sr.ReadToEnd();
                textBox9.Text = str.ToString();
            }
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var sr = new StreamReader(@"C:\Users\Влад\Documents\Visual Studio 2017\Projects\WindowsFormsApp1\WindowsFormsApp1\Information\4.txt", Encoding.Default))
            {
                var str = sr.ReadToEnd();
                textBox9.Text = str.ToString();
            }
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var sr = new StreamReader(@"C:\Users\Влад\Documents\Visual Studio 2017\Projects\WindowsFormsApp1\WindowsFormsApp1\Information\5.txt", Encoding.Default))
            {
                var str = sr.ReadToEnd();
                textBox9.Text = str.ToString();
            }
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var sr = new StreamReader(@"C:\Users\Влад\Documents\Visual Studio 2017\Projects\WindowsFormsApp1\WindowsFormsApp1\Information\6.txt", Encoding.Default))
            {
                var str = sr.ReadToEnd();
                textBox9.Text = str.ToString();
            }
        }

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var sr = new StreamReader(@"C:\Users\Влад\Documents\Visual Studio 2017\Projects\WindowsFormsApp1\WindowsFormsApp1\Information\7.txt", Encoding.Default))
            {
                var str = sr.ReadToEnd();
                textBox9.Text = str.ToString();
            }
        }

        private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var sr = new StreamReader(@"C:\Users\Влад\Documents\Visual Studio 2017\Projects\WindowsFormsApp1\WindowsFormsApp1\Information\8.txt", Encoding.Default))
            {
                var str = sr.ReadToEnd();
                textBox9.Text = str.ToString();
            }
        }

        private void linkLabel10_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var sr = new StreamReader(@"C:\Users\Влад\Documents\Visual Studio 2017\Projects\WindowsFormsApp1\WindowsFormsApp1\Information\9.txt", Encoding.Default))
            {
                var str = sr.ReadToEnd();
                textBox9.Text = str.ToString();
            }
        }

        private void linkLabel11_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var sr = new StreamReader(@"C:\Users\Влад\Documents\Visual Studio 2017\Projects\WindowsFormsApp1\WindowsFormsApp1\Information\10.txt", Encoding.Default))
            {
                var str = sr.ReadToEnd();
                textBox9.Text = str.ToString();
            }
        }

        private void linkLabel12_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var sr = new StreamReader(@"C:\Users\Влад\Documents\Visual Studio 2017\Projects\WindowsFormsApp1\WindowsFormsApp1\Information\11.txt", Encoding.Default))
            {
                var str = sr.ReadToEnd();
                textBox9.Text = str.ToString();
            }
        }

        private void linkLabel13_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var sr = new StreamReader(@"C:\Users\Влад\Documents\Visual Studio 2017\Projects\WindowsFormsApp1\WindowsFormsApp1\Information\12.txt", Encoding.Default))
            {
                var str = sr.ReadToEnd();
                textBox9.Text = str.ToString();
            }
        }

        private void linkLabel14_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var sr = new StreamReader(@"C:\Users\Влад\Documents\Visual Studio 2017\Projects\WindowsFormsApp1\WindowsFormsApp1\Information\13.txt", Encoding.Default))
            {
                var str = sr.ReadToEnd();
                textBox9.Text = str.ToString();
            }
        }

        private void linkLabel15_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var sr = new StreamReader(@"C:\Users\Влад\Documents\Visual Studio 2017\Projects\WindowsFormsApp1\WindowsFormsApp1\Information\14.txt", Encoding.Default))
            {
                var str = sr.ReadToEnd();
                textBox9.Text = str.ToString();
            }
        }

        private void linkLabel16_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var sr = new StreamReader(@"C:\Users\Влад\Documents\Visual Studio 2017\Projects\WindowsFormsApp1\WindowsFormsApp1\Information\15.txt", Encoding.Default))
            {
                var str = sr.ReadToEnd();
                textBox9.Text = str.ToString();
            }
        }

        private void linkLabel17_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var sr = new StreamReader(@"C:\Users\Влад\Documents\Visual Studio 2017\Projects\WindowsFormsApp1\WindowsFormsApp1\Information\17.txt", Encoding.Default))
            {
                var str = sr.ReadToEnd();
                textBox9.Text = str.ToString();
            }
        }
    }
}




//SqlCommand cmd = new SqlCommand(@"INSERT INTO [User] VALUES(N'" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "', '" + CheckedIDg + "', '" + CheckedIDt + "', '" + CheckedIDp + "')", con);
//conn.Open();
//cmd.ExecuteNonQuery();
//conn.Close();