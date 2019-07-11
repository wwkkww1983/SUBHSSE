<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectSuperviseCheckRectifyEdit.aspx.cs"
    Async="true" Inherits="FineUIPro.Web.Hazard.ProjectSuperviseCheckRectifyEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑企业监督检查整改</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
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
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow ColumnWidths="25% 34%">
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
            <f:FormRow ColumnWidths="10% 25% 30% 30%">
                <Items>
                    <f:Label runat="server" ID="Label22" Text="编号：">
                    </f:Label>
                    <f:Label runat="server" ID="lbSuperviseCheckRectifyCode">
                    </f:Label>
                    <f:Label ID="txtIssueMan" runat="server" Label="签发人">
                    </f:Label>
                    <f:Label runat="server" Label="签发时间" ID="txtIssueDate">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server"
                        BoxFlex="1" DataKeyNames="SuperviseCheckRectifyItemId" AllowCellEditing="true"
                        ClicksToEdit="1" DataIDField="SuperviseCheckRectifyItemId" EnableColumnLines="true">
                        <Columns>
                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                            <f:RenderField Width="90px" ColumnID="RectifyName" DataField="RectifyName" SortField="RectifyName"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="作业类别">
                            </f:RenderField>

                            <f:TemplateField Width="110px" HeaderText="隐患源点" HeaderTextAlign="Center" TextAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="Label15" runat="server" Text='<%# Bind("HazardSourcePoint") %>' ToolTip='<%#Bind("HazardSourcePoint") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>

                            <f:TemplateField Width="110px" HeaderText="风险分析" HeaderTextAlign="Center" TextAlign="Left" >
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("RiskAnalysis") %>' ToolTip='<%#Bind("RiskAnalysis") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>

                            <f:TemplateField Width="120px" HeaderText="风险防范" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                <ItemTemplate>
                                    <asp:Label ID="Label24" runat="server" Text='<%# Bind("RiskPrevention") %>' ToolTip='<%#Bind("RiskPrevention") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:WindowField ColumnID="AttachUrl" Width="90px" WindowID="Window6" HeaderText="附件"
                                DataToolTipField="AttachUrlName" DataTextField="AttachUrlName" DataTextFormatString="{0}"
                                DataIFrameUrlFields="AttachUrl,HazardSourcePoint" DataIFrameUrlFormatString="~/common/ShowUpFile.aspx?fileUrl={0}"
                                DataWindowTitleFormatString="编辑 - {1}" >
                            </f:WindowField>
                            <f:RenderField Width="90px" ColumnID="ConfirmMan" DataField="ConfirmMan" FieldType="String"
                                HeaderTextAlign="Center" TextAlign="Center" HeaderText="立项人">
                                <Editor>
                                    <f:TextBox ID="txtConfirmMan" Text='<%# Eval("ConfirmMan")%>' runat="server">
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="ConfirmDate" DataField="ConfirmDate"
                                FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="立项时间"
                                TextAlign="Center" HeaderTextAlign="Center">                                
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="OrderEndDate" DataField="OrderEndDate"
                                FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="要求消项时间"
                                TextAlign="Center" HeaderTextAlign="Center">                               
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="OrderEndPerson" DataField="OrderEndPerson"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Center" HeaderText="消项责任人">
                                <Editor>
                                    <f:TextBox ID="txtOrderEndPerson" Text='<%# Eval("OrderEndPerson")%>' runat="server">
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="100px"  ColumnID="RealEndDate" DataField="RealEndDate"
                                FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="实际消项时间"
                                TextAlign="Center" HeaderTextAlign="Center">
                                <Editor>
                                    <f:DatePicker ID="txtRealEndDate" runat="server" Text='<%# Eval("RealEndDate")%>'>
                                    </f:DatePicker>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="SuperviseCheckRectifyItemId" DataField="SuperviseCheckRectifyItemId"
                                FieldType="String" HeaderText="主键" Hidden="true" HeaderTextAlign="Center">
                                <Editor>
                                    <f:TextBox runat="server" ID="txtSuperviseCheckRectifyItemId" Text='<%# Eval("SuperviseCheckRectifyItemId")%>'>
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                        </Columns>
                        <Listeners>
                            <f:Listener Event="dataload" Handler="onGridDataLoad" />
                        </Listeners>
                    </f:Grid>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1"
                        Hidden="true" OnClick="btnSave_Click">
                    </f:Button>
                   <%-- <f:Button ID="btnUpdata" Icon="SystemSave" runat="server" Hidden="true" ConfirmText="确定保存并上报？"
                        ToolTip="保存并上报" ValidateForms="SimpleForm1" OnClick="btnUpdata_Click">
                    </f:Button>--%>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    <f:Window ID="Window6" Title="请点击下方保存下载附件到本地" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" Hidden="true" IsModal="true"
        Width="400px" Height="10px" EnableAjax="false">
    </f:Window>
    </form>
    <script type="text/javascript">
        function onGridDataLoad(event) {
            this.mergeColumns(['RectifyName']);
        }
    </script>
</body>
</html>
