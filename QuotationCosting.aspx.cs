using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.Net;

public partial class QuotationCosting : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
    Romasoft RSClass = new Romasoft();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            try
            {
                ViewState["RefUrl"] = Request.UrlReferrer.ToString();
                if ((bool)Session["Validate"] == true)
                {
                    string cid = Request.QueryString["id"].ToString();
                    Session["cid"] = cid;
                    try
                    {
                        string sid = Request.QueryString["sid"].ToString();
                        ViewState["SurveyID"] = int.Parse(sid);
                        //btnFinishSurvey.Enabled = false;
                    }
                    catch
                    {
                        Response.Redirect("HomeSurvey.aspx");
                    }

                    //int sur = int.Parse(ViewState["SurveyID"].ToString());
                    //Session["sur"] = sur;
                    CurrencyMaster();
                    //ddlEmployee.SelectedValue = "";
                    CallClientData(cid);
                    
                }
            }
            catch
            {
                Response.Redirect("default.aspx");
            }
        }
    }

    protected void CurrencyMaster()
    {
        SqlDataAdapter adpt = new SqlDataAdapter("Select * from CurrencyMaster", conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        DataTable dtc = new DataTable();
        adpt.Fill(dtc);
        ddlCurrency.DataTextField = "Currency";
        ddlCurrency.DataValueField = "CurrencyID";
        ddlCurrency.DataSource = dtc;
        ddlCurrency.DataBind();
        ddlCurrency.SelectedItem.Text = "INR";
        txtValue.Text = "1.00";

        SqlCommand cmdcur = new SqlCommand("Select * from QuotationHeader where surveyID=" + ViewState["SurveyID"], conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        SqlDataReader drc = cmdcur.ExecuteReader();
        if(drc.Read())
        {
            ddlCurrency.SelectedValue = drc["CurrencyID"].ToString();
            if(drc["CurrencyValue"]!=DBNull.Value)
            txtValue.Text = double.Parse(drc["CurrencyValue"].ToString()).ToString();
            txtPaymentTerm.Text = drc["PaymentTerm"].ToString().Trim();
        }
        drc.Close();
        CallTempData(Session["cid"].ToString(), int.Parse(ViewState["SurveyID"].ToString()), double.Parse(txtValue.Text));
    }

    protected void CallTempData(string ClientID, int SurveyID, double CurrencyValue)
    {
        string query = "";

        query = "Select SCID.*, IM.ItemName, IM.Unit, '' Currency, CurrencyUnit, CurrencyTotal, IGM.GroupName, SCIH.quotationdone, (Select SupplierName from SupplierMaster where SupplierID=SCID.SupplierID) SupplierName from SurveyClientItemDetails SCID, ItemMaster IM, ItemGroupMaster IGM, SurveyClientItemHead SCIH where SCID.SurveyID=SCIH.surveyID and IM.GroupID=IGM.GroupID and SCID.ItemID=IM.ItemID and SCIH.SurveyID=" + SurveyID;

        SqlCommand cmd = new SqlCommand(query, conn);
        //cmd.Parameters.Add(new SqlParameter("@ClientID", SqlDbType.NVarChar, ClientID.Trim().Length)).Value = ClientID.Trim();
        //cmd.Parameters.Add(new SqlParameter("@SurveyEngineerID", SqlDbType.NVarChar, Session["userID"].ToString().Trim().Length)).Value = Session["userID"].ToString().Trim();
        SqlDataAdapter adpt = new SqlDataAdapter();
        adpt.SelectCommand = cmd;
        DataTable dt = new DataTable();
        DataColumn c = new DataColumn("sno", typeof(int));
        c.AutoIncrement = true;
        c.AutoIncrementSeed = 1;
        c.AutoIncrementStep = 1;
        dt.Columns.Add(c);
        adpt.Fill(dt);
        bool finishsurvey = true;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            double total = double.Parse(dt.Rows[i]["QuotedPriceTotal"].ToString());
            double qty = double.Parse(dt.Rows[i]["Quantity"].ToString());
            double CurrTotal = total / CurrencyValue;
            double unit = CurrTotal / qty;
            dt.Rows[i]["CurrencyUnit"] = unit;
            dt.Rows[i]["CurrencyTotal"] = CurrTotal;
            dt.Rows[i]["Currency"] = ddlCurrency.SelectedItem.ToString();
            if (total <= 0 || bool.Parse(dt.Rows[i]["Changestatus"].ToString())==true)
                finishsurvey = false;
        }
        if (SurveyID > 0)
        {
            if (dt.Rows.Count > 0)
            {
                //if ((bool)dt.Rows[dt.Rows.Count - 1]["quotationdone"] == true)
                //{
                //    gvitemdisplay.DataSource = dt;
                //    gvitemdisplay.DataBind();
                //    txtClientName.Enabled = false;
                //}
                //else
                //{
                    gvFeed.DataSource = dt;
                    gvFeed.DataBind();
                    txtClientName.Enabled = false;
                //}
            }
        }

        if (finishsurvey == true)
            btnFinishSurvey.Enabled = false;
        else
            btnFinishSurvey.Enabled = true;

        SqlCommand cmdQNo = new SqlCommand("Select * from QuotationHeader where SurveyID=" + SurveyID, conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        SqlDataReader drq = cmdQNo.ExecuteReader();
        bool quotationdone = false;
        if(drq.Read())
        {
            lblQuotationNumber.Text = drq["QuotationNo"].ToString();
            quotationdone = true;
        }
        drq.Close();
        if(quotationdone==false)
        { lblQuotationNumber.Text = "Quotation Number will generate once costing done for all Item and Click on Finish Quotation Button"; }
        //else
        //{
        //    gvFeed.DataSource = dt;
        //    gvFeed.DataBind();
        //    txtClientNamze.Enabled = true;
        //    txtQuantity.Enabled = true;
        //    txtRemark.Enabled = true;
        //    btnSubmit.Enabled = true;

        //    ddlItem.Enabled = true;

        //}
        //if (conn.State == ConnectionState.Closed)
        //    conn.Open();

        //conn.Close();
    }

    protected void CallClientData(string ClientID)
    {
        string query = "Select CM.*, SCIH.SurveyID, SCIH.StartDate from SurveyClientItemHead SCIH, ClientMaster CM, ClientServiceMaster CSM where SCIH.ClientID=CM.ClientID and CM.ClientID=CSM.ClientID and CM.ClientID='" + ClientID + "' and SCIH.SurveyID=" + ViewState["SurveyID"];

        SqlCommand cmd = new SqlCommand(query, conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            txtClientName.Text = "Site Survey No." + dr["SurveyID"].ToString().Trim() + " - Start Date: " + DateTime.Parse(dr["StartDate"].ToString().Trim()).ToString("dd-MMM-yyyy") + " for : " + dr["ClientName"].ToString().Trim();
            //ddlEmployee.SelectedValue = dr["SurveyEngineerID"].ToString().Trim();
        }
        conn.Close();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        object refUrl = ViewState["RefUrl"];
        if (refUrl != null)
            Response.Redirect((string)refUrl);
    }
    
   
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(ViewState["RefUrl"].ToString());
    }
   
    protected void gvFeed_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ID = Convert.ToInt32(gvFeed.DataKeys[e.RowIndex].Value);
        SqlCommand cmd = new SqlCommand("Delete from SurveyClientItemDetails where ID=" + ID, conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        cmd.ExecuteNonQuery();
        CallTempData(Session["cid"].ToString(), int.Parse(ViewState["SurveyID"].ToString()), double.Parse(txtValue.Text));
    }

    protected void btnFinishSurvey_Click(object sender, EventArgs e)
    {
        int surveyID = int.Parse(ViewState["SurveyID"].ToString());
        //string startDate = "", finishDate = "";
        //SqlCommand cmdfind = new SqlCommand("Select min(checkedDate) StartDate, max(CheckedDate) FinishDate from SurveyClientItemDetails where SurveyID=0 and clientID=@clientID and SurveyEngineerID=@SurveyEngineerID", conn);
        //cmdfind.Parameters.Add(new SqlParameter("@ClientID", SqlDbType.NVarChar, Session["cid"].ToString().Trim().Length)).Value = Session["cid"].ToString().Trim();
        //cmdfind.Parameters.Add(new SqlParameter("@SurveyEngineerID", SqlDbType.NVarChar, Session["userID"].ToString().Trim().Length)).Value = Session["userID"].ToString().Trim();
        //if (conn.State == ConnectionState.Closed)
        //    conn.Open();
        //SqlDataReader dr = cmdfind.ExecuteReader();
        //if (dr.Read())
        //{
        //    startDate = DateTime.Parse(dr["StartDate"].ToString()).ToString("yyyy-MMM-dd");
        //    finishDate = DateTime.Parse(dr["FinishDate"].ToString()).ToString("yyyy-MMM-dd");
        //}
        //dr.Close();

        
        SqlCommand cmd = new SqlCommand("Update SurveyClientItemHead set QuotationSubmittedDate=@QuotationSubmittedDate, QuotationDone=1 where SurveyID=" + surveyID, conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        cmd.Parameters.Add(new SqlParameter("@QuotationSubmittedDate", SqlDbType.DateTime)).Value = DateTime.Now;
        cmd.ExecuteNonQuery();
        conn.Close();
        lblMessage.Text = "Quotation Submitted successfully";
        btnFinishSurvey.Enabled = false;


        SqlCommand cmdQNo = new SqlCommand("Select * from QuotationHeader where SurveyID=" + surveyID, conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        SqlDataReader drq = cmdQNo.ExecuteReader();
        bool quotationdone = false;
        int QuotationID=0;
        if (drq.Read())
        {
            lblQuotationNumber.Text = drq["QuotationNo"].ToString();
            QuotationID=int.Parse(drq["QuotationID"].ToString());
            quotationdone = true;
        }
        drq.Close();
        if (quotationdone == false)
        { 
            SqlCommand last = new SqlCommand("Select top 1 * from QuotationHeader order by QuotationID desc",conn);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            SqlDataReader drlast = last.ExecuteReader();
            string lastnumber = "00001";
            if(drlast.Read())
            {
                lastnumber = RSClass.IncreaseNext(drlast["QuotationNo"].ToString());
            }
            drlast.Close();
            lblQuotationNumber.Text = lastnumber;
            SqlCommand cmdi = new SqlCommand("Insert into QuotationHeader (QuotationNo, QuotationDate, SurveyID) values(@QuotationNo, @QuotationDate, @SurveyID)", conn);
            cmdi.Parameters.Add(new SqlParameter("@QuotationNo", SqlDbType.NVarChar)).Value = lastnumber;
            cmdi.Parameters.Add(new SqlParameter("@QuotationDate", SqlDbType.DateTime)).Value = DateTime.Now.ToShortDateString();
            cmdi.Parameters.Add(new SqlParameter("@SurveyID", SqlDbType.Int)).Value = surveyID;
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            cmdi.ExecuteNonQuery();

            }

        SqlCommand cmditemupd = new SqlCommand("Update SurveyClientItemDetails set CurrencyTotal=QuotedPriceTotal/" + txtValue.Text + " Where SurveyID=" + ViewState["SurveyID"], conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();

        cmditemupd.ExecuteNonQuery();
        conn.Close();

        SqlCommand cmditemupdn = new SqlCommand("Update SurveyClientItemDetails set CurrencyUnit=CurrencyTotal/Quantity Where SurveyID=" + ViewState["SurveyID"], conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();

        SqlCommand cmdQNov = new SqlCommand("Select * from QuotationHeader where SurveyID=" + surveyID, conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        SqlDataReader drqv = cmdQNov.ExecuteReader();
        if (drqv.Read())
        {
            QuotationID=int.Parse(drqv["QuotationID"].ToString());
        }
        drqv.Close();

           SqlCommand lastv = new SqlCommand("Select top 1 * from QuotationHeaderVersion where surveyID="+surveyID+" order by VersionID desc",conn);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            SqlDataReader drlastv = lastv.ExecuteReader();
            string Version = "V01";
            if(drlastv.Read())
            {
                Version = RSClass.IncreaseNext(drlastv["Version"].ToString());
            }
            drlastv.Close();
            //lblQuotationNumber.Text = lastnumber;
            SqlCommand cmdiv = new SqlCommand("Insert into QuotationHeaderVersion (SurveyID, Version) output inserted.VersionID values(@SurveyID, @Version)", conn);
            cmdiv.Parameters.Add(new SqlParameter("@SurveyID", SqlDbType.Int)).Value = surveyID;
        cmdiv.Parameters.Add(new SqlParameter("@Version", SqlDbType.NVarChar,Version.Trim().Length)).Value = Version;
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            int versionID =(int)cmdiv.ExecuteScalar();

        SqlDataAdapter adptv = new SqlDataAdapter("Select * from SurveyClientItemDetails where SurveyID=" + surveyID, conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        DataTable dt = new DataTable();
        adptv.Fill(dt);
        SqlCommand cmdvi;
        for (int i=0;i<dt.Rows.Count;i++)
        {
            cmdvi = new SqlCommand("INSERT INTO [dbo].[SurveyClientItemDetailsVersion]([SurveyID],[VersionID],[ItemID],[Quantity],[ClientID],[SurveyEngineerID],[Remark],[CheckedDate],[QuotedPriceTotal],[QuotedPriceUnit],[CurrencyTotal],[CurrencyUnit],[SupplierID],[SalesMargin],[PurchaseCost],[ChangeStatus]) VALUES(@SurveyID,@VersionID,@ItemID,@Quantity,@ClientID,@SurveyEngineerID,@Remark,@CheckedDate,@QuotedPriceTotal,@QuotedPriceUnit,@CurrencyTotal,@CurrencyUnit,@SupplierID,@SalesMargin,@PurchaseCost,@ChangeStatus)", conn);
            cmdvi.Parameters.Add(new SqlParameter("@SurveyID", dt.Rows[i]["SurveyID"]));
            cmdvi.Parameters.Add(new SqlParameter("@VersionID", versionID));
            cmdvi.Parameters.Add(new SqlParameter("@ItemID", dt.Rows[i]["ItemID"]));
            cmdvi.Parameters.Add(new SqlParameter("@Quantity", dt.Rows[i]["Quantity"]));
            cmdvi.Parameters.Add(new SqlParameter("@ClientID", dt.Rows[i]["ClientID"]));
            cmdvi.Parameters.Add(new SqlParameter("@SurveyEngineerID", dt.Rows[i]["SurveyEngineerID"]));
            cmdvi.Parameters.Add(new SqlParameter("@Remark", dt.Rows[i]["Remark"]));
            cmdvi.Parameters.Add(new SqlParameter("@CheckedDate", dt.Rows[i]["CheckedDate"]));
            cmdvi.Parameters.Add(new SqlParameter("@QuotedPriceTotal", dt.Rows[i]["QuotedPriceTotal"]));
            cmdvi.Parameters.Add(new SqlParameter("@QuotedPriceUnit", dt.Rows[i]["QuotedPriceUnit"]));
            cmdvi.Parameters.Add(new SqlParameter("@CurrencyTotal", dt.Rows[i]["CurrencyTotal"]));
            cmdvi.Parameters.Add(new SqlParameter("@CurrencyUnit", dt.Rows[i]["CurrencyUnit"]));
            cmdvi.Parameters.Add(new SqlParameter("@SupplierID", dt.Rows[i]["SupplierID"]));
            cmdvi.Parameters.Add(new SqlParameter("@SalesMargin", dt.Rows[i]["SalesMargin"]));
            cmdvi.Parameters.Add(new SqlParameter("@PurchaseCost", dt.Rows[i]["PurchaseCost"]));
            cmdvi.Parameters.Add(new SqlParameter("@ChangeStatus", dt.Rows[i]["ChangeStatus"]));
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            cmdvi.ExecuteNonQuery();
            cmdvi.Parameters.Clear();
        }

        SqlCommand cmdst = new SqlCommand("Update SurveyClientItemDetails set ChangeStatus=0 where SurveyID=" + surveyID, conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        cmdst.ExecuteNonQuery();
        conn.Close();

    }

    protected void gvFeed_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvFeed.EditIndex=e.NewEditIndex;
        CallTempData(Session["cid"].ToString(), int.Parse(Session["sur"].ToString()), double.Parse(txtValue.Text));
    }
    
    protected void gvFeed_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvFeed.EditIndex = -1;
        CallTempData(Session["cid"].ToString(), int.Parse(Session["sur"].ToString()), double.Parse(txtValue.Text));
    }
    
    protected void gvFeed_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int sitemid = Convert.ToInt32(gvFeed.DataKeys[e.RowIndex].Value.ToString());

        GridViewRow row = (GridViewRow)gvFeed.Rows[e.RowIndex];
        
        TextBox tcost = (TextBox)row.FindControl("txtTotalCost");
        Label lqty = (Label)row.FindControl("lblQuantity");
        double totalcost = 0, unitcost = 0;
        try
        {
            totalcost = double.Parse(tcost.Text);
            unitcost = totalcost / double.Parse(lqty.Text);
        }
        catch
        {
            lblMessage.Text = "Please Input correct Amount";
            return;
        }
        SqlCommand cmdu = new SqlCommand("Update SurveyClientItemDetails set QuotedPriceTotal=@QuotedPriceTotal, QuotedPriceUnit=@QuotedPriceUnit where ID=" + sitemid, conn);
        cmdu.Parameters.Add(new SqlParameter("@QuotedPriceTotal", SqlDbType.Float)).Value = totalcost;
        cmdu.Parameters.Add(new SqlParameter("@QuotedPriceUnit", SqlDbType.Float)).Value = unitcost;
        
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        cmdu.ExecuteNonQuery();
        gvFeed.EditIndex = -1;
        CallTempData(Session["cid"].ToString(), int.Parse(Session["sur"].ToString()), double.Parse(txtValue.Text));
        checkQuotationbalanceItem(int.Parse(ViewState["SurveyID"].ToString()));
    }
    
    protected void checkQuotationbalanceItem(int SurveyID)
    {
        SqlCommand cmd = new SqlCommand("Select Count(*) from SurveyClientItemDetails SCID, SurveyClientItemHead SCIH where SCID.SurveyID=SCIH.surveyID and SCID.QuotedPriceTotal=0 and SCIH.SurveyID=" + SurveyID, conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        int chk=(int)cmd.ExecuteScalar();
        if(chk<=0)
        btnFinishSurvey.Enabled = true;
        else
            btnFinishSurvey.Enabled = false;
    }
    protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlCommand cmdc = new SqlCommand("Select * from Currencymaster where CurrencyID=" + ddlCurrency.SelectedValue, conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        SqlDataReader drc = cmdc.ExecuteReader();
        if (drc.Read())
        {
            txtValue.Text = drc["LatestValue"].ToString();
        }
        drc.Close();
        CallTempData(Session["cid"].ToString(), int.Parse(ViewState["SurveyID"].ToString()), double.Parse(txtValue.Text));
        btnFinishSurvey.Enabled = true;
    }
    protected void btnApply_Click(object sender, EventArgs e)
    {
        string currency = ddlCurrency.SelectedItem.ToString().Trim();
        SqlCommand cmd = new SqlCommand("update CurrencyMaster set LatestValue=@LatestValue, LatestValueDate=@LatestValueDate where Currency=@Currency", conn);
        cmd.Parameters.Add(new SqlParameter("@LatestValue", SqlDbType.Float)).Value = double.Parse(txtValue.Text);
        cmd.Parameters.Add(new SqlParameter("@LatestValueDate", SqlDbType.DateTime)).Value = DateTime.Now.ToString("yyyy-MM-dd");
        cmd.Parameters.Add(new SqlParameter("@Currency", SqlDbType.NVarChar,currency.Length)).Value = currency;
        if (conn.State == ConnectionState.Closed)
            conn.Open();

        cmd.ExecuteNonQuery();

        SqlCommand cmdu = new SqlCommand("Update QuotationHeader set CurrencyID=" + ddlCurrency.SelectedValue + ", CurrencyValue="+txtValue.Text+",PaymentTerm=@PaymentTerm Where SurveyID=" + ViewState["SurveyID"], conn);
        cmdu.Parameters.Add(new SqlParameter("@PaymentTerm", SqlDbType.NVarChar)).Value = txtPaymentTerm.Text.Trim();
        if (conn.State == ConnectionState.Closed)
            conn.Open();

        cmdu.ExecuteNonQuery();
        conn.Close();

        SqlCommand cmduver = new SqlCommand("Update QuotationHeaderVersion set CurrencyID=" + ddlCurrency.SelectedValue + ", CurrencyValue=" + txtValue.Text + ",PaymentTerm=@PaymentTerm Where VersionID in (Select Top 1 VersionID from QuotationHeaderVersion where SurveyID=" + ViewState["SurveyID"]+" order by VersionID desc)", conn);
        cmduver.Parameters.Add(new SqlParameter("@PaymentTerm", SqlDbType.NVarChar)).Value = txtPaymentTerm.Text.Trim();
        if (conn.State == ConnectionState.Closed)
            conn.Open();

        cmduver.ExecuteNonQuery();
        conn.Close();

        SqlCommand cmditemupd = new SqlCommand("Update SurveyClientItemDetails set CurrencyTotal=QuotedPriceTotal/" + txtValue.Text + " Where SurveyID=" + ViewState["SurveyID"], conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();

        cmditemupd.ExecuteNonQuery();
        conn.Close();

        SqlCommand cmditemupdn = new SqlCommand("Update SurveyClientItemDetails set CurrencyUnit=CurrencyTotal/Quantity Where SurveyID=" + ViewState["SurveyID"], conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();

        cmditemupdn.ExecuteNonQuery();
        conn.Close();

        Response.Redirect("QuotationView.aspx?sid=" + ViewState["SurveyID"]);
    }
    protected void txtValue_TextChanged(object sender, EventArgs e)
    {
        CallTempData(Session["cid"].ToString(), int.Parse(ViewState["SurveyID"].ToString()), double.Parse(txtValue.Text));
    }
}