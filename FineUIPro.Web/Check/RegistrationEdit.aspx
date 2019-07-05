<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistrationEdit.aspx.cs"
    Inherits="FineUIPro.Web.Check.RegistrationEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>隐患巡检（手机端）</title>
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
                    <f:DropDownList runat="server" ID="drpWorkArea" Label="区域"
                        EnableEdit="true" Required="true" ShowRedStar="true"></f:DropDownList>
                    <f:DropDownList runat="server" ID="drpResponsibilityUnit" Label="责任单位"
                        EnableEdit="true" Required="true" ShowRedStar="true" AutoPostBack="true" 
                        OnSelectedIndexChanged="drpResponsibilityUnit_SelectedIndexChanged"></f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtProblemTypes" runat="server" Label="问题类型" MaxLength="200">
                    </f:TextBox>
                     <f:DropDownList runat="server" ID="drpResponsibilityMan" Label="责任人"
                        EnableEdit="true" Required="true" ShowRedStar="true"></f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtProblemDescription" runat="server" Label="问题描述" MaxLength="800">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtTakeSteps" runat="server" Label="采取措施" MaxLength="500">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="50% 25% 25%">
                <Items>
                    <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="整改期限" 
                        ID="txtRectificationPeriod">
                    </f:DatePicker>
                    <f:NumberBox ID="txtH" runat="server" MinValue="0" MaxValue="24" 
                    NoDecimal="true" NoNegative="true" LabelWidth="25" LabelAlign="Right" Label="时">
                    </f:NumberBox>                   
                     <f:NumberBox ID="txtM" runat="server" MinValue="0" MaxValue="60" 
                     NoDecimal="true" NoNegative="true" LabelWidth="25" LabelAlign="Right" Label="分">
                     </f:NumberBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtCheckManName" runat="server" Label="检查人" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtCheckTime" runat="server" Label="检查时间" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtStates" runat="server" Label="状态" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="8% 92%">
                <Items>
                    <f:Label runat="server" ID="lblImageUrl" Label="整改前图片">
                    </f:Label>
                    <f:ContentPanel ID="ContentPanel2" runat="server" ShowHeader="false" ShowBorder="false"
                        Title="整改前图片">
                        <table>
                            <tr style="height: 28px">
                                <td align="left">
                                    <div id="divImageUrl" runat="server">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                    </f:ToolbarFill>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false"  runat="server" Icon="SystemClose" MarginRight="10px">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    </form>
</body>
</html>
