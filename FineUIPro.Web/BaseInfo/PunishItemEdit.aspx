<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PunishItemEdit.aspx.cs" Inherits="FineUIPro.Web.BaseInfo.PunishItemEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑处罚项信息</title>
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
                    <f:TextBox ID="txtPunishItemCode" runat="server" Label="编号" MaxLength="50" ShowRedStar="true"
                        Required="true" FocusOnPageLoad="true">
                    </f:TextBox>
                    <f:RadioButtonList runat="server" ID="rblPunishItemType" Label="类型" ShowRedStar="true"
                        Required="true">
                        <f:RadioItem Value="1" Text="安全" Selected="true" />
                        <f:RadioItem Value="2" Text="质量" />
                    </f:RadioButtonList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtPunishItemContent" runat="server" Label="处罚内容" ShowRedStar="true"
                        Required="true">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpDeduction" runat="server" ShowRedStar="true" Required="true"
                        EnableEdit="true" Label="扣分">
                        <f:ListItem Value="1" Text="一般（1）" Selected="true" />
                        <f:ListItem Value="3" Text="较重（3）" />
                        <f:ListItem Value="6" Text="严重（6）" />
                        <f:ListItem Value="9" Text="非常严重（9）" />
                        <f:ListItem Value="12" Text="极其严重（12）" />
                    </f:DropDownList>
                    <f:NumberBox ID="txtPunishMoney" runat="server" Label="罚款" NoNegative="true" NoDecimal="true">
                    </f:NumberBox>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" Text="保存数据" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" Text="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    </form>
</body>
</html>
