<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubUnitCheckRectifyEdit.aspx.cs"
    Inherits="FineUIPro.Web.Supervise.SubUnitCheckRectifyEdit" Async="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>安全监督检查报告</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .labcenter
        {
            text-align: center;
        }
        .f-grid-row .f-grid-cell-inner
        {
            white-space: normal;
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        Title="安全监督检查报告" BodyPadding="10px" runat="server" RedStarPosition="BeforeText"
        LabelAlign="Right">
        <Rows>
            <f:FormRow ColumnWidths="50% 5% 25% 20%">
                <Items>
                    <f:DropDownList ID="drpUnitId" runat="server" Label="单位" LabelWidth="70px">
                    </f:DropDownList>
                    <f:DropDownList ID="drpCheckRectType" runat="server" Label="评价类型" AutoSelectFirstItem="False"
                        EmptyText="-请选择评价类型-" Hidden="true">
                    </f:DropDownList>
                    <f:DatePicker ID="dpkUpDateTime" runat="server" Label="检查开始时间"  LabelWidth="120px">
                    </f:DatePicker>
                    <f:DatePicker ID="dpkCheckEndDate" runat="server" Label="结束时间"  LabelWidth="80px">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtValues1" runat="server" Label="一、检查目的" MaxLength="4000" LabelWidth="130px"
                        LabelAlign="Left">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtValues2" runat="server" Label="二、依据" MaxLength="4000" LabelWidth="130px"
                        LabelAlign="Left">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtValues3" runat="server" Label="三、本次检查概况" MaxLength="4000"
                        LabelWidth="130px" LabelAlign="Left">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtValues4" runat="server" Label="四、符合项" MaxLength="4000" LabelWidth="130px"
                        LabelAlign="Left">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtValues5" runat="server" Label="五、不符合项" MaxLength="4000" LabelWidth="130px"
                        LabelAlign="Left">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtValues6" runat="server" Label="六、观察项" MaxLength="4000" LabelWidth="130px"
                        LabelAlign="Left" Hidden="true">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtValues7" runat="server" Label="六、改进建议" MaxLength="4000" LabelWidth="130px"
                        LabelAlign="Left">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtValues8" runat="server" Label="七、检查结论" MaxLength="4000" LabelWidth="130px"
                        LabelAlign="Left">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Label ID="Label6" runat="server" Label="八、检查工作组成员" LabelWidth="150px" LabelAlign="Left">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server"
                        BoxFlex="1" DataKeyNames="SubUnitCheckRectifyItemId" AllowCellEditing="true" SortField="Name" AllowSorting="true"
                        ClicksToEdit="1" DataIDField="SubUnitCheckRectifyItemId" EnableColumnLines="true"
                        OnRowCommand="Grid1_RowCommand" EnableHeaderMenu="false" Width="1300px" Height="300px">
                        <Columns>
                            <f:LinkButtonField Width="40px" ConfirmTarget="Parent" CommandName="Add" Icon="Add"
                                TextAlign="Center" ToolTip="新增" />
                            <f:LinkButtonField Width="40px" ConfirmText="删除选中行？" ConfirmTarget="Parent" CommandName="Delete"
                                Icon="Delete" TextAlign="Center" ToolTip="删除" />
                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                            <f:RenderField Width="85px" ColumnID="Name" DataField="Name" SortField="Name" FieldType="String"
                                HeaderTextAlign="Center" TextAlign="Center" HeaderText="姓名">
                                <Editor>
                                    <f:TextBox ID="txtName" MaxLength="50" Text='<%# Eval("Name")%>' runat="server">
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="70px" ColumnID="Sex" DataField="Sex" SortField="Sex" FieldType="String"
                                HeaderTextAlign="Center" TextAlign="Center" HeaderText="性别">
                                <Editor>
                                    <f:TextBox ID="txtSex" MaxLength="50" Text='<%# Eval("Sex")%>' runat="server">
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="200px" ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="所在单位" ExpandUnusedSpace="true">
                                <Editor>
                                    <f:TextBox ID="txtUnitName" MaxLength="500" Text='<%# Eval("UnitName")%>' runat="server">
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="120px" ColumnID="PostName" DataField="PostName" SortField="PostName"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Center" HeaderText="所在单位职务">
                                <Editor>
                                    <f:TextBox ID="txtPostName" MaxLength="200" Text='<%# Eval("PostName")%>' runat="server">
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="110px" ColumnID="WorkTitle" DataField="WorkTitle" SortField="WorkTitle"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Center" HeaderText="职称">
                                <Editor>
                                    <f:TextBox ID="txtWorkTitle" MaxLength="50" Text='<%# Eval("WorkTitle")%>' runat="server">
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="120px" ColumnID="CheckPostName" DataField="CheckPostName" SortField="CheckPostName"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Center" HeaderText="检查工作组职务">
                                <Editor>
                                    <f:TextBox ID="txtCheckPostName" MaxLength="50" Text='<%# Eval("CheckPostName")%>'
                                        runat="server">
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="110px" ColumnID="CheckDate" DataField="CheckDate" SortField="CheckDate"
                                FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderTextAlign="Center"
                                TextAlign="Center" HeaderText="检查日期">
                                <Editor>
                                    <f:DatePicker ID="dpkCheckDate" runat="server" Text='<%# Eval("CheckDate") %>'>
                                    </f:DatePicker>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="SubUnitCheckRectifyItemId" DataField="SubUnitCheckRectifyItemId"
                                FieldType="String" HeaderText="主键" Hidden="true" HeaderTextAlign="Center">
                                <Editor>
                                    <f:TextBox runat="server" ID="txtSubUnitCheckRectifyItemId" Text='<%# Eval("SubUnitCheckRectifyItemId")%>'>
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                        </Columns>
                    </f:Grid>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:FileUpload runat="server" ID="fuAttachUrl" EmptyText="请上传附件" Label="附件">
                    </f:FileUpload>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Label runat="server" ID="lbAttachUrl" BoxConfigPosition="Right" MarginLeft="120">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="85% 5% 5% 5%">
                 <Items>
                    <f:Label runat="server"></f:Label>
                    <f:Button ID="btnUpAttachUrl" Icon="Tick" runat="server"  OnClick="btnUpAttachUrl_Click"
                        ValidateForms="SimpleForm1" ToolTip="上传附件" Hidden="false">
                    </f:Button>
                    <f:Button ID="btnDeleteAttachUrl" Icon="Delete" runat="server"  OnClick="btnDeleteAttachUrl_Click"
                        ToolTip="删除附件" Hidden="false">
                    </f:Button>
                    <f:Button ID="btnSeeAttachUrl" Icon="Find" runat="server" OnClick="btnSeeAttachUrl_Click"
                        ToolTip="查看附件">
                    </f:Button>
                   </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click" Hidden="true">
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
