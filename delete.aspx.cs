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
        if (HelperClass.isExpired())
        {
            Response.Redirect("login.aspx");
        }
        string LoginStatus = HelperClass.IsLogin();
        string comID = HttpContext.Current.Session["CompanyID"].ToString();
        string id = string.Empty;
        if (Request.QueryString["id"] != null)
        {
            id = Request.QueryString["id"];
        }
        else
        {
            Session.RemoveAll();
            Response.Redirect("login.aspx");
        }

        if (Request.QueryString["type"] != null)
        {
            string type = Request.QueryString["type"];
            if (type == "item")
            {
                if (LoginStatus != null || LoginStatus != string.Empty || LoginStatus != "")
                {
                    HelperClass.DeleteItem(id);
                    Response.Redirect("items.aspx");
                }
            }
            else if (type == "sr")
            {
                HelperClass.DeleteSR(id);
                Response.Redirect("srs.aspx");
            }
            
        }
        else
        {
            Response.Redirect("login.aspx");
            
        }
    }

}