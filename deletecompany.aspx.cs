using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class delete : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        string LoginStatus = HelperClass.IsLogin();
        
        string id = string.Empty;
        if (Request.QueryString["id"] != null)
        {
            id = Request.QueryString["id"];
            HelperClass.DeleteCompany(id);
            Response.Redirect("admin.aspx");
        }
        else
        {
            Session.RemoveAll();
            Response.Redirect("login-admin.aspx");
        }

    }

}