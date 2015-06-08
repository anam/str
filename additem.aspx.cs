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


    [WebMethod]
    public static string[] GetCategory(string term)
    {
        List<string> retCategory = new List<string>();
        string ConnectionString = @"Data Source=FALTU\SQLEXPRESS;Initial Catalog=DealerManagementSystem;Integrated Security=True";
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            string query = string.Format("SELECT unit_name from Prices where unit_name Like '%{0}%'", term);
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    retCategory.Add(reader.GetString(0));
                }
            }
            con.Close();
        }
        return retCategory.ToArray();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string name = txtName.Text;
        string company = txtCompany.Text;
        string unit_name1 = txtUnit1.Text;
        string unit_name2 = txtUnit2.Text;
        string unit_name3 = txtUnit3.Text;
        string unit_price1 = txtUnitPrice1.Text;
        string unit_price2 = txtUnitPrice2.Text;
        string unit_price3 = txtUnitPrice3.Text;
        string comission = txtComission.Text;
        string Error = "";
        bool isPass = true;
        if(name == null || name == string.Empty || name == "")
        {
            Error = "Please enter the Name of the Item<br/>";
            isPass = false;
        }
        if ((unit_name1 == string.Empty && unit_name2 == string.Empty && unit_name3 == string.Empty) || (unit_name1 == null && unit_name2 == null && unit_name3 == null) || (unit_name1 == "" && unit_name2 == "" && unit_name3 == ""))
        {
            Error += "Please enter the Unit Name and Unit Price<br/>";
            isPass = false;
        }

        if ((unit_name1 != string.Empty && unit_price1 == string.Empty) || (unit_name2 != string.Empty && unit_price2 == string.Empty) || (unit_name3 != string.Empty && unit_price3 == string.Empty))
        {
            Error += "Please enter the Price of Unit.<br/>";
            isPass = false;
        }

        if ((unit_name1 == string.Empty && unit_price1 != string.Empty) || (unit_name2 == string.Empty && unit_price2 != string.Empty) || (unit_name3 == string.Empty && unit_price3 != string.Empty))
        {
            Error += "Please enter the Name of Unit.<br/>";
            isPass = false;
        }
        double p1, p2, p3;

        if(unit_price1 != string.Empty)
        {
            if (!HelperClass.isNumber(unit_price1))
            {
                Error += "Please Enter any numberic number in price 1.<br/>";
                isPass = false;
            }
        }

        if (unit_price2 != string.Empty)
        {
            if (!HelperClass.isNumber(unit_price1))
            {
                Error += "Please Enter any numberic number in price 1.<br/>";
                isPass = false;
            }
        }

        if (unit_price3 != string.Empty)
        {
            if (!HelperClass.isNumber(unit_price1))
            {
                Error += "Please Enter any numberic number in price 1.<br/>";
                Session.Remove("isItemAdded");
                isPass = false;
            }
        }

        lblMessage.Text = Error;

        if (isPass)
        {
            HelperClass.AddItem(name, company, unit_name1, unit_name2, unit_name3, unit_price1, unit_price2, unit_price3, comission);
            Session.Add("isItemAdded", true);
            lblSuccess.Text = "Item Added";
            txtName.Text = txtComission.Text = txtCompany.Text = txtUnit1.Text = txtUnit2.Text = txtUnit3.Text = txtUnitPrice1.Text = txtUnitPrice2.Text = txtUnitPrice3.Text = "";
        }
    }
}