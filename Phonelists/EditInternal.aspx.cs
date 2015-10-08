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

public partial class EditInternal : System.Web.UI.Page
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

        string strSQL = "SELECT C.[ID],[Fullname],[First_name],[Last_name],D.[Dept] As Department,D.[Dept_Number] AS Dept_Number,[Email],[Work_Phone],[Work_PhoneSC],[Work_mob],[Work_mobSC],[Home_Phone],[Home_PhoneSC],[Home_mob],[Home_mobSC]" +
                        " FROM [PhoneList].[dbo].[Contactlst] C" +
                        " INNER JOIN [PhoneList].[dbo].[Department] D ON C.[Department] = D.[Dept_Number]" +
                        " ORDER BY [Fullname]";
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
        DropDownList DropDown = row.FindControl("ddlDepts") as DropDownList;

        if (((LinkButton)GridView1.Rows[0].Cells[0].Controls[0]).Text == "Insert")
        {
            SqlConnection con = new SqlConnection(GetConnectionString());
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO [PhoneList].[dbo].[Contactlst] ([Fullname],[First_name],[Last_name],[Department],[Email],[Work_Phone],[Work_PhoneSC],[Work_mob],[Work_mobSC],[Home_Phone],[Home_PhoneSC],[Home_mob],[Home_mobSC]) VALUES (@Fullname,@First_name,@Last_name,@Department,@Email,@Work_Phone,@Work_PhoneSC,@Work_mob,@Work_mobSC,@Home_Phone,@Home_PhoneSC,@Home_mob,@Home_mobSC)";
            cmd.Parameters.Add("@Fullname", SqlDbType.NVarChar).Value = ((TextBox)GridView1.Rows[0].Cells[2].Controls[0]).Text;
            cmd.Parameters.Add("@First_name", SqlDbType.NVarChar).Value = ((TextBox)GridView1.Rows[0].Cells[3].Controls[0]).Text;
            cmd.Parameters.Add("@Last_name", SqlDbType.NVarChar).Value = ((TextBox)GridView1.Rows[0].Cells[4].Controls[0]).Text;
            cmd.Parameters.Add("@Department", SqlDbType.NVarChar).Value = DropDown.SelectedValue.ToString();
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = ((TextBox)GridView1.Rows[0].Cells[8].Controls[0]).Text;
            cmd.Parameters.Add("@Work_Phone", SqlDbType.NVarChar).Value = ((TextBox)GridView1.Rows[0].Cells[6].Controls[0]).Text;
            cmd.Parameters.Add("@Work_PhoneSC", SqlDbType.NVarChar).Value = ((TextBox)GridView1.Rows[0].Cells[7].Controls[0]).Text;
            cmd.Parameters.Add("@Work_mob", SqlDbType.NVarChar).Value = ((TextBox)GridView1.Rows[0].Cells[9].Controls[0]).Text;
            cmd.Parameters.Add("@Work_mobSC", SqlDbType.NVarChar).Value = ((TextBox)GridView1.Rows[0].Cells[10].Controls[0]).Text;
            cmd.Parameters.Add("@Home_Phone", SqlDbType.NVarChar).Value = ((TextBox)GridView1.Rows[0].Cells[11].Controls[0]).Text;
            cmd.Parameters.Add("@Home_PhoneSC", SqlDbType.NVarChar).Value = ((TextBox)GridView1.Rows[0].Cells[12].Controls[0]).Text;
            cmd.Parameters.Add("@Home_mob", SqlDbType.NVarChar).Value = ((TextBox)GridView1.Rows[0].Cells[13].Controls[0]).Text;
            cmd.Parameters.Add("@Home_mobSC", SqlDbType.NVarChar).Value = ((TextBox)GridView1.Rows[0].Cells[14].Controls[0]).Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        else
        {
            SqlConnection con = new SqlConnection(GetConnectionString());
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE [PhoneList].[dbo].[Contactlst] SET Fullname=@Fullname,First_name=@First_name,Last_name=@Last_name,Department=@Department,Email=@Email,Work_Phone=@Work_Phone,Work_PhoneSC=@Work_PhoneSC,Work_mob=@Work_mob,Work_mobSC=@Work_mobSC,Home_Phone=@Home_Phone,Home_PhoneSC=@Home_PhoneSC,Home_mob=@Home_mob,Home_mobSC=@Home_mobSC WHERE ID=@ID";
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt32(GridView1.Rows[myRow].Cells[1].Text);
            cmd.Parameters.Add("@Fullname", SqlDbType.NVarChar).Value = ((TextBox)GridView1.Rows[myRow].Cells[2].Controls[0]).Text;
            cmd.Parameters.Add("@First_name", SqlDbType.NVarChar).Value = ((TextBox)GridView1.Rows[myRow].Cells[3].Controls[0]).Text;
            cmd.Parameters.Add("@Last_name", SqlDbType.NVarChar).Value = ((TextBox)GridView1.Rows[myRow].Cells[4].Controls[0]).Text;
            cmd.Parameters.Add("@Department", SqlDbType.NVarChar).Value = DropDown.SelectedValue.ToString();
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = ((TextBox)GridView1.Rows[myRow].Cells[8].Controls[0]).Text;
            cmd.Parameters.Add("@Work_Phone", SqlDbType.NVarChar).Value = ((TextBox)GridView1.Rows[myRow].Cells[6].Controls[0]).Text;
            cmd.Parameters.Add("@Work_PhoneSC", SqlDbType.NVarChar).Value = ((TextBox)GridView1.Rows[myRow].Cells[7].Controls[0]).Text;
            cmd.Parameters.Add("@Work_mob", SqlDbType.NVarChar).Value = ((TextBox)GridView1.Rows[myRow].Cells[9].Controls[0]).Text;
            cmd.Parameters.Add("@Work_mobSC", SqlDbType.NVarChar).Value = ((TextBox)GridView1.Rows[myRow].Cells[10].Controls[0]).Text;
            cmd.Parameters.Add("@Home_Phone", SqlDbType.NVarChar).Value = ((TextBox)GridView1.Rows[myRow].Cells[11].Controls[0]).Text;
            cmd.Parameters.Add("@Home_PhoneSC", SqlDbType.NVarChar).Value = ((TextBox)GridView1.Rows[myRow].Cells[12].Controls[0]).Text;
            cmd.Parameters.Add("@Home_mob", SqlDbType.NVarChar).Value = ((TextBox)GridView1.Rows[myRow].Cells[13].Controls[0]).Text;
            cmd.Parameters.Add("@Home_mobSC", SqlDbType.NVarChar).Value = ((TextBox)GridView1.Rows[myRow].Cells[14].Controls[0]).Text;
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
        SqlCommand cmd = new SqlCommand("delete FROM [PhoneList].[dbo].[Contactlst] where id='" + Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString()) + "'", con);
        cmd.ExecuteNonQuery();
        con.Close();
        BindData();



    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(GetConnectionString());
        string strSQL = "SELECT C.[ID],[Fullname],[First_name],[Last_name],D.[Dept] As Department,[Email],[Work_Phone],[Work_PhoneSC],[Work_mob],[Work_mobSC],[Home_Phone],[Home_PhoneSC],[Home_mob],[Home_mobSC],[Dept_Number]" +
                        " FROM [PhoneList].[dbo].[Contactlst] C" +
                        " INNER JOIN [PhoneList].[dbo].[Department] D ON C.[Department] = D.[Dept_Number]";
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

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow) && (GridView1.EditIndex == e.Row.RowIndex))
        {
            DropDownList ddlDept = (DropDownList)e.Row.FindControl("ddlDepts");
            string myDept = (e.Row.FindControl("lblStatusValue") as Label).Text.TrimEnd();
            ddlDept.SelectedValue = myDept;
            //ddlDept.Items.FindByValue(myDept).Selected = true;
        }  
    }
}
