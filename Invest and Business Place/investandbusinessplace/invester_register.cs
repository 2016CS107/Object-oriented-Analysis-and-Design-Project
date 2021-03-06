using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace investandbusinessplace
{
    public partial class invester_register : UserControl
    {
        public static string person;
        public static string Email;
        public static string Contact;
        public static string Category;
        public static string Password;
        public static string Question;
        public static string Answer;
        string query;
        public invester_register()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "" && txtEmail.Text != "" && txtContact.Text != "" && cmbCategory.Text != "" && txtPassword.Text != "" && txtConfirmPassword.Text != "" && cmbSelectQuestion.Text != "" && txtAnswer.Text != "")
            {
                System.Text.RegularExpressions.Regex expr = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
                if (expr.IsMatch(txtEmail.Text))
                {
                    if (txtPassword.Text == txtConfirmPassword.Text)
                    {
                        person = txtName.Text;
                        Email = txtEmail.Text;
                        Contact = txtContact.Text;
                        Category = cmbCategory.Text;
                        Password = txtPassword.Text;
                        Question = cmbSelectQuestion.Text;
                        Answer = txtAnswer.Text;
                        MessageBox.Show("Approve by admin within 1 day");
                        clear();
                    }
                    else
                    {
                        lblmessages.Text = "Passwords are not matches.";
                        txtPassword.Text = txtConfirmPassword.Text = "";
                    }
                }
                else
                {
                    lblmessages.Text = "Invalid Email";
                    txtEmail.Text = "";
                }
            }
            else
            {
                lblmessages.Text = "You missing important fields.";
            }
        }

        private void clear()
        {
            txtName.Text = txtEmail.Text = txtContact.Text = txtPassword.Text = txtConfirmPassword.Text = txtAnswer.Text = cmbCategory.Text = cmbSelectQuestion.Text = "";
        }

        public void ApproveData(string name, string email, string contact, string category, string password, string question, string answer)
        {
            using (SqlConnection connection = new SqlConnection(StringConnection.connectionstring))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("Select count(*) from Invester where Email = '" + email + "'", connection);
                int result = Convert.ToInt32(cmd.ExecuteScalar());
                if (result > 0)
                {
                    lblmessages.Text = email + " Already Exist";
                    txtEmail.Text = "";
                }
                else
                {
                    try
                    {
                        query = "Insert Into Invester(Name, Email, Contact, Category, Password, Question, Answer) VALUES ('" + name + "', '" + email + "', '" + contact + "', '" + category + "', '" + password + "', '" + question + "' , '" + answer + "')";
                        SqlCommand sqlcmd = new SqlCommand(query, connection);
                        sqlcmd.ExecuteNonQuery();
                        MessageBox.Show("Registeration Successful");
                    }
                    catch
                    {
                        MessageBox.Show("No user for approval");
                    }
                }
            }
        }
    }

}
