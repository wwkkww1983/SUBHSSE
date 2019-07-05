<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HSSEStandardListAudit.aspx.cs"
    Inherits="FineUIPro.Web.Law.HSSEStandardListAudit" Async="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>安全标准规范资源审核</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="安全标准规范" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="StandardId" AllowCellEditing="true"
                EnableColumnLines="true" ClicksToEdit="2" DataIDField="StandardId" AllowSorting="true"
                SortField="CompileDate" SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="true"
                IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" AllowFilters="true"
                OnFilterChange="Grid1_FilterChange" OnRowCommand="Grid1_RowCommand" EnableTextSelection="True">
                <Columns>
                   <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                    <f:RenderField Width="90px" ColumnID="StandardNo" DataField="StandardNo" SortField="StandardNo"
                        FieldType="String" HeaderText="标准号" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="250px" ColumnID="StandardName" DataField="StandardName" SortField="StandardName"
                        FieldType="String" HeaderText="标准名称" EnableFilter="true" TextAlign="Center">
                        <Filter EnableMultiFilter="true" ShowMatcher="true">
                            <Operator>
                                <f:DropDownList ID="DropDownList1" runat="server">
                                    <f:ListItem Text="等于" Value="equal" />
                                    <f:ListItem Text="包含" Value="contain" Selected="true" />
                                </f:DropDownList>
                            </Operator>
                        </Filter>
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="TypeName" DataField="TypeName" SortField="StandardNo"
                        FieldType="String" HeaderText="分类" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="StandardGrade" DataField="StandardGrade" FieldType="String"
                        HeaderText="标准级别" TextAlign="Center">
                    </f:RenderField>
                    <f:GroupField HeaderText="对应HSSE索引" TextAlign="Center">
                        <Columns>
                            <f:RenderField Width="100px" ColumnID="IsSelected1" DataField="IsSelected1" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="01(地基处理)" HeaderToolTip="地基处理">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="IsSelected2" DataField="IsSelected2" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="02(打桩)" HeaderToolTip="打桩">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="IsSelected3" DataField="IsSelected3" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="03(基坑支护与降水工程)" HeaderToolTip="基坑支护与降水工程">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="IsSelected4" DataField="IsSelected4" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="04(土方开挖工程)" HeaderToolTip="土方开挖工程">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="IsSelected5" DataField="IsSelected5" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="05(模板工程)" HeaderToolTip="模板工程">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="IsSelected6" DataField="IsSelected6" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="06(基础施工)" HeaderToolTip="基础施工">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="IsSelected7" DataField="IsSelected7" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="07(钢筋混凝土结构)" HeaderToolTip="钢筋混凝土结构">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="IsSelected8" DataField="IsSelected8" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="08(地下管道)" HeaderToolTip="地下管道">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="IsSelected9" DataField="IsSelected9" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="09(钢结构)" HeaderToolTip="钢结构">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="IsSelected10" DataField="IsSelected10" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="10(设备安装)" HeaderToolTip="设备安装">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="IsSelected11" DataField="IsSelected11" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="11(大型起重吊装工程)" HeaderToolTip="大型起重吊装工程">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="IsSelected12" DataField="IsSelected12" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="12(脚手架工程)" HeaderToolTip="脚手架工程">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="IsSelected13" DataField="IsSelected13" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="13(机械安装)" HeaderToolTip="机械安装">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="IsSelected14" DataField="IsSelected14" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="14(管道安装)" HeaderToolTip="管道安装">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="IsSelected15" DataField="IsSelected15" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="15(电气仪表安装)" HeaderToolTip="电气仪表安装">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="IsSelected16" DataField="IsSelected16" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="16(防腐保温防火)" HeaderToolTip="防腐保温防火">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="IsSelected17" DataField="IsSelected17" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="17(拆除)" HeaderToolTip="拆除">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="IsSelected18" DataField="IsSelected18" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="18(爆破工程)" HeaderToolTip="爆破工程">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="IsSelected19" DataField="IsSelected19" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="19(试压)" HeaderToolTip="试压">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="IsSelected20" DataField="IsSelected20" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="20(吹扫)" HeaderToolTip="吹扫">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="IsSelected21" DataField="IsSelected21" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="21(试车)" HeaderToolTip="试车">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="IsSelected22" DataField="IsSelected22" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="22(无损检测)" HeaderToolTip="无损检测">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="IsSelected23" DataField="IsSelected23" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="23(其他危险性较大的工程)" HeaderToolTip="其他危险性较大的工程">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="IsSelected24" DataField="IsSelected24" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="24(设计)" HeaderToolTip="设计">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="IsSelected25" DataField="IsSelected25" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="25(工厂运行)" HeaderToolTip="工厂运行">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="IsSelected90" DataField="IsSelected90" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="90(全部标准)" HeaderToolTip="全部标准">
                            </f:RenderField>
                        </Columns>
                    </f:GroupField>
                    <%--<f:LinkButtonField EnableAjax="false" Width="150px" CommandName="Attach" DataTextField="AttachUrlName"
                        HeaderText="附件" ColumnID="AttachUrl" DataToolTipField="AttachUrlName" TextAlign="Center" />--%>
                        <f:WindowField TextAlign="Center" Width="120px" WindowID="WindowAtt" HeaderText="附件" Text="附件上传查看"
                       ToolTip="附件上传查看" DataIFrameUrlFields="StandardId" DataIFrameUrlFormatString="../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/HSSEStandardsList&menuId=EFDSFVDE-RTHN-7UMG-4THA-5TGED48F8IOL"
                          />
                    <f:RenderField Width="90px" ColumnID="CompileMan" DataField="CompileMan" SortField="CompileMan"
                        FieldType="String" HeaderText="整理人" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="CompileDate" DataField="CompileDate" SortField="CompileDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="整理日期"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                </Columns>
                <Listeners>
                    <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                </Listeners>
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
    </f:Panel>
    <f:Window ID="Window1" Title="安全标准规" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="1000px" Height="480px">
    </f:Window>
     <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true"
            EnableMaximize="true" Target="Self" EnableResize="true" runat="server"
            IsModal="true" Width="670px" Height="460px">
       </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnPass" OnClick="btnPass_Click" EnablePostBack="true" runat="server"
            Text="采用" Hidden="true">
        </f:MenuButton>
        <f:MenuButton ID="btnUpPass" OnClick="btnUpPass_Click" EnablePostBack="true" runat="server"
            Text="采用并上报" Hidden="true">
        </f:MenuButton>
        <f:MenuButton ID="btnNoPass" OnClick="btnNoPass_Click" EnablePostBack="true" runat="server"
            Text="不采用" Hidden="true">
        </f:MenuButton>
    </f:Menu>
    </form>
    <script type="text/javascript">
        function renderSelect(value) {
            return value == "True" ? '<font size="5">●</font>' : '';
        }

        var menuID = '<%= Menu1.ClientID %>';
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
    </script>
</body>
</html>
