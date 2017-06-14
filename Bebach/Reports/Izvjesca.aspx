<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Izvjesca.aspx.cs" Inherits="Bebach.Reports.Izvjesca" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PREGLEDI PODATAKA</title>

    <link href="../Content/jquery.datepick.css" rel="stylesheet" />
   <%-- <link rel="Stylesheet" type="text/css" href="../Styles/style.css" />--%>
   <%-- <script src="../Scripts/jquery-1.10.2.min.js"></script>--%>
    <script src="../Scripts/jquery-1.4.2.min.js"></script>
 <%--   <script src="/Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>--%>
    <script type="text/javascript" src="../Scripts/jquery.datepick.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.datepick-hr.js"></script>
  <%--  <script type="text/javascript" src="/Scripts/hajan.datevalidator.js"></script>--%>
    <script type="text/javascript">
        $(function () {


            $("#<%= txtOd.ClientID %>").datepick({ dateFormat: 'dd/mm/yyyy', showAnim: 'fadeIn' });
            $("#<%= txtDo.ClientID %>").datepick({ dateFormat: 'dd/mm/yyyy', showAnim: 'fadeIn' });
            $.datepick.setDefaults($.datepick.regional['hr']);
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     
          <fieldset>
            <legend>Pretraživanje aktivnosti po datumu.</legend>
            <p>
                Odaberite razdoblje od:&nbsp;&nbsp;
                <asp:TextBox ID="txtOd" runat="server"></asp:TextBox>
                &nbsp;&nbsp; do
                <asp:TextBox ID="txtDo" runat="server"></asp:TextBox>
            </p>
            <p>
                &nbsp;</p>
            <p>
                <asp:DropDownList ID="ddlVrsta" runat="server" Height="20px" Width="196px">
                    <asp:ListItem Value="0">Odaberite vrstu izvješća</asp:ListItem>
                    <asp:ListItem Value="1">Pregledi bebe</asp:ListItem>
                   
                    <asp:ListItem Value="2">Izvješće o aktivnostima</asp:ListItem>
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button1" runat="server" Text="Pokreni" OnClick="Button1_Click1" CssClass="btn btn-default" />
            </p>
        </fieldset>
          <asp:ScriptManager ID="ScriptManager1" runat="server">
          </asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="1026px" Height="430px">
        </rsweb:ReportViewer>
    
    </div>
    </form>
</body>
</html>
