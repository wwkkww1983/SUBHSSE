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
            <f:FormRow ColumnWidths="13% 87%">
                <Items>
                    <f:CheckBox ID="cbNext" runat="server" Checked="true" AutoPostBack="true" 
                        OnCheckedChanged="cbNext_CheckedChanged"></f:CheckBox>
                    <f:DropDownList ID="drpHandleMan" Label="下一步办理人" LabelWidth="110" AutoPostBack="false" EnableSimulateTree="true" 
                        runat="server" EnableEdit="true">
                    </f:DropDownList>
                </Items>
            </f:FormRow>    
            <f:FormRow ColumnWidths="13% 87%">
                <Items>
                    <f:CheckBox ID="cbEnd" runat="server" AutoPostBack="true" 
                        OnCheckedChanged="cbEnd_CheckedChanged" ></f:CheckBox>
                    <f:DropDownList ID="drpHandleMan2" Label="完成返回上报人" LabelWidth="110" AutoPostBack="false" EnableSimulateTree="true" 
                        runat="server" EnableEdit="true">
                    </f:DropDownList>
                </Items>
            </f:FormRow>   
            <f:FormRow ColumnWidths="13% 87%">
                <Items>
                     <f:Label runat="server" Text="办理意见"></f:Label>
                     <f:TextArea ID="txtOpinion" runat="server" Height="70px" MaxLength="1000">
                    </f:TextArea>
                </Items>
            </f:FormRow>            
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="确认" ValidateForms="SimpleForm1" 
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
