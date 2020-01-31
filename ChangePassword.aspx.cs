using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Account_ChangePassword : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
    //DataClass DB = new DataClass();
    //PPClass ppc = new PPClass();
    //protected override void OnPreInit(EventArgs e)
    //{
    //    this.MasterPageFile = "~/" + Session["MasterPage"].ToString();
    //    base.OnPreInit(e);
    //} 
    //protected override void OnPreInit(EventArgs e)
    //{
    //    try
    //    {
    //        if (Session["userType"].ToString().Trim() == "Admin")
    //        {
    //            this.MasterPageFile = "~/MainMasterPagea.master";
    //        }
    //        else
    //        {
    //            this.MasterPageFile = "~/MainMasterPage.master";
    //        }
    //    }
    //    catch
    //    {
    //        Response.Redirect("default.aspx");
    //    }
    //}
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void ChangePasswordPushButton_Click(object sender, EventArgs e)
    {
        if (CurrentPassword.Text == Session["password"].ToString())
        {
            if (NewPassword.Text == ConfirmNewPassword.Text)
            {
                //conn = DB.DynamicConnection("MOSPlanner");
                SqlCommand cmd = new SqlCommand("Update admin set password=@pwd where userID=" + Session["userID"].ToString().Trim(), conn);
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                cmd.Parameters.Add(new SqlParameter("@pwd", SqlDbType.VarChar, NewPassword.Text.Trim().Length)).Value = NewPassword.Text.Trim();
                cmd.ExecuteNonQuery();
                conn.Close();
                Response.Redirect("ChangePasswordSuccess.aspx");
            }
        }
    }
}
