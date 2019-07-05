<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DrillRecordListEdit.aspx.cs"
    Inherits="FineUIPro.Web.Emergency.DrillRecordListEdit" ValidateRequest="false" %>
<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl" TagPrefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑应急演练</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="应急演练" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtDrillRecordCode" runat="server" Label="编号" LabelAlign="Right"
                        MaxLength="50" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtDrillRecordName" runat="server" Label="演练名称" Required="true" ShowRedStar="true"
                        LabelAlign="Right" MaxLength="500" FocusOnPageLoad="true">
                    </f:TextBox> 
                     <f:DatePicker ID="txtDrillRecordDate" runat="server" Label="演练时间" LabelAlign="Right"
                        EnableEdit="true">
                    </f:DatePicker>  
                                      
                </Items>
            </f:FormRow>
             <f:FormRow>
                <Items>                   
                    <f:DropDownList ID="drpUnits" runat="server" Label="参与单位" EnableEdit="true" EnableMultiSelect="true"
                        EnableCheckBoxSelect="true"  LabelAlign="Right">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>                   
                    <f:DropDownList ID="drpDrillRecordType" runat="server" Label="演练类型" LabelAlign="Right" ShowRedStar="true">
                    </f:DropDownList>   
                    <f:NumberBox ID="txtJointPersonNum" runat="server" Label="参演人数" LabelAlign="Right" ShowRedStar="true" Required="true"
                                        NoDecimal="true" MinValue="0" NoNegative="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtDrillCost" runat="server" Label="直接投入" LabelAlign="Right" ShowRedStar="true" Required="true"
                                        NoDecimal="false" MinValue="0" NoNegative="true">
                                    </f:NumberBox>
                </Items>
            </f:FormRow>                         
            <f:FormRow>
                <Items>
                    <f:HtmlEditor runat="server" Label="应急演练内容" ID="txtDrillRecordContents" ShowLabel="false"
                        Editor="UMEditor" BasePath="~/res/umeditor/" ToolbarSet="Full" Height="220px" LabelAlign="Right">
                    </f:HtmlEditor>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" EnableCollapse="true"
                        BodyPadding="0px">
                        <uc1:FlowOperateControl ID="ctlAuditFlow" runat="server" />
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>                   
                    <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                        OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1" MarginLeft="5px">
                    </f:Button>
                    <f:ToolbarFill runat="server"> </f:ToolbarFill>
                     <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                     <f:Button ID="btnSubmit" Icon="SystemSaveNew" runat="server" ToolTip="提交" ValidateForms="SimpleForm1"
                        OnClick="btnSubmit_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    </form>
</body>
</html>
