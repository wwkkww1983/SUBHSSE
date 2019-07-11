<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubUnitReport.aspx.cs"
    Inherits="FineUIPro.Web.ServerCheck.SubUnitReport" Async="true" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>企业安全文件上报</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
            <f:Panel runat="server" ID="panelLeftRegion" Width="250px" RegionPosition="Left" RegionSplit="true"
                EnableCollapse="true" Layout="HBox" Title="企业安全文件上报" TitleToolTip="企业安全文件上报"
                ShowBorder="true" ShowHeader="true" BodyPadding="5px" IconFont="ArrowCircleLeft">
                <Items>
                    <f:Tree ID="trSubUnitReport" EnableCollapse="true" ShowHeader="true" 
                        Title="企业安全文件上报" OnNodeCommand="trSubUnitReport_NodeCommand" AutoLeafIdentification="true"
                        runat="server">
                    </f:Tree>
                </Items>
            </f:Panel>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                Layout="VBox" ShowHeader="false" BodyPadding="5px"  AutoScroll="true"  Hidden="true"
                Title="企业安全文件上报明细" TitleToolTip="企业安全文件上报明细">
                <Items>
                    <f:Form runat="server" ID="formTitle" RegionPosition="Center" ShowBorder="true" ShowHeader="true"
                        BodyPadding="5px" IconFont="PlusCircle" AutoScroll="true">
                        <Items>
                            <f:FormRow>
                                <Items>
                                    <f:Label ID="lblTitle" runat="server" LabelAlign="right" LabelWidth="150px">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:TextBox ID="txtUnitName" runat="server" Label="单位名称" LabelAlign="right" LabelWidth="150px"
                                        Readonly="true">
                                    </f:TextBox>
                                    <f:TextBox ID="txtPlanReortDate" runat="server" Label="要求上报时间" LabelAlign="right"
                                       LabelWidth="150px" Readonly="true">
                                    </f:TextBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:TextArea ID="txtReportTitle" runat="server" Label="标题" Required="true" MaxLength="500"
                                        ShowRedStar="true" LabelAlign="right" LabelWidth="150px" Height="64px" FocusOnPageLoad="true">
                                    </f:TextArea>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:TextArea ID="txtReportContent" runat="server" Label="主要内容" Required="true" MaxLength="1000"
                                        ShowRedStar="true" LabelAlign="right" LabelWidth="150px" Height="200px">
                                    </f:TextArea>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="上报时间" ID="dpkReportDate"
                                        LabelWidth="150px" LabelAlign="right">
                                    </f:DatePicker>
                                    <f:Label ID="lbl" runat="server">
                                    </f:Label>
                                </Items>
                            </f:FormRow>                          
                        </Items>
                        <Toolbars>
                            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                                <Items>
                                    <f:Button ID="btnUploadResources" ToolTip="上传附件" Icon="TableCell" runat="server" OnClick="btnUploadResources_Click"
                                        ValidateForms="SimpleForm1" Text="附件">
                                    </f:Button>
                                    <f:ToolbarFill runat="server"></f:ToolbarFill>
                                    <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1"
                                        OnClick="btnSave_Click" Hidden="true">
                                    </f:Button>
                                    <f:Button ID="btnSaveUp" Icon="PageSave" runat="server" ToolTip="保存并上报" ValidateForms="SimpleForm1"
                                        OnClick="btnSaveUp_Click" Hidden="true">
                                    </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                    </f:Form>
                </Items>
            </f:Panel>
        </Items>
    </f:Panel>
    </form>
    <f:Window ID="WindowAtt" Title="附件" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
</body>
</html>
