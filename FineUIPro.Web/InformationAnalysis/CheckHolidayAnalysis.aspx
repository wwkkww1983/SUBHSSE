<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckHolidayAnalysis.aspx.cs" Inherits="FineUIPro.Web.InformationAnalysis.CheckHolidayAnalysis" %>
<%@ Register Src="~/Controls/ChartControl.ascx" TagName="ChartControl" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>季节性/节假日检查分析</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="RegionPanel1" AjaxAspnetControls="divAnalyse,divCheck"/>
    <f:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server" Margin="5px">
        <Regions>
            <f:Region ID="Region1" ShowBorder="false" ShowHeader="false" RegionPosition="Top"
                BodyPadding="0 5 0 0" Width="200px" Layout="Fit" runat="server" EnableCollapse="true">
                <Items>
                    <f:Form ID="Form2" ShowHeader="false" ShowBorder="false" runat="server">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:DatePicker runat="server" Label="开始时间" ID="txtStarTime" EnableEdit="true" LabelWidth="80px"></f:DatePicker>
                                    <f:DatePicker runat="server" Label="结束时间" ID="txtEndTime" EnableEdit="true" LabelWidth="80px"></f:DatePicker>
                                    <f:DropDownList ID="drpChartType" runat="server" LabelWidth="80px" Label="图形类型" 
                                        AutoPostBack="true" OnSelectedIndexChanged="drpChartType_SelectedIndexChanged" >
                                    </f:DropDownList>
                                    <f:CheckBox ID="ckbShow" runat="server" LabelWidth="80px" Label="三维效果" 
                                        AutoPostBack="true" OnCheckedChanged="ckbShow_CheckedChanged">
                                    </f:CheckBox>
                                    <f:Button ID="BtnAnalyse" Text="统计" Icon="ChartPie" runat="server" OnClick="BtnAnalyse_Click"></f:Button>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
            </f:Region>
            <f:Region ID="Region2" ShowBorder="false" ShowHeader="false" Position="Center" Layout="VBox"
                BoxConfigAlign="Stretch" BoxConfigPosition="Left" runat="server">
                <Items>
                    <f:TabStrip ID="TabStrip1" CssClass="f-tabstrip-theme-simple" Height="500px" ShowBorder="true"
                        TabPosition="Top" MarginBottom="5px" EnableTabCloseMenu="false" runat="server">
                        <Tabs>
                            <f:Tab ID="Tab1" Title="表格" BodyPadding="5px" Layout="Fit" IconFont="Bookmark" runat="server"
                                TitleToolTip="按表格样式显示">
                                <Items>  
                                    <f:ContentPanel ShowHeader="false" runat="server" ID="ContentPanel1" Margin="0 0 0 0">
                                         <div id="divCheck">                                 
                                            <asp:GridView ID="gvCheck" runat="server" AllowSorting="True" PageSize="100"
                                                HorizontalAlign="Justify" Width="100%">                                                
                                                <Columns>
                                                </Columns>                                                
                                                <RowStyle CssClass="GridRow" />
                                                <PagerStyle HorizontalAlign="Left" />
                                            </asp:GridView>
                                        </div>
                                     </f:ContentPanel>
                                </Items>
                            </f:Tab>
                            <f:Tab ID="Tab2" Title="图形" BodyPadding="5px" Layout="Fit" IconFont="Bookmark"
                                runat="server"  TitleToolTip="按图形显示">
                                <Items>
                                      <f:ContentPanel ShowHeader="false" runat="server" ID="cpCostTime" Margin="0 0 0 0">
                                        <div id="divAnalyse">
                                            <uc1:ChartControl ID="ChartCostTime" runat="server" />    
                                        </div>                                           
                                    </f:ContentPanel>
                                </Items>
                            </f:Tab>
                        </Tabs>
                    </f:TabStrip>
                </Items>
            </f:Region>
        </Regions>
    </f:RegionPanel>
    </form>
</body>
</html>
