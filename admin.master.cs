using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class layout : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["isAdminLogin"] == null)
        {
            Response.Redirect("login-admin.aspx");
        }

        lblUserNameRightSideBarBottom.Text = lblUserSideBar.Text = Session["isAdminLogin"].ToString();
        lblDestignation.Text = "Administrator";
        lblCompanyName.Text = "Management";
    }
}
