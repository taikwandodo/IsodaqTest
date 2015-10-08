<%@ Page Language="C#" AutoEventWireup="true" CodeFile="internal.aspx.cs" Inherits="phonelist_internal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="css/main.css" type="text/css" rel="StyleSheet" />
    <link href="css/grid.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" style="width:100%">
        <div id="div_menu" style="width:1000px;margin-left:auto;margin-right:auto">
	        <a href="http://www.hydro-logic.co.uk"><img src="http://192.168.0.1/new/images/HL_Logo2.jpg" alt="Hydro-Logic Logo" align="middle" border="0" /></a>
            <table id="m1mainSXMenu1" cellspacing="0" cellpadding="0"  style="background-color:#B0C4DE;width:1000px">
                <tr>
                    <td>
                        <!-- ***** This is the section of code you need to paste into your web pages ***** -->
                        <script type="text/javascript" src="http://192.168.0.1/new/milonic/milonic_src.js"></script>	
                        <script type="text/javascript" src="http://192.168.0.1/new/milonic/mmenudom.js"></script>
                        <!-- The next file contains your menu data, links and menu structure etc -->
                        <script type="text/javascript" src="http://192.168.0.1/new/milonic/menu_data.js"></script>	
                        <!-- **** JavaScript Menu HTML Code -->                
                    </td>
                </tr>
            </table>
            <a style="display:none" href="http://www.milonic.com/>DHTML"> Menu By Milonic JavaScript</a>
            <br />
        </div>
        <div class="grid" style="margin-left:auto;margin-right:auto">
        <h3 style="color:#284775">Phone List - Internal</h3>
        <label style=" color:#5D7B9D; font-size: small">Sort columns by clicking on headers</label>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="3" ForeColor="#333333" GridLines="Both" AllowSorting="true" onrowdatabound="GridView1_RowDataBound" onsorting="GridView1_Sorting" Width="100%">
            <Columns>
                <asp:BoundField SortExpression="First_name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="First_name" HeaderText="Firstname" />
                <asp:BoundField SortExpression="Last_name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="Last_name" HeaderText="Surname" />
                <asp:BoundField SortExpression="Office" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="Office" HeaderText="Office" />
                <asp:BoundField SortExpression="Mobile" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="Mobile" HeaderText="Mobile" />
                <asp:BoundField SortExpression="Home" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="Home" HeaderText="Home" />
                <asp:BoundField SortExpression="HomeMobile" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="HomeMobile" HeaderText="Home Mobile" />
            </Columns>
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" VerticalAlign="top" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" VerticalAlign="top" BorderWidth="2px" BorderColor="Black" BorderStyle="Solid" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>

        <br />
        <table width="100%">
        <tr>
            <td align="left">
                <asp:LinkButton runat="server" ID="LinkButton1" onclick="LinkButton1_Click">Edit list</asp:LinkButton>
            </td>
            <td align="right">
                <asp:Label ID="Label3" style="font-style:italic;font-size:small;" runat="server" Text="export results to Excel"></asp:Label>
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="images/excel.gif" ToolTip="Export results to Excel" onclick="btnExportToExcel_Click" />
            </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
