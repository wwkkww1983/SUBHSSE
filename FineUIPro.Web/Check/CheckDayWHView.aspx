<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckDayWHView.aspx.cs" ValidateRequest="false" Inherits="FineUIPro.Web.Check.CheckDayWHView" %>
<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查看日常巡检（五环）</title>
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
                    <f:TextBox ID="txtWeather" runat="server" Label="天气" Readonly="true" MaxLength="50">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtCheckPerson" runat="server" Label="检查人" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtCheckDate" runat="server" Label="检查日期" Readonly="true" MaxLength="50">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" runat="server" ClicksToEdit="1" DataIDField="CheckDayDetailId"
                        DataKeyNames="CheckDayDetailId" EnableMultiSelect="false" ShowGridHeader="true" Height="220px"
                        EnableColumnLines="true" >
                        <%--<Toolbars>
                            <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                                <Items>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>--%>
                        <Columns>
                            <f:RowNumberField HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center" />
                            <f:RenderField Width="220px" ColumnID="Unqualified" DataField="Unqualified" SortField="Unqualified"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="隐患内容"
                                ExpandUnusedSpace="true">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="WorkArea" DataField="WorkArea" SortField="WorkArea"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="检查区域">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="HiddenDangerType" DataField="HiddenDangerType" SortField="HiddenDangerType"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="隐患类型">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="HiddenDangerLevel" DataField="HiddenDangerLevel" SortField="HiddenDangerLevel"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="隐患级别">
                            </f:RenderField>
                            <f:RenderField Width="200px" ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="责任单位">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="PersonName" DataField="PersonName" SortField="PersonName"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="责任人">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="LimitedDate" DataField="LimitedDate" SortField="LimitedDate"
                                FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="整改限时"
                                HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="Suggestions" DataField="Suggestions" SortField="Suggestions"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="整改要求">
                            </f:RenderField>
                            <f:TemplateField Width="200px" HeaderText="处理措施" HeaderTextAlign="Center" TextAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# HandleStepStr(Eval("HandleStep")) %>'
                                        ToolTip='<%# HandleStepStr(Eval("HandleStep")) %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            
                            <f:RenderField Width="10px" ColumnID="UnitId" DataField="UnitId" SortField="UnitId"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Hidden="true" HeaderText="单位Id">
                            </f:RenderField>
                            <f:RenderField Width="10px" ColumnID="PersonId" DataField="PersonId" SortField="PersonId"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Hidden="true" HeaderText="责任人Id">
                            </f:RenderField>
                        </Columns>
                    </f:Grid>
                </Items>
            </f:FormRow>
            <%--<f:FormRow>
                <Items>                   
                    <f:HtmlEditor runat="server" Label="其他情况日小结" ID="txtDaySummary" ShowLabel="false"
                        Editor="UMEditor" BasePath="~/res/umeditor/" ToolbarSet="Full" Height="200" LabelAlign="Right">
                    </f:HtmlEditor>
                </Items>
            </f:FormRow>--%>
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
                    <f:Label runat="server" ID="lbTemp"></f:Label>
                     <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server" OnClick="btnAttachUrl_Click"
                        ValidateForms="SimpleForm1">
                    </f:Button>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                   <%-- <f:HiddenField ID="hdId" runat="server">
                    </f:HiddenField>
                    <f:HiddenField ID="hdAttachUrl" runat="server">
                    </f:HiddenField>--%>
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
