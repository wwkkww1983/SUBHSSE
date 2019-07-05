<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupervisionNoticeEdit.aspx.cs"
    ValidateRequest="false" Inherits="FineUIPro.Web.Check.SupervisionNoticeEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑监理整改通知单</title>
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
                    <f:TextBox ID="txtSupervisionNoticeCode" runat="server" Label="编号" Required="true" ShowRedStar="true"
                        MaxLength="50">
                    </f:TextBox>
                    <f:DropDownList ID="drpUnitId" runat="server" Label="责任单位" EnableEdit="true" Required="true"
                        ShowRedStar="true" AutoPostBack="true" OnSelectedIndexChanged="drpUnitId_SelectedIndexChanged">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpWorkAreaId" runat="server" Label="检查区域" EnableEdit="true">
                    </f:DropDownList>
                    <f:DatePicker ID="txtCheckedDate" runat="server" Label="受检时间" EnableEdit="true">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtWrongContent" runat="server" Label="安全隐患内容及整改意见" MaxLength="3000">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtSignPerson" runat="server" Label="签发人" Readonly="true">
                    </f:TextBox>
                    <f:DatePicker ID="txtSignDate" runat="server" Label="日期" EnableEdit="true">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:GroupPanel ID="GroupPanel1" Layout="Anchor" Title="下栏内容由整改单位填写，并在以上整改内容要求的时间内，将此表交本部"
                        runat="server">
                        <Items>
                            <f:TextArea ID="txtCompleteStatus" runat="server" Label="整改措施和完成情况" MaxLength="3000">
                            </f:TextArea>
                        </Items>
                    </f:GroupPanel>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtDutyPerson" runat="server" Label="责任人" MaxLength="50">
                    </f:TextBox>
                    <f:DatePicker ID="txtCompleteDate" runat="server" Label="日期" EnableEdit="true">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpIsRectify" runat="server" Label="检查是否完成整改" EnableEdit="true" Hidden="true">
                        <f:ListItem Value="True" Text="是" />
                        <f:ListItem Value="False" Text="否" />
                    </f:DropDownList>
                    <f:DropDownList ID="drpCheckPerson" runat="server" Label="检查人" EnableEdit="true" Hidden="true">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Label runat="server" ID="lbTemp"></f:Label>
                     <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server" OnClick="btnUploadResources_Click"
                        ValidateForms="SimpleForm1">
                    </f:Button>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>                
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
      <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true"
            EnableMaximize="true" Target="Parent" EnableResize="true" runat="server"
            IsModal="true" Width="700px" Height="500px">
       </f:Window>
    </form>    
</body>
</html>
