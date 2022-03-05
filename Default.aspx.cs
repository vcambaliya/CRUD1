using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class _Default : System.Web.UI.Page
{
    SqlConnection con;
    protected void Page_Load(object sender, EventArgs e)
    {
        con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\ABC\Documents\Visual Studio 2010\WebSites\WebSite3\App_Data\Database.mdf;Integrated Security=True;User Instance=True");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("INSERT INTO [registration]([FirstName],[LastName],[Email],[Contact],[Password]) VALUES(@fname,@lname,@email,@contact,@password)", con);
        cmd.Parameters.AddWithValue("@fname",TextBox1.Text);
        cmd.Parameters.AddWithValue("@lname",TextBox2.Text);
        cmd.Parameters.AddWithValue("@email",TextBox3.Text);
        cmd.Parameters.AddWithValue("@contact",TextBox4.Text);
        cmd.Parameters.AddWithValue("@password",TextBox5.Text);
        con.Open();
        int a = cmd.ExecuteNonQuery();
        con.Close();
        if (a == 1)
        {
            Literal1.Text = "Registration is Successfully Done";
            Clear();
            Print();
        }
        else
        {
            Literal1.Text = "Error";
        }

    }
    public void Clear()
    {
        TextBox1.Text = "";
        TextBox2.Text = "";
        TextBox3.Text = "";
        TextBox4.Text = "";
        TextBox5.Text = "";
    }
    public void Print()
    {
        SqlDataAdapter adpt = new SqlDataAdapter("SELECT [id],[FirstName],[LastName],[Email],[Contact],[Password] FROM [registration]",con);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        GridView1.DataSource = dt;
        GridView1.DataBind();

    }
    
    protected void Button2_Click(object sender, EventArgs e)
    {
        Button btn=(Button)sender;
        SqlCommand cmd = new SqlCommand("DELETE FROM [registration] WHERE [id]=@id",con);
        cmd.Parameters.AddWithValue("@id",btn.CommandArgument);
        con.Open();
        int s=cmd.ExecuteNonQuery();
        con.Close();
        if (s == 1)
        {
            Literal1.Text = "Successfully Delete";
            Clear();
            Print();
        }
        else
        {
            Literal1.Text = "Error";
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        SqlDataAdapter adpt = new SqlDataAdapter("SELECT [id],[FirstName],[LastName],[Email], [Contact],[Password] FROM [registration] WHERE [id]="+btn.CommandArgument, con);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        TextBox1.Text = dt.Rows[0][1].ToString(); 
        TextBox2.Text = dt.Rows[0][2].ToString();
        TextBox3.Text = dt.Rows[0][3].ToString();
        TextBox4.Text = dt.Rows[0][4].ToString();
        TextBox5.Text = dt.Rows[0][5].ToString();
        ViewState["id"] = btn.CommandArgument;

    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("UPDATE [registration] SET [FirstName]=@fname,[LastName]=@lname,[Email]=@email,[Contact]=@contact,[Password]=@password WHERE [id]=@id", con);
        cmd.Parameters.AddWithValue("@fname", TextBox1.Text);
        cmd.Parameters.AddWithValue("@lname", TextBox2.Text);
        cmd.Parameters.AddWithValue("@email", TextBox3.Text);
        cmd.Parameters.AddWithValue("@contact", TextBox4.Text);
        cmd.Parameters.AddWithValue("@password", TextBox5.Text);
        cmd.Parameters.AddWithValue("@id",ViewState["id"]);
        con.Open();
        int a = cmd.ExecuteNonQuery();
        con.Close();
        if (a == 1)
        {
            Literal1.Text = "Record Update Successfully";
            Clear();
            Print();
        }
        else
        {
            Literal1.Text = "Error";
        }
    }
    protected void TextBox5_TextChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}