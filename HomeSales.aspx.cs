using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class HomeSales : System.Web.UI.Page
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
            query = "Select *, (Select EmployeeName from EmployeeUserMaster where UserID=CSM.SurveyEngineerID) SurveyEngineer, EUM.EmployeeName from ClientMaster CM, ClientServiceMaster CSM, EmployeeUserMaster EUM, SurveyClientItemHead SCIH where CM.ClientID=CSM.ClientID and CM.ClientID=SCIH.ClientID and CSM.EmployeeID=EUM.UserID and SCIH.projectStatus=0";
        }
        else
        {
            query = "Select *, (Select EmployeeName from EmployeeUserMaster where UserID=CSM.SurveyEngineerID) SurveyEngineer, EUM.EmployeeName from ClientMaster CM, ClientServiceMaster CSM, EmployeeUserMaster EUM, SurveyClientItemHead SCIH where CM.ClientID=CSM.ClientID and CM.ClientID=SCIH.ClientID and CSM.EmployeeID=EUM.UserID and CSM.EmployeeID='" + Session["userID"] + "' and SCIH.projectStatus=0";
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

    protected string CustomizeMessage(object ob, string msg)
    {
        string typ = ob.ToString(); //selected value stored in ob
        if (typ.Length <= 0)
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
        while (dr.Read())
        {
            if (cnt > 0)
                typ = "<br/>" + dr["EmployeeName"].ToString().Trim();
            else
                typ = dr["EmployeeName"].ToString().Trim();
        }
        dr.Close();
        conn.Close();

        return typ; //value return to <%# ChandanIdiot( Eval("product_unitprice"))%>
    }

}