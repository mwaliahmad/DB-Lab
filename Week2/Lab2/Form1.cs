using Microsoft.VisualBasic;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Lab2
{
    public partial class Form1 : Form
    {
        private string Stdreg;
        private string Stdname;
        private string Stddept;
        private string Stdsession;
        private string Stdcgpa;
        private string Stdaddress;

        private string CrsID;
        private string Crsname;
        private string CrsTeacher;
        private string CrsSemester;
        public Form1()
        {
            InitializeComponent();
        }

        private void std_btn_Click(object sender, EventArgs e)
        {
            tabs.SelectedIndex = 1;
        }
        private void course_btn_Click(object sender, EventArgs e)
        {
            tabs.SelectedIndex = 2;
        }
        private void enroll_btn_Click(object sender, EventArgs e)
        {
            tabs.SelectedIndex = 3;
        }

        private void addStd_btn_Click(object sender, EventArgs e)
        {
            tabs.SelectedIndex = 4;
            stdG_btn.Text = "Add Student";

        }
        private void deleteStd_btn_Click(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("Enter Registration No.", "Delete Student", "", 10, 10);
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Student where RegistrationNumber = @RegistrationNumber", con);
            cmd.Parameters.AddWithValue("@RegistrationNumber", input);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                tabs.SelectTab(4);
                stdG_btn.Text = "Delete Student";
                textBox1.Text = reader["RegistrationNumber"].ToString();
                textBox2.Text = reader["Name"].ToString();
                textBox3.Text = reader["Department"].ToString();
                textBox4.Text = reader["Session"].ToString();
                textBox5.Text = reader["CGPA"].ToString();
                textBox6.Text = reader["Address"].ToString();
            }
            else
            {
                MessageBox.Show("No Student Exist");
            }
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            reader.Close();
        }
        private void updateStd_btn_Click(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("Enter Registration No.", "Update Student", "", 10, 10);
            var con = Configuration.getInstance().getConnection();

            SqlCommand cmd = new SqlCommand("Select * from Student where RegistrationNumber = @RegistrationNumber", con);

            cmd.Parameters.AddWithValue("@RegistrationNumber", input);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                tabs.SelectTab(4);
                stdG_btn.Text = "Update Student";
                Stdreg = reader["RegistrationNumber"].ToString();
                Stdname = reader["Name"].ToString();
                Stddept = reader["Department"].ToString();
                Stdsession = reader["Session"].ToString();
                Stdcgpa = reader["CGPA"].ToString();
                Stdaddress = reader["Address"].ToString();
                textBox1.Text = Stdreg;
                textBox2.Text = Stdname;
                textBox3.Text = Stddept;
                textBox4.Text = Stdsession;
                textBox5.Text = Stdcgpa;
                textBox6.Text = Stdaddress;
            }
            else
            {
                MessageBox.Show("No Student Exist");
            }
            textBox1.Enabled = false;
            reader.Close();
        }
        private void searchStd_btn_Click(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("Enter Registration No.", "Search Student", "", 10, 10);
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Student where RegistrationNumber = @RegistrationNumber", con);
            cmd.Parameters.AddWithValue("@RegistrationNumber", input);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                tabs.SelectTab(4);
                stdG_btn.Text = "OK";
                textBox1.Text = reader["RegistrationNumber"].ToString();
                textBox2.Text = reader["Name"].ToString();
                textBox3.Text = reader["Department"].ToString();
                textBox4.Text = reader["Session"].ToString();
                textBox5.Text = reader["CGPA"].ToString();
                textBox6.Text = reader["Address"].ToString();
            }
            else
            {
                MessageBox.Show("No Student Exist");
            }
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            reader.Close();
        }
        private void viewStd_btn_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Student", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gv.DataSource = dt;
            gv.Refresh();
            tabs.SelectTab(6);
        }
        private void stdG_btn_Click(object sender, EventArgs e)
        {
            if (stdG_btn.Text == "Add Student")
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Insert into Student values (@RegistrationNumber, @Name, @Department, @Session, @CGPA, @Address)", con);
                cmd.Parameters.AddWithValue("@RegistrationNumber", textBox1.Text);
                cmd.Parameters.AddWithValue("@Name", textBox2.Text);
                cmd.Parameters.AddWithValue("@Department", textBox3.Text);
                cmd.Parameters.AddWithValue("@Session", textBox4.Text);
                cmd.Parameters.AddWithValue("@CGPA", textBox5.Text);
                cmd.Parameters.AddWithValue("@Address", textBox6.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully saved");
            }
            else if (stdG_btn.Text == "Delete Student")
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("DELETE FROM Student WHERE RegistrationNumber = @RegistrationNumber", con);
                cmd.Parameters.AddWithValue("@RegistrationNumber", textBox1.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully saved");
            }
            else if (stdG_btn.Text == "Update Student")
            {
                var con = Configuration.getInstance().getConnection();
                string query = "UPDATE Student SET " +
                    (textBox2.Text != Stdname ? "Name = @Name, " : "") +
                    (textBox3.Text != Stddept ? "Department = @Department, " : "") +
                    (textBox4.Text != Stdsession ? "Session = @Session, " : "") +
                    (textBox5.Text != Stdcgpa ? "CGPA = @CGPA, " : "") +
                    (textBox6.Text != Stdaddress ? "Address = @Address, " : "");
                query = query.Remove(query.Length - 2);
                query += " WHERE RegistrationNumber = @RegistrationNumber";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@RegistrationNumber", textBox1.Text);

                if (textBox2.Text != Stdname)
                {
                    cmd.Parameters.AddWithValue("@Name", textBox2.Text);
                }
                if (textBox3.Text != Stddept)
                {
                    cmd.Parameters.AddWithValue("@Department", textBox3.Text);
                }
                if (textBox4.Text != Stdsession)
                {
                    cmd.Parameters.AddWithValue("@Session", textBox4.Text);
                }
                if (textBox5.Text != Stdcgpa)
                {
                    cmd.Parameters.AddWithValue("@CGPA", textBox5.Text);
                }
                if (textBox6.Text != Stdaddress)
                {
                    cmd.Parameters.AddWithValue("@Address", textBox6.Text);
                }
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully saved");
            }
            else if (stdG_btn.Text == "OK")
            {
                tabs.SelectTab(1);
            }
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            textBox5.Enabled = true;
            textBox6.Enabled = true;
            tabs.SelectTab(1);
        }
        
        private void addCrs_btn_Click(object sender, EventArgs e)
        {
            tabs.SelectedIndex = 5;
            crsG_btn.Text = "Add Course";
        }
        private void deleteCrs_btn_Click(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("Enter Course ID:", "Delete Course", "", 10, 10);
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Course where Course_ID = @Course_ID", con);
            cmd.Parameters.AddWithValue("@Course_ID", input);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                tabs.SelectTab(5);
                crsG_btn.Text = "Delete Course";
                textBox7.Text = reader["Course_ID"].ToString();
                textBox8.Text = reader["Course_Name"].ToString();
                textBox9.Text = reader["Teacher_Name"].ToString();
                textBox10.Text = reader["Semester"].ToString();
            }
            else
            {
                MessageBox.Show("No Course Exist");
            }
            textBox7.Enabled = false;
            textBox8.Enabled = false;
            textBox9.Enabled = false;
            textBox10.Enabled = false;
            reader.Close();
        }
        private void updateCrs_btn_Click(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("Enter Course ID", "Update Course", "", 10, 10);
            var con = Configuration.getInstance().getConnection();

            SqlCommand cmd = new SqlCommand("Select * from Course where Course_ID = @Course_ID", con);

            cmd.Parameters.AddWithValue("@Course_ID", input);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                tabs.SelectTab(5);
                crsG_btn.Text = "Update Course";
                CrsID = reader["Course_ID"].ToString();
                Crsname = reader["Course_Name"].ToString();
                CrsTeacher = reader["Teacher_Name"].ToString();
                CrsSemester = reader["Semester"].ToString();

                textBox7.Text = CrsID;
                textBox8.Text = Crsname;
                textBox9.Text = CrsTeacher;
                textBox10.Text = CrsSemester;
            }
            else
            {
                MessageBox.Show("No Course Exist");
            }
            textBox7.Enabled = false;
            reader.Close();
        }
        private void searchCrs_btn_Click(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("Enter Course ID:", "Search Course", "", 10, 10);
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Course where Course_ID = @Course_ID", con);
            cmd.Parameters.AddWithValue("@Course_ID", input);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                tabs.SelectTab(5);
                crsG_btn.Text = "OK";
                textBox7.Text = reader["Course_ID"].ToString();
                textBox8.Text = reader["Course_Name"].ToString();
                textBox9.Text = reader["Teacher_Name"].ToString();
                textBox10.Text = reader["Semester"].ToString();
            }
            else
            {
                MessageBox.Show("No Course Exist");
            }
            textBox7.Enabled = false;
            textBox8.Enabled = false;
            textBox9.Enabled = false;
            textBox10.Enabled = false;
            reader.Close();
        }
        private void viewCrs_btn_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Course", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gv.DataSource = dt;
            gv.Refresh();
            tabs.SelectTab(6);
        }
        private void crsG_btn_Click(object sender, EventArgs e)
        {
            if (crsG_btn.Text == "Add Course")
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Insert into Course values (@Course_ID, @Course_Name, @Teacher_Name, @Semester)", con);
                cmd.Parameters.AddWithValue("@Course_ID", textBox7.Text);
                cmd.Parameters.AddWithValue("@Course_Name", textBox8.Text);
                cmd.Parameters.AddWithValue("@Teacher_Name", textBox9.Text);
                cmd.Parameters.AddWithValue("@Semester", textBox10.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully saved");
            }
            else if (crsG_btn.Text == "Delete Course")
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("DELETE FROM Course WHERE Course_ID = @Course_ID", con);
                cmd.Parameters.AddWithValue("@Course_ID", textBox7.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully saved");
            }
            else if (crsG_btn.Text == "Update Course")
            {
                var con = Configuration.getInstance().getConnection();
                string query = "UPDATE Course SET " +
                    (textBox8.Text != Crsname ? "Course_Name = @Course_Name, " : "") +
                    (textBox9.Text != Crsname ? "Teacher_Name = @Teacher_Name, " : "") +
                    (textBox10.Text != CrsSemester ? "Semester = @Semester, " : "");
                query = query.Remove(query.Length - 2);
                query += " WHERE Course_ID = @Course_ID";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@Course_ID", textBox7.Text);

                if (textBox8.Text != Crsname)
                {
                    cmd.Parameters.AddWithValue("@Course_Name", textBox8.Text);
                }
                if (textBox9.Text != Crsname)
                {
                    cmd.Parameters.AddWithValue("@Teacher_Name", textBox9.Text);
                }
                if (textBox10.Text != CrsSemester)
                {
                    cmd.Parameters.AddWithValue("@Semester", textBox10.Text);
                }
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully saved");
            }
            else if (crsG_btn.Text == "OK")
            {
                tabs.SelectTab(2);
            }
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            textBox7.Enabled = true;
            textBox8.Enabled = true;
            textBox9.Enabled = true;
            textBox10.Enabled = true;
            tabs.SelectTab(2);
        }

        private void register_btn_Click(object sender, EventArgs e)
        {
            tabs.SelectedIndex = 7;
            EnrollG_btn.Text = "Register Student";
        }
        private void unregister_btn_Click(object sender, EventArgs e)
        {
            string regNo = Interaction.InputBox("Enter Student Reg NO:", "Unregister Student", "", 10, 10);
            string crsName = Interaction.InputBox("Enter Course Name:", "Unregister Student", "", 10, 10);
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Enrollments where StudentRegNo = @StudentRegNo AND CourseName = @CourseName", con);
            cmd.Parameters.AddWithValue("@StudentRegNo", regNo);
            cmd.Parameters.AddWithValue("@CourseName", crsName);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                tabs.SelectTab(7);
                EnrollG_btn.Text = "Unregister Student";
                textBox11.Text = reader["StudentRegNo"].ToString();
                textBox12.Text = reader["CourseName"].ToString();
            }
            else
            {
                MessageBox.Show("No Register Student Exist");
            }
            textBox11.Enabled = false;
            textBox12.Enabled = false;
            reader.Close();
        }
        private void view_registration_btn_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Enrollments", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gv.DataSource = dt;
            gv.Refresh();
            tabs.SelectTab(6);
        }
        private void EnrollG_btn_Click(object sender, EventArgs e)
        {
            if (EnrollG_btn.Text == "Register Student")
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Insert into Enrollments values (@StudentRegNo, @CourseName)", con);
                cmd.Parameters.AddWithValue("@StudentRegNo", textBox11.Text);
                cmd.Parameters.AddWithValue("@CourseName", textBox12.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully saved");
            }
            else if(EnrollG_btn.Text == "Unregister Student")
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("DELETE FROM Enrollments WHERE StudentRegNo = @StudentRegNo AND CourseName = @CourseName", con);
                cmd.Parameters.AddWithValue("@StudentRegNo", textBox11.Text);
                cmd.Parameters.AddWithValue("@CourseName", textBox12.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully saved");
            }   
            textBox11.Clear();
            textBox12.Clear();
            textBox11.Enabled = true;
            textBox12.Enabled = true;
            tabs.SelectTab(3);
        }

        private void ok_btn_Click(object sender, EventArgs e)
        {
            tabs.SelectTab(0);
        }
    }
}
