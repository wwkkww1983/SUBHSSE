<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TotalPayRegistrationView.aspx.cs"
    Inherits="FineUIPro.Web.CostGoods.TotalPayRegistrationView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>当月总投入登记表</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
                <Items>
                    <f:ToolbarFill runat="server"></f:ToolbarFill>
                    <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                        EnableAjax="false" DisableControlBeforePostBack="false">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Items>
            <f:ContentPanel runat="server" AutoScroll="true" ShowHeader="false" Height="580px"
                ID="ContentPanel2">
                <table id="Table5" runat="server" width="100%" border="1" cellpadding="0" cellspacing="0"
                    bordercolor="#bcd2e7" bordercolordark="#bcd2e7" bordercolorlight="#bcd2e7">
                    <%--<tr>
                        <td align="right" >
                            <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/Images/Export.gif" OnClick="btnExport_Click" />
                        </td>
                    </tr>--%>
                    <tr>
                        <td align="center" valign="top">
                            <asp:GridView ID="gvTotalPayRegistration" runat="server" AllowPaging="false" AllowSorting="True"
                                AutoGenerateColumns="True" HorizontalAlign="Justify" Width="100%" OnRowCreated="gvTotalPayRegistration_RowCreated"
                                OnDataBound="gvTotalPayRegistration_DataBound">
                                <AlternatingRowStyle CssClass="GridBgColr" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </f:ContentPanel>
        </Items>
    </f:Panel>
    </form>
</body>
</html>
