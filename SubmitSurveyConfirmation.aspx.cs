using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class SubmitSurveyConfirmation : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            try
            {
                if ((bool)Session["Validate"] == true)
                {
                    string cid = Request.QueryString["cid"].ToString();
                    SqlCommand cmd = new SqlCommand("Select * from SurveyClientItemHead where clientID='" + cid + "' and projectstatus=0", conn);
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    SqlDataReader drs = cmd.ExecuteReader();

                    if (drs.Read())
                    {
                        lblMessage.Text = "Previous Survey No " + drs["SurveyID"].ToString() + " Submitted on " + DateTime.Parse(drs["SubmittedDate"].ToString()).ToString("dd-MMM-yyyy") + " Already in Progress <br> Do you still want to create new Fresh survey, Please Confirm!";
                    }
                    conn.Close();
                }
            }
            catch
            {
                Response.Redirect("default.aspx");
            }
        }
    }

    //protected void gvClient_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        DataRowView rowView = (DataRowView)e.Row.DataItem;
    //        string mySurveyID = gvClient.DataKeys[e.Row.RowIndex].Value.ToString();
    //        Label mylblSurveyID = e.Row.FindControl("lblSurveyID") as Label;
    //        Label mylblSubmittedDate = e.Row.FindControl("lblSubmittedDate") as Label;
    //        Label mylblstartDate = e.Row.FindControl("lblStartDate") as Label;
    //        Label mylblFinishDate = e.Row.FindControl("lblFinishDate") as Label;

    //        //btn.Attributes.Add("Click", "javascript:return " +
    //        //"confirm('Are you sure you want to Submit for Survey " +
    //        //DataBinder.Eval(e.Row.DataItem, "ClientID") + "')");
    //        foreach (Button button in e.Row.Cells[4].Controls.OfType<Button>())
    //        {
    //            if (button.CommandName == "Delete")
    //            {
    //                button.Attributes["onclick"] = "if(!confirm('Are you sure want to submit for Survey ?')){ return false; };";
    //            }
    //            if(fnd==true)
    //            {
    //                button.Enabled = false;
    //            }
    //        }
            
    //    }

    //    //if (e.Row.RowType == DataControlRowType.DataRow)
    //    //{
    //    //    DataRowView rowView = (DataRowView)e.Row.DataItem;
    //    //    string mySurveyID = gvClient.DataKeys[e.Row.RowIndex].Value.ToString();
    //    //    Label mylblstartDate = e.Row.FindControl("lblStartDate") as Label ;
    //    //    Label mylblFinishDate = e.Row.FindControl("lblFinishDate") as Label;
    //    //    SqlCommand cmd = new SqlCommand("Select * from SurveyClientItemHead where SurveyID=" + mySurveyID, conn);
    //    //    if (conn.State == ConnectionState.Closed)
    //    //        conn.Open();
    //    //    SqlDataReader drs = cmd.ExecuteReader();
    //    //    if (drs.Read())
    //    //    {
    //    //        try
    //    //        {
    //    //            mylblstartDate.Text = "Survey Started on: " + DateTime.Parse(drs["StartDate"].ToString()).ToString("dd-MMM-yyyy");
    //    //            mylblFinishDate.Text = "Survey Finished on: " + DateTime.Parse(drs["FinishDate"].ToString()).ToString("dd-MMM-yyyy");
    //    //        }
    //    //        catch { }
    //    //    }
    //    //    conn.Close();

    //    //}
    //}
    //protected void gvClient_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    //Determine the RowIndex of the Row whose Button was clicked.
    //    //int rowIndex = Convert.ToInt32(e.CommandArgument);

    //    //Get the value of column from the DataKeys using the RowIndex.
    //    string id = gvClient.DataKeys[e.RowIndex].Value.ToString();
        
    //    SqlCommand cmd = new SqlCommand("Insert into SurveyClientItemHead (ClientID,submittedforsurvey) Values(@ClientID,1)", conn);
    //    if (conn.State == ConnectionState.Closed)
    //        conn.Open();
    //    cmd.Parameters.Add(new SqlParameter("@ClientID", SqlDbType.NVarChar, id.Trim().Length)).Value = id.Trim();
    //    cmd.ExecuteNonQuery();
    //    conn.Close();

    //    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Client Submited for Survey Sucessfully')", true);
    //}
    protected void btnYes_Click(object sender, EventArgs e)
    {
        string cid = Request.QueryString["cid"].ToString();
        SqlCommand cmdi = new SqlCommand("Insert into SurveyClientItemHead (ClientID,submittedforsurvey) Values(@ClientID,1)", conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        cmdi.Parameters.Add(new SqlParameter("@ClientID", SqlDbType.NVarChar, cid.Trim().Length)).Value = cid.Trim();
        cmdi.ExecuteNonQuery();
        conn.Close();

        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Client Submited for Survey Sucessfully')", true);
        Response.Redirect("ClientList.aspx");
    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        Response.Redirect("ClientList.aspx");
    }
}