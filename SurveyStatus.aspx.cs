using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class SurveyStatus : System.Web.UI.Page
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
                    GetClientList();
                    BindDetailsView("");
                    
                }
            }
            catch
            {
                Response.Redirect("default.aspx");
            }
        }
    }
   

    protected void GetClientList()
    {
        SqlDataAdapter adpt = new SqlDataAdapter("Select * from ClientMaster order by clientName", conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        DataTable dtc = new DataTable();
        adpt.Fill(dtc);
        listClient.DataSource = dtc;
        listClient.DataBind();
    }

    protected void  BindDetailsView(string clientID)
    {
        string query = "";
        
            if(clientID.Trim().Length>0)
                query = "Select * , EUM.EmployeeName from ClientMaster CM, EmployeeUserMaster EUM, ClientServiceMaster CSM where CM.ClientID=CSM.ClientID and CSM.EmployeeID=EUM.UserID and CM.clientID='"+clientID+"'";
            else
                query = "Select * , EUM.EmployeeName from ClientMaster CM, EmployeeUserMaster EUM, ClientServiceMaster CSM where CM.ClientID=CSM.ClientID and CSM.EmployeeID=EUM.UserID";
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

    public static string ReformatDate(string valueFromDatabase)
    {
        DateTime dtvalue ;
        dtvalue = DateTime.Parse(valueFromDatabase);
        try
        {
            return dtvalue.ToString("dd-MMM-yyyy");
        }
        catch{
            return string.Empty;
        }

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
    //protected void gvClient_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    if (e.CommandName == "SubmitForSurvey")
    //    {
    //        //Determine the RowIndex of the Row whose Button was clicked.
    //        int rowIndex = Convert.ToInt32(e.CommandArgument);

    //        //Get the value of column from the DataKeys using the RowIndex.
    //        string id = gvClient.DataKeys[rowIndex].Value.ToString();

    //        SqlCommand cmd = new SqlCommand("Insert into SurveyClientItemHead (ClientID,submittedforsurvey) Values(@ClientID,1)", conn);
    //        if (conn.State == ConnectionState.Closed)
    //            conn.Open();
    //        cmd.Parameters.Add(new SqlParameter("@ClientID", SqlDbType.NVarChar, id.Trim().Length)).Value = id.Trim();
    //        cmd.ExecuteNonQuery();
    //        conn.Close();
    //    }

    //}

    //protected void gvClient_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        DataRowView rowView = (DataRowView)e.Row.DataItem;
    //        string myClientID = gvClient.DataKeys[e.Row.RowIndex].Value.ToString();
    //        Label mylblSurveyID = e.Row.FindControl("lblSurveyID") as Label;
    //        Label mylblSubmittedDate = e.Row.FindControl("lblSubmittedDate") as Label;
    //        Label mylblstartDate = e.Row.FindControl("lblStartDate") as Label;
    //        Label mylblFinishDate = e.Row.FindControl("lblFinishDate") as Label;
    //        Repeater myRepContact = e.Row.FindControl("rpContact") as Repeater;
    //        SqlCommand cmd = new SqlCommand("Select top 1 * from SurveyClientItemHead where clientID='" + myClientID+"' order by SurveyID Desc", conn);
    //        if (conn.State == ConnectionState.Closed)
    //            conn.Open();
    //        SqlDataReader drs = cmd.ExecuteReader();
    //        //bool fnd = false;
    //        if (drs.Read())
    //        {
    //            //fnd = true;
    //            e.Row.BackColor = System.Drawing.Color.Aqua;
    //            try
    //            {
    //                mylblSurveyID.Text = drs["SurveyID"].ToString().Trim();
    //                mylblSubmittedDate.Text = DateTime.Parse(drs["SubmittedDate"].ToString()).ToString("dd-MMM-yyyy");
    //                mylblstartDate.Text = "Survey Started on: " + DateTime.Parse(drs["StartDate"].ToString()).ToString("dd-MMM-yyyy");
    //                mylblFinishDate.Text = "Survey Finished on: " + DateTime.Parse(drs["FinishDate"].ToString()).ToString("dd-MMM-yyyy");
    //            }
    //            catch { }
    //        }
    //        conn.Close();

    //        SqlDataAdapter adptcontact = new SqlDataAdapter("Select * from tbl_orgKeyPeople where ClientID='" + myClientID + "'", conn);
    //        DataTable rpcontact = new DataTable();
    //        adptcontact.Fill(rpcontact);
    //        myRepContact.DataSource=rpcontact;
    //        myRepContact.DataBind();
    //        //btn.Attributes.Add("Click", "javascript:return " +
    //        //"confirm('Are you sure you want to Submit for Survey " +
    //        //DataBinder.Eval(e.Row.DataItem, "ClientID") + "')");
    //        foreach (Button button in e.Row.Cells[4].Controls.OfType<Button>())
    //        {
    //            if (button.CommandName == "Delete")
    //            {
    //                button.Attributes["onclick"] = "if(!confirm('Are you sure want to submit for Survey ?')){ return false; };";
    //            }
    //            //if(fnd==true)
    //            //{
    //            //    button.Enabled = false;
    //            //}
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

    //    SqlCommand cmd = new SqlCommand("Select count(*) from SurveyClientItemHead where clientID='" + id + "' and projectstatus=0", conn);
    //    if (conn.State == ConnectionState.Closed)
    //        conn.Open();
    //    int chk=(int)cmd.ExecuteScalar();
    //    conn.Close();
    //    if(chk>0)
    //    {
    //        Response.Redirect("SubmitSurveyConfirmation.aspx?cid=" + id);
    //    }
    //    else
    //    {
    //        SqlCommand cmdi = new SqlCommand("Insert into SurveyClientItemHead (ClientID,submittedforsurvey) Values(@ClientID,1)", conn);
    //        if (conn.State == ConnectionState.Closed)
    //            conn.Open();
    //        cmdi.Parameters.Add(new SqlParameter("@ClientID", SqlDbType.NVarChar, id.Trim().Length)).Value = id.Trim();
    //        cmdi.ExecuteNonQuery();
    //        conn.Close();

    //        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Client Submited for Survey Sucessfully')", true);
    //    }
    //}

    protected void gvClient_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            string clientID = (e.Item.FindControl("hfClientID") as HiddenField).Value;
            Repeater rptContact = e.Item.FindControl("rpContact") as Repeater;
            SqlDataAdapter adptcontact = new SqlDataAdapter("Select * from tbl_orgKeyPeople where ClientID='" + clientID + "'", conn);
            DataTable rpcontact = new DataTable();
            adptcontact.Fill(rpcontact);
            rptContact.DataSource = rpcontact;
            rptContact.DataBind();

            Repeater rptSurvey = e.Item.FindControl("rpSurvey") as Repeater;
            SqlDataAdapter adptSurvey = new SqlDataAdapter("Select SCIH.*, (Select EmployeeName from EmployeeUserMaster where userID=SCIH.SurveyEngineerID) SurveyEngineer from SurveyClientItemHead SCIH where ClientID='" + clientID + "' order by SurveyID Desc", conn);
            DataTable rptSurveyt = new DataTable();
            adptSurvey.Fill(rptSurveyt);
            rptSurvey.DataSource = rptSurveyt;
            rptSurvey.DataBind();
            //rptOrders.DataSource = GetData(string.Format("SELECT TOP 3 * FROM Orders WHERE CustomerId='{0}'", customerId));
            //rptOrders.DataBind();
        }
    }

    //protected void rpSurvey_ItemDataBound(object sender, RepeaterItemEventArgs e)
    //{
    //    if (e.Item.ItemType == ListItemType.Item ||
    //          e.Item.ItemType == ListItemType.AlternatingItem)
    //    {
    //        string clientID = (e.Item.FindControl("hfClientID") as HiddenField).Value;
    //        Repeater tempRpt =
    //               (Repeater)e.Item.FindControl("PropertyResults");
    //        if (tempRpt != null)
    //        {
    //            tempRpt.DataSource =
    //              ((DataRowView)e.Item.DataItem).CreateChildView("PropertiesInArea");
    //            tempRpt.DataBind();
    //        }
    //    }
    //}

    protected string CustomizeMessage(object ob, string msg)
    {
        string typ = ob.ToString(); //selected value stored in ob
        if (typ.Length<=0)
        {
            if (msg == "SurveyAssign")
                typ = "Not yet assignned survey Engineer";
            else
                typ = "Pending";
        }

        return typ; //value return to <%# ChandanIdiot( Eval("product_unitprice"))%>
    }

    protected string CustomizeMessageEngineer(object sid)
    {
        int cnt = 0;
        string typ = "Not yet Assigned"; //selected value stored in ob
        SqlCommand cmd = new SqlCommand("Select PIP.*, EUM.EmployeeName from ProjectImplementationPeople PIP, EmployeeUserMaster EUM where PIP.UserID=EUM.UserID and PIP.SurveyID=" + sid, conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        while(dr.Read())
        {
            if(cnt>0)
            typ = "<br/>"+dr["EmployeeName"].ToString().Trim() ;
            else
            typ = dr["EmployeeName"].ToString().Trim();
        }
        dr.Close();
        conn.Close();

        return typ; //value return to <%# ChandanIdiot( Eval("product_unitprice"))%>
    }

    protected void listClient_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(listClient.SelectedItem.ToString()!="(Select Client Name)")
        BindDetailsView(listClient.SelectedValue);
        else
            BindDetailsView("");
    }
}