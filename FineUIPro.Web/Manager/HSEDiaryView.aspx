<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HSEDiaryView.aspx.cs"
    Inherits="FineUIPro.Web.Manager.HSEDiaryView" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HSE日志</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
     <style type="text/css">
        .formtitle .f-field-body {
            text-align: center;           
            margin: 10px 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="false" AutoScroll="true" TitleAlign="Center"
        BodyPadding="5px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right" EnableTableStyle="true">
        <Rows>
             <f:FormRow>
                 <Items>
                    <f:TextBox ID="txtDiaryDate" runat="server" Label="日期" Readonly="true" LabelWidth="90px">
                    </f:TextBox>
                      <f:TextBox ID="txtUserName" runat="server" Label="工程师" Readonly="true" LabelWidth="90px">
                    </f:TextBox>                       
                 </Items>
             </f:FormRow>
            <f:FormRow ColumnWidths="40% 30% 30%"> 
                 <Items>
                    <f:TextBox ID="TextBox1" runat="server" Text="1HSE检查情况及检查次数" Readonly="true" LabelWidth="90px">
                    </f:TextBox>
                      <f:TextBox ID="txtValue1" runat="server" Text="0" Readonly="true" LabelWidth="90px">
                    </f:TextBox>   
                     <f:TextBox ID="TextBox3" runat="server" Text="来源于“安全检查管理”中各类型检查。" Readonly="true" LabelWidth="90px">
                    </f:TextBox>   
                 </Items>
             </f:FormRow>
             <f:FormRow>
                 <Items>
                    <f:TextBox ID="txtDailySummary" runat="server" Label="今日小结" Readonly="true" LabelWidth="90px">
                    </f:TextBox>                    
                 </Items>
             </f:FormRow> 
             <f:FormRow>
                 <Items>
                    <f:TextBox ID="txtTomorrowPlan" runat="server" Label="明日计划" Readonly="true" LabelWidth="90px">
                    </f:TextBox>                    
                 </Items>
             </f:FormRow> 
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                    </f:ToolbarFill>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose" MarginRight="10px">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    </form>
</body>
</html>
