<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckRectifyEdit.aspx.cs"
    Inherits="FineUIPro.Web.ServerCheck.CheckRectifyEdit" Async="true" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>隐患整改单</title>
    <style type="text/css">
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
        BodyPadding="5px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:Panel ID="Panel4" runat="server" ShowBorder="False" Layout="Table" TableConfigColumns="2"
                        ShowHeader="false">
                        <Items>
                            <f:Panel ID="Panel1" Title="Panel1" runat="server" BodyPadding="5px" ShowBorder="false"
                                ShowHeader="false">
                                <Items>
                                    <f:Label runat="server" ID="lbUnitName">
                                    </f:Label>
                                    <f:Label runat="server" ID="lbProjectName">
                                    </f:Label>
                                </Items>
                            </f:Panel>
                            <f:Panel ID="Panel5" Title="Panel3" TableRowspan="2" runat="server" BodyPadding="5px"
                                ShowBorder="false" ShowHeader="false">
                                <Items>
                                    <f:Label runat="server" ID="Label1" Text="：你单位存在以下安全事故隐患，请立即组织治理整改并消项。">
                                    </f:Label>
                                </Items>
                            </f:Panel>
                        </Items>
                    </f:Panel>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Label runat="server" ID="lbCheckRectifyCode" Label="编号" >
                    </f:Label>
                    <f:Label runat="server" ID="txtIssueMan" Label="签发人">
                    </f:Label>
                     <f:Label runat="server" ID="txtIssueDate" Label="签发时间">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server"
                        BoxFlex="1" DataKeyNames="CheckRectifyItemId" AllowCellEditing="true" Height="380px"
                        ClicksToEdit="1" DataIDField="CheckRectifyItemId" EnableColumnLines="true">
                        <Columns>
                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="45px" HeaderTextAlign="Center" TextAlign="Center"/>
                            <f:RenderField Width="100px" ColumnID="WorkType" DataField="WorkType" SortField="WorkType"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="作业类别">
                            </f:RenderField>
                            <f:TemplateField Width="140px" HeaderText="隐患源点" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                <ItemTemplate>
                                    <asp:Label ID="Label15" runat="server" Text='<%# Bind("DangerPoint") %>' ToolTip='<%#Bind("DangerPoint") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="150px" HeaderText="存在风险" HeaderTextAlign="Center" TextAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("RiskExists") %>' ToolTip='<%#Bind("RiskExists") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                             <f:WindowField TextAlign="Center" Width="70px" WindowID="WindowAtt" HeaderText="整改前" Text="查看"                       
                                ToolTip="相关照片附件" DataIFrameUrlFields="Table5ItemId" DataIFrameUrlFormatString="../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/Check&type=-1"
                                Title="相关照片" ColumnID="WindowAtt">
                             </f:WindowField>
                             <f:WindowField TextAlign="Center" Width="70px" WindowID="WindowAtt" HeaderText="整改后" Text="查看"                       
                                ToolTip="相关照片附件" DataIFrameUrlFields="CheckRectifyItemId" DataIFrameUrlFormatString="../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/Check&menuId=4A87774E-FEA5-479A-97A3-9BBA09E4862E"
                                Title="相关照片" ColumnID="WindowAtt1">
                             </f:WindowField>
                            <f:RenderField Width="75px" ColumnID="ConfirmMan" DataField="ConfirmMan" FieldType="String"
                                HeaderTextAlign="Center" TextAlign="Left" HeaderText="立项人">                             
                            </f:RenderField>
                            <f:RenderField Width="100px" EnableLock="true" ColumnID="ConfirmDate" DataField="ConfirmDate"
                                FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="立项时间"
                                TextAlign="Center" HeaderTextAlign="Center">                               
                            </f:RenderField>
                            <f:RenderField Width="110px" EnableLock="true" ColumnID="OrderEndDate" DataField="OrderEndDate"
                                FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="要求消项时间"
                                TextAlign="Center" HeaderTextAlign="Center">                              
                            </f:RenderField>
                            <f:RenderField Width="95px" ColumnID="OrderEndPerson" DataField="OrderEndPerson"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Center" HeaderText="消项责任人">
                                <Editor>
                                    <f:TextBox ID="txtOrderEndPerson" Text='<%# Eval("OrderEndPerson")%>' MaxLength="50" runat="server">
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="110px" EnableLock="true" ColumnID="RealEndDate" DataField="RealEndDate"
                                FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="实际消项时间"
                                TextAlign="Center" HeaderTextAlign="Center">
                                <Editor>
                                    <f:DatePicker ID="txtRealEndDate" runat="server" Text='<%# Eval("RealEndDate")%>'>
                                    </f:DatePicker>
                                </Editor>
                            </f:RenderField>
                             <f:RenderField ColumnID="Verification" DataField="Verification" Width="80px" HeaderToolTip="企业安全管理部门验证人"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="验证人">
                                <Editor>
                                    <f:TextBox ID="txtVerification" Text='<%# Eval("Verification")%>' MaxLength="50" runat="server">
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="1px" ColumnID="CheckRectifyItemId" DataField="CheckRectifyItemId"
                                FieldType="String" HeaderText="主键" Hidden="true" HeaderTextAlign="Center">                              
                            </f:RenderField>
                        </Columns>                      
                    </f:Grid>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" Hidden="true"  ToolTip="保存" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnSaveUp" Icon="PageSave" runat="server" ToolTip="保存并上报" Hidden="true"  ValidateForms="SimpleForm1"
                        OnClick="btnSaveUp_Click">
                    </f:Button>
                    <f:Button ID="btnClose" Icon="SystemClose" runat="server" ToolTip="关闭" OnClick="btnClose_Click">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>     
    </f:Form>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true"
        EnableMaximize="true" Target="Self" EnableResize="true" runat="server"
        IsModal="true" Width="670px" Height="460px">
    </f:Window>
    </form>
</body>
</html>
