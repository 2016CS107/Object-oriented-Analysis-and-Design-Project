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
    public partial class innovator_reset : UserControl
    {
        new string Select;
        string newselect;
        public innovator_reset()
        {
            InitializeComponent();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(StringConnection.connectionstring))
            {
                connection.Open();
                Select = "Select * From Innovator where Email = '" + txtEmail.Text + "' and Question = '" + cmbSelectQuestion.Text + "' and Answer = '" + txtAnswer.Text + "'";
                SqlDataAdapter adp = new SqlDataAdapter(Select, connection);
                DataSet set = new DataSet();
                adp.Fill(set);
                DataTable tbl = set.Tables[0];
                if (tbl.Rows.Count >= 1)
                {
                    if (txtPassword.Text == txtConfirmPssword.Text)
                    {
                        newselect = "Update Innovator set Password = '" + txtPassword.Text + "' where Email = '" + txtEmail.Text + "' and Question = '" + cmbSelectQuestion.Text + "' and Answer = '" + txtAnswer.Text + "'";
                        SqlDataAdapter newadp = new SqlDataAdapter(newselect, connection);
                        DataSet newset = new DataSet();
                        newadp.Fill(newset);
                        lblmessage.Text = "Password Reset";
                    }
                    else
                    {
                        lblmessage.Text = "Passwords are not matches";
                    }
                }
                else
                {
                    lblmessage.Text = "Invalid Information";
                }
            }
        }
    }
}
