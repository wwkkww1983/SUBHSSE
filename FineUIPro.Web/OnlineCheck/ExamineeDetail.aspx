<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExamineeDetail.aspx.cs"
    Inherits="FineUIPro.Web.OnlineCheck.ExamineeDetail" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .f-grid-row.special
        {
            background-color: Red;
            background-image: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" >
        <Items>
            <f:Grid runat="server" AllowCellEditing="false" ClicksToEdit="1" DataIDField="ExamineeDetailId" 
                EnableColumnLines="True" EnableRowLines="True" DataKeyNames="ExamineeDetailId"  Height="445px"
                ShowHeader="False"  ID="Grid1" EnableMultiSelect="false" OnRowDataBound="Grid1_RowDataBound" EnableTextSelection="True">
                <Columns>
                    <f:RenderField ID="RenderField1" runat="server"  FieldType="String"
                        DataField="TestCode" ColumnID="TestCode" HeaderText="试卷题号" Width="100px" TextAlign="Center"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField ID="RenderField3" runat="server"  FieldType="String"
                        DataField="TestType" ColumnID="TestType" HeaderText="试题类型" Width="120px" TextAlign="Center"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField ID="RenderField5" runat="server"  DataField="ItemType"
                        ColumnID="ItemType" HeaderText="题型" Width="80px" TextAlign="Center" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField ID="RenderField6" runat="server" EnableColumnEdit="False" FieldType="String"
                        DataField="TestScore" ColumnID="TestScore" HeaderText="分数/题" Width="120px" TextAlign="Center"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField ID="RenderField7" runat="server"  DataField="AnswerKey"
                        ColumnID="AnswerKey" HeaderText="答案" Width="80px" TextAlign="Center" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField ID="RenderField8" runat="server"  DataField="TestKey"
                        ColumnID="TestKey" HeaderText="正确答案" Width="80px">
                    </f:RenderField>
                </Columns>
            </f:Grid>
        </Items>
    </f:Panel>
    </form>
</body>
</html>
