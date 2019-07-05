<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HazardListUpload.aspx.cs"
    Inherits="FineUIPro.Web.Technique.HazardListUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>危险源清单资源上传</title>
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
                                    <f:TextBox ID="txtHazardCode" runat="server" Label="危险源代码" Required="true" ShowRedStar="true"
                                        MaxLength="50" LabelWidth="120px" AutoPostBack="true" OnTextChanged="TextBox_TextChanged"
                                        LabelAlign="Right">
                                    </f:TextBox>
                                    <f:TextBox ID="txtHazardItems" runat="server" Label="危险因素明细" Required="true" ShowRedStar="true"
                                        MaxLength="150" LabelWidth="120px" LabelAlign="Right">
                                    </f:TextBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:TextBox ID="txtDefectsType" runat="server" Label="缺陷类型" LabelWidth="120px" MaxLength="50"
                                        LabelAlign="Right">
                                    </f:TextBox>
                                    <f:TextBox ID="txtMayLeadAccidents" runat="server" Label="可能导致的事故" LabelWidth="120px"
                                        MaxLength="100" LabelAlign="Right">
                                    </f:TextBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:DropDownList ID="ddlHelperMethod" runat="server" Label="辅助方法" LabelWidth="120px"
                                        LabelAlign="Right">
                                    </f:DropDownList>
                                    <f:TextBox ID="txtHazardJudge_L" runat="server" Label="作业条件危险性评价(L)" LabelWidth="120px"
                                        LabelAlign="Right">
                                    </f:TextBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:TextBox ID="txtHazardJudge_E" runat="server" Label="作业条件危险性评价(E)" LabelWidth="120px"
                                        LabelAlign="Right">
                                    </f:TextBox>
                                    <f:TextBox ID="txtHazardJudge_C" runat="server" Label="作业条件危险性评价(C)" LabelWidth="120px"
                                        LabelAlign="Right">
                                    </f:TextBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:TextBox ID="txtHazardJudge_D" runat="server" Label="作业条件危险性评价(D)" LabelWidth="120px"
                                        LabelAlign="Right">
                                    </f:TextBox>
                                    <f:DropDownList ID="ddlHazardLevel" runat="server" Label="危险级别" LabelWidth="120px"
                                        LabelAlign="Right">
                                    </f:DropDownList>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:TextArea ID="txtControlMeasures" runat="server" Label="控制措施" LabelWidth="120px"
                                        MaxLength="200" Height="70px" LabelAlign="Right">
                                    </f:TextArea>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:Label ID="txtCompileMan" runat="server" Label="整理人" LabelWidth="120px" LabelAlign="Right">
                                    </f:Label>
                                    <f:Label runat="server" Label="整理时间" ID="txtCompileDate" LabelWidth="120px" LabelAlign="Right">
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
