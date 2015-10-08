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

public partial class phonelist_internal : System.Web.UI.Page
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
        string strSQL = "SELECT [ID],[Fullname],[First_name],[Last_name],[Department],[Email],([Work_Phone] + '[' + ISNULL([Work_PhoneSC],'') + ']') As Office" +
                        " ,([Work_mob] + '[' + ISNULL([Work_mobSC],'') + ']') As Mobile,([Home_Phone] + '[' + ISNULL([Home_PhoneSC],'') + ']') As Home,([Home_mob] + '[' + ISNULL([Home_mobSC],'') + ']') As HomeMobile, Email" +
                        " FROM [PhoneList].[dbo].[Contactlst]" +
                        " ORDER BY [First_name]";

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

        GridView1.DataSource = myDataView;
        GridView1.DataBind();
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
        Response.AddHeader("content-disposition", "attachment;filename=Internal.xls");
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




//                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Email" SortExpression="Email">
//                    <ItemTemplate>
//                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("email", "mailto:{0}") %>' Text='<%# Eval("email", "{0}") %>'></asp:HyperLink>
//                    </ItemTemplate>
//                </asp:TemplateField>