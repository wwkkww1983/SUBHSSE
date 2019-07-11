<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AnalyseHiddenDanger.aspx.cs" Inherits="FineUIPro.Web.Information.AnalyseHiddenDanger" %>
<%@ Register Src="~/Controls/ChartControl.ascx" TagName="ChartControl" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>安全隐患分析</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="RegionPanel1" AjaxAspnetControls="divHiddenDangerUnit,divHiddenDangerTime"/>
    <f:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server" Margin="5px">
        <Regions>
            <f:Region ID="Region1" ShowBorder="false" ShowHeader="false" RegionPosition="Top"
                BodyPadding="0 5 0 0" Width="200px" Layout="Fit" runat="server" EnableCollapse="true">
                <Items>
                    <f:Form ID="Form2" ShowHeader="false" ShowBorder="false" runat="server">
                        <Rows>
                            <f:FormRow ColumnWidths="45% 45% 10%">                                
                                <Items>                                  
                                    <f:DropDownList ID="drpYear" runat="server" LabelWidth="90px" Label="年度"  OnClearIconClick="drpYear_ClearIconClick"
                                         EnableMultiSelect="true" AutoShowClearIcon="true" EnableClearIconClickEvent="true" EnableEdit="true" EnableCheckBoxSelect="true">
                                    </f:DropDownList>       
                                     <f:DropDownList ID="drpMonth" runat="server" LabelWidth="90px" Label="月份" OnClearIconClick="drpMonth_ClearIconClick"
                                        EnableMultiSelect="true" AutoShowClearIcon="true" EnableClearIconClickEvent="true" EnableEdit="true" EnableCheckBoxSelect="true">
                                    </f:DropDownList> 
                                    <f:Button ID="BtnAnalyse" Text="统计" Icon="ChartPie" runat="server" OnClick="BtnAnalyse_Click" ToolTip="数据来源：企业监督检查整改"></f:Button>
                                </Items>
                            </f:FormRow>
                           <f:FormRow ColumnWidths="45% 45% 10%">                              
                                <Items>
                                    <f:DropDownList ID="drpChartType" runat="server" LabelWidth="90px" Label="图形类别" 
                                        AutoPostBack="true" OnSelectedIndexChanged="drpChartType_SelectedIndexChanged" >
                                    </f:DropDownList>                                 
                                    <f:CheckBox ID="ckbShow" runat="server" LabelWidth="90px" Label="三维效果" 
                                        AutoPostBack="true" OnCheckedChanged="ckbShow_CheckedChanged">
                                    </f:CheckBox>
                                    <f:Label ID="Label1" runat="server" >
                                    </f:Label>
                                    
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
                           <%-- <f:Tab ID="Tab1" Title="按单位" BodyPadding="5px" Layout="Fit" IconFont="Bookmark" runat="server"
                                TitleToolTip="按单位统计">
                                <Items>                                   
                                    <f:ContentPanel ShowHeader="false" runat="server" ID="cpHiddenDangerUnit" Margin="0 0 0 0">
                                        <div id="divHiddenDangerUnit">
                                            <uc1:ChartControl ID="ChartHiddenDangerUnit" runat="server" />    
                                        </div>                                           
                                    </f:ContentPanel>
                                </Items>
                            </f:Tab>--%>
                            <f:Tab ID="Tab2" Title="按类别" BodyPadding="5px" Layout="Fit" IconFont="Bookmark"
                                runat="server"  TitleToolTip="按类别统计">
                                <Items>
                                      <f:ContentPanel ShowHeader="false" runat="server" ID="cpHiddenDangerTime" Margin="0 0 0 0">
                                        <div id="divHiddenDangerTime">
                                            <uc1:ChartControl ID="ChartHiddenDangerTime" runat="server" />    
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
