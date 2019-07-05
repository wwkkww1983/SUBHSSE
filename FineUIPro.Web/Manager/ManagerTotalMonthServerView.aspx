<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManagerTotalMonthServerView.aspx.cs" ValidateRequest="false" Inherits="FineUIPro.Web.Manager.ManagerTotalMonthServerView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>月总结</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="true" AutoScroll="true" Title="月总结"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow runat="server" ID="FormRow1">
                <Items>
                    <f:HtmlEditor runat="server" Label="月总结" ID="txtFileContent" ShowLabel="false" Editor="UMEditor"
                        BasePath="~/res/umeditor/" ToolbarSet="Full" Height="500" LabelAlign="Right">
                    </f:HtmlEditor>
                </Items>
            </f:FormRow>       
        </Rows>   
    </f:Form>
    </form>
</body>
</html>
