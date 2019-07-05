<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonthReportCEdit1.aspx.cs"
    Inherits="FineUIPro.Web.Manager.MonthReportCEdit1" %>

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
                    <f:GroupPanel ID="GroupPanel2" Layout="Anchor" Title="1.项目概况" runat="server">
                        <Items>
                            <f:Label ID="lbProjectName" runat="server" Label="项目名称" LabelWidth="150px">
                            </f:Label>
                            <f:Label ID="lblMainUnitName" runat="server" Label="用户名称" LabelWidth="150px">
                            </f:Label>
                            <f:Label ID="lblProjectAddress" runat="server" Label="项目地址" LabelWidth="150px">
                            </f:Label>
                            <f:Label ID="lblProjectCode" runat="server" Label="项目号" LabelWidth="150px">
                            </f:Label>
                            <f:Label ID="lblContractNo" runat="server" Label="合同号" LabelWidth="150px">
                            </f:Label>
                            <f:Label ID="lblProjectType" runat="server" Label="项目类型" LabelWidth="150px">
                            </f:Label>
                            <f:Label ID="lblWorkRange" runat="server" Label="工作范围" LabelWidth="150px">
                            </f:Label>
                            <f:Label ID="lblDuration" runat="server" Label="项目建设工期(月)" LabelWidth="150px">
                            </f:Label>
                            <f:Label ID="lblStartDate" runat="server" Label="项目施工计划开工时间" LabelWidth="150px">
                            </f:Label>
                            <f:Label ID="lblEndDate" runat="server" Label="工程中间交接时间" LabelWidth="150px">
                            </f:Label>
                        </Items>
                    </f:GroupPanel>
                </Items>
            </f:FormRow>
        </Rows>
    </f:Form>
    </form>
</body>
</html>
