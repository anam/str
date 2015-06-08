using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
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
        if (!IsPostBack)
        {
            DataTable dt = HelperClass.getSales();
            StringBuilder html = new StringBuilder();
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                List<string> ListofStock = new List<string>();
                DataTable dt3 = HelperClass.getSingleItem(row["item_id"].ToString());
                html.Append("<tr>");
                html.Append("<td>");
                html.Append(row["id"].ToString());
                html.Append("</td>");
                foreach (DataRow rowTest in dt3.Rows)
                {
                    html.Append("<td width='20%'>");
                    html.Append(rowTest["name"].ToString());
                    html.Append("</td>");

                    html.Append("<td width='20%'>");
                    html.Append(rowTest["company"].ToString());
                    html.Append("</td>");
                }

                html.Append("<td>");
                html.Append(row["quantity"].ToString());
                html.Append("</td>");

                html.Append("<td>");
                html.Append(row["gift"].ToString());
                html.Append("</td>");

                html.Append("<td>");
                html.Append(row["payment_amount"].ToString());
                html.Append("</td>");

                html.Append("<td>");
                html.Append(row["payment_method"].ToString());
                html.Append("</td>");
                html.Append("<td>");
                html.Append(row["bank_name"].ToString());
                html.Append("</td>");
                html.Append("<td>");
                html.Append(row["dealer_name"].ToString());
                html.Append("</td>");
                html.Append("<td>");
                html.Append(row["area"].ToString());
                html.Append("</td>");
                html.Append("<td>");
                string officer = row["officer_name"].ToString();
                if (officer == "")
                    officer = "Manager";
                html.Append(officer);
                html.Append("</td>");
                html.Append("<td>");
                html.Append(row["date"].ToString());
                html.Append("</td>");
                html.Append("</tr>");
            }

            ItemsData.Controls.Add(new Literal { Text = html.ToString() });
        }
    }
}