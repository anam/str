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
            Response.Redirect("~/login.aspx");
        }
        string LoginStatus = HelperClass.IsLogin();
        string target = "";
        string bonusAmount = "";
        if (LoginStatus != "Manager")
        {
            Response.Redirect("login.aspx");
        }
        if (!IsPostBack)
        {
            target = HelperClass.GetTarget();
            bonusAmount = HelperClass.GetBonusAmount();
            txtTarget.Text = target;
            txtAmount.Text = bonusAmount;
        }

    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string target = txtTarget.Text;
        string amount = txtAmount.Text;
        HelperClass.updateTarget(target, amount);
    }
}