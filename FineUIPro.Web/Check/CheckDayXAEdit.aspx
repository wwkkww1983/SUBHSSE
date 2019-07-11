<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckDayXAEdit.aspx.cs" Inherits="FineUIPro.Web.Check.CheckDayXAEdit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑日常巡检</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .labcenter
        {
            text-align: center;
        }
        
        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtCheckDayCode" runat="server" Label="检查编号" Readonly="true" MaxLength="50">
                    </f:TextBox>
                    <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="检查日期" ID="txtCheckDate">
                    </f:DatePicker>
                    <f:DropDownList ID="drpCheckMan" runat="server" Label="检查人" EnableEdit="true">
                                    </f:DropDownList>   
                    <f:NumberBox ID="txtNotOKNum" NoDecimal="false" NoNegative="false" MinValue="0" runat="server" ShowRedStar="true" Required="true"
                                Label="不合格数">
                            </f:NumberBox>
                </Items>
            </f:FormRow>
            <f:FormRow runat="server" ID="trUnit" Hidden="true">
                <Items>
                    <f:DropDownList ID="drpDutyUnitIds" runat="server" Label="受检单位" EnableCheckBoxSelect="true" EnableMultiSelect="true">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow runat="server" ID="trTeamGroup" Hidden="true">
                <Items>
                    <f:DropDownList ID="drpDutyTeamGroupIds" runat="server" Label="受检班组" EnableCheckBoxSelect="true" EnableMultiSelect="true">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpWorkAreaIds" runat="server" Label="检查区域" EnableCheckBoxSelect="true" EnableMultiSelect="true">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtUnqualified" runat="server" Label="不合格项描述">
                    </f:TextArea>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Label runat="server" ID="lbTemp"></f:Label>
                     <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server" OnClick="btnAttachUrl_Click"
                        ValidateForms="SimpleForm1">
                    </f:Button>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                    <f:HiddenField ID="hdId" runat="server">
                    </f:HiddenField>
                    <f:HiddenField ID="hdAttachUrl" runat="server">
                    </f:HiddenField>
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
