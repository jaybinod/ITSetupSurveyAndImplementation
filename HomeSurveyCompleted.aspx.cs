using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class HomeSurveyCompleted : System.Web.UI.Page
{

    SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
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
    }

    protected void  BindDetailsView()
    {
        string query = "";
        if (Session["userType"].ToString().Trim() == "Super")
        {
            query = "Select *, (Select EmployeeName from EmployeeUserMaster where UserID=SCIH.SurveyEngineerID) SurveyEngineer, EUM.EmployeeName SalesPerson from ClientMaster CM, SurveyClientItemHead SCIH, EmployeeUserMaster EUM, ClientServiceMaster CSM where CM.ClientID=SCIH.ClientID and SCIH.ClientID=CSM.ClientID and CSM.EmployeeID=EUM.UserID and SCIH.SubmittedForQuotation=1 order by SurveyID";
            //query = "Select *, (Select EmployeeName from EmployeeUserMaster where UserID=CSM.EmployeeID) SalesPerson from ClientMaster CM, ClientServiceMaster CSM where CM.ClientID=CSM.ClientID and CSM.SurveyEngineerID='" + Session["userID"] + "'"
        }
        else
        {
            query = "Select *, (Select EmployeeName from EmployeeUserMaster where UserID=SCIH.SurveyEngineerID) SurveyEngineer, EUM.EmployeeName SalesPerson from ClientMaster CM, SurveyClientItemHead SCIH, EmployeeUserMaster EUM, ClientServiceMaster CSM where CM.ClientID=SCIH.ClientID and SCIH.ClientID=CSM.ClientID and CSM.EmployeeID=EUM.UserID and SCIH.SubmittedForQuotation=1 and SCIH.SurveyEngineerID='" + Session["userID"] + "' Order By SurveyID";
        }

        SqlDataAdapter cmd = new SqlDataAdapter(query, conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        DataTable dt = new DataTable();
        cmd.Fill(dt);
        gvClient.DataSource = dt;
        gvClient.DataBind();
        //lblCompany.Text = dt.Rows[0]["Company"].ToString();
        //lblGST.Text = dt.Rows[0]["GSTNo"].ToString();
        //lblAddress.Text = dt.Rows[0]["Address"].ToString();
        //lblCity.Text = dt.Rows[0]["City"].ToString();
        //lblPincode.Text = dt.Rows[0]["Pincode"].ToString();
        //lblContact.Text = dt.Rows[0]["Contact"].ToString();
        //lblMobile.Text = dt.Rows[0]["Mobile"].ToString();
        //lblEmail.Text = dt.Rows[0]["Email"].ToString();
        //lblAddNewDealerAlert.Text = dt.Rows[0]["NewDealerAdded"].ToString();
        //lblDealerModificationAlert.Text = dt.Rows[0]["ExistingDealerModification"].ToString();
    }

    //protected void btnEdit_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("updatesupplier.aspx");
    //}
    //protected void gvEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow &&
    //    (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
    //    {
    //        // Here you will get the Control you need like:
    //        DropDownList dl = (DropDownList)e.Row.FindControl("ddlDepartment");
    //        SqlCommand cmd = new SqlCommand("Select * from DepartmentMaster order by Department", conn);
    //        if (conn.State == ConnectionState.Closed)
    //            conn.Open();
    //        dl.DataSource = cmd.ExecuteReader();
    //        dl.DataBind();
    //        conn.Close();
    //    }
    //}
    //protected void gvEmployee_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    gvEmployee.EditIndex = e.NewEditIndex;
    //    gvEmployee.DataBind();
    //}
    protected void gvClient_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                string mySurveyID = gvClient.DataKeys[e.Row.RowIndex].Value.ToString();
                HyperLink myhlsurvey = e.Row.FindControl("hlsurvey") as HyperLink;
                Label mylblStartedOn = e.Row.FindControl("lblStartedOn") as Label;
                SqlCommand cmd = new SqlCommand("Select * from SurveyClientItemHead where SurveyID=" + mySurveyID, conn);
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlDataReader drs = cmd.ExecuteReader();
                if (drs.Read())
                {
                    try
                    {
                        myhlsurvey.Text = "Survey ID: "+drs["SurveyID"]+" Submitted on: " + DateTime.Parse(drs["SubmittedDate"].ToString()).ToString("dd-MMM-yyyy");
                        mylblStartedOn.Text = "Survey Started on: " + DateTime.Parse(drs["StartDate"].ToString()).ToString("dd-MMM-yyyy");
                    }
                    catch { }
                }
                conn.Close();

            }
        }
    }
    //protected void rpopen_ItemDataBound(object sender, RepeaterItemEventArgs e)
    //{
    //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
    //    {
    //        //Reference the Repeater Item.
    //        RepeaterItem item = e.Item;
           
    //        //Reference the Controls.
    //        string surveyID = (item.FindControl("lblSurveyID") as Label).Text;
    //        SqlCommand cmdf = new SqlCommand("Select * from SurveyClientItemHead where SurveyID=" + surveyID, conn);
    //        if (conn.State == ConnectionState.Closed)
    //            conn.Open();
    //        SqlDataReader dr = cmdf.ExecuteReader();
    //        if(dr.Read())
    //        {
    //            (item.FindControl("Panel1") as Panel).GroupingText = "Survey No.: " + dr["SurveyID"] + " Started on " + DateTime.Parse(dr["SubmittedDate"].ToString()).ToString("dd-MMM-yyyy");
    //            bool sfq = bool.Parse(dr["Submittedforquotation"].ToString());
    //            if (sfq == true)
    //            {
    //                (item.FindControl("lblSubmittedForQuotation") as Label).Text = "Submtted for Quotation on " + DateTime.Parse(dr["FinishDate"].ToString()).ToString("dd-MMM-yyyy");
    //                //(item.FindControl("HyperLink2") as HyperLink).Text = "View Survey Details " + dr["SurveyID"] + " Started on " + DateTime.Parse(dr["StartDate"].ToString()).ToString("dd-MMM-yyyy");
    //                (item.FindControl("HyperLink2") as HyperLink).Text = "Click Here to View Survey Details ";
    //            }
    //            else
    //            {
    //                (item.FindControl("lblSubmittedForQuotation") as Label).Text = "Survey in Progress";
    //                (item.FindControl("HyperLink2") as HyperLink).Text = "Click Here to view, Edit and Add New item";
    //            }
    //            if (sfq == true)
    //            {
    //                bool sfd = bool.Parse(dr["QuotationDone"].ToString());
    //                if (sfd == true)
    //                    (item.FindControl("lblQuotationDate") as Label).Text = "Quotation Submitted to Client on " + DateTime.Parse(dr["QuotationSubmittedDate"].ToString()).ToString("dd-MMM-yyyy");
    //                else
    //                    (item.FindControl("lblQuotationDate") as Label).Text = "Quotation Preparation stage. Please check and complete";
    //                if(sfd==true)
    //                {
    //                    bool ps = bool.Parse(dr["ProjectStatus"].ToString());
    //                    if (ps == true)
    //                        (item.FindControl("lblProjectStatus") as Label).Text = "Project Implementation Finished Date " + DateTime.Parse(dr["ProjectFinishDate"].ToString()).ToString("dd-MMM-yyyy");
    //                    else
    //                        (item.FindControl("lblProjectStatus") as Label).Text = "Project Implementation in Progress";
    //                }
    //            }
    //        }
    //        dr.Close();
    //        //string name = (item.FindControl("lblName") as Label).Text;
    //        //string country = (item.FindControl("lblCountry") as Label).Text;
    //    }
    //}
}
