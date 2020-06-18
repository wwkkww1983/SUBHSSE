<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RectifyNoticesEdit.aspx.cs" Inherits="FineUIPro.Web.Check.RectifyNoticesEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Panel4"  />
        <f:Panel ID="Panel4" runat="server" BodyPadding="10px" ShowBorder="true" ShowHeader="false" AutoScroll="true">
            <Items>
                <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                    BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                    <Rows>
                        <f:FormRow runat="server">
                            <Items>                            
                                <f:TextBox ID="txtRectifyNoticesCode" runat="server" Label="编号" MaxLength="70" >
                                </f:TextBox>
                                    <f:DropDownList ID="drpUnitId" runat="server" Label="受检单位"  LabelAlign="Right" EnableEdit="true">
                                </f:DropDownList>
                                <f:DropDownList ID="drpWorkAreaId" runat="server" Label="单位工程"  LabelAlign="Right" EnableEdit="true">
                                </f:DropDownList>
                            </Items>
                        </f:FormRow>
                        <f:FormRow runat="server">
                            <Items>
                                <f:DropDownList ID="drpCheckPerson" runat="server" Label="检查人员"  LabelAlign="Right" EnableEdit="true" EnableMultiSelect="true" AutoPostBack="true" OnSelectedIndexChanged="drpCheckPerson_SelectedIndexChanged">
                                </f:DropDownList>
                                <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="检查日期" ID="txtCompleteDate"
                                    LabelAlign="right"  ShowRedStar="true">
                                </f:DatePicker>
                                <f:DropDownList ID="drpHiddenHazardType" runat="server" Label="隐患类别"  LabelAlign="Right" EnableEdit="true" EmptyText="--请选择--">
                                    <f:ListItem Text="一般" Value="1" />
                                    <f:ListItem Text="较大" Value="2" />
                                    <f:ListItem Text="重大" Value="3" />
                                </f:DropDownList>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
                <f:Form ID="Form2" ShowBorder="true" ShowHeader="true" Title="安全隐患及整改要求" AutoScroll="true"
                    runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                    <Rows>
                        <f:FormRow ID="Itemcontent">
                            <Items>
                                <f:HiddenField runat="server" ID="hdTestPlanTrainingId"></f:HiddenField>
                                <f:Form runat="server" ShowBorder="false" ShowHeader="false" BodyPadding="10px">
                                    <Items>
                                        <f:FormRow ID="before" ColumnWidths="35% 25% 25% 15%">
                                            <Items >
                                                <f:TextBox ID="txtWrongContent" runat="server" Label="具体位置及隐患内容" 
                                                    MaxLength="3000"  ShowRedStar="true" Required="true" LabelWidth="160px" Width="400px">
                                                </f:TextBox>
                                                <f:TextBox ID="txtRequirement" runat="server" Label="整改要求" 
                                                    MaxLength="3000"  ShowRedStar="true" Required="true">
                                                </f:TextBox>
                                                <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="整改期限" ID="txtLimitTime"
                                                    LabelAlign="right" LabelWidth="110px" ShowRedStar="true" Required="true">
                                                </f:DatePicker>
                                                <f:Button ID="btnAttachUrl" Text="整改前照片" ToolTip="附件上传及查看" Icon="TableCell" runat="server" OnClick="btnAttachUrl_Click"
                                                    ValidateForms="Form2">
                                                </f:Button>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow ID="after" Hidden="true">
                                            <Items>
                                                <f:TextBox ID="txtRectifyResults" runat="server" Label="整改结果" MaxLength="800"  ShowRedStar="true" Required="true">
                                                </f:TextBox>
                                                <f:Button ID="btnAttachUrlafter" Text="整改后照片" ToolTip="附件上传及查看" Icon="TableCell" runat="server" OnClick="btnAttachUrlafter_Click"
                                                    ValidateForms="FormAfter">
                                                </f:Button>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow ID="end" Hidden="true">
                                            <Items>
                                                <f:DropDownList ID="drpIsRectify" runat="server" Label="是否合格"  LabelAlign="Right" EnableEdit="true" EmptyText="--请选择--" AutoPostBack="true" OnSelectedIndexChanged="drpIsRectify_SelectedIndexChanged">
                                                    <f:ListItem Text="合格" Value="true" />
                                                    <f:ListItem Text="不合格" Value="false" />
                                                </f:DropDownList>
                                            </Items>
                                        </f:FormRow>
                                    </Items>
                                </f:Form>
                            </Items>
                        </f:FormRow>
                        <f:FormRow>
                            <Items>
                            </Items>
                        </f:FormRow>
                        <f:FormRow>
                            <Items>
                                <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="true" EnableColumnLines="true" EnableColumnMove="true" runat="server" BoxFlex="1" DataKeyNames="RectifyNoticesItemId,IsRectify" DataIDField="RectifyNoticesItemId" AllowSorting="true" SortField="RectifyNoticesItemId" SortDirection="ASC" EnableTextSelection="True" Height="260px"
                                    EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick">
                                    <Toolbars>
                                        <f:Toolbar ID="Toolbar2" Position="Top" ToolbarAlign="Right" runat="server">
                                            <Items>
                                                <f:ToolbarFill runat="server"></f:ToolbarFill>
                                                <f:Button ID="btnSure" Icon="Accept" runat="server" ValidateForms="SimpleForm1" OnClick="btnSure_Click" ToolTip="确认">
                                                </f:Button>
                                            </Items>
                                        </f:Toolbar>
                                    </Toolbars>
                                    <Columns>
                                        <f:RenderField Width="200px" ColumnID="WrongContent" DataField="WrongContent"
                                            FieldType="String" HeaderText="具体位置及隐患内容" HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField>
                                        <f:RenderField Width="200px" ColumnID="Requirement" DataField="Requirement" ExpandUnusedSpace="true"
                                            FieldType="String" HeaderText="整改要求" HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField>
                                        <f:RenderField Width="150px" ColumnID="LimitTime" DataField="LimitTime"
                                            FieldType="string" HeaderText="整改期限" HeaderTextAlign="Center" TextAlign="Right">
                                        </f:RenderField>
                                        <f:RenderField Width="150px" ColumnID="RectifyResults" DataField="RectifyResults" FieldType="string" HeaderText="整改结果" HeaderTextAlign="Center" TextAlign="Right">
                                        </f:RenderField>
                                        <f:TemplateField ColumnID="IsRectify" Width="110px" HeaderText="是否合格" HeaderTextAlign="Center" TextAlign="Center" EnableLock="true" Locked="False">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# ConvertIsRectify(Eval("IsRectify")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </f:TemplateField>
                                    </Columns>
                                    <Listeners>
                                        <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                                    </Listeners>
                                </f:Grid>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>

                <f:Form ID="Form3" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                    BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                    <Rows>
                        <f:FormRow runat="server" ID="next">
                            <Items>
                                <f:DropDownList ID="drpSignPerson" runat="server" Label="项目安全经理"   LabelWidth="110px"
                                    LabelAlign="Right" EnableEdit="true">
                                </f:DropDownList>
                                <f:Label runat="server"></f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="next1">
                            <Items>
                                <f:Form ID="Form6" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                                    BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                    <Rows>
                                        <f:FormRow>
                                            <Items>
                                                <f:Panel ID="Panel1" Width="300px" ShowHeader="false" ShowBorder="false" Layout="Column" CssClass="" runat="server">
                                                    <Items>
                                                        <f:RadioButtonList runat="server" ID="rdbIsAgree" Label="是否同意" ShowRedStar="true" AutoPostBack="true"  OnSelectedIndexChanged="rdbIsAgree_SelectedIndexChanged">
                                                            <f:RadioItem Text="同意" Value="true" Selected="true" />
                                                            <f:RadioItem Text="不同意" Value="false" />
                                                        </f:RadioButtonList>
                                                    </Items>
                                                </f:Panel>

                                                <f:Label runat="server" CssStyle="display:none" ></f:Label>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow ID="step1_person">
                                            <Items>
                                                <f:DropDownList ID="drpDutyPerson" runat="server" Label="接收人" EmptyText="--请选择--" AutoSelectFirstItem="false"  LabelAlign="Right" EnableEdit="true">
                                                </f:DropDownList>
                                                <f:Label runat="server"></f:Label>
                                                <f:Label runat="server"></f:Label>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow ID="step1_person2">
                                            <Items>
                                                <f:DropDownList ID="drpProfessionalEngineer" runat="server" Label="专业工程师" EmptyText="--请选择--" AutoSelectFirstItem="false"  LabelAlign="Right" EnableEdit="true">
                                                </f:DropDownList>
                                                <f:DropDownList ID="drpConstructionManager" runat="server" Label="施工经理" EmptyText="--请选择--" AutoSelectFirstItem="false" LabelAlign="Right" EnableEdit="true">
                                                </f:DropDownList>
                                                <f:DropDownList ID="drpProjectManager" runat="server" Label="项目经理" EmptyText="--请选择--" AutoSelectFirstItem="false" LabelAlign="Right" EnableEdit="true">
                                                </f:DropDownList>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:TextArea runat="server" Hidden="true"  ID="reason" Label="请输入原因"></f:TextArea>
                                            </Items>
                                        </f:FormRow>
                                    </Rows>
                                </f:Form>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="next2">
                            <Items>
                                <f:Form ID="Form4" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                                    BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                    <Rows>
                                        <f:FormRow>
                                            <Items>
                                                <f:DropDownList ID="drpUnitHeadManId" runat="server" Label="施工单位负责人" EmptyText="--请选择--" AutoSelectFirstItem="false"  LabelAlign="Right" EnableEdit="true">
                                                </f:DropDownList>
                                                <f:Label runat="server">
                                                </f:Label>
                                            </Items>
                                        </f:FormRow>
                                    </Rows>
                                </f:Form>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="next3">
                            <Items>
                                <f:Form ID="Form5" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                                    BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                    <Rows>
                                        <f:FormRow>
                                            <Items>
                                                <f:Panel ID="Panel2" Width="300px" ShowHeader="false" ShowBorder="false" Layout="Column" CssClass="" runat="server">
                                                    <Items>
                                                        <f:RadioButtonList runat="server" ID="rdbUnitHeadManAgree" Label="是否同意" ShowRedStar="true" AutoPostBack="true"  OnSelectedIndexChanged="rdbUnitHeadManAgree_SelectedIndexChanged">
                                                            <f:RadioItem Text="同意" Value="true" Selected="true" />
                                                            <f:RadioItem Text="不同意" Value="false" />
                                                        </f:RadioButtonList>
                                                    </Items>
                                                </f:Panel>

                                                <f:Label runat="server" CssStyle="display:none" LabelWidth="170px"></f:Label>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow ID="step3_CheckPerson">
                                            <Items>
                                                <f:DropDownList ID="drpSignPerson1" runat="server" Label="安全经理/安全工程师" EmptyText="--请选择--" AutoSelectFirstItem="false"  LabelAlign="Right" EnableEdit="true">
                                                </f:DropDownList>
                                                <f:Label runat="server"></f:Label>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:TextArea runat="server" Hidden="true"  ID="reason1" Label="请输入原因"></f:TextArea>
                                            </Items>
                                        </f:FormRow>
                                    </Rows>
                                </f:Form>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="next4">
                            <Items>
                                <f:Form ID="Form7" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                                    BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                    <Rows>
                                        <f:FormRow>
                                            <Items>
                                                <f:DropDownList ID="drpReCheckOpinion" runat="server" Label="复查意见"  LabelAlign="Right" EnableEdit="true" EmptyText="--请选择--" Readonly="true">
                                                    <f:ListItem Text="整改通过" Value="整改通过" />
                                                    <f:ListItem Text="整改不通过" Value="整改不通过" />
                                                </f:DropDownList>
                                            </Items>
                                        </f:FormRow>
                                    </Rows>
                                </f:Form>
                            </Items>
                        </f:FormRow>
                    </Rows>
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                            <Items>
                                <f:HiddenField ID="hdRectifyNoticesId" runat="server"></f:HiddenField>
                                <f:Button ID="btnSave" OnClick="btnSave_Click" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1">
                                </f:Button>
                                <f:Button ID="btnSubmit" OnClick="btnSubmit_Click" Icon="SystemSaveNew" runat="server" ToolTip="提交" ValidateForms="SimpleForm1">
                                </f:Button>
                                <f:Button ID="btnClose" EnablePostBack="false"
                                    runat="server" Icon="SystemClose">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                </f:Form>
            </Items>
        </f:Panel>
        <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
            Height="500px">
        </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
                runat="server" Text="修改" Icon="TableEdit">
            </f:MenuButton>
            <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
                ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除"
                Icon="Delete">
            </f:MenuButton>
        </f:Menu>
    </form>
    <script type="text/javascript">
        var menuID = '<%= Menu1.ClientID %>';

        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
    </script>
</body>
</html>
