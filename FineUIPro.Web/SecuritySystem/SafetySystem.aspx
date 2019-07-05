<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SafetySystem.aspx.cs" Async="true"
    Inherits="FineUIPro.Web.SecuritySystem.SafetySystem" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>项目组织体系</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Panel1" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
            <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                Layout="HBox" EnableCollapse="true" Width="300" Title="项目组织体系" TitleToolTip="项目组织体系"
                ShowBorder="true" ShowHeader="true" AutoScroll="true" BodyPadding="5px" IconFont="ArrowCircleLeft">
                <Items>
                    <f:Tree ID="trSafetySystem" EnableCollapse="true" ShowHeader="false" ShowBorder="false"
                        EnableIcons="true" Title="项目组织体系" OnNodeCommand="trSafetySystem_NodeCommand"
                        AutoLeafIdentification="true" AutoScroll="true" runat="server" EnableTextSelection="True"
                        EnableSingleClickExpand="true">
                    </f:Tree>
                </Items>
            </f:Panel>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="项目组织体系"
                TitleToolTip="项目组织体系" AutoScroll="true">
                <Items>
                    <f:HtmlEditor runat="server" Label="详细" ID="txtSeeFile" ShowLabel="false" Editor="UMEditor"
                        BasePath="~/res/umeditor/" ToolbarSet="Full" Height="500">
                    </f:HtmlEditor>
                </Items>
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Bottom" ToolbarAlign="Right" runat="server" Height="30px">
                        <Items>
                            <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                                OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1" MarginLeft="5px">
                            </f:Button>
                            <f:ToolbarFill ID="ToolbarFill2" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" Hidden="true"
                                OnClick="btnSave_Click">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
            </f:Panel>
        </Items>
    </f:Panel>
    </form>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
</body>
</html>
