<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckTypeSet.aspx.cs" Inherits="FineUIPro.Web.Administrative.CheckTypeSet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" Title="行政管理检查类别"  ShowHeader="false"
        Layout="HBox">
        <Items>
            <f:Tree ID="tvCheckTypeSet" Width="700px" EnableCollapse="true" ShowBorder="true"
                ShowHeader="false" EnableIcons="False" AutoScroll="true" EnableSingleClickExpand="true"
                Expanded="true" Title="行政管理检查类别" AutoLeafIdentification="true" runat="server"
                EnableTextSelection="True" OnNodeCommand="tvCheckTypeSet_NodeCommand">
            </f:Tree>
            <f:SimpleForm ID="SimpleForm1" runat="server" ShowBorder="true" ShowHeader="false"
                LabelWidth="100px" BodyPadding="5px" Width="400px">
                <Items>
                    <f:TextBox ID="txtCheckTypeCode" Label="检查类别编码" ShowRedStar="true" Required="true"
                        runat="server" LabelAlign="right" FocusOnPageLoad="true" LabelWidth="120px">
                    </f:TextBox>
                    <f:TextArea ID="txtCheckTypeContent" runat="server" Label="检查类别内容" ShowRedStar="true"
                        Required="true" LabelAlign="right" LabelWidth="120px">
                    </f:TextArea>
                    <f:NumberBox ID="txtSortIndex" runat="server" Label="排列序号" ShowRedStar="true" Required="true"
                        LabelAlign="right" LabelWidth="120px" NoDecimal="true" NoNegative="true">
                    </f:NumberBox>
                    <f:DropDownList ID="drpIsEndLevel" runat="server" Label="是否末级" LabelAlign="right"
                        LabelWidth="120px">
                        <f:ListItem Value="True" Text="是" />
                        <f:ListItem Value="False" Text="否" />
                    </f:DropDownList>
                </Items>
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Bottom" runat="server">
                        <Items>
                            <f:Button ID="btnNew" Text="" Icon="Add" ToolTip="新增" runat="server" OnClick="btnNew_Click">
                            </f:Button>
                            <f:Button ID="btnDelete" Text="" ToolTip="删除" Icon="Delete" ConfirmText="确定删除当前数据？"
                                OnClick="btnDelete_Click" runat="server">
                            </f:Button>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1"
                                OnClick="btnSave_Click">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
            </f:SimpleForm>
        </Items>
    </f:Panel>
    </form>
</body>
</html>
