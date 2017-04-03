<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MegaChallengeCasino.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Image ID="Image1" runat="server" />
        <asp:Image ID="Image2" runat="server" />
        <asp:Image ID="Image3" runat="server" />
        <br />
        <br />
        Your Bet:
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Button" />
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        1 Cherry - x2 Your Bet<br />
        2 Cherries - x3 Your Bet<br />
        3 Cherries - x4 Your Bet<br />
        3 7&#39;s - Jackpot - x100 Your Bet<br />
        HOWEVER ... if there&#39;s even one BAR you win nothing</div>
    </form>
</body>
</html>
