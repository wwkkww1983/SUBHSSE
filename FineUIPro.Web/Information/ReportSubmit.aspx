<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportSubmit.aspx.cs" Inherits="FineUIPro.Web.Information.ReportSubmit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>办理流程</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow ColumnWidths="8% 92%">
                <Items>
                    <f:CheckBox ID="cbNext" runat="server" Checked="true" AutoPostBack="true" 
                        OnCheckedChanged="cbNext_CheckedChanged"></f:CheckBox>                    
                    <f:DropDownBox runat="server" ID="drpHandleMan" Label="下一步办理人" EmptyText="请从下拉表格中选择" 
                        MatchFieldWidth="false" LabelAlign="Left" LabelWidth="110px"
                        EnableMultiSelect="false">
                        <PopPanel>
                            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" runat="server" DataIDField="UserId" DataTextField="UserName"
                                DataKeyNames="UserId"  AllowSorting="true" SortField="UserName" SortDirection="ASC" EnableColumnLines="true"
                                Hidden="true" Width="600px" Height="420px" EnableMultiSelect="false" KeepCurrentSelection="true" PageSize="10000">
                                <Toolbars>
                                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                                        <Items>
                                            <f:TextBox runat="server" Label="查询" ID="txtUserName" EmptyText="输入查询条件" FocusOnPageLoad="true"
                                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px">
                                            </f:TextBox>                                                                     
                                        </Items>
                                    </f:Toolbar>
                                </Toolbars>
                                <Columns>
                                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" 
                                        HeaderTextAlign="Center" TextAlign="Center"/>
                                     <f:RenderField Width="90px" ColumnID="UserCode" DataField="UserCode" SortField="UserCode"
                                        FieldType="String" HeaderText="编号" HeaderTextAlign="Center" 
                                        TextAlign="Left">                        
                                    </f:RenderField>
                                    <f:RenderField Width="90px" ColumnID="UserName" DataField="UserName" 
                                        SortField="UserName" FieldType="String" HeaderText="姓名" HeaderTextAlign="Center"
                                        TextAlign="Left">                       
                                    </f:RenderField>
                                    <f:RenderField Width="90px" ColumnID="RoleName" DataField="RoleName" 
                                        SortField="RoleName" FieldType="String" HeaderText="角色" HeaderTextAlign="Center"
                                        TextAlign="Left">                       
                                    </f:RenderField>
                                    <f:RenderField Width="160px" ColumnID="IdentityCard" DataField="IdentityCard" SortField="IdentityCard" FieldType="String"
                                        HeaderText="身份证号码" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                    </f:RenderField>
                                </Columns>
                            </f:Grid>
                        </PopPanel>
                    </f:DropDownBox>
                </Items>
            </f:FormRow>    
            <f:FormRow ColumnWidths="8% 92%">
                <Items>
                    <f:CheckBox ID="cbEnd" runat="server" AutoPostBack="true" 
                        OnCheckedChanged="cbEnd_CheckedChanged" ></f:CheckBox>                   
                    <f:DropDownBox runat="server" ID="drpHandleMan2" Label="完成返回上报人" EmptyText="请从下拉表格中选择" 
                        MatchFieldWidth="false" LabelAlign="Left" LabelWidth="110px"
                        EnableMultiSelect="false">
                        <PopPanel>
                            <f:Grid ID="Grid2" ShowBorder="true" ShowHeader="false" runat="server" DataIDField="UserId" DataTextField="UserName"
                                DataKeyNames="UserId"  AllowSorting="true" SortField="UserName" SortDirection="ASC" EnableColumnLines="true"
                                Hidden="true" Width="600px" Height="420px" EnableMultiSelect="false" KeepCurrentSelection="true" PageSize="10000">
                                <Toolbars>
                                    <f:Toolbar ID="Toolbar3" Position="Top" runat="server">
                                        <Items>
                                            <f:TextBox runat="server" Label="查询" ID="TextBox1" EmptyText="输入查询条件" FocusOnPageLoad="true"
                                                AutoPostBack="true" OnTextChanged="TextBox1_TextChanged" Width="250px" LabelWidth="80px">
                                            </f:TextBox>                                                                     
                                        </Items>
                                    </f:Toolbar>
                                </Toolbars>
                                <Columns>
                                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" 
                                        HeaderTextAlign="Center" TextAlign="Center"/>
                                     <f:RenderField Width="90px" ColumnID="UserCode" DataField="UserCode" SortField="UserCode"
                                        FieldType="String" HeaderText="编号" HeaderTextAlign="Center" 
                                        TextAlign="Left">                        
                                    </f:RenderField>
                                    <f:RenderField Width="90px" ColumnID="UserName" DataField="UserName" 
                                        SortField="UserName" FieldType="String" HeaderText="姓名" HeaderTextAlign="Center"
                                        TextAlign="Left">                       
                                    </f:RenderField>
                                    <f:RenderField Width="90px" ColumnID="RoleName" DataField="RoleName" 
                                        SortField="RoleName" FieldType="String" HeaderText="角色" HeaderTextAlign="Center"
                                        TextAlign="Left">                       
                                    </f:RenderField>
                                    <f:RenderField Width="160px" ColumnID="IdentityCard" DataField="IdentityCard" SortField="IdentityCard" FieldType="String"
                                        HeaderText="身份证号码" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                    </f:RenderField>
                                </Columns>
                            </f:Grid>
                        </PopPanel>
                    </f:DropDownBox>
                </Items>
            </f:FormRow>   
            <f:FormRow ColumnWidths="8% 92%">
                <Items>
                     <f:Label runat="server" Text="办理意见"></f:Label>
                     <f:TextArea ID="txtOpinion" runat="server" Height="70px" MaxLength="1000">
                    </f:TextArea>
                </Items>
            </f:FormRow>            
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="确认" ValidateForms="SimpleForm1" 
                        OnClick="btnSave_Click">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    </form>
</body>
</html>
