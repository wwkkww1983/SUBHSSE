<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SafetyDataPlanEdit.aspx.cs" Inherits="FineUIPro.Web.SafetyData.SafetyDataPlanEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />    
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageSafetyData1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtTitle" runat="server" Label="资料名称"  Readonly="true">
                    </f:TextBox>
                     <f:TextBox ID="txtCode" runat="server" Label="编码"  Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DatePicker ID="txtStartDate" runat="server" Label="开始日期" LabelAlign="Right"  
                        EnableEdit="true">
                    </f:DatePicker>  
                        <f:DatePicker ID="txtEndDate" runat="server" Label="结束日期" LabelAlign="Right"  
                        EnableEdit="true">
                    </f:DatePicker>
                </Items>
            </f:FormRow>      
             <f:FormRow >                
                <Items>                    
                    <f:DatePicker ID="txtCheckDate" runat="server" Label="考核截止日期" LabelAlign="Right"  
                        EnableEdit="true">
                    </f:DatePicker>
                     <f:DatePicker ID="txtReminderDate" runat="server" Label="提醒日期" LabelAlign="Right"  
                        EnableEdit="true">
                    </f:DatePicker> 
                </Items>
            </f:FormRow>       
            <f:FormRow >                
                <Items>
                    <f:NumberBox ID="txtShouldScore" runat="server" Label="应得分" LabelAlign="Right" DecimalPrecision="1" >
                    </f:NumberBox>
                    <f:Label ID="lmb" runat="server"></f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtRemark" runat="server" Label="备注">
                    </f:TextArea>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Center" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
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
