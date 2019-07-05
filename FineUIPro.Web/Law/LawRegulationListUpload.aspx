<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LawRegulationListUpload.aspx.cs"
    Inherits="FineUIPro.Web.Law.LawRegulationListUpload" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>法律法规上报</title>
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
                                    <f:TextBox ID="txtLawRegulationCode" runat="server" FocusOnPageLoad="true" Label="编号"
                                        Required="true" ShowRedStar="true" MaxLength="50">
                                    </f:TextBox>
                                    <f:DropDownList ID="ddlLawsRegulationsTypeId" runat="server" Label="类别">
                                    </f:DropDownList>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:TextBox ID="txtLawRegulationName" runat="server" Label="名称" Required="true" ShowRedStar="true"
                                        MaxLength="50" AutoPostBack="true" OnTextChanged="TextBox_TextChanged">
                                    </f:TextBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="批准日" EmptyText="请选择批准日"
                                        ID="dpkApprovalDate">
                                    </f:DatePicker>
                                    <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="生效日" ID="dpkEffectiveDate"
                                        EmptyText="请选择生效日">
                                    </f:DatePicker>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:TextArea ID="txtDescription" runat="server" Label="简介及重点关注条款" MaxLength="1000">
                                    </f:TextArea>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:Label ID="txtCompileMan" runat="server" Label="整理人">
                                    </f:Label>
                                    <f:Label ID="txtCompileDate" runat="server" Label="整理时间">
                                    </f:Label>
                                </Items>
                            </f:FormRow>                          
                            <f:FormRow>
                                <Items>
                                    <f:Label runat="server" ID="lbAttachUrl" BoxConfigPosition="Right" MarginLeft="120">
                                    </f:Label>
                                    <f:Button ID="btnUploadResources" Text="上传附件" Icon="SystemNew" runat="server" 
                                        OnClick="btnUploadResources_Click" ValidateForms="SimpleForm1">
                                    </f:Button>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Right" runat="server">
                        <Items>
                            <f:Button ID="btnNew"  Icon="Add" ToolTip="新增" runat="server" OnClick="btnNew_Click"
                                Hidden="true">
                            </f:Button>
                            <f:Button ID="btnDelete"  ToolTip="删除" Icon="Delete" ConfirmText="确定删除当前数据？"
                                OnClick="btnDelete_Click" runat="server" Hidden="true">
                            </f:Button>
                            <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1"
                                OnClick="btnSave_Click" Hidden="true">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
            </f:Panel>
        </Items>
    </f:Panel>
    </form>
     <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true"
            EnableMaximize="true" Target="Parent" EnableResize="true" runat="server"
            IsModal="true" Width="700px" Height="500px">
       </f:Window>
</body>
</html>
