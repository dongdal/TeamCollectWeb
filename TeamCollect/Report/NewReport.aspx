<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="NewReport.aspx.vb" Inherits="TeamCollect.NewReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>

<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
          <div style="background-color:white">
             <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="500px" Width="100%" SizeToReportContent="True" ZoomMode="FullPage" BorderStyle="Solid" BackColor="#333333" BorderWidth="4" DocumentMapWidth="100%" InternalBorderColor="#FFCC00" BorderColor="#b6d935" InternalBorderWidth="2"></rsweb:ReportViewer>
          </div>
    </form>
   
</body>