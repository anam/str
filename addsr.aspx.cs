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
        if (HelperClass.isExpired())
        {
            Response.Redirect("login.aspx");
        }
        string LoginStatus = HelperClass.IsLogin();
        if (LoginStatus != "Manager")
        {
            Response.Redirect("login.aspx");
        }
        btnReset.Attributes.Add("onClick", "document.forms[0].reset();return false;");

    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string name = txtName.Text;
        string salary = txtSalary.Text;
        string email = txtEmail.Text;
        string password = txtPassword.Text;
        string phone = txtPhone.Text;
        string Error = "";
        bool isPass = true;
        if(name == null || name == string.Empty || name == "")
        {
            Error += "Please enter the Name<br/>";
            isPass = false;
        }

        if (salary == null || salary == string.Empty || salary == "")
        {
            Error += "Please enter the Salary of SR<br/>";
            isPass = false;
        }

        if (email == null || email == string.Empty || email == "")
        {
            Error += "Please enter the Email<br/>";
            isPass = false;
        }

        if (HelperClass.IsSREmailExist(email))
        {
            Error += "Email already exist<br/>";
            isPass = false;
        }

        if (password == null || password == string.Empty || password == "")
        {
            Error += "Please enter the Password<br/>";
            isPass = false;
        }

        if (phone == null || phone == string.Empty || phone == "")
        {
            Error += "Please enter the Phone Number of SR<br/>";
            isPass = false;
        }

        
        if(salary != string.Empty)
        {
            if (!HelperClass.isNumber(salary))
            {
                Error += "Please Enter any numberic number in Salary.<br/>";
                isPass = false;
            }
        }

        lblMessage.Text = Error;

        if (isPass)
        {
            HelperClass.AddSR(name, salary, email, password, phone);
            Session.Add("isItemAdded", true);
            lblSuccess.Text = "Sales Representive Added.";
            txtEmail.Text = txtName.Text = txtPassword.Text = txtPhone.Text = txtSalary.Text = "";
        }
        
    }
}