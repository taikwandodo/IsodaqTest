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

public partial class EditOther : System.Web.UI.Page
{
    public string GetConnectionString()
    {
        return System.Configuration.ConfigurationManager.ConnectionStrings["WebSqlServer"].ConnectionString;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
        else
        {

        }
    }

    private void BindData()
    {
        SqlConnection con = new SqlConnection(GetConnectionString());



        string strSQL = "SELECT C.[ID],[Fullname],[Work_Phone],[Work_PhoneSC] " +
                " FROM [PhoneList].[dbo].[Otherlst] C WHERE [Type] = 2";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            btnAdd.Visible = true;
        }
        else
        {
            btnAdd.Visible = false;
        }
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        BindData();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        BindData();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int myRow = e.RowIndex;
        GridViewRow row = GridView1.Rows[e.RowIndex];
        GridView1.EditIndex = -1;


        if (((LinkButton)GridView1.Rows[0].Cells[0].Controls[0]).Text == "Insert")
        {
            SqlConnection con = new SqlConnection(GetConnectionString());
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO [PhoneList].[dbo].[Otherlst] ([Fullname],[Work_Phone],[Work_PhoneSC],[Type]) VALUES (@Fullname,@Work_Phone,@Work_PhoneSC,2)";
            cmd.Parameters.Add("@Fullname", SqlDbType.NVarChar).Value = ((TextBox)GridView1.Rows[0].Cells[2].Controls[0]).Text;


            cmd.Parameters.Add("@Work_Phone", SqlDbType.NVarChar).Value = ((TextBox)GridView1.Rows[0].Cells[3].Controls[0]).Text;
            cmd.Parameters.Add("@Work_PhoneSC", SqlDbType.NVarChar).Value = ((TextBox)GridView1.Rows[0].Cells[4].Controls[0]).Text;
            
            

            cmd.Connection = con;

            con.Open();


            cmd.ExecuteNonQuery();
            con.Close();
        }
        else
        {
            SqlConnection con = new SqlConnection(GetConnectionString());
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE [PhoneList].[dbo].[Otherlst] SET Fullname=@Fullname,Work_Phone=@Work_Phone,Work_PhoneSC=@Work_PhoneSC WHERE ID=@ID";
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt32(GridView1.Rows[myRow].Cells[1].Text);
            cmd.Parameters.Add("@Fullname", SqlDbType.NVarChar).Value = ((TextBox)GridView1.Rows[myRow].Cells[2].Controls[0]).Text;

            cmd.Parameters.Add("@Work_Phone", SqlDbType.NVarChar).Value = ((TextBox)GridView1.Rows[myRow].Cells[3].Controls[0]).Text;
            cmd.Parameters.Add("@Work_PhoneSC", SqlDbType.NVarChar).Value = ((TextBox)GridView1.Rows[myRow].Cells[4].Controls[0]).Text;
            
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        GridView1.EditIndex = -1;
        BindData();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SqlConnection con = new SqlConnection(GetConnectionString());
        GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
        Label lbldeleteid = (Label)row.FindControl("ID");
        con.Open();
        SqlCommand cmd = new SqlCommand("delete FROM [PhoneList].[dbo].[Otherlst] where id='" + Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString()) + "'", con);
        cmd.ExecuteNonQuery();
        con.Close();
        BindData();



    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(GetConnectionString());
        string strSQL = "SELECT C.[ID],[Fullname],[Work_Phone],[Work_PhoneSC]" +
                        " FROM [PhoneList].[dbo].[Otherlst] C WHERE [Type] = 2";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        // Here we'll add a blank row to the returned DataTable
        DataRow dr = dt.NewRow();
        dt.Rows.InsertAt(dr, 0);
        //Creating the first row of GridView to be Editable
        GridView1.EditIndex = 0;
        GridView1.DataSource = dt;
        GridView1.DataBind();
        //Changing the Text for Inserting a New Record
        ((LinkButton)GridView1.Rows[0].Cells[0].Controls[0]).Text = "Insert";

    }

    protected void ddlUsers_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

}
