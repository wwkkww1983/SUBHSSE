<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SuperviseCheckReportEdit.aspx.cs"
    Inherits="FineUIPro.Web.Supervise.SuperviseCheckReportEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑安全监督检查报告</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .labcenter
        {
            text-align: center;
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
                    <f:TextBox ID="txtSuperviseCheckReportCode" runat="server" Label="检查编号" Required="true"   FocusOnPageLoad="true"
                        MaxLength="50" ShowRedStar="true" AutoPostBack="true" OnTextChanged="TextBox_TextChanged">
                    </f:TextBox>
                    <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="检查日期" ID="dpkCheckDate">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="ddlUnitId" runat="server" Label="检查对象" Width="400px" EmptyText="请选择单位"
                        Enabled="false">
                    </f:DropDownList>
                    <f:DropDownList ID="ddlProjectId" runat="server" Width="400px" EmptyText="请选择项目" EnableEdit="true">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtCheckTeam" runat="server" Label="检查组/人">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" runat="server" ClicksToEdit="1"
                        DataKeyNames="SuperviseCheckReportItemId" EnableMultiSelect="false" ShowGridHeader="true"
                        EnableColumnLines="true" OnRowCommand="Grid1_RowCommand" AutoScroll="true">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                                <Items>
                                    <f:HiddenField runat="server" ID="hdItemId">
                                    </f:HiddenField>
                                    <f:Button ID="btnSelect" Icon="Pencil" runat="server" Text="选择" ValidateForms="SimpleForm1"
                                        OnClick="btnSelect_Click">
                                    </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>
                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                            <f:RenderField Width="100px" ColumnID="RectifyName" DataField="RectifyName" SortField="RectifyName"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Center" HeaderText="作业类别">
                            </f:RenderField>
                            <f:TemplateField Width="220px" HeaderText="隐患源点" HeaderTextAlign="Center" TextAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("HazardSourcePoint") %>' ToolTip='<%#Bind("HazardSourcePoint") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="170px" HeaderText="风险分析" HeaderTextAlign="Center" TextAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("RiskAnalysis") %>' ToolTip='<%#Bind("RiskAnalysis") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="250px" HeaderText="风险防范" HeaderTextAlign="Center" TextAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("RiskPrevention") %>' ToolTip='<%#Bind("RiskPrevention") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="110px" HeaderText="同类风险" HeaderTextAlign="Center" TextAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("SimilarRisk") %>' ToolTip='<%#Bind("SimilarRisk") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:CheckBoxField ColumnID="ckbIsSelected" Width="80px" RenderAsStaticField="false"
                                DataField="IsSelected" AutoPostBack="true" CommandName="IsSelected" HeaderText="隐患立项" />
                            <f:RenderField Width="90px" ColumnID="SuperviseCheckReportItemId" DataField="SuperviseCheckReportItemId"
                                FieldType="String" HeaderText="主键" Hidden="true" HeaderTextAlign="Center">
                                <Editor>
                                    <f:TextBox runat="server" ID="txtSuperviseCheckReportItemId" Text='<%# Eval("SuperviseCheckReportItemId")%>'>
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="5px" ColumnID="AttachUrl" DataField="AttachUrl" FieldType="String"
                                HeaderText="附件路径" Hidden="true" HeaderTextAlign="Center">
                                <Editor>
                                    <f:TextBox runat="server" ID="txtAttachUrl" Text='<%# Eval("AttachUrl")%>'>
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                            <f:LinkButtonField EnableAjax="false" Width="100px" CommandName="Attach" DataTextField="AttachUrlName"
                                HeaderText="附件" ColumnID="AttachUrl" DataToolTipField="AttachUrlName" TextAlign="Left" />
                            <f:LinkButtonField Width="30px" ConfirmText="删除选中行？" ConfirmTarget="Top" CommandName="Delete"
                                Icon="Delete" Text="删除" />
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
    <f:Window ID="Window1" Title="上传附件" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="500px" Height="300px">
    </f:Window>
    <f:Window ID="Window2" Title="选择隐患项" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window2_Close" IsModal="false"
        Width="1300px" Height="640px">
    </f:Window>
    </form>
    <script>

        function onGridDataLoad(event) {
            this.mergeColumns(['RectifyName']);
        }
    </script>
</body>
</html>
