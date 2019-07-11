<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CostLedger.aspx.cs" Inherits="FineUIPro.Web.CostGoods.CostLedger" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>合同HSE费用及支付台账</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="RegionPanel1" AjaxAspnetControls="divCheck" />
    <f:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server" Margin="5px">
        <Regions>
            <f:Region ID="Region1" ShowBorder="false" ShowHeader="false" RegionPosition="Top"
                BodyPadding="0 5 0 0" runat="server">
                <Items>
                    <f:Form ID="Form2" ShowHeader="false" ShowBorder="false" runat="server" >
                        <Rows>
                            <f:FormRow ColumnWidths="95% 5%">
                                <Items>
                                    <f:Label ID="Label1" runat="server"></f:Label>
                                    <f:Button ID="btnOut" runat="server" Icon="FolderUp" ToolTip="导出" OnClick="btnOut_Click"
                                        EnableAjax="false" DisableControlBeforePostBack="false">
                                    </f:Button>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
            </f:Region>
            <f:Region ID="Region2" ShowBorder="false" ShowHeader="false" Position="Center" Layout="HBox"
                BoxConfigAlign="Stretch" BoxConfigPosition="Left" runat="server" AutoScroll="true">
                <Items>
                    <f:ContentPanel ShowHeader="false" runat="server" ID="ContentPanel1" Margin="0 0 0 0">
                        <div id="div1" style="height: 520px; ">
                            <asp:GridView ID="gvCostLedger" runat="server" AllowSorting="false" HorizontalAlign="Justify"
                                 AllowPaging="false" OnRowCreated="gvCostLedger_RowCreated" OnDataBound="gvCostLedger_DataBound">
                                <Columns>
                                </Columns>
                                <HeaderStyle CssClass="GridBgColr" />
                                <RowStyle CssClass="GridRow" BorderColor="#C7D9EA" BorderStyle="Solid" BorderWidth="1px" />
                                <PagerStyle HorizontalAlign="Left" />
                            </asp:GridView>
                        </div>
                    </f:ContentPanel>
                </Items>
            </f:Region>
        </Regions>
    </f:RegionPanel>
    </form>
</body>
</html>
