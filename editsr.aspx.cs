using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    static string id;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HelperClass.isExpired())
        {
            Response.Redirect("login.aspx");
        }
        string LoginStatus = HelperClass.IsLogin();

        string comID = HttpContext.Current.Session["CompanyID"].ToString();

        if (LoginStatus != "Manager")
        {
            Response.Redirect("login.aspx");
        }
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                id = Request.QueryString["id"];
                DataTable dt = HelperClass.getSingleSR(id);
                
                foreach (DataRow row in dt.Rows)
                {
                    txtEmail.Text = row["email"].ToString();
                    txtName.Text = row["name"].ToString();
                    txtPassword.Text = row["password"].ToString();
                    txtPhone.Text = row["phone"].ToString();
                    txtSalary.Text = row["salary"].ToString();
                }
            }
            else
            {
                Session.RemoveAll();
                Response.Redirect("login.aspx");
            }
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string name, email, password, phone, salary = "";
        name = txtName.Text;
        email = txtEmail.Text;
        password = txtPassword.Text;
        phone = txtPhone.Text;
        salary = txtSalary.Text;
        if (password != "")
        {
            HelperClass.UpdateSR(id, name, email, password, phone, salary);
            Session["ActionMessage"] = "SR information Saved";
            Response.Redirect("srs.aspx");
        }
        else
        {
            lblMessage.Text = "Please enter the password.";
        }
        
    }
}