<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BuildTest.aspx.cs" Inherits="FineUIPro.Web.OnlineCheck.BuildTest" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style>
        .f-grid-row-summary .f-grid-cell-inner
        {
            font-weight: bold;
            color: red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" runat="server" RedStarPosition="BeforeText" ShowBorder="false"
        ShowHeader="false">
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
                <Items>
                    <f:DropDownList runat="server" ID="ddlWorkPost" EnableSimulateTree="true" Width="250px"
                        Label="岗位" ShowLabel="false" CompareType="String" CompareValue="null" CompareOperator="NotEqual">
                    </f:DropDownList>
                    <f:DropDownList runat="server" ID="ddlABVolume" EnableSimulateTree="true" Width="200px"
                        Label="AB卷" ShowLabel="false" CompareType="String" CompareValue="null" CompareOperator="NotEqual"
                        OnSelectedIndexChanged="ddlABVolume_SelectedIndexChanged" AutoPostBack="true">
                        <f:ListItem Text="A" Value="A" />
                        <f:ListItem Text="B" Value="B" />
                    </f:DropDownList>
                    <f:ToolbarFill runat="server"></f:ToolbarFill>
                    <f:Button ID="btnSave" ToolTip="保存条件" Icon="Add" runat="server" OnClick="btnSave_Click"
                        ValidateForms="Toolbar1">
                    </f:Button>
                    <f:Button ID="btnBuild" ToolTip="生成试卷" Icon="tick" runat="server" OnClick="btnBuild_Click">
                    </f:Button>
                    <f:Button ID="btnDelete" ToolTip="删除试卷" Icon="Delete" ConfirmText="确定删除当前数据？" OnClick="btnDelete_Click"
                        runat="server" ValidateForms="Toolbar1">
                    </f:Button>                    
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server"
                BoxFlex="1" DataKeyNames="TestConditionId" AllowCellEditing="true" ClicksToEdit="1"
                DataIDField="TestConditionId" EnableSummary="true" SummaryPosition="Flow" EnableColumnLines="true" EnableTextSelection="True">
                <Columns>
                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                    <f:RenderField Width="120px" ColumnID="TestType" DataField="TestType" SortField="TestType"
                        FieldType="String" HeaderText="试题类型" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="80px" ColumnID="ItemType" DataField="ItemType" FieldType="String"
                        HeaderText="题型" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="80px" ColumnID="Testnum" DataField="Testnum" FieldType="String"
                        HeaderText="题数" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="80px" ColumnID="SelectNumber" DataField="SelectNumber" FieldType="String"
                        HeaderText="选数" HeaderTextAlign="Center" TextAlign="Center">
                        <Editor>
                            <f:NumberBox ID="tbxSelectNumber" NoDecimal="true" NoNegative="true" MinValue="0"
                                runat="server">
                            </f:NumberBox>
                        </Editor>
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="TestScore" DataField="TestScore" FieldType="String"
                        HeaderText="分数/题" HeaderTextAlign="Center" TextAlign="Center">
                        <Editor>
                            <f:NumberBox ID="tbxTestScore" NoDecimal="true" NoNegative="true" MinValue="0" runat="server">
                            </f:NumberBox>
                        </Editor>
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="TestTotalScore" DataField="TestTotalScore"
                        FieldType="String" HeaderText="总分数" HeaderTextAlign="Center" TextAlign="Center">
                        <Editor>
                            <f:NumberBox ID="NumberBox2" NoDecimal="true" NoNegative="true" runat="server">
                            </f:NumberBox>
                        </Editor>
                    </f:RenderField>
                </Columns>
                <Listeners>
                    <f:Listener Event="afteredit" Handler="onGridAfterEdit" />
                    <f:Listener Event="dataload" Handler="onGridDataLoad" />
                </Listeners>
            </f:Grid>
        </Items>
    </f:Form>
    </form>
    <script>
        function updateSummary() {
            // 回发到后台更新
            __doPostBack('', 'UPDATE_SUMMARY');
        }

        function onGridAfterEdit(event, value, params) {
            var me = this, columnId = params.columnId, rowId = params.rowId;
            var tbxSelectNumberClientID = '<%= tbxSelectNumber.ClientID %>';

            if (columnId === 'SelectNumber' || columnId === 'TestScore' || columnId === 'Testnum') {
                var selectNumber = me.getCellValue(rowId, 'SelectNumber');
                var testScore = me.getCellValue(rowId, 'TestScore');
                var testnum = me.getCellValue(rowId, 'Testnum');

                if (Number(selectNumber) > Number(testnum)) {
                    me.updateCellValue(rowId, 'SelectNumber', '');
                    alert("选题数不能大于题库数!")
                    return;
                }

                if (selectNumber.toString() != "" && testScore.toString() != "") {
                    me.updateCellValue(rowId, 'TestTotalScore', selectNumber * testScore);
                }
            }
            updateSummary();
        }


        function onGridDataLoad(event) {
            this.mergeColumns(['TestType', 'ItemType']);
        }
    </script>
</body>
</html>
