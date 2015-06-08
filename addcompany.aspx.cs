using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        btnReset.Attributes.Add("onClick", "document.forms[0].reset();return false;");
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string name = txtName.Text;
        string manager = txtManager.Text;
        string email = txtEmail.Text;
        string password = txtPassword.Text;


        string Error = "";
        bool isPass = true;
        if(name == null || name == string.Empty || name == "")
        {
            Error = "Please enter the Company Name<br/>";
            isPass = false;
        }
        if (manager == string.Empty && manager == string.Empty)
        {
            Error += "Please enter the Name of Manager<br/>";
            isPass = false;
        }

        if (email == string.Empty && email == string.Empty)
        {
            Error += "Please enter the email of Manager<br/>";
            isPass = false;
        }

        if (password == string.Empty && password == string.Empty)
        {
            Error += "Please enter the password of Manager<br/>";
            isPass = false;
        }

        lblMessage.Text = Error;

        if (isPass)
        {
            HelperClass.AddCompany(name, manager, email, password);
            lblSuccess.Text = name + " company added.";
        }
    }
}