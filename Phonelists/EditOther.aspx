<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditOther.aspx.cs" Inherits="EditOther" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="css/main.css" type="text/css" rel="StyleSheet" />
    <link href="css/grid.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
    function filterDigits(eventInstance) {
        eventInstance = eventInstance || window.event;
        key = eventInstance.keyCode || eventInstance.which;
        //alert(key);
        if ((47 <= key) && (key <= 58) || (97 <= key) && (key <= 122) || (65 <= key) && (key <= 90) || key == 46 || key == 64 || key == 35 || key == 32 || key == 45 || key == 8) {
            return true;
        }
        else {
            if (eventInstance.preventDefault) eventInstance.preventDefault();
            eventInstance.returnValue = false;
            return false;
        }
    } 
</script>
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
            <br /><br />
        </div>
        <div class="grid" style="margin-left:auto;margin-right:auto">
        <label style="color:#284775; font-weight:normal;">Select a user from the list below to edit their contact details</label>      
        <br /><br />
        
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="2"
            DataKeyNames="id" ForeColor="#333333" GridLines="Both" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting"
            OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating"  Width="100%" onkeypress="filterDigits(event);">
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <Columns>
                <asp:CommandField ItemStyle-HorizontalAlign="Left" HeaderText="Edit-Update" HeaderStyle-HorizontalAlign="Left" ShowEditButton="True" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ID" HeaderText="ID" ReadOnly="True" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="Fullname" HeaderText="Name" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="Work_Phone" HeaderText="Work Phone" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="Work_PhoneSC" HeaderText="Work PhoneSC" />
                <asp:CommandField ItemStyle-HorizontalAlign="Left" HeaderText="Delete" HeaderStyle-HorizontalAlign="Left" ShowDeleteButton="true" />
               
            </Columns>
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <EditRowStyle BackColor="#999999" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" BorderColor="Black" BorderWidth="2px" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>

        <br />
            <table width="800px">
            <tr>
                <td align="left">
                    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add new person" Visible="false" style="color:#284775; font-weight:normal;" />
                </td>
                <td align="right">
                    &nbsp;
                </td>
            </tr>
            </table>
        <br />
    </div>
    </form>
</body>
</html>
