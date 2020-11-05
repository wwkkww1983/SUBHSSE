﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DrillConductedQuarterlyReportEdit.aspx.cs"
    Inherits="FineUIPro.Web.InformationProject.DrillConductedQuarterlyReportEdit"
    ValidateRequest="false" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑应急演练开展情况季报</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="ddlYearId" runat="server" Label="年度" Required="True" ShowRedStar="True" AutoPostBack="true" OnSelectedIndexChanged="drpYear_SelectedIndexChanged">
                    </f:DropDownList>
                    <f:DropDownList ID="ddlQuarter" runat="server" Label="季度" Required="True" ShowRedStar="True" AutoPostBack="true" OnSelectedIndexChanged="drpYear_SelectedIndexChanged">
                    </f:DropDownList>
                    <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="制表时间" ID="txtCompileDate"
                        Required="True" ShowRedStar="True">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server"
                        BoxFlex="1" DataKeyNames="DrillConductedQuarterlyReportItemId" AllowCellEditing="true"
                        ClicksToEdit="1" DataIDField="DrillConductedQuarterlyReportItemId" EnableColumnLines="true"
                        OnRowCommand="Grid1_RowCommand" EnableHeaderMenu="false" Width="1300px" Height="280px">
                        <Columns>
                            <f:LinkButtonField Width="40px" ConfirmTarget="Parent" CommandName="Add" Icon="Add"
                                TextAlign="Center" />
                            <f:LinkButtonField Width="40px" ConfirmText="删除选中行？" ConfirmTarget="Parent" CommandName="Delete"
                                Icon="Delete" TextAlign="Center" />
                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                TextAlign="Center" />
                            <f:RenderField Width="120px" ColumnID="IndustryType" DataField="IndustryType" SortField="IndustryType"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Center" HeaderText="行业类别">
                                <Editor>
                                    <f:TextBox ID="txtIndustryType" Text='<%# Eval("IndustryType")%>' runat="server">
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                            <f:GroupField HeaderText="总体情况" TextAlign="Center" HeaderTextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="80px" ColumnID="TotalConductCount" DataField="TotalConductCount"
                                        FieldType="Int" HeaderText="举办次数" HeaderToolTip="举办次数" HeaderTextAlign="Center"
                                        TextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtTotalConductCount" NoDecimal="true" NoNegative="true" MinValue="0"
                                                Text='<%# Eval("TotalConductCount")%>' runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="80px" ColumnID="TotalPeopleCount" DataField="TotalPeopleCount"
                                        FieldType="Int" HeaderText="参演人数" HeaderToolTip="参演人数" TextAlign="Center" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtTotalPeopleCount" NoDecimal="true" NoNegative="true" MinValue="0"
                                                Text='<%# Eval("TotalPeopleCount")%>' runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="80px" ColumnID="TotalInvestment" DataField="TotalInvestment"
                                        FieldType="Double" HeaderText="直接投入" HeaderToolTip="直接投入" TextAlign="Center"
                                        HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtTotalInvestment" NoDecimal="true" NoNegative="true" MinValue="0"
                                                Text='<%# Eval("TotalInvestment")%>' runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField HeaderText="企业总部" TextAlign="Center" HeaderTextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="80px" ColumnID="HQConductCount" DataField="HQConductCount"
                                        FieldType="Int" HeaderText="举办次数" HeaderToolTip="举办次数" TextAlign="Center" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtHQConductCount" NoDecimal="true" NoNegative="true" MinValue="0"
                                                Text='<%# Eval("HQConductCount")%>' runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="80px" ColumnID="HQPeopleCount" DataField="HQPeopleCount" FieldType="Int"
                                        HeaderText="参演人数" HeaderToolTip="参演人数" TextAlign="Center" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtHQPeopleCount" NoDecimal="true" NoNegative="true" MinValue="0"
                                                Text='<%# Eval("HQPeopleCount")%>' runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="80px" ColumnID="HQInvestment" DataField="HQInvestment" FieldType="Double"
                                        HeaderText="直接投入" HeaderToolTip="直接投入" TextAlign="Center" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtHQInvestment" NoDecimal="true" NoNegative="true" MinValue="0"
                                                Text='<%# Eval("HQInvestment")%>' runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField HeaderText="基层单位" TextAlign="Center" HeaderTextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="80px" ColumnID="BasicConductCount" DataField="BasicConductCount"
                                        FieldType="Int" HeaderText="举办次数" HeaderToolTip="举办次数" TextAlign="Center" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtBasicConductCount" NoDecimal="true" NoNegative="true" MinValue="0"
                                                Text='<%# Eval("BasicConductCount")%>' runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="80px" ColumnID="BasicPeopleCount" DataField="BasicPeopleCount"
                                        FieldType="Int" HeaderText="参演人数" HeaderToolTip="参演人数" TextAlign="Center" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtBasicPeopleCount" NoDecimal="true" NoNegative="true" MinValue="0"
                                                Text='<%# Eval("BasicPeopleCount")%>' runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="80px" ColumnID="BasicInvestment" DataField="BasicInvestment"
                                        FieldType="Double" HeaderText="直接投入" HeaderToolTip="直接投入" TextAlign="Center"
                                        HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtBasicInvestment" NoDecimal="true" NoNegative="true" MinValue="0"
                                                Text='<%# Eval("BasicInvestment")%>' runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:RenderField Width="150px" ColumnID="ComprehensivePractice" DataField="ComprehensivePractice"
                                SortField="ComprehensivePractice" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Center" HeaderText="综合演练">
                                <Editor>
                                    <f:NumberBox ID="txtComprehensivePractice" NoDecimal="true" NoNegative="true" MinValue="0"
                                        Text='<%# Eval("ComprehensivePractice")%>' runat="server">
                                    </f:NumberBox>
                                </Editor>
                            </f:RenderField>
                            <f:GroupField HeaderText="其中" TextAlign="Center" HeaderTextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="120px" ColumnID="CPScene" DataField="CPScene" FieldType="String"
                                        HeaderText="现场" HeaderToolTip="现场" TextAlign="Center" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtCPScene" NoDecimal="true" NoNegative="true" MinValue="0" Text='<%# Eval("CPScene")%>'
                                                runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="120px" ColumnID="CPDesktop" DataField="CPDesktop" FieldType="String"
                                        HeaderText="桌面" HeaderToolTip="桌面" TextAlign="Center" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtCPDesktop" NoDecimal="true" NoNegative="true" MinValue="0" Text='<%# Eval("CPDesktop")%>'
                                                runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:RenderField Width="150px" ColumnID="SpecialDrill" DataField="SpecialDrill" SortField="SpecialDrill"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Center" HeaderText="专项演练">
                                <Editor>
                                    <f:TextBox ID="txtSpecialDrill" Text='<%# Eval("SpecialDrill")%>' runat="server">
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                            <f:GroupField HeaderText="其中" TextAlign="Center" HeaderTextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="120px" ColumnID="SDScene" DataField="SDScene" FieldType="String"
                                        HeaderText="现场" HeaderToolTip="现场" TextAlign="Center" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:TextBox ID="txtSDScene" Text='<%# Eval("SDScene")%>' runat="server">
                                            </f:TextBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="120px" ColumnID="SDDesktop" DataField="SDDesktop" FieldType="String"
                                        HeaderText="桌面" HeaderToolTip="桌面" TextAlign="Center" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:TextBox ID="txtSDDesktop" Text='<%# Eval("SDDesktop")%>' runat="server">
                                            </f:TextBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="100px" ColumnID="DrillConductedQuarterlyReportItemId" DataField="DrillConductedQuarterlyReportItemId"
                                        FieldType="String" HeaderText="主键" Hidden="true" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:TextBox runat="server" ID="TextBox2" Text='<%# Eval("DrillConductedQuarterlyReportItemId")%>'>
                                            </f:TextBox>
                                        </Editor>
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                        </Columns>
                    </f:Grid>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" EnableCollapse="true"
                        BodyPadding="0px">
                        <uc1:FlowOperateControl ID="ctlAuditFlow" runat="server" />
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar2" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:ToolbarFill ID="ToolbarFill2" runat="server">
                    </f:ToolbarFill>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnSubmit" Icon="SystemSaveNew" runat="server" ToolTip="提交" ValidateForms="SimpleForm1"
                        OnClick="btnSubmit_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    </form>
</body>
</html>
