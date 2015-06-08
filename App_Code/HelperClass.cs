using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;
using System.Data;

/// <summary>
/// Summary description for HelperClass
/// </summary>
public class HelperClass
{
    string keyBase = "LearningFromMistake";
	public HelperClass()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    public static string UserLoginStatus()
    {
        string result = string.Empty ;

        if(HttpContext.Current.Session["user"] != null && HttpContext.Current.Session != null)
        {
            result = HttpContext.Current.Session["user"].ToString();
        }
        return result;
    }

    public static string Login(string email, string password)
    {
        var connection = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;
        
        using (SqlConnection con = new SqlConnection(connection))
        {
            string sql;
            sql = "SELECT * FROM Users WHERE email='" + email + "' AND password='" + password + "'";
            con.Open();
            SqlCommand cmd;
            cmd = new SqlCommand(sql, con);
            var result = cmd.ExecuteReader();
            if (result.HasRows)
            {
                
                HttpContext.Current.Session["user"] = "Manager";
                while (result.Read())
                {
                    HttpContext.Current.Session["userid"] = result["id"].ToString();
                    HttpContext.Current.Session["userName"] = result["name"].ToString();
                    HttpContext.Current.Session["CompanyID"] = result["com_id"].ToString();
                }
                con.Close();
                return "manager";
            }
            else
            {
                con.Close();
                sql = "SELECT * FROM SRs WHERE email='" + email + "' AND password='" + password + "'";
                con.Open();
                cmd = new SqlCommand(sql, con);
                result = cmd.ExecuteReader();
                if (result.HasRows)
                {
                    
                    HttpContext.Current.Session["user"] = "SR";
                    while (result.Read())
                    {
                        HttpContext.Current.Session["userid"] = result["id"].ToString();
                        HttpContext.Current.Session["userName"] = result["name"].ToString();
                        HttpContext.Current.Session["CompanyID"] = result["com_id"].ToString();
                    }
                    con.Close();
                    
                    return "sr";
                }
            }
            return string.Empty;
        }
    }

    public static string IsLogin()
    {
        string userStatus = HelperClass.UserLoginStatus();
        if (userStatus != string.Empty)
        {
            if (userStatus != "Manager")
            {
                return "SR";
            }
            else
            {
                return "Manager";
            }
        }
        else
        {
            HttpContext.Current.Session["LoginFailed"] = "Yes";
            return string.Empty;
        }
    }

    public static void AddItem(string name, string company="",string unit1 ="", string unit2 = "", string unit3 = "", string price1 = "", string price2 = "", string price3 = "", string comission = "")
    {
        var connection = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connection))
        {
            string sql;
            string ID;
            string comID = HttpContext.Current.Session["CompanyID"].ToString();
            sql = "INSERT INTO Items (com_id, name, company, commission) VALUES (" + comID + ",'" +name+"','" + company + "','" + comission + "')";
            con.Open();
            SqlCommand cmd;
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            ID = HelperClass.GetItemID("Items", "id");
            con.Close();

            if (unit1 != "")
            {
                HelperClass.AddPrice(ID, unit1, price1);
                HelperClass.AddQuantity(comID, ID, unit1, "0");
            }
            if (unit2 != "")
            {
                HelperClass.AddPrice(ID, unit2, price2);
                HelperClass.AddQuantity(comID, ID, unit2, "0");
            }
            if (unit3 != "")
            {
                HelperClass.AddPrice(ID, unit3, price3);
                HelperClass.AddQuantity(comID, ID, unit3, "0");
            }

            
        }
    }

    public static void AddQuantity(string com_id, string item_id, string name, string amount)
    {
        var connection = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connection))
        {
            string sql;
            string comID = HttpContext.Current.Session["CompanyID"].ToString();
            sql = "INSERT INTO Quantity (com_id, item_id, name, amount) VALUES (" + comID + ",'" + item_id + "','" + name + "','" + amount + "')";
            con.Open();
            SqlCommand cmd;
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }

    public static void AddPrice(string item_id, string unit_name, string unit_price)
    {
        
        var connection = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connection))
        {
            string sql;
            string comID = HttpContext.Current.Session["CompanyID"].ToString();
            sql = "INSERT INTO Prices (com_id, item_id, unit_name, unit_price) VALUES (" + comID + ",'" + item_id + "','" + unit_name + "','" + unit_price + "')";
            con.Open();
            SqlCommand cmd;
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }

    public static void UpdatePrice(string id, string itemId, string unit_name, string unit_price)
    {

        var connection = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connection))
        {
            string sql;
            string comID = HttpContext.Current.Session["CompanyID"].ToString();
            sql = "SELECT * FROM Prices WHERE id = " + id;
            con.Open();
            SqlCommand cmd;
            cmd = new SqlCommand(sql, con);
            var result = cmd.ExecuteReader();
            if (result.HasRows)
            {
                con.Close();
                sql = "UPDATE Prices SET unit_name = '" + unit_name + "', unit_price ='" + unit_price + "' WHERE com_id=" + comID + " AND id=" + id;
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                HelperClass.AddPrice(itemId, unit_name, unit_price);
            }
            
        }
    }

    public static bool isNumber(string number)
    {
        int n;
        if (int.TryParse(number,out n))
        {
            return true;
        }
        return false;
    }

    public static string GetItemID(string table, string column)
    {
        var connection = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;
        string i="";
        using (SqlConnection con = new SqlConnection(connection))
        {
            string sql;
            string comID = HttpContext.Current.Session["CompanyID"].ToString();
            sql = "SELECT TOP 1 "+column+" FROM "+table+" WHERE com_id = '"+ comID +"' ORDER BY "+column+" DESC";
            con.Open();
            SqlCommand cmd;
            cmd = new SqlCommand(sql, con);
            var result = cmd.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    i = result["id"].ToString();
                }
                
                return i;
            }
            con.Close();
        }
        return i;
    }

    public static DataTable GetItems()
    {
        string constr = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;
        string comID = HttpContext.Current.Session["CompanyID"].ToString();
        string sql = "SELECT * FROM Items  WHERE com_id='"+comID+"'";
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(sql))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }

    public static DataTable GetProducts()
    {
        string constr = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;
        string comID = HttpContext.Current.Session["CompanyID"].ToString();
        string sql = "SELECT * FROM Items  WHERE com_id='" + comID + "'";
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(sql))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }

    public static DataTable GetPricesSingle(string item_id)
    {
        string constr = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;
        string comID = HttpContext.Current.Session["CompanyID"].ToString();
        string sql = "SELECT * FROM Prices  WHERE item_id='" + item_id + "' AND com_id='"+comID+"'";
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(sql))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }

    public static bool IsSREmailExist(string email)
    {
        var connection = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;
        using (SqlConnection con = new SqlConnection(connection))
        {
            string sql;
            
            string comID = HttpContext.Current.Session["CompanyID"].ToString();
            sql = "SELECT * FROM SRs WHERE com_id = '"+comID+"' AND email = '"+email+"'";
            con.Open();
            SqlCommand cmd;
            cmd = new SqlCommand(sql, con);
            var result = cmd.ExecuteReader();
            if (result.HasRows)
            {
                return true;
            }
            con.Close();
        }
        return false;
    }

    public static void AddSR(string name, string salary, string email, string password, string phone= null)
    {
        var connection = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connection))
        {
            string sql;
            string comID = HttpContext.Current.Session["CompanyID"].ToString();
            sql = "INSERT INTO SRs (com_id, name, salary, email, password, phone) VALUES (" + comID + ",'" + name + "','" + salary + "','" + email + "','"+ password + "','" + phone + "')";
            con.Open();
            SqlCommand cmd;
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }

    public static DataTable GetSRs()
    {
        string constr = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;
        string comID = HttpContext.Current.Session["CompanyID"].ToString();
        string sql = "SELECT * FROM SRs WHERE com_id='" + comID + "'";
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(sql))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }

    public static string GetTarget()
    {
        string target = string.Empty;
        string constr = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;
        string comID = HttpContext.Current.Session["CompanyID"].ToString();
        
        using (SqlConnection con = new SqlConnection(constr))
        {
            string sql = "SELECT target FROM Bonus WHERE com_id=" + comID;
            con.Open();
            SqlCommand cmd;
            cmd = new SqlCommand(sql, con);
            var result = cmd.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    target =  result["target"].ToString();
                }
            }
        }
        return target;
    }

    public static void updateTarget(string target, string amount)
    {
        var connection = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connection))
        {
            string sql;
            string day = DateTime.Now.Day.ToString();
            string month = HelperClass.GetMonthName(DateTime.Now.Month);
            string year = DateTime.Now.Year.ToString();
            string date = month + " " + day + ", " + year;
            string comID = HttpContext.Current.Session["CompanyID"].ToString();
            sql = "UPDATE Bonus SET target='" + target + "', bonus_amount="+ amount +", date='"+ date + "' WHERE com_id = " + comID;
            con.Open();
            SqlCommand cmd;
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }

    public static string GetBonusAmount()
    {
        string bonus = string.Empty;
        string constr = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;
        string comID = HttpContext.Current.Session["CompanyID"].ToString();
        
        using (SqlConnection con = new SqlConnection(constr))
        {
            string sql = "SELECT bonus_amount FROM Bonus WHERE com_id=" + comID;
            con.Open();
            SqlCommand cmd;
            cmd = new SqlCommand(sql, con);
            var result = cmd.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    bonus = result["bonus_amount"].ToString();
                }
            }
        }
        return bonus;
    }

    public static string GetSRSale(string id)
    {
        string constr = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;
        string comID = HttpContext.Current.Session["CompanyID"].ToString();
        
        string sql = "SELECT * FROM Sales WHERE com_id=" + comID + " AND officer_id="+id;
        int i = 0;
        using (SqlConnection con = new SqlConnection(constr))
        {
            con.Open();
            SqlCommand cmd;
            cmd = new SqlCommand(sql, con);
            var result = cmd.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    i++;
                }
            }
        }
        return i.ToString();
    }


    public static void DeleteItem(string id)
    {
        string constr = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;
        string comID = HttpContext.Current.Session["CompanyID"].ToString();

        string sql = "DELETE FROM Items WHERE com_id='"+comID+"' AND id = '"+id+"'";
        int i = 0;
        using (SqlConnection con = new SqlConnection(constr))
        {
            con.Open();
            SqlCommand cmd;
            cmd = new SqlCommand(sql, con);
            if (cmd.ExecuteNonQuery() > 0)
            {
                HttpContext.Current.Session["ActionMessage"] = "Item Deleted";
            }
            else
            {
                HttpContext.Current.Session["ActionMessage"] = "Faild to Delete";
            }
        }
    }

    public static void DeleteSR(string id)
    {
        string constr = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;
        string comID = HttpContext.Current.Session["CompanyID"].ToString();

        string sql = "DELETE FROM SRs WHERE com_id='" + comID + "' AND id = '" + id + "'";
        int i = 0;
        using (SqlConnection con = new SqlConnection(constr))
        {
            con.Open();
            SqlCommand cmd;
            cmd = new SqlCommand(sql, con);
            if (cmd.ExecuteNonQuery() > 0)
            {
                HttpContext.Current.Session["ActionMessage"] = "Item Deleted";
            }
            else
            {
                HttpContext.Current.Session["ActionMessage"] = "Faild to Delete";
            }
        }
        
    }

    public static DataTable getSingleItem(string id)
    {

        string constr = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;
        string comID = HttpContext.Current.Session["CompanyID"].ToString();
        string sql = "SELECT * FROM Items WHERE com_id='" + comID + "' AND id="+id;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(sql))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }

    }

    public static DataTable getSingleItemUnitAndPrice(string id)
    {
        string constr = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;
        string comID = HttpContext.Current.Session["CompanyID"].ToString();
        string sql = "SELECT * FROM Prices WHERE com_id='" + comID + "' AND item_id=" + id;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(sql))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }

    public static void UpdateItem(string id, string name, string company = "", string unit1 = "", string unit2 = "", string unit3 = "", string price1 = "", string price2 = "", string price3 = "", string comission = "", string unitid1 = "", string unitid2 = "", string unitid3="")
    {
        var connection = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connection))
        {
            string sql;
            
            string comID = HttpContext.Current.Session["CompanyID"].ToString();
            sql = "INSERT INTO Items (com_id, name, company, commission) VALUES (" + comID + ",'" + name + "','" + company + "','" + comission + "')";
            sql = "UPDATE Items SET name='" + name + "', company ='" + company + "', commission = '" + comission + "' WHERE id="+id;
            con.Open();
            SqlCommand cmd;
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();

            if(unitid1 != "")
                HelperClass.UpdatePrice(unitid1, id, unit1, price1);
            if(unitid2 != "")
                HelperClass.UpdatePrice(unitid2, id, unit2, price2);
            if(unitid3!="")
                HelperClass.UpdatePrice(unitid3, id, unit3, price3);
            
        }
    }

    public static DataTable getSingleSR(string id)
    {

        string constr = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;
        string comID = HttpContext.Current.Session["CompanyID"].ToString();
        string sql = "SELECT * FROM SRs WHERE com_id='" + comID + "' AND id=" + id;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(sql))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }

    public static void UpdateSR(string id, string name, string email = "", string password = "", string phone = "",  string salary = "")
    {
        var connection = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connection))
        {
            string sql;
            
            string comID = HttpContext.Current.Session["CompanyID"].ToString();
            sql = "UPDATE SRs SET name='" + name + "', email ='" + email + "', password = '" + password + "', phone = '"+phone+"', salary = "+salary+" WHERE id=" + id + " AND com_id = "+comID;
            con.Open();
            SqlCommand cmd;
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }

    public static DataTable getSetting()
    {
        var connection = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;
        string comID = HttpContext.Current.Session["CompanyID"].ToString();
        string sql = "SELECT * FROM Settings WHERE com_id=" + comID;
        using (SqlConnection con = new SqlConnection(connection))
        {
            using (SqlCommand cmd = new SqlCommand(sql))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }

    public static void updateCompanyName(string name)
    {
        var connection = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;
        
        using (SqlConnection con = new SqlConnection(connection))
        {
            string sql;

            string comID = HttpContext.Current.Session["CompanyID"].ToString();
            sql = "UPDATE Settings SET name='" + name +"' WHERE com_id = " + comID;
            con.Open();
            SqlCommand cmd;
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }

    public static void updatePassword(string password)
    {
        var connection = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connection))
        {
            string sql;
            string id = HttpContext.Current.Session["userid"].ToString();
            string comID = HttpContext.Current.Session["CompanyID"].ToString();
            sql = "UPDATE SRs SET password='" + password + "' WHERE id=" + id + " AND com_id = " + comID;
            con.Open();
            SqlCommand cmd;
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
    public static DataTable getItemTypesName()
    {
        var connection = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;
        string comID = HttpContext.Current.Session["CompanyID"].ToString();
        string sql = "select * from (select *,row_number() over (partition by company order by company) as row_number from Items ) as rows where row_number=1 AND com_id="+comID;
        using (SqlConnection con = new SqlConnection(connection))
        {
            using (SqlCommand cmd = new SqlCommand(sql))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }

    public static DataTable getItemsOfType(string itemname)
    {
        var connection = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;
        string comID = HttpContext.Current.Session["CompanyID"].ToString();
        string sql = "SELECT * FROM Items WHERE com_id=" + comID + " AND company='" + itemname + "'";
        using (SqlConnection con = new SqlConnection(connection))
        {
            using (SqlCommand cmd = new SqlCommand(sql))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }

    public static DataTable getItmesUnitName(string id)
    {
        var connection = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;
        string comID = HttpContext.Current.Session["CompanyID"].ToString();
        string sql = "SELECT * FROM Prices WHERE com_id=" + comID + " AND item_id=" + id;
        using (SqlConnection con = new SqlConnection(connection))
        {
            using (SqlCommand cmd = new SqlCommand(sql))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }

    public static DataTable getSingleItemStock(string item_id, string name)
    {
        var connection = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;
        string comID = HttpContext.Current.Session["CompanyID"].ToString();
        string sql = "SELECT * FROM Quantity WHERE com_id=" + comID + " AND item_id=" + item_id + " AND name='"+name+"'";
        using (SqlConnection con = new SqlConnection(connection))
        {
            using (SqlCommand cmd = new SqlCommand(sql))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }

    public static void AddItemsInStock(string item_id, string name, string amount)
    {
        var connection = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connection))
        {
            string sql;
            string comID = HttpContext.Current.Session["CompanyID"].ToString();
            sql = "UPDATE Quantity SET amount='" + amount + "' WHERE item_id=" + item_id + " AND com_id = " + comID +" AND name='"+name+"'";
            con.Open();
            SqlCommand cmd;
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }

    public static DataTable getSingleStock(string item_id, string item_unit=null)
    {
        var connection = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;
        string comID = HttpContext.Current.Session["CompanyID"].ToString();
        string sql = "SELECT * FROM Quantity WHERE com_id=" + comID + " AND item_id=" + item_id;
        if (item_unit != null)
        {
            sql = "SELECT * FROM Quantity WHERE com_id=" + comID + " AND item_id=" + item_id + " AND name='" + item_unit + "'" ;
        }
        using (SqlConnection con = new SqlConnection(connection))
        {
            using (SqlCommand cmd = new SqlCommand(sql))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }

    public static string GetMonthName(int number)
    {
        if (number == 1)
        {
            return "January";
        }
        else if (number == 2)
        {
            return "February";
        }
        else if (number == 3)
        {
            return "March";
        }
        else if (number == 4)
        {
            return "April";
        }
        else if (number == 5)
        {
            return "May";
        }
        else if (number == 6)
        {
            return "June";
        }
        else if(number == 7)
        {
            return "July";
        }
        else if (number == 8)
        {
            return "August";
        }
        else if (number == 9)
        {
            return "September";
        }
        else if (number == 10)
        {
            return "October";
        }
        else if (number == 11)
        {
            return "November";
        }
        else if (number == 12)
        {
            return "December";
        }
        return "";
    }

    public static void AddSaleItem(string itemId, string quantity, string price, string gift, string bank, string payment_method, string dealer, string area, string officer)
    {

    }

    public static void SaleUnit(string itemId, string unit_name, string unit_value)
    {
        DataTable dt = HelperClass.getSingleStock(itemId, unit_name);
        string UnitValue = "";
        foreach (DataRow row in dt.Rows)
        {
            UnitValue = row["amount"].ToString();
        }

        int UnitValueInInt = int.Parse(UnitValue);
        int lastValue = UnitValueInInt - int.Parse(unit_value);


        var connection = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connection))
        {
            string sql;
            string comID = HttpContext.Current.Session["CompanyID"].ToString();
            sql = "UPDATE Quantity SET amount='" + lastValue + "' WHERE item_id=" + itemId + " AND com_id = " + comID + " AND name='" + unit_name + "'";
            con.Open();
            SqlCommand cmd;
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }

    public static void AddSalesReport(string item_id, string quantity, string gift, string bank_name, string payment_method, string officer_id, string dealer_name, string area, string payment_amount, string officer_name)
    {
        var connection = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;
        string day = DateTime.Now.Day.ToString();
        string month = HelperClass.GetMonthName(DateTime.Now.Month);
        string year = DateTime.Now.Year.ToString();
        string date = month + " " + day + ", " + year;


        using (SqlConnection con = new SqlConnection(connection))
        {
            string sql;
            string comID = HttpContext.Current.Session["CompanyID"].ToString();
            sql = "INSERT INTO Sales (com_id, item_id, quantity, gift, bank_name, payment_method, officer_id, dealer_name, area, payment_amount,date,officer_name) VALUES (" + comID + "," + item_id + ",'" + quantity + "','" + gift + "','" + bank_name + "','" + payment_method + "','" + officer_id + "','" + dealer_name + "','" + area + "','" + payment_amount + "','" + date + "','"+officer_name+"')";
            con.Open();
            SqlCommand cmd;
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }

    public static DataTable getSales()
    {
        string constr = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;
        string comID = HttpContext.Current.Session["CompanyID"].ToString();
        string sql = "SELECT * FROM Sales  WHERE com_id='" + comID + "'";
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(sql))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }

    public static string GetSalesOfCurrentMonth()
    {
        string constr = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;
        string comID = HttpContext.Current.Session["CompanyID"].ToString();
        int _month = DateTime.Now.Month;
        int _year = DateTime.Now.Year;
        string month = HelperClass.GetMonthName(_month);
        string date = month + "%, " + _year.ToString();
        int count;
        string sql = "SELECT COUNT(id) FROM Sales  WHERE com_id='" + comID + "' AND date like '"+date+"'";
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(sql))
            {
                cmd.Connection = con;
                con.Open();
                count = (int)cmd.ExecuteScalar();
            }
        }
        return count.ToString();
    }


    public static string GetAvailableItems()
    {
        string constr = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;
        string comID = HttpContext.Current.Session["CompanyID"].ToString();
        int _month = DateTime.Now.Month;
        int _year = DateTime.Now.Year;
        string month = HelperClass.GetMonthName(_month);
        string date = month + "%, " + _year.ToString();
        int count;
        string sql = "SELECT COUNT(id) FROM Items  WHERE com_id=" + comID;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(sql))
            {
                cmd.Connection = con;
                con.Open();
                count = (int)cmd.ExecuteScalar();
            }
        }
        return count.ToString();
    }

    public static string GetAvailableSRs()
    {
        string constr = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;
        string comID = HttpContext.Current.Session["CompanyID"].ToString();
        int _month = DateTime.Now.Month;
        int _year = DateTime.Now.Year;
        string month = HelperClass.GetMonthName(_month);
        string date = month + "%, " + _year.ToString();
        int count;
        string sql = "SELECT COUNT(id) FROM SRs  WHERE com_id=" + comID;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(sql))
            {
                cmd.Connection = con;
                con.Open();
                count = (int)cmd.ExecuteScalar();
            }
        }
        return count.ToString();
    }

    public static string AdminLogin(string username, string password)
    {
        if (username == "fagun" || username == "anam")
        {
            if (password == "ReadMore&LearnMore")
            {
                HttpContext.Current.Session["isAdminLogin"] = username;

                return "admin.aspx";
            }
        }
        return "login-admin.aspx";
    }

    public static DataTable getCompanyNames(string id=null)
    {
        string constr = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;
        string sql;
        if (id == null)
        {
            sql = "SELECT * FROM Settings";
        }
        else
        {
            sql = "SELECT * FROM Settings WHERE com_id="+id;
        }
        
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(sql))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }

    public static DataTable getCompanyDetails(string com_id)
    {
        string constr = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;
        
        string sql = "SELECT * FROM Users WHERE com_id='"+com_id+"'";
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(sql))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }

    public static string newCompanyId()
    {
        string constr = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;
        int count;
        string sql = "SELECT TOP 1 id FROM Users ORDER BY id DESC";
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(sql))
            {
                cmd.Connection = con;
                con.Open();
                count = (int)cmd.ExecuteScalar();
                count++;
            }
        }
        return count.ToString();
    }

    public static void AddCompany(string name, string manager, string email, string password)
    {
        var connection = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connection))
        {
            string sql;
            string com_id = newCompanyId();

            sql = "INSERT INTO Settings (com_id, name) VALUES (" + com_id + ",'" + name + "')";
            con.Open();
            SqlCommand cmd;
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
            sql = "INSERT INTO Users (com_id, name, password, email) VALUES (" + com_id + ",'" + manager + "', '"+password+"','"+email+"')";
            con.Open();
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }

    public static void DeleteCompany(string com_id)
    {
        string constr = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;
        
        using (SqlConnection con = new SqlConnection(constr))
        {
            con.Open();
            string sql = "DELETE Bonus WHERE com_id=" + com_id;
            SqlCommand cmd;
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "DELETE Items WHERE com_id=" + com_id;
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "DELETE Prices WHERE com_id=" + com_id;
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "DELETE Quantity WHERE com_id=" + com_id;
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            sql = "DELETE Sales WHERE com_id=" + com_id;
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            sql = "DELETE Settings WHERE com_id=" + com_id;
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            sql = "DELETE SRs WHERE com_id=" + com_id;
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            sql = "DELETE Stock WHERE com_id=" + com_id;
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            sql = "DELETE Users WHERE com_id=" + com_id;
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
        }
    }

    public static bool isExpired()
    {
        if (HelperClass.IsLogin() == "")
        {
            return true;
        }
        
        return false;
    }

    public static DataTable getCompanyInformation(string id)
    {
        string constr = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;

        string sql = "SELECT Users.*, Settings.* FROM Users, Settings WHERE Users.com_id=" + id + " AND Settings.com_id="+id;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(sql))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }

    public static void EditCompany(string id, string name, string manager, string email, string password)
    {
        var connection = ConfigurationManager.ConnectionStrings["DBDealerManagement"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connection))
        {
            string sql;

            sql = "UPDATE Users SET name='" + manager + "', email='" + email + "', password='" + password + "' WHERE com_id = " + id;
            con.Open();
            SqlCommand cmd;
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            sql = "UPDATE Settings SET name='" + name + "' WHERE com_id = " + id;
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}