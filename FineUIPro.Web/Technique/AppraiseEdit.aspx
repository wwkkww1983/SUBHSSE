﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppraiseEdit.aspx.cs" Inherits="FineUIPro.Web.Technique.AppraiseEdit"
    Async="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑安全评价</title>
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
                    <f:TextBox ID="txtAppraiseCode" runat="server" Label="编号" Required="true" ShowRedStar="true"
                        MaxLength="50" FocusOnPageLoad="true" AutoPostBack="true" OnTextChanged="TextBox_TextChanged">
                    </f:TextBox>
                    <f:TextBox ID="txtAppraiseTitle" runat="server" Label="标题" Required="true" ShowRedStar="true"
                        MaxLength="500" AutoPostBack="true" OnTextChanged="TextBox_TextChanged">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtAbstract" runat="server" Label="摘要" MaxLength="800">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="ddlArrangementPerson" runat="server" Label="整理人">
                    </f:DropDownList>
                    <f:DatePicker runat="server" Label="整理日期" ID="dpkArrangementDate">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DatePicker runat="server" Label="评价时间" ID="dpkAppraiseDate">
                    </f:DatePicker>
                    <f:Label runat="server" ID="Label1" BoxConfigPosition="Right">
                    </f:Label>
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
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1"
                        Hidden="true" OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnSaveUp" Icon="PageSave" runat="server" ToolTip="保存并上报" ValidateForms="SimpleForm1"
                        Hidden="true" OnClick="btnSaveUp_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    </form>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
</body>
</html>
