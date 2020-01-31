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
using Microsoft.Reporting.WebForms;

public partial class QuotationView : System.Web.UI.Page
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
                    try
                    {
                        string sid = Request.QueryString["sid"].ToString();
                        Session["SurveyID"] = sid;
                        CallClientData(sid);
                        version();
                        CallTempData(int.Parse(Session["SurveyID"].ToString()), 1);
                        //btnFinishSurvey.Enabled = false;
                    }
                    catch
                    {
                        Response.Redirect(ViewState["RefUrl"].ToString());
                    }

                    //int sur = int.Parse(ViewState["SurveyID"].ToString());
                    //Session["sur"] = sur;
                    //CurrencyMaster();
                    //ddlEmployee.SelectedValue = "";
                }
            }
            catch
            {
                Response.Redirect("default.aspx");
            }
        }
    }

    protected void version()
    {
        //string constr = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;
       
            string query = "SELECT * from QuotationHeaderVersion where SurveyID="+Session["SurveyID"]+" Order by VersionID";
            SqlCommand cmd = new SqlCommand(query,conn);
                conn.Open();
                rblVersion.DataSource = cmd.ExecuteReader();
                rblVersion.DataTextField = "Version";
                rblVersion.DataValueField = "VersionID";
                rblVersion.DataBind();
                conn.Close();
                if (rblVersion.Items.Count <= 0)
                    UpdateVersion();
                else
                    rblVersion.Items[rblVersion.Items.Count - 1].Selected = true;
    }

    protected void UpdateVersion()
    {
        SqlCommand lastv = new SqlCommand("Select top 1 * from QuotationHeaderVersion where surveyID=" + Session["SurveyID"] + " order by VersionID desc", conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        SqlDataReader drlastv = lastv.ExecuteReader();
        string Version = "V01";
        if (drlastv.Read())
        {
            Version = RSClass.IncreaseNext(drlastv["Version"].ToString());
        }
        drlastv.Close();
        //lblQuotationNumber.Text = lastnumber;
        SqlCommand cmdiv = new SqlCommand("Insert into QuotationHeaderVersion (SurveyID, Version) output inserted.VersionID values(@SurveyID, @Version)", conn);
        cmdiv.Parameters.Add(new SqlParameter("@SurveyID", SqlDbType.Int)).Value = Session["SurveyID"];
        cmdiv.Parameters.Add(new SqlParameter("@Version", SqlDbType.NVarChar, Version.Trim().Length)).Value = Version;
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        int versionID = (int)cmdiv.ExecuteScalar();

        SqlDataAdapter adptv = new SqlDataAdapter("Select * from SurveyClientItemDetails where SurveyID=" + Session["SurveyID"], conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        DataTable dt = new DataTable();
        adptv.Fill(dt);
        SqlCommand cmdvi;
        for (int i = 0; i < dt.Rows.Count; i++)
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
    }

    protected void CallTempData(int SurveyID, int ReportNumber)
    {
        string query = "";
        DataSet ReportSet = new DataSet();
        query = "Select SCID.*, SCID.Remark Description, IM.ItemName, IM.Unit, IM.HSNCode, IM.CGST, IM.SGST, IM.IGST, 0 IGSTValue, QH.QuotationNo, QH.QuotationDate, QH.PaymentTerm, QH.CurrencyID, CM.Currency, CurrencyUnit, CurrencyTotal, IGM.GroupName, SCIH.quotationdone, SCIH.ClientID, CMM.ClientName, CMM.Address1, CMM.Address2, CMM.City, CMM.State, CMM.PostalCode, CMM.Country, CMM.GSTNo from SurveyClientItemDetailsVersion SCID, ItemMaster IM, ItemGroupMaster IGM, SurveyClientItemHead SCIH, QuotationHeader QH, QuotationHeaderVersion QHV, CurrencyMaster CM, ClientMaster CMM where SCID.SurveyID=SCIH.surveyID and SCIH.ClientID=CMM.ClientID and IM.GroupID=IGM.GroupID and SCID.ItemID=IM.ItemID and SCIH.SurveyID=QH.SurveyID and QH.CurrencyID=CM.CurrencyID and SCIH.SurveyID=QHV.SurveyID and SCID.versionid=QHV.versionid and SCIH.SurveyID=" + SurveyID + " and SCID.VersionID=QHV.VersionID and QHV.VersionID=" + rblVersion.SelectedValue;

        SqlDataAdapter adpt = new SqlDataAdapter(query, conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        adpt.Fill(ReportSet, "QuotationTable");
        if(ReportSet.Tables["QuotationTable"].Rows.Count<=0)
        {
            UpdateVersion();
            CallTempData(int.Parse(Session["SurveyID"].ToString()), 1);
        }

        double totalamt = 0;
        double amount = 0;
        string quotationNo = "";
        for (int i = 0; i < ReportSet.Tables["QuotationTable"].Rows.Count; i++)
        {
            quotationNo = ReportSet.Tables["QuotationTable"].Rows[i]["QuotationNo"].ToString();
            double total = double.Parse(ReportSet.Tables["QuotationTable"].Rows[i]["CurrencyTotal"].ToString());
            double gst = 0;
            if (ReportSet.Tables["QuotationTable"].Rows[i]["IGST"].ToString().Trim().Length > 0)
                gst = double.Parse(ReportSet.Tables["QuotationTable"].Rows[i]["IGST"].ToString());
            else
                gst = 0;
            //double qty = double.Parse(dt.Rows[i]["Quantity"].ToString());
            double gstTotal = (total *gst)/ 100;
            ReportSet.Tables["QuotationTable"].Rows[i]["IGSTValue"] = gstTotal;
            totalamt = totalamt + (total + gstTotal);
            amount = amount + total;
        }
        lblQuotationNumber.Text = quotationNo;
        string wordamount = "";
        rv.LocalReport.DataSources.Clear(); //clear report
        //string reportHeading = "";
        //if (rbActDetails.Checked == true)
        //{
        //string path = Server.MapPath("");
        if (ReportNumber == 1)
        {
            rv.LocalReport.ReportPath = Server.MapPath("QuotationReportDescription.rdlc"); // bind reportviewer with .rdlc
            wordamount = NumberToWords(Int32.Parse(Math.Round(totalamt, 0).ToString()));
        }
        if (ReportNumber == 2)
        {
            rv.LocalReport.ReportPath = Server.MapPath("QuotationReport.rdlc"); // bind reportviewer with .rdlc
            wordamount = NumberToWords(Int32.Parse(Math.Round(totalamt, 0).ToString()));
        }
        if (ReportNumber == 3)
        {
            rv.LocalReport.ReportPath = Server.MapPath("QuotationReportwithoutGSTWithDescription.rdlc"); // bind reportviewer with .rdlc
            wordamount = NumberToWords(Int32.Parse(Math.Round(amount, 0).ToString()));
        }
        if (ReportNumber == 4)
        {
            rv.LocalReport.ReportPath = Server.MapPath("QuotationReportwithoutGSTWithwithoutDescription.rdlc"); // bind reportviewer with .rdlc
            wordamount = NumberToWords(Int32.Parse(Math.Round(amount, 0).ToString()));
        }
        //    reportHeading = "Degtails Report - Account Wise Grey Challan Register from " + dtpFrom.Value.ToString("dd-MMM-yyyy") + " to " + dtpTo.Value.ToString("dd-MMM-yyyy");
        //}
        //if (rbActSummary.Checked == true)
        //{
        //    DataReportViewer.LocalReport.ReportEmbeddedResource = "WeaverStock.net.Reports.ChallanRegisterGreyActSummary.rdlc"; // bind reportviewer with .rdlc
        //    reportHeading = "Summary Report - Account Wise Grey Challan Register from " + dtpFrom.Value.ToString("dd-MMM-yyyy") + " to " + dtpTo.Value.ToString("dd-MMM-yyyy");
        //}
        Microsoft.Reporting.WebForms.ReportDataSource dataset = new Microsoft.Reporting.WebForms.ReportDataSource("QuotationTable", ReportSet.Tables["QuotationTable"]); // set the datasource
        rv.LocalReport.DataSources.Add(dataset);
        /// Get Firm Data to Print on Header
        /// 
        List<ReportParameter> paramList = new List<ReportParameter>();

        //conn = DC.FirmDataConnection();
        ////lstChallan.Items.Add("(All)");
        //conn.Close();
        //cmd = new SqlCommand("Select * from FirmMaster", conn);
        //if (conn.State == ConnectionState.Closed)
        //    conn.Open();
        //SqlDataReader drFirm;
        //drFirm = cmd.ExecuteReader();
        //paramList.Add(new ReportParameter("ReportHeading", reportHeading));
        //while (drFirm.Read())
        //{
        paramList.Add(new ReportParameter("CustomerName", "Name"));
        paramList.Add(new ReportParameter("Address1", "Address1"));
        paramList.Add(new ReportParameter("Address2", ""));
        paramList.Add(new ReportParameter("City", "City"));
        paramList.Add(new ReportParameter("State", "State"));
        paramList.Add(new ReportParameter("PostalCode", "Pin Code"));
        paramList.Add(new ReportParameter("Country", "India"));
        paramList.Add(new ReportParameter("GSTNo", "GST"));
        paramList.Add(new ReportParameter("AmountInWord", wordamount));
        rv.LocalReport.SetParameters(paramList);
        //}
        //drFirm.Close();
        //conn.Close();

        rv.LocalReport.Refresh();
        //rv.ReportRefresh(); // refresh report

        //SqlCommand cmd = new SqlCommand(query, conn);
        ////cmd.Parameters.Add(new SqlParameter("@ClientID", SqlDbType.NVarChar, ClientID.Trim().Length)).Value = ClientID.Trim();
        ////cmd.Parameters.Add(new SqlParameter("@SurveyEngineerID", SqlDbType.NVarChar, Session["userID"].ToString().Trim().Length)).Value = Session["userID"].ToString().Trim();
        //SqlDataAdapter adpt = new SqlDataAdapter();
        //adpt.SelectCommand = cmd;
        //DataTable dt = new DataTable();
        //DataColumn c = new DataColumn("sno", typeof(int));
        //c.AutoIncrement = true;
        //c.AutoIncrementSeed = 1;
        //c.AutoIncrementStep = 1;
        //dt.Columns.Add(c);
        //adpt.Fill(dt);
        //bool finishsurvey = true;
        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    //double total = double.Parse(dt.Rows[i]["QuotedPriceTotal"].ToString());
        //    //double qty = double.Parse(dt.Rows[i]["Quantity"].ToString());
        //    //double CurrTotal = total / CurrencyValue;
        //    //double unit = CurrTotal / qty;
        //    //dt.Rows[i]["CurrencyUnit"] = unit;
        //    //dt.Rows[i]["CurrencyTotal"] = CurrTotal;
        //    //dt.Rows[i]["Currency"] = ddlCurrency.SelectedItem.ToString();
        //    //if (total <= 0 || bool.Parse(dt.Rows[i]["Changestatus"].ToString())==true)
        //    //    finishsurvey = false;
        //}
        //if (SurveyID > 0)
        //{
        //    if (dt.Rows.Count > 0)
        //    {
        //        //if ((bool)dt.Rows[dt.Rows.Count - 1]["quotationdone"] == true)
        //        //{
        //        //    gvitemdisplay.DataSource = dt;
        //        //    gvitemdisplay.DataBind();
        //        //    txtClientName.Enabled = false;
        //        //}
        //        //else
        //        //{
        //            gvFeed.DataSource = dt;
        //            gvFeed.DataBind();
        //            txtClientName.Enabled = false;
        //        //}
        //    }
        //}

        //if (finishsurvey == true)
        //    btnFinishSurvey.Enabled = false;
        //else
        //    btnFinishSurvey.Enabled = true;

        //SqlCommand cmdQNo = new SqlCommand("Select * from QuotationHeader where SurveyID=" + SurveyID, conn);
        //if (conn.State == ConnectionState.Closed)
        //    conn.Open();
        //SqlDataReader drq = cmdQNo.ExecuteReader();
        //bool quotationdone = false;
        //if(drq.Read())
        //{
        //    lblQuotationNumber.Text = drq["QuotationNo"].ToString();
        //    quotationdone = true;
        //}
        //drq.Close();
        //if(quotationdone==false)
        //{ lblQuotationNumber.Text = "Quotation Number will generate once costing done for all Item and Click on Finish Quotation Button"; }
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

    public static string NumberToWords(int number)
    {
        if (number == 0)
            return "Zero";

        if (number < 0)
            return "Minus " + NumberToWords(Math.Abs(number));

        string words = "";

        if ((number / 1000000) > 0)
        {
            words += NumberToWords(number / 1000000) + " Million ";
            number %= 1000000;
        }

        if ((number / 1000) > 0)
        {
            words += NumberToWords(number / 1000) + " Thousand ";
            number %= 1000;
        }

        if ((number / 100) > 0)
        {
            words += NumberToWords(number / 100) + " Hundred ";
            number %= 100;
        }

        if (number > 0)
        {
            if (words != "")
                words += "and ";

            var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
            var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

            if (number < 20)
                words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0)
                    words += "-" + unitsMap[number % 10];
            }
        }

        return words;
    }

    protected void CallClientData(string SurveyID)
    {
        string query = "Select CM.*, SCIH.SurveyID, SCIH.StartDate from SurveyClientItemHead SCIH, ClientMaster CM where SCIH.ClientID=CM.ClientID and SCIH.SurveyID=" + Session["SurveyID"];

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

    protected void btnWithGSTwithDescription_Click(object sender, EventArgs e)
    {
        CallTempData(int.Parse(Session["SurveyID"].ToString()), 1);
    }
    protected void btnWithGSTWithoutDescription_Click(object sender, EventArgs e)
    {
        CallTempData(int.Parse(Session["SurveyID"].ToString()), 2);
    }
    protected void btnWithoutGSTWithDescription_Click(object sender, EventArgs e)
    {
        CallTempData(int.Parse(Session["SurveyID"].ToString()), 3);
    }
    protected void btnWithoutGSTWithoutDescription_Click(object sender, EventArgs e)
    {
        CallTempData(int.Parse(Session["SurveyID"].ToString()), 4);
    }
}