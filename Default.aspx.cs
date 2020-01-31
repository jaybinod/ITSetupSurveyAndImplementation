using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Default : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
    SqlCommand cmd = new SqlCommand();
    //DataClass DB = new DataClass();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void LoginButton_Click(object sender, EventArgs e)
    {

        if (isauthenticated())
        {
            lblError.Text = "";
            //if (Session["userType"].ToString()=="Super")
            //Response.Redirect("dashboard.aspx");
            //else
            //{
                Response.Redirect(Session["HomePage"].ToString());
            //}
        }
        else
        {
            lblError.Text = "User Name " + txtUserName.Text.Trim() + " not valid. The login Action was not sucessful";
        }
    }


    public bool isauthenticated()
    {
        bool chk = false;
        try
        {
            //con = DB.DynamicConnection("MOSPlanner");
            
            string selectuser = "select EM.*, DM.HomePage, DM.MasterPage from [dbo].[EmployeeuserMaster] EM, DepartmentMaster DM where EM.DepartmentID=DM.DepartmentID and EM.username=@username and EM.password=@password and EM.Active=1";
            SqlCommand cmdselectuser = new SqlCommand(selectuser, conn);
            cmdselectuser.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar)).Value = txtUserName.Text;
            cmdselectuser.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar)).Value = txtPassword.Text;
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            
            SqlDataReader dr = cmdselectuser.ExecuteReader();
            
            if (dr.Read())
            {
               
                    Session["username"] = txtUserName.Text;
                    Session["emailid"] = dr["emailid"].ToString().Trim();
                    Session["password"] = txtPassword.Text;
                    Session["userID"] = dr["userID"].ToString().Trim();
                    Session["HomePage"] = dr["HomePage"].ToString().Trim();
                    Session["MasterPage"] = dr["MasterPage"].ToString().Trim();
                    Session["Validate"] = true;
                    Session["userType"] = dr["userType"].ToString().Trim();
                    Session["EmployeeName"] = dr["EmployeeName"].ToString().Trim();
                    Session["DepartmentID"] = dr["DepartmentID"];
                    chk = true;
                
            }
            

            dr.Close();
            conn.Close();

        }
        catch(SqlException ex)
        {
            chk= false;
        }
        return chk;
    }
}