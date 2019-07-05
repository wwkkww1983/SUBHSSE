<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DrillPlanHalfYearReportAdd.aspx.cs"
    Async="true" Inherits="FineUIPro.Web.Information.DrillPlanHalfYearReportAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑应急演练工作计划半年报表</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="false"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">      
        <Rows>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="ddlUnitId" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUnitId_SelectedIndexChanged"
                        Label="单位" Required="True" ShowRedStar="True" FocusOnPageLoad="true">
                    </f:DropDownList>
                    <f:DropDownList ID="ddlYearId" runat="server" Label="年度" Required="True" ShowRedStar="True">
                    </f:DropDownList>
                    <f:DropDownList ID="ddlHalfYearId" runat="server" Label="半年" Required="True" ShowRedStar="True">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtCompileMan" runat="server" Label="联系人" MaxLength="50" Required="True"
                        ShowRedStar="True">
                    </f:TextBox>
                    <f:TextBox ID="txtTel" runat="server" MaxLength="50" Label="联系电话">
                    </f:TextBox>
                    <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="制表时间" ID="dpkCompileDate"
                        Required="True" ShowRedStar="True">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server"
                        BoxFlex="1" DataKeyNames="DrillPlanHalfYearReportItemId" AllowCellEditing="true"
                        ClicksToEdit="1" DataIDField="DrillPlanHalfYearReportItemId" EnableColumnLines="true"
                        OnRowCommand="Grid1_RowCommand" EnableHeaderMenu="false" Width="1300px" Height="450px">
                        <Columns>
                            <f:LinkButtonField Width="40px" ConfirmTarget="Top" CommandName="Add" Icon="Add"
                                TextAlign="Center" />
                            <f:LinkButtonField Width="40px" ConfirmText="删除选中行？" ConfirmTarget="Top" CommandName="Delete"
                                Icon="Delete" TextAlign="Center" />
                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                            <f:RenderField Width="180px" ColumnID="DrillPlanName" DataField="DrillPlanName" SortField="IndustryType"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="演练名称">
                                <Editor>
                                    <f:TextBox ID="txtDrillPlanName" MaxLength="200" Text='<%# Eval("DrillPlanName")%>'
                                        runat="server">
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="OrganizationUnit" DataField="OrganizationUnit"
                                SortField="IndustryType" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                HeaderText="组织单位">
                                <Editor>
                                    <f:TextBox ID="txtOrganizationUnit" MaxLength="100" Text='<%# Eval("OrganizationUnit")%>'
                                        runat="server">
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="DrillPlanDate" DataField="DrillPlanDate" SortField="IndustryType"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="演练时间">
                                <Editor>
                                    <f:TextBox ID="txtDrillPlanDate" MaxLength="50" Text='<%# Eval("DrillPlanDate")%>'
                                        runat="server">
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="380px" ColumnID="AccidentScene" DataField="AccidentScene" SortField="IndustryType"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="主要事故情景">
                                <Editor>
                                    <f:TextBox ID="txtAccidentScene" MaxLength="1000" Text='<%# Eval("AccidentScene")%>'
                                        runat="server">
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="200px" ColumnID="ExerciseWay" DataField="ExerciseWay" SortField="IndustryType"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="演练方式">
                                <Editor>
                                    <f:TextBox ID="txtExerciseWay" MaxLength="50" Text='<%# Eval("ExerciseWay")%>' runat="server">
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="DrillPlanHalfYearReportItemId" DataField="DrillPlanHalfYearReportItemId"
                                FieldType="String" HeaderText="主键" Hidden="true" HeaderTextAlign="Center">
                                <Editor>
                                    <f:TextBox runat="server" ID="txtDrillPlanHalfYearReportItemId" Text='<%# Eval("DrillPlanHalfYearReportItemId")%>'>
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                        </Columns>
                    </f:Grid>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server" Margin="0 50 30 50">
                <Items>
                     <f:Button ID="btnCopy" Icon="Database" runat="server" ToolTip="复制上半年数据" ValidateForms="SimpleForm1"
                        OnClick="btnCopy_Click" Hidden="true">
                    </f:Button>
                    <f:ToolbarFill runat="server"></f:ToolbarFill>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" Hidden="true" 
                        ValidateForms="SimpleForm1" OnClick="btnSave_Click" ToolTip="保存">
                    </f:Button>
                    <f:Button ID="btnSubmit" Icon="SystemSaveNew" runat="server" Hidden="true" ToolTip="提交"
                        ValidateForms="SimpleForm1" OnClick="btnSubmit_Click">
                    </f:Button>
                    <f:Button ID="btnUpdata" Icon="PageSave" runat="server" Hidden="true" ConfirmText="确定上报？"
                        ToolTip="上报" ValidateForms="SimpleForm1" OnClick="btnUpdata_Click">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    <f:Window ID="Window1" IconUrl="~/res/images/16/11.png" runat="server" Hidden="true"
        IsModal="false" Target="Parent" EnableMaximize="true" EnableResize="true" OnClose="Window1_Close"
        Title="办理流程" CloseAction="HidePostBack" EnableIFrame="true" Height="250px" Width="500px">
    </f:Window>
    </form>
</body>
</html>
