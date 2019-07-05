<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonthReportCView10.aspx.cs" Inherits="FineUIPro.Web.Manager.MonthReportCView10" %>

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
                    <f:GroupPanel ID="GroupPanel60" Layout="Anchor" Title="10.存在的主要问题及改进措施、 需要项目经理、项目主管、公司相关部门、业主协调解决事宜。 <br/>（简要说明项目HSE管理存在的主要问题和需要项目经理、项目主管、公司相关部门、业主协调解决事宜，并提出具体改进措施和建议。）"
                        runat="server">
                        <Items>
                            <f:TextArea runat="server" ID="txtQuestion" Label="" Readonly="true" Height="420px">
                            </f:TextArea>
                        </Items>
                    </f:GroupPanel>
                </Items>
            </f:FormRow>
        </Rows>
    </f:Form>
    </form>
</body>
</html>
