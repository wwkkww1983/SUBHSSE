<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GasCylinderInView.aspx.cs"
    Inherits="FineUIPro.Web.InApproveManager.GasCylinderInView" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>查看气瓶入场报批</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .customlabel span
        {
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtGasCylinderInCode" runat="server" Label="气瓶入场编号" LabelAlign="Right"
                        Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtUnitName" runat="server" Label="单位名称" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtInDate" runat="server" Label="入场时间" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtDriverMan" runat="server" Label="驾驶员姓名" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtDriverNum" runat="server" Label="驾驶证号" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtCarNum" runat="server" Label="车牌号" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtLeadCarMan" runat="server" Label="带车人" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                    <f:Label ID="lblE" runat="server">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="气瓶基本情况" EnableCollapse="true"
                        runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="GasCylinderInItemId"
                        AllowCellEditing="true" ClicksToEdit="2" DataIDField="GasCylinderInItemId" AllowPaging="true"
                        IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                        EnableTextSelection="True" Height="200px">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                                <Items>
                                    <f:Label ID="lblT" runat="server" Text="气瓶基本情况" CssClass="customlabel span">
                                    </f:Label>
                                    <f:ToolbarFill ID="ToolbarFill2" runat="server">
                                    </f:ToolbarFill>               
                            <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                EnableAjax="false" DisableControlBeforePostBack="false">
                            </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>
                            <f:TemplateField ColumnID="tfNumber" Width="50px" HeaderText="序号" HeaderTextAlign="Center"
                        TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblNumber" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                            <f:TemplateField ColumnID="tfGasCylinderId" HeaderText="气瓶类型" Width="200px" HeaderTextAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblGasCylinderId" runat="server" Text='<%#ConvertGasCylinder(Eval("GasCylinderId")) %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField Width="150px" ColumnID="GasCylinderNum" DataField="GasCylinderNum"
                                SortField="GasCylinderNum" FieldType="Int" HeaderText="数量" HeaderTextAlign="Center"
                                TextAlign="Left">
                            </f:RenderField>
                           <f:TemplateField ColumnID="tfPM_IsFull" HeaderText="瓶帽齐全" Width="150px" HeaderTextAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblPM_IsFull" runat="server" Text='<%# ConvertB(Eval("PM_IsFull"))%>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>                            
                            <f:TemplateField ColumnID="tfFZQ_IsFull" HeaderText="防震圈齐全" Width="150px" HeaderTextAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblFZQ_IsFull" runat="server" Text='<%#  ConvertB(Eval("FZQ_IsFull"))%>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>                            
                            <f:TemplateField ColumnID="tfIsSameCar" HeaderText="是否同车运输" Width="150px" HeaderTextAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblIsSameCar" runat="server" Text='<%#  ConvertB(Eval("IsSameCar"))%>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                        </Columns>
                        <PageItems>
                            <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                            </f:ToolbarSeparator>
                            <f:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                            </f:ToolbarText>
                            <f:DropDownList runat="server" ID="ddlPageSize" Width="80px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                <f:ListItem Text="10" Value="10" />
                                <f:ListItem Text="15" Value="15" />
                                <f:ListItem Text="20" Value="20" />
                                <f:ListItem Text="25" Value="25" />
                                <f:ListItem Text="所有行" Value="100000" />
                            </f:DropDownList>
                        </PageItems>
                    </f:Grid>
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
                    <f:Label runat="server" ID="lbTemp">
                    </f:Label>
                    <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                        OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1">
                    </f:Button>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                    </f:ToolbarFill>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    <f:Window ID="WindowAtt" Title="附件" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    </form>
    <script type="text/javascript">
        function renderIs(value) {
            return value == true ? '是' : '否';
        }
    </script>
</body>
</html>
