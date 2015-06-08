using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
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
        if (LoginStatus == null && LoginStatus == "")
        {
            Response.Redirect("login.aspx");
        }

        if (LoginStatus == "Manager")
        {
            password.Visible = false;
        }
        else
        {
            company.Visible = false;
        }

        if (!IsPostBack)
        {
            var dt = HelperClass.getSetting();
            foreach (DataRow row in dt.Rows)
            {
                txtCompanyName.Text = row["name"].ToString();
            }
        }
        
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string id = HelperClass.UserLoginStatus();
        if (txtPassword.Text != "" && txtPassword.Text != null)
        {
            HelperClass.updatePassword(txtPassword.Text);
            
        }
        if (txtCompanyName.Text != "" && txtCompanyName.Text != null)
        {
            HelperClass.updateCompanyName(txtCompanyName.Text);
        }
    }
}