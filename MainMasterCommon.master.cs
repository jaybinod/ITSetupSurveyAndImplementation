using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class MainMasterCommon : System.Web.UI.MasterPage
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);

    protected void Page_Load(object sender, EventArgs e)
    {
        SqlDataAdapter cmd = new SqlDataAdapter("Select UM.* from userMenu UM, UserAccessMenu UAM where UM.menuID=UAM.menuID and UAM.DepartmentID="+Session["DepartmentID"]+" Order by sequence", conn);
        DataTable menudt = new DataTable();
        cmd.Fill(menudt);
        //for(int i=0;i<menudt.Rows.Count;i++)
        //{
        //    //if(int.Parse(menudt.Rows[i]["DepartmentID"].ToString())!=int.Parse(Session["DepartmentID"].ToString()))
        //    //{
        //    //    menudt.Rows[i]["hyperlink"] = "#";
        //    //}
        //}
        rpMenu.DataSource = menudt;
        rpMenu.DataBind();

        //if(conn.State == ConnectionState.Closed)
            
        //idcost..Visible = false;
        // Session["Admin"] = null;
         
        //Control cntrl = HeadLoginView.FindControl("HeadLoginName");
        ////LinkButton lnkHeadLoginStatus = HeadLoginView.FindControl("lnkHeadLoginStatus") as  LinkButton;
        //HyperLink hyprHeadLoginStatus = HeadLoginView.FindControl("hyprHeadLoginStatus") as HyperLink;
        //HtmlAnchor MyAnchor = HeadLoginView.FindControl("MyAnchor") as HtmlAnchor;
        ////
        //if (Session["UserFullName"] != null)
        //{
        //    //Control view2 = HeadLoginView.LoggedInTemplate.InstantiateIn(view1);
        //   // messageLabel2.Text = Convert.ToString(Session["UserFullName"]);
        //    hyprHeadLoginStatus.Text = "Log Out";
        //    MyAnchor.InnerText = Convert.ToString(Session["UserFullName"]);

        //}
        //else
        //{
        //    MyAnchor.InnerText = "";
        //    hyprHeadLoginStatus.Text = "Log In";

        //}
    }
}
