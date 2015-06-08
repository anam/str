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
        string LoginStatus = HelperClass.IsLogin();
        if (LoginStatus != "Manager")
        {
            Response.Redirect("login.aspx");
        }

        if (Session["ActionMessage"] != null)
        {
            lblMessage.Text = Session["ActionMessage"].ToString();
            Session.Remove("ActionMessage");
        }

        if (!IsPostBack)
        {
            DataTable dt = HelperClass.GetSRs();
            StringBuilder html = new StringBuilder();
            int i = 0;

            string name, sales, target;
            string currentsalary, bonus;
            foreach (DataRow row in dt.Rows)
            {
                name = row["name"].ToString();
                sales = HelperClass.GetSRSale(row["id"].ToString());
                target = HelperClass.GetTarget();
                currentsalary = row["salary"].ToString();
                bonus = HelperClass.GetBonusAmount();
                i++;
                html.Append("<tr>");
                html.Append("<td>");
                html.Append(i);
                html.Append("</td>");

                html.Append("<td width='20%'>");
                html.Append(name);
                html.Append("</td>");

                html.Append("<td width='20%'>");
                html.Append(sales);
                html.Append("</td>");
                html.Append("<td>");
                html.Append(target);
                html.Append("</td>");
                html.Append("<td>");
                html.Append(currentsalary);
                html.Append("</td>");
                html.Append("<td>");
                int _target;
                int.TryParse(target, out _target);
                int _sales;
                int.TryParse(sales, out _sales);

                int _bonus;
                if (_sales >= _target)
                {
                    html.Append(bonus);
                }
                
                html.Append("</td>");

                html.Append("<td width='14%'>");
                
                int _salary = int.Parse(currentsalary);
                
                if(bonus.Contains(" "))
                {
                    bonus.Replace(" ", "");
                }
                if (bonus.Contains("%"))
                {
                    string _bonusTemp = bonus.Replace("%", "");
                    int.TryParse(_bonusTemp, out _bonus);
                    _bonus = (_salary * _bonus) / 100;
                }
                else
                {
                    int.TryParse(bonus, out _bonus);
                }
                int grandSalary;
                if (_sales >= _target)
                {
                    grandSalary = _salary + _bonus;
                }
                else
                {
                    grandSalary = _salary;
                }
                
                html.Append(grandSalary);
                html.Append("</td>");
                html.Append("<td>");
                html.Append("<a href = 'editsr.aspx?id=" + row["id"].ToString() + "'><button class='btn btn-primary btnedit'>Edit</button></a>");
                html.Append("<a href = 'delete.aspx?id=" + row["id"].ToString() + "&type=sr'><button class='btn btn-primary btnedit'>Delete</button></a>");
                html.Append("</td>");
                html.Append("</tr>");
            }

            SRsData.Controls.Add(new Literal { Text = html.ToString() });

        }
    }
}