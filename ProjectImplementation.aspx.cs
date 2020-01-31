using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class ProjectImplementation : System.Web.UI.Page
{

    SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);

    //protected void Page_PreInit(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (Session["userType"].ToString().Trim() == "Super")
    //        {
    //            this.MasterPageFile = "~/MainMasterPagea.master";
    //        }
    //        else
    //        {
    //            this.MasterPageFile = "~/"+Session["MasterPage"];
    //        }
    //    }
    //    catch
    //    {
    //        Response.Redirect("default.aspx");
    //    }
    //}
    protected void Page_Load(object sender, EventArgs e)
    {
        
        try
        {
            if ((bool)Session["Validate"] == true)
            {
                BindDetailsView();
            }
        }
        catch
        {
            Response.Redirect("default.aspx");
        }
    }

    protected void  BindDetailsView()
    {
        string query = "";
        if (Session["userType"].ToString().Trim() == "Super")
        {
            query = "Select distinct CM.*, EUM.EmployeeName from ClientMaster CM, EmployeeUserMaster EUM, ClientServiceMaster CSM, SurveyClientItemHead SCIH where CM.ClientID=SCIH.ClientID and CM.ClientID=CSM.ClientID and CSM.EmployeeID=EUM.UserID and SCIH.quotationdone=1 and SCIH.SubmittedForQuotation=1 and projectstatus=0";
        }
        else
        {
            query = "Select distinct CM.*, EUM.EmployeeName from ClientMaster CM, EmployeeUserMaster EUM, ClientServiceMaster CSM, SurveyClientItemHead SCIH, ProjectImplementationPeople PIP where CM.ClientID=SCIH.ClientID and CM.ClientID=CSM.ClientID and CSM.EmployeeID=EUM.UserID and SCIH.SurveyID=PIP.SurveyID and SCIH.quotationdone=1 and SCIH.SubmittedForQuotation=1 and PIP.UserID='" + Session["userID"] + "' and projectstatus=0";
            //query = "Select *, (Select EmployeeName from EmployeeUserMaster where UserID=CSM.SurveyEngineerID) SurveyEngineer, EUM.EmployeeName from ClientMaster CM, ClientServiceMaster CSM, EmployeeUserMaster EUM where CM.ClientID=CSM.ClientID and CSM.EmployeeID=EUM.UserID and CSM.EmployeeID='" + Session["userID"] + "'";
        }
        SqlDataAdapter cmd = new SqlDataAdapter(query, conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        DataTable dt = new DataTable();
        cmd.Fill(dt);
        gvClient.DataSource = dt;
        gvClient.DataBind();
    }

    protected void gvClient_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string clientid = gvClient.DataKeys[e.Row.RowIndex].Value.ToString();
            if (Session["userType"].ToString().Trim() == "Super")
            {
                SqlDataAdapter adpts = new SqlDataAdapter("Select * from SurveyClientItemHead SCIH, ProjectImplementationPeople PIP where SCIH.surveyID=PIP.SurveyID and SCIH.ClientID='" + clientid + "' and SCIH.quotationdone=1", conn);
                DataTable dtrr = new DataTable();
                adpts.Fill(dtrr);
                Repeater rp = (Repeater)e.Row.FindControl("rpopen");
                rp.DataSource = dtrr;
                rp.DataBind();
            }
            else
            {
                SqlDataAdapter adpts = new SqlDataAdapter("Select * from SurveyClientItemHead SCIH, ProjectImplementationPeople PIP where SCIH.surveyID=PIP.SurveyID and SCIH.ClientID='" + clientid + "' and SCIH.quotationdone=1 and PIP.UserID='" + Session["userID"] + "'", conn);
                DataTable dtrr = new DataTable();
                adpts.Fill(dtrr);
                Repeater rp = (Repeater)e.Row.FindControl("rpopen");
                rp.DataSource = dtrr;
                rp.DataBind();
            }
        }
    }
}
