using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;

namespace SiqiDemo1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string path = dlg.SelectedPath;
            String connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "Delete from T_Numbers";
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            string[] files = Directory.GetFiles(path, "*.txt", SearchOption.AllDirectories);
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "Insert into T_Numbers (StartNo, EndNo, Name) values (@StartNo, @EndNo, @Name)";
                    foreach (string file in files)
                    {
                        string carrier = Path.GetFileNameWithoutExtension(file);
                        string[] lines = File.ReadAllLines(file, Encoding .Default );
                        foreach (string line in lines)
                        {
                            string[] strs = line.Split('-');
                            string startNumber = strs[0];
                            string endNumber = strs[1];
                            string state = strs[2];
                            cmd.Parameters.Clear();
                            cmd.Parameters.Add(new SqlParameter("StartNo", startNumber));
                            cmd.Parameters.Add(new SqlParameter("EndNo", endNumber));
                            cmd.Parameters.Add(new SqlParameter("Name", state + "-" + carrier));
                            cmd.ExecuteNonQuery();
                        }
                    }

                }
                conn.Close();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            String connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select * from T_Numbers where StartNo<=@No and EndNo>=@No";
                    cmd.Parameters.Add(new SqlParameter("No", textBox1.Text));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string name = reader.GetString(reader.GetOrdinal("Name"));
                            MessageBox.Show("Cell phone location and carrier is " + name);
                        }
                        else
                        {
                            MessageBox.Show("No carrier information");
                        }
                    }
                }
            }
        }
    }
}
