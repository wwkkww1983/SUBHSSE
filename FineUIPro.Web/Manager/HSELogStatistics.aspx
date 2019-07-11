<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HSELogStatistics.aspx.cs"
    Inherits="FineUIPro.Web.Manager.HSELogStatistics" %>

<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>导出HSSE日志暨管理数据收集</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="RegionPanel1" AjaxAspnetControls="divCheck" />
    <f:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server" Margin="5px">
        <Regions>
            <f:Region ID="Region1" ShowBorder="false" ShowHeader="false" RegionPosition="Top"
                BodyPadding="0 5 0 0" runat="server" >
                <Items>
                    <f:Form ID="Form2" ShowHeader="false" ShowBorder="false" runat="server">
                        <Rows>
                            <f:FormRow ColumnWidths="30% 30% 30% 5% 5%">
                                <Items>
                                    <f:DropDownList ID="drpCompileMan" runat="server" LabelWidth="80px" Label="编制人" LabelAlign="Right">
                                    </f:DropDownList>
                                    <f:DatePicker runat="server" Label="开始时间" ID="txtStartDate" EnableEdit="true" LabelWidth="80px"
                                        LabelAlign="Right">
                                    </f:DatePicker>
                                    <f:DatePicker runat="server" Label="结束时间" ID="txtEndDate" EnableEdit="true" LabelWidth="80px"
                                        LabelAlign="Right">
                                    </f:DatePicker>
                                    <f:Button ID="btnStatistics" ToolTip="统计" Icon="ChartPie" runat="server" OnClick="btnStatistics_Click">
                                    </f:Button>
                                    <f:Button ID="btnOut" runat="server" Icon="FolderUp" ToolTip="导出" OnClick="btnOut_Click" EnableAjax="false" DisableControlBeforePostBack="false">
                                    </f:Button>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
            </f:Region>
            <f:Region ID="Region2" ShowBorder="false" ShowHeader="false" Position="Center" Layout="VBox"
                BoxConfigAlign="Stretch" BoxConfigPosition="Left" runat="server">
                <Items>
                    <f:ContentPanel ShowHeader="false" runat="server" ID="ContentPanel1" Margin="0 0 0 0">
                        <div id="divCheck" style="overflow-y: scroll; height: 600px; width: 100%;">
                            <asp:GridView ID="gvHSELog" runat="server" AllowSorting="false" HorizontalAlign="Justify"
                                Width="100%" AllowPaging="false">
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
