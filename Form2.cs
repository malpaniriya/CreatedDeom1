using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CreatedDeom1
{
    public partial class Form2 : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommandBuilder builder;
        DataSet ds;


        public Form2()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString);
        }





        private void Form2_Load(object sender, EventArgs e)
        {

            try
            {
                //write query
                string qry = "select * from Dept";
                //assign query to adapter->will fire query
                da = new SqlDataAdapter(qry, con);
                //created object of Dataset
                ds = new DataSet();
                //fill() will fire the select query and load data in the dataset
                //dept is the name given to the table in the dataset
                da.Fill(ds, "Dept");
                cmbDepartment.DataSource = ds.Tables["Dept"];
                cmbDepartment.DisplayMember = "danme";
                cmbDepartment.ValueMember = "did";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private DataSet GetEmployees()
        {
            string qry = "select*from Employee1";
            da = new SqlDataAdapter(qry, con);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            builder = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "Employee1");
            return ds;
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            ds = GetEmployees();
            //create new record to add record
            DataRow row = ds.Tables["Employee1"].NewRow();
            //assign value to the row 
            row["name"] = txtname.Text;
            row["email"] = txtEmail.Text;
            row["age"] = txtAge.Text;
            row["salary"] = txtSalary.Text;
            row["did"] = cmbDepartment.SelectedValue;
            //attach this row in dataset table
            ds.Tables["Employee1"].Rows.Add(row);
            //update the changes from dataset to DB
            int result = da.Update(ds.Tables["Employee1"]);
            if (result >= 1)
            {
                MessageBox.Show("Record is inserted");
            }


        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetEmployees();
                //find the row
                DataRow row = ds.Tables["Employee1"].Rows.Find(txtID.Text);
                if (row != null)
                {
                    row["name"] = txtname.Text;
                    row["email"] = txtEmail.Text;
                    row["age"] = txtAge.Text;
                    row["salary"] = txtSalary.Text;
                    row["did"] = cmbDepartment.SelectedValue;
                    // update the changes from DataSet to DB
                    int result = da.Update(ds.Tables["Employee"]);
                    if (result >= 1)
                    {
                        MessageBox.Show("Record updated");
                    }
                }
                else
                {
                    MessageBox.Show("Id not matched");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetEmployees();
                // find the row
                DataRow row = ds.Tables["Employee1"].Rows.Find(txtID.Text);
                if (row != null)
                {
                    // delete the current row from DataSet table
                    row.Delete();
                    // update the changes from DataSet to DB
                    int result = da.Update(ds.Tables["Employee1"]);
                    if (result >= 1)
                    {
                        MessageBox.Show("Record deleted");
                    }
                }
                else
                {
                    MessageBox.Show("Id not matched");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select e.*, d.danme from Employee1 e inner join dept d on d.did = e.did";
                da = new SqlDataAdapter(qry, con);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                ds = new DataSet();
                da.Fill(ds, "emp");
                dataGridView1.DataSource = ds.Tables["emp"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select e.*, d.danme from Employee1 e inner join dept d on d.did = e.did";
                da = new SqlDataAdapter(qry, con);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                ds = new DataSet();
                da.Fill(ds, "emp");
                //find method can only seach the data if PK is applied in the DataSet table
                DataRow row = ds.Tables["emp"].Rows.Find(txtID.Text);
                if (row != null)
                {
                    txtname.Text = row["name"].ToString();
                    txtEmail.Text = row["email"].ToString();
                    txtAge.Text = row["age"].ToString();
                    txtSalary.Text = row["salary"].ToString();
                    cmbDepartment.Text = row["danme"].ToString();
                }
                else
                {
                    MessageBox.Show("Record not found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }







        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
            txtname.Text = dataGridView1.CurrentRow.Cells["name"].Value.ToString();
            txtEmail.Text = dataGridView1.CurrentRow.Cells["email"].Value.ToString();
            txtAge.Text = dataGridView1.CurrentRow.Cells["Age"].Value.ToString();
            txtSalary.Text = dataGridView1.CurrentRow.Cells["salary"].Value.ToString();
            cmbDepartment.Text = dataGridView1.CurrentRow.Cells["danme"].Value.ToString() ;




        }

        
    }
}

            
    


          


    
    

