<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServerSafeReportSave.aspx.cs" Inherits="FineUIPro.Web.ManagementReport.ServerSafeReportSave" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true" EnableTableStyle="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
             <f:FormRow>
                <Items>
                   <f:TextBox ID="txtSafeReportName" runat="server" Label="标题" Required="true" ShowRedStar="true"
                        FocusOnPageLoad="true" MaxLength="500">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtSafeReportCode" runat="server" Label="编号" Required="true" ShowRedStar="true"
                        MaxLength="50">
                    </f:TextBox>
                     <f:DropDownList ID="drpIsEndLever" Label="是否末级" AutoPostBack="true" 
                        runat="server" OnSelectedIndexChanged="drpIsEndLever_SelectedIndexChanged">
                    </f:DropDownList>                    
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Form ID="SimpleForm2" ShowBorder="false" ShowHeader="false" AutoScroll="true" runat="server" RedStarPosition="BeforeText">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:TextArea runat="server" ID="txtRequirement" Label="上报要求" MaxLength="2000" Height="64px"></f:TextArea>
                                </Items>
                           </f:FormRow>
                           <f:FormRow>
                                <Items>
                                     <f:DatePicker ID="txtRequestTime" runat="server" Label="上报时间" >
                                    </f:DatePicker>
                                     <f:Button ID="btnAttachUrl" Text="标准模板" Icon="TableCell" runat="server"
                                        OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1">
                                    </f:Button>
                                </Items>
                            </f:FormRow>
                           <f:FormRow>
                                <Items>
                                    <f:DropDownList ID="drpCompileMan" runat="server" Label="编制人" EnableEdit="true">
                                    </f:DropDownList>
                                     <f:DatePicker ID="txtCompileTime" runat="server" Label="编制时间" >
                                    </f:DatePicker>
                                </Items>
                            </f:FormRow>
                        </Rows>
                  </f:Form>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                     <f:Button ID="btnSubmit" Icon="SystemSaveNew" runat="server" ToolTip="提交"
                        ValidateForms="SimpleForm1" OnClick="btnSubmit_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
     <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    </form>
</body>
</html>
