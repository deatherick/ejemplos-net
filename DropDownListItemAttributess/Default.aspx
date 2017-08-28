<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="DropDownListItemAttributess._Default" %>
<%@ Register TagPrefix="asp" Namespace="DropDownListItemAttributess" Assembly="App_Code" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Adding Attributes and Keeping DropDownList ListItems Attributes on Postback in ASP.NET</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lbl1" runat="server" Text="This is not Postback"></asp:Label><br /><br />
        <asp:DropDownListAttributes ID="ddl1" runat="server"></asp:DropDownListAttributes>       
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="ddl2" runat="server"></asp:DropDownList>
        <br /><br />
        <asp:Button ID="btn1" runat="server" OnClick="btn1_Click" Text="Click" />
    </div>
    </form>
</body>
</html>
