<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrainingUpload.aspx.cs"
    Inherits="FineUIPro.Web.EduTrain.TrainingUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>上传资源</title>
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
                                    <f:TextBox ID="txtTrainingItemCode" runat="server" Label="教材编号" Required="true" ShowRedStar="true"
                                        FocusOnPageLoad="true" MaxLength="50" LabelAlign="Right">
                                    </f:TextBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:TextBox ID="txtTrainingItemName" runat="server" Label="教材名称" Required="true" ShowRedStar="true"
                                        MaxLength="50" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" LabelAlign="Right">
                                    </f:TextBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:Label ID="txtCompileMan" runat="server" Label="整理人" LabelAlign="Right">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:Label runat="server" Label="整理时间" ID="txtCompileDate" LabelAlign="Right">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:Label runat="server" ID="lbAttachUrl" BoxConfigPosition="Left" MarginLeft="120">
                                    </f:Label>
                                    <f:Button ID="btnUploadResources" Text="上传附件" Icon="SystemNew" runat="server" OnClick="btnUploadResources_Click"
                                        ValidateForms="SimpleForm1">
                                    </f:Button>
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
                            <f:Button ID="btnDelete" ToolTip="删除" Icon="Delete" ConfirmText="确定删除当前数据？" OnClick="btnDelete_Click"
                                runat="server" Hidden="true">
                            </f:Button>
                            <f:Button ID="btnSave" Icon="SystemSave" runat="server" ValidateForms="SimpleForm1"
                                OnClick="btnSave_Click" Hidden="true">
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
