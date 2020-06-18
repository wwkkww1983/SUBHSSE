<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RectifyNoticesView.aspx.cs" Inherits="FineUIPro.Web.Check.RectifyNoticesView1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Panel4"/>
        <f:Panel ID="Panel4" runat="server" BodyPadding="10px" ShowBorder="true" ShowHeader="false" AutoScroll="true">
            <Items>
                <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                    BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                    <Rows>
                        <f:FormRow runat="server">
                            <Items>
                                <f:TextBox ID="txtProjectName" runat="server" Label="项目名称" MaxLength="70" LabelWidth="160px" Readonly="true">
                                </f:TextBox>
                                <f:TextBox ID="txtRectifyNoticesCode" runat="server" Label="编号" MaxLength="70" LabelWidth="160px" Readonly="true">
                                </f:TextBox>

                            </Items>
                        </f:FormRow>
                        <f:FormRow runat="server">
                            <Items>
                                <f:DropDownList ID="drpUnitId" runat="server" Label="受检单位名称" LabelWidth="160px" LabelAlign="Right" EnableEdit="true" Readonly="true">
                                </f:DropDownList>
                                <f:DropDownList ID="drpWorkAreaId" runat="server" Label="单位工程名称" LabelWidth="160px" LabelAlign="Right" EnableEdit="true" Readonly="true">
                                </f:DropDownList>
                            </Items>
                        </f:FormRow>
                        <f:FormRow runat="server">
                            <Items>
                                <f:DropDownList ID="drpCheckPerson" runat="server" Label="检查人员" LabelWidth="160px" LabelAlign="Right" EnableEdit="true" EnableMultiSelect="true" AutoPostBack="true" Readonly="true">
                                </f:DropDownList>
                                <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="检查日期" ID="txtCompleteDate"
                                    LabelAlign="right" LabelWidth="160px" ShowRedStar="true" Readonly="true">
                                </f:DatePicker>
                            </Items>
                        </f:FormRow>
                        <f:FormRow runat="server">
                            <Items>
                                <f:DropDownList ID="drpHiddenHazardType" runat="server" Label="隐患类别" LabelWidth="160px" LabelAlign="Right" EnableEdit="true" EmptyText="--请选择--" Readonly="true">
                                    <f:ListItem Text="一般" Value="1" />
                                    <f:ListItem Text="较大" Value="2" />
                                    <f:ListItem Text="重大" Value="3" />
                                </f:DropDownList>
                                <f:Label runat="server"></f:Label>
                            </Items>
                        </f:FormRow>
                    </Rows>

                </f:Form>
                <f:Form ID="Form2" ShowBorder="true" ShowHeader="true" Title="安全隐患及整改要求" AutoScroll="true"
                    runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                    <Rows>
                        <f:FormRow>
                            <Items>
                                <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="true" EnableColumnLines="true" EnableColumnMove="true" runat="server" BoxFlex="1" DataKeyNames="RectifyNoticesItemId" DataIDField="RectifyNoticesItemId" AllowSorting="true" SortField="RectifyNoticesItemId" SortDirection="ASC" EnableTextSelection="True" Height="260px"
                                    EnableRowDoubleClickEvent="true" >
                                    <Columns>
                                        <f:RenderField Width="200px" ColumnID="WrongContent" DataField="WrongContent"
                                            FieldType="String" HeaderText="具体位置及隐患内容" HeaderTextAlign="Center" TextAlign="Center">
                                        </f:RenderField>
                                        <f:RenderField Width="200px" ColumnID="Requirement" DataField="Requirement" ExpandUnusedSpace="true"
                                            FieldType="String" HeaderText="整改要求" HeaderTextAlign="Center" TextAlign="Center">
                                        </f:RenderField>
                                        <f:RenderField Width="150px" ColumnID="LimitTime" DataField="LimitTime"
                                            FieldType="string" HeaderText="整改期限" HeaderTextAlign="Center" TextAlign="Center">
                                        </f:RenderField>
                                        <f:RenderField Width="150px" ColumnID="RectifyResults" DataField="RectifyResults" FieldType="string" HeaderText="整改结果" HeaderTextAlign="Center" TextAlign="Center">
                                        </f:RenderField>
                                        <f:RenderField Width="150px" ColumnID="IsRectify" DataField="IsRectify" FieldType="string" HeaderText="是否合格" HeaderTextAlign="Center" TextAlign="Center">
                                        </f:RenderField>
                                    </Columns>
                                </f:Grid>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>

                <f:Form ID="Form3" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                    BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                    <Rows>
                        <f:FormRow ID="next4">
                            <Items>
                                <f:Form ID="Form7" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                                    BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                    <Rows>
                                        <f:FormRow>
                                            <Items>
                                                 <f:Label runat="server" ID="txtReCheckOpinion"></f:Label>
                                            </Items>
                                        </f:FormRow>
                                    </Rows>
                                </f:Form>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
                <f:Form ID="Form4" ShowBorder="true" ShowHeader="true" Title="安全隐患操作步骤" AutoScroll="true"
                    runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                    <Rows>
                        <f:FormRow>
                            <Items>
                                <f:Grid ID="gvFlowOperate" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server" DataIDField="FlowOperateId" AllowSorting="true" SortField="OperateTime" SortDirection="ASC" EnableTextSelection="True" Height="260px">
                                    <Columns>
                                        <f:RenderField Width="200px" ColumnID="OperateName" DataField="OperateName" FieldType="String" HeaderText="操作步骤" ExpandUnusedSpace="true"  HeaderTextAlign="Center" TextAlign="Center">
                                        </f:RenderField>
                                        <f:RenderField Width="200px" ColumnID="UserName" DataField="UserName" FieldType="String" HeaderText="操作人" HeaderTextAlign="Center" TextAlign="Center">
                                        </f:RenderField>
                                         <f:RenderField Width="150px" ColumnID="Opinion" DataField="Opinion" FieldType="string" HeaderText="操作意见" HeaderTextAlign="Center" TextAlign="Center">
                                        </f:RenderField>
                                        <f:RenderField Width="150px" ColumnID="OperateTime" DataField="OperateTime" FieldType="string" HeaderText="操作时间" HeaderTextAlign="Center" TextAlign="Center">
                                        </f:RenderField>
                                       
                                    </Columns>
                                </f:Grid>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
            </Items>
        </f:Panel>
        <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
            Height="500px">
        </f:Window>
    </form>
</body>
</html>
