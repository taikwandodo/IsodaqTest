using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

public partial class phonelist_Department : System.Web.UI.Page
{
    public string GetConnectionString()
    {
        return System.Configuration.ConfigurationManager.ConnectionStrings["WebSqlServer"].ConnectionString;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["sortColumn"] = "";
            ViewState["sortOrder"] = "";
            bindGridView("", "");
        }
        else
        {
            //do nothing
        }
    }

    private void bindGridView(string sortExp, string sortDir)
    {
        string strSQL = "SELECT D.[Dept] As Department,C.[ID] AS ID,[Fullname],[First_name],[Last_name],[Email],([Work_Phone] + '[' + ISNULL([Work_PhoneSC],'') + ']') As Office" +
                        " ,([Work_mob] + '[' + ISNULL([Work_mobSC],'') + ']') As Mobile,([Home_Phone] + '[' + ISNULL([Home_PhoneSC],'') + ']') As Home,([Home_mob] + '[' + ISNULL([Home_mobSC],'') + ']') As HomeMobile, Email" +
                        " FROM [PhoneList].[dbo].[Contactlst] C" +
                        " INNER JOIN [PhoneList].[dbo].[Department] D ON C.[Department] = D.[Dept_Number]" +
                        " ORDER BY D.[Dept_Order],[Last_name]";
        //Response.Write(strSQL);
        //Response.End();

        SqlConnection con = new SqlConnection(GetConnectionString());
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataSet myDataSet = new DataSet();
        da.Fill(myDataSet);

        DataView myDataView = new DataView();
        myDataView = myDataSet.Tables[0].DefaultView;

        if (sortExp != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", sortExp, sortDir);
        }

        int i  = 0; 
        string prevCat = "";
        while (i <= myDataSet.Tables[0].Rows.Count - 1)
        {
            string curCat = myDataSet.Tables[0].Rows[i]["Department"].ToString();
            if (curCat != prevCat)
            {
                prevCat = curCat;
                DataTable table = myDataSet.Tables[0];
                string myName = myDataSet.Tables[0].Rows[i][0].ToString();
                DataRow barackRow = getRow(table, myName);
                table.Rows.InsertAt(barackRow, i);
                i ++;
            }
               
            i ++;
        }

        GridView1.DataSource = myDataView;
        GridView1.DataBind();
    }

    static DataRow getRow(DataTable table, string userName)
    {
        DataRow row = table.NewRow();
        row["First_name"] = userName;
        row["Last_name"] = "subhead";
        return row;
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[2].Text.Trim() == "[]")
            {
                e.Row.Cells[2].Text = "";
            }

            if (e.Row.Cells[2].Text.Trim() != "" && e.Row.Cells[2].Text.Trim().Substring(e.Row.Cells[2].Text.Trim().Length - 2) == "[]")
            {
                e.Row.Cells[2].Text = e.Row.Cells[2].Text.Trim().Substring(0, e.Row.Cells[2].Text.Trim().Length - 2);
            }

            if (e.Row.Cells[3].Text.Trim() == "[]")
            {
                e.Row.Cells[3].Text = "";
            }

            if (e.Row.Cells[3].Text.Trim() != "" && e.Row.Cells[3].Text.Trim().Substring(e.Row.Cells[3].Text.Trim().Length - 2) == "[]")
            {
                e.Row.Cells[3].Text = e.Row.Cells[3].Text.Trim().Substring(0, e.Row.Cells[3].Text.Trim().Length - 2);
            }

            if (e.Row.Cells[4].Text.Trim() == "[]")
            {
                e.Row.Cells[4].Text = "";
            }

            if (e.Row.Cells[4].Text.Trim() != "" && e.Row.Cells[4].Text.Trim().Substring(e.Row.Cells[4].Text.Trim().Length - 2) == "[]")
            {
                e.Row.Cells[4].Text = e.Row.Cells[4].Text.Trim().Substring(0, e.Row.Cells[4].Text.Trim().Length - 2);
            }

            if (e.Row.Cells[5].Text.Trim() == "[]")
            {
                e.Row.Cells[5].Text = "";
            }

            if (e.Row.Cells[5].Text.Trim() != "" && e.Row.Cells[5].Text.Trim().Substring(e.Row.Cells[5].Text.Trim().Length - 2) == "[]")
            {
                e.Row.Cells[5].Text = e.Row.Cells[5].Text.Trim().Substring(0, e.Row.Cells[5].Text.Trim().Length - 2);
            }

            if (e.Row.Cells[1].Text.Trim() == "subhead")
            {
                e.Row.Cells.RemoveAt(6);
                e.Row.Cells.RemoveAt(5);
                e.Row.Cells.RemoveAt(4);
                e.Row.Cells.RemoveAt(3);
                e.Row.Cells.RemoveAt(2);
                e.Row.Cells.RemoveAt(1);
                e.Row.Cells[0].ColumnSpan = 7;
                e.Row.BackColor = System.Drawing.Color.LightGray;
                e.Row.ForeColor = System.Drawing.Color.Black;
                //e.Row.BorderColor = System.Drawing.Color.Black;
                e.Row.Font.Bold = true;
            }
        }

    }

    public string sortOrder
    {
        get
        {
            if (ViewState["sortOrder"].ToString() == "desc")
            {
                ViewState["sortOrder"] = "asc";
            }
            else
            {
                ViewState["sortOrder"] = "desc";
            }

            return ViewState["sortOrder"].ToString();
        }
        set
        {
            ViewState["sortOrder"] = value;
        }
    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["sortColumn"] = e.SortExpression;
        bindGridView(e.SortExpression, sortOrder);
    }

    protected void ExportToExcel()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("content-disposition", "attachment;filename=departments.xls");
        Response.Charset = "";
        this.EnableViewState = false;

        System.IO.StringWriter sw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

        GridView1.RenderControl(htw);

        Response.Write(Regex.Replace(sw.ToString(), "(<a[^>]*>)|(</a>)", " ", RegexOptions.IgnoreCase));

        Response.End();
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        //Turn off the view state
        this.EnableViewState = false;

        //Remove the charset from the Content-Type header
        Response.Charset = String.Empty;
        ExportToExcel();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("editinternal.aspx");
    }

}