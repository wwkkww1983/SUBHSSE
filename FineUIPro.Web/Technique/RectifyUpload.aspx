<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RectifyUpload.aspx.cs"
    Inherits="FineUIPro.Web.Technique.RectifyUpload" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>安全隐患资源上传</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .black
        {
            color: Black;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
            <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                EnableCollapse="true" Width="200" Title="上传列表" TitleToolTip="上传列表" ShowBorder="true"
                ShowHeader="false" AutoScroll="true" BodyPadding="0 2 0 0" Layout="Fit" IconFont="ArrowCircleLeft">
                <Items>
                    <f:Tree ID="tvUploadResources" Width="220px" EnableCollapse="true" ShowHeader="false"
                        Title="上传列表" OnNodeCommand="tvUploadResources_NodeCommand" AutoLeafIdentification="true"
                        runat="server" EnableIcons="true" AutoScroll="true" EnableSingleClickExpand="true">
                    </f:Tree>
                </Items>
            </f:Panel>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="资源明细" TitleToolTip="资源明细"
                AutoScroll="true">
                <Items>
                    <f:Form BodyPadding="5px" ID="SimpleForm1" Layout="VBox" EnableCollapse="false" runat="server"
                        Title="资源明细" IconFont="Anchor" ShowHeader="false">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:Label ID="lblRectifyName" runat="server" Label="作业类别">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:TextArea ID="txtHazardSourcePoint" runat="server" Label="隐患源点" Height="60px" MaxLength="150">
                                    </f:TextArea>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:TextArea ID="txtRiskAnalysis" runat="server" Label="风险分析" Height="70px" MaxLength="500">
                                    </f:TextArea>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:TextArea ID="txtRiskPrevention" runat="server" Label="风险防范" Height="70px" MaxLength="150">
                                    </f:TextArea>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:TextArea ID="txtSimilarRisk" runat="server" Label="同类风险" Height="80px" MaxLength="500">
                                    </f:TextArea>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:Label ID="txtCompileMan" runat="server" Label="整理人">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:Label runat="server" Label="整理时间" ID="txtCompileDate">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Right" runat="server">
                        <Items>
                            <f:Button ID="btnNew" Icon="Add" ToolTip="新增" runat="server" OnClick="btnNew_Click"
                                Hidden="true">
                            </f:Button>
                            <f:Button ID="btnDelete" ToolTip="删除" Icon="Delete" ConfirmText="确定删除当前数据？" Hidden="true"
                                OnClick="btnDelete_Click" runat="server">
                            </f:Button>
                            <f:Button ID="btnSave" Icon="SystemSave" runat="server" ValidateForms="SimpleForm1"
                                Hidden="true" OnClick="btnSave_Click">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
            </f:Panel>
        </Items>
    </f:Panel>
    </form>
</body>
</html>
