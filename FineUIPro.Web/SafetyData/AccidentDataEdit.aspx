<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccidentDataEdit.aspx.cs" Inherits="FineUIPro.Web.SafetyData.AccidentDataEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
                    <f:DropDownList ID="drpProject" Label="项目名称" AutoPostBack="false" EnableSimulateTree="true"
                        runat="server">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpAccidentType" Label="事故类型" AutoPostBack="false" EnableSimulateTree="true"
                        runat="server">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                   <f:DatePicker ID="txtAccidentDate" runat="server" Label="清零日期" LabelAlign="Right" ShowRedStar="true" Required="true"
                        EnableEdit="true">
                    </f:DatePicker>
                </Items>
            </f:FormRow>         
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click" >
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
