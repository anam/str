using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        

        if (IsPostBack)
        {

            string username = txtemailid.Text;
            string password = txtpassword.Text;
            string LoginStatus = string.Empty;

            if (username != string.Empty && password != string.Empty)
            {
                LoginStatus = HelperClass.AdminLogin(username, password);
            }

            Response.Redirect(LoginStatus);
        }
    }
}