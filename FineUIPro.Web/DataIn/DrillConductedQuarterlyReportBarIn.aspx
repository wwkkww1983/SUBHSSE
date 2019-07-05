<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DrillConductedQuarterlyReportBarIn.aspx.cs" Inherits="FineUIPro.Web.DataIn.DrillConductedQuarterlyReportBarIn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>导入应急演练开展情况季报</title>
</head>
<body>
    <form id="form1" runat="server">
   <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <%-- <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:Label ID="Label1" runat="server">
                    </f:Label>
                    <f:Label ID="Label" runat="server" Text="数据导入处理中，请稍等......">
                    </f:Label>
                    <f:HiddenField ID="hdShowItems" runat="server">
                    </f:HiddenField>
                </Items>
            </f:FormRow>
        </Rows>
    </f:Form>
   <div id="ProgressBarSide" style="position: absolute; height: 21px; width: 100px;
        color: Silver; border-width: 1px; border-style: Solid; display: none">
        <div id="ProgressBar" style="position: absolute; left: 0px; height: 21px; width: 0%;
            background-color: #3366FF">
        </div>
        <div id="ProgressText" style="position: absolute; left: 0px; height: 21px; width: 100%;
            text-align: center">
        </div>
    </div>--%>
    </form>
</body>
</html>