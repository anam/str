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
        string login = HelperClass.UserLoginStatus();
        if (login == "Manager")
        {
            manager1.Visible = manager2.Visible = manager3.Visible = true;
        }
        else
        {
            manager1.Visible = manager2.Visible = manager3.Visible = false;
        }

        lblUserSideBar.Text = Session["userName"].ToString();
        lblUserNameRightSideBarBottom.Text = Session["userName"].ToString();
        lblDestignation.Text = HttpContext.Current.Session["user"].ToString();

        var comname = HelperClass.getSetting();
        var dt = HelperClass.getSetting();
        foreach (DataRow row in dt.Rows)
        {
            lblCompanyName.Text = row["name"].ToString();
        }
    }
}
