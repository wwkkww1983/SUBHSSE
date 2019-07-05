<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViolationPersonEdit.aspx.cs"
    Inherits="FineUIPro.Web.Check.ViolationPersonEdit" ValidateRequest="false" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑违规人员记录</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="违规人员记录" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtViolationPersonCode" runat="server" Label="编号" LabelAlign="Right"
                        MaxLength="50" Readonly="true">
                    </f:TextBox>
                    <f:DropDownBox runat="server" ID="drpPersonId" Label="人员姓名" EmptyText="请从下拉表格中选择"
                        MatchFieldWidth="false" LabelAlign="Right" AutoPostBack="true" OnTextChanged="drpPersonId_TextChanged"
                        EnableMultiSelect="false" Required="true" ShowRedStar="true" LabelWidth="120px">
                        <PopPanel>
                            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" runat="server" DataIDField="PersonId"
                                DataTextField="PersonName" DataKeyNames="PersonId" AllowSorting="true" SortField="UnitName,CardNo"
                                SortDirection="ASC" EnableColumnLines="true" Hidden="true" Width="800px" OnSort="Grid1_Sort"
                                Height="350px" EnableMultiSelect="false" PageSize="10000"> 
                                <Toolbars>
                                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                                        <Items>
                                            <f:TextBox runat="server" Label="单位" ID="txtSelectUnitName" EmptyText="输入查询条件" AutoPostBack="true"
                                                OnTextChanged="TextBox_TextChanged" Width="210px" LabelWidth="60px" LabelAlign="Right">
                                            </f:TextBox>
                                            <f:TextBox runat="server" Label="卡号" ID="txtCardNo" EmptyText="输入查询条件" AutoPostBack="true"
                                                OnTextChanged="TextBox_TextChanged" Width="210px" LabelWidth="60px" LabelAlign="Right">
                                            </f:TextBox>
                                            <f:TextBox runat="server" Label="姓名" ID="txtPersonName" EmptyText="输入查询条件" AutoPostBack="true"
                                                OnTextChanged="TextBox_TextChanged" Width="210px" LabelWidth="60px" LabelAlign="Right">
                                            </f:TextBox>
                                        </Items>
                                    </f:Toolbar>
                                </Toolbars>
                                <Columns>
                                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                        TextAlign="Center" />
                                     <f:RenderField Width="200px" ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                                        FieldType="String" HeaderText="所在单位" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                    </f:RenderField>
                                    <f:RenderField Width="150px" ColumnID="CardNo" DataField="CardNo" SortField="CardNo"
                                        FieldType="String" HeaderText="卡号" HeaderTextAlign="Center" TextAlign="Left"
                                        ExpandUnusedSpace="true">
                                    </f:RenderField>
                                    <f:RenderField Width="100px" ColumnID="PersonName" DataField="PersonName" EnableFilter="true"
                                        SortField="PersonName" FieldType="String" HeaderText="姓名" HeaderTextAlign="Center"
                                        TextAlign="Left">
                                    </f:RenderField>
                                    <f:RenderField Width="120px" ColumnID="WorkPostName" DataField="WorkPostName" SortField="WorkPostName"
                                        FieldType="String" HeaderText="岗位" HeaderTextAlign="Center" TextAlign="Left">
                                    </f:RenderField>
                                </Columns>
                            </f:Grid>
                        </PopPanel>
                    </f:DropDownBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtUnitName" runat="server" Label="单位名称" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtWorkPostName" runat="server" Label="岗位" LabelAlign="Right" Readonly="true"
                        LabelWidth="120px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:HiddenField ID="hdUnitId" runat="server">
                    </f:HiddenField>
                    <f:HiddenField ID="hdWorkPostId" runat="server">
                    </f:HiddenField>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DatePicker ID="txtViolationDate" runat="server" Label="违规日期">
                    </f:DatePicker>
                    <f:DropDownList ID="drpViolationName" runat="server" Label="违章名称" EnableEdit="true"
                        LabelWidth="120px" AutoPostBack="true" OnSelectedIndexChanged="drpViolationName_SelectedIndexChanged">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpViolationType" runat="server" Label="违章类型" EnableEdit="true">
                    </f:DropDownList>
                    <f:DropDownList ID="drpHandleStep" runat="server" Label="处理措施" EnableEdit="true"
                        ShowRedStar="true" Required="true" LabelWidth="120px">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtViolationDef" runat="server" Height="60px" Label="违章描述">
                    </f:TextArea>
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
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
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
