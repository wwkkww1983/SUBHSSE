<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonthReportCEdit3.aspx.cs" Inherits="FineUIPro.Web.Manager.MonthReportCEdit3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server">
    </f:PageManager>
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
           <f:FormRow>
                <Items>
                    <f:GroupPanel ID="GroupPanel10" Layout="Anchor" Title="3.本月项目现场HSE人工日统计" runat="server" AutoScroll="true">
                        <Items>
                            <f:Grid ID="gvHSEDay" ShowBorder="true" ShowHeader="false" Title="本月项目现场HSE人工日统计" 
                                runat="server" AllowCellEditing="true" ClicksToEdit="1" DataIDField="MonthHSEDay" Width="1200px"
                                DataKeyNames="MonthHSEDay" EnableMultiSelect="false" ShowGridHeader="true" Height="420px"
                                EnableColumnLines="true" AutoScroll="true">
                                <Columns>
                                    <f:GroupField EnableLock="true" HeaderText="连续安全工作天数" TextAlign="Center">
                                        <Columns>
                                            <f:RenderField Width="200px" ColumnID="MonthHSEDay" DataField="MonthHSEDay" FieldType="Int"
                                                HeaderText="本月连续安全工作天数" HeaderTextAlign="Center" TextAlign="Left">
                                                <Editor>
                                                    <f:NumberBox ID="nbMonthHSEDay" NoDecimal="true" NoNegative="true" MinValue="0" runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="200px" ColumnID="SumHSEDay" DataField="SumHSEDay" FieldType="Int"
                                                HeaderText="累计连续安全工作天数" HeaderTextAlign="Center" TextAlign="Left">
                                                <Editor>
                                                    <f:NumberBox ID="nbSumHSEDay" NoDecimal="true" NoNegative="true" MinValue="0" runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                        </Columns>
                                    </f:GroupField>
                                    <f:GroupField EnableLock="true" HeaderText="HSE人工日" TextAlign="Center">
                                        <Columns>
                                            <f:RenderField Width="130px" ColumnID="MonthHSEWorkDay" DataField="MonthHSEWorkDay"
                                                FieldType="Int" HeaderText="本月HSE人工日" HeaderTextAlign="Center" TextAlign="Left">
                                                <Editor>
                                                    <f:NumberBox ID="nbMonthHSEWorkDay" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="170px" ColumnID="YearHSEWorkDay" DataField="YearHSEWorkDay"
                                                FieldType="Int" HeaderText="年度累计HSE人工日" HeaderTextAlign="Center" TextAlign="Left">
                                                <Editor>
                                                    <f:NumberBox ID="nbYearHSEWorkDay" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="170px" ColumnID="SumHSEWorkDay" DataField="SumHSEWorkDay" FieldType="Int"
                                                HeaderText="总累计HSE人工日" HeaderTextAlign="Center" TextAlign="Left">
                                                <Editor>
                                                    <f:NumberBox ID="nbSumHSEWorkDay" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                        </Columns>
                                    </f:GroupField>
                                    <f:GroupField EnableLock="true" HeaderText="安全人工时" TextAlign="Center">
                                        <Columns>
                                            <f:GroupField EnableLock="true" HeaderText="本月安全人工时" TextAlign="Center">
                                                <Columns>
                                                    <f:RenderField Width="100px" ColumnID="HseManhours" DataField="HseManhours" FieldType="Int"
                                                        HeaderText="本单位" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:NumberBox ID="nbHseManhours" NoDecimal="true" NoNegative="true" MinValue="0" runat="server">
                                                            </f:NumberBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="100px" ColumnID="SubcontractManHours" DataField="SubcontractManHours"
                                                        FieldType="Int" HeaderText="分包商" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:NumberBox ID="nbSubcontractManHours" NoDecimal="true" NoNegative="true" MinValue="0"
                                                                runat="server">
                                                            </f:NumberBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                </Columns>
                                            </f:GroupField>
                                            <f:RenderField Width="155px" ColumnID="TotalHseManhours" DataField="TotalHseManhours"
                                                ExpandUnusedSpace="true" FieldType="Int" HeaderText="累计安全人工时" HeaderTextAlign="Center"
                                                TextAlign="Left">
                                                <Editor>
                                                    <f:NumberBox ID="nbTotalHseManhours" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                        </Columns>
                                    </f:GroupField>
                                </Columns>
                            </f:Grid>
                        </Items>
                    </f:GroupPanel>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                </Items>
            </f:Toolbar>
            </Toolbars>
    </f:Form>
    </form>
</body>
</html>
