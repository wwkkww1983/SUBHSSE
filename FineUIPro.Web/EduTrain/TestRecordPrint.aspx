<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestRecordPrint.aspx.cs" Inherits="FineUIPro.Web.EduTrain.TestRecordPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <base target="_self" />
    <link href="../../Styles/Style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function pagesetup_null() {
            try {
                var RegWsh = new ActiveXObject("WScript.Shell")
                hkey_key = "header"
                RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, "")
                hkey_key = "footer"
                RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, "")
            } catch (e) { }
        }
        //设置网页打印的页眉页脚为默认值
        function pagesetup_default() {
            try {
                var RegWsh = new ActiveXObject("WScript.Shell")
                hkey_key = "header"
                RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, "&w&b页码，&p/&P")
                hkey_key = "footer"
                RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, "&u&b&d")
            } catch (e) { }
        }
        function printpr() //预览函数
        {
            pagesetup_null(); //预览之前去掉页眉，页脚
            document.getElementById("div1").style.display = "none";
            var a = window.open('about:blank', 'Print');
            a.document.write('<OBJECT ID="WebBrowser1" WIDTH=0 HEIGHT=0 CLASSID="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2"></OBJECT>' + document.getElementById('Table5').innerHTML);
            //ar WebBrowser = '<OBJECT ID="WebBrowser1" WIDTH=0 HEIGHT=0 CLASSID="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2"></OBJECT>';
            a.document.all.WebBrowser1.ExecWB(7, 1);
            a.close();
            //document.body.insertAdjacentHTML('beforeEnd', WebBrowser); //在body标签内加入html（WebBrowser activeX控件）
            //WebBrowser1.ExecWB(7, 1); //打印预览
            //WebBrowser1.outerHTML = ""; //从代码中清除插入的html代码
            pagesetup_default(); //预览结束后页眉页脚恢复默认值
            document.getElementById("div1").style.display = "block";
        }
        function print() //打印函数
        {
            pagesetup_null(); //打印之前去掉页眉，页脚
            document.getElementById("div1").style.display = "none";

            var WebBrowser = '<OBJECT ID="WebBrowser1" WIDTH=0 HEIGHT=0 CLASSID="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2"></OBJECT>';
            document.body.insertAdjacentHTML('beforeEnd', WebBrowser); //在body标签内加入html（WebBrowser activeX控件）
            WebBrowser1.ExecWB(6, 6); //打印
            WebBrowser1.outerHTML = ""; //从代码中清除插入的html代码
            pagesetup_default(); //打印结束后页眉页脚恢复默认值
            document.getElementById("div1").style.display = "block";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table id="Table1" runat="server" width="95%" cellpadding="0" cellspacing="0" align="center"
        border="0">
        <tr>
            <td style="width: 100%; background: url('../Images/bg-1.gif')">
                <div id="div1">
                    <table id="tabbtn" runat="server" width="100%"  cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="right" valign="middle" style="width: 100%; height: 30px;">
                                <img src="../Images/PageSetup.gif" runat="server" id="Img2" onclick="document.all.WebBrowser.ExecWB(8,1)"
                                    alt="页面设置" style="cursor: pointer" />
                                <img src="../Images/PrintSetup.gif" runat="server" id="btnPrint" onclick="document.all.WebBrowser.ExecWB(6,1)"
                                    alt="打印设置" style="cursor: pointer" />
                                <img src="../Images/PrintPreview.gif" runat="server" id="Img1" onclick="printpr()"
                                    alt="打印预览" style="cursor: pointer" />
                                <img src="../Images/Print.gif" runat="server" id="Img3" onclick="print()" alt="打印"
                                    style="cursor: pointer" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <table id="Table5" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0">                                       
                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="gvTest" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                HorizontalAlign="Center" Width="100%" OnDataBound="gvTest_DataBound">
                                <AlternatingRowStyle CssClass="GridRow" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left" ShowHeader="false" ItemStyle-Width="100%" 
                                            ItemStyle-Font-Size="12" HeaderStyle-Font-Size="13"
                                            ItemStyle-Height="35px" HeaderStyle-Height="60px" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate><%=Namea%></HeaderTemplate>
                                        <ItemTemplate>
                                           <asp:Label  runat="server" ID="lbTestTypeName" Text='<%# Bind("TestTypeName") %>' Width="50px" Font-Bold="true"></asp:Label>
                                            <asp:Label ID="labNumber" runat="server" Text=' <%# gvTest.PageIndex * gvTest.PageSize + Container.DataItemIndex + 1%>'
                                                Width="20px" Font-Bold="true"></asp:Label>
                                            <asp:Label runat="server" ID="Abstracts" Text='<%# Bind("Abstracts") %>'></asp:Label>
                                            <asp:Label runat="server" ID="TableCell9" Text='<%# Bind("SelectedItem") %>' Width="120px" ForeColor="#33cc33" Font-Size="13" Font-Bold="true"></asp:Label>
                                            <asp:Table runat="server">
                                                 <asp:TableRow >       
                                                     <asp:TableCell runat="server" ID="TableCell1" Text='<%# Bind("AItem") %>' ColumnSpan="4" Font-Size="12"></asp:TableCell>
                                                      </asp:TableRow>
                                                 <asp:TableRow > 
                                                     <asp:TableCell runat="server" ID="TableCell2" Text='<%# Bind("BItem") %>' ColumnSpan="4" Font-Size="12"></asp:TableCell>
                                                      </asp:TableRow>
                                                 <asp:TableRow > 
                                                     <asp:TableCell runat="server" ID="TableCell3" Text='<%# Bind("CItem") %>' ColumnSpan="4" Font-Size="12"></asp:TableCell>
                                                      </asp:TableRow>
                                                 <asp:TableRow > 
                                                     <asp:TableCell runat="server" ID="TableCell4" Text='<%# Bind("DItem") %>' ColumnSpan="4" Font-Size="12"></asp:TableCell>
                                                      </asp:TableRow>
                                                 <asp:TableRow > 
                                                     <asp:TableCell runat="server" ID="TableCell5" Text='<%# Bind("EItem") %>' ColumnSpan="4" Font-Size="12"></asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow > 
                                                    <asp:TableCell runat="server" ID="TableCell6" Text='<%# Bind("AnswerItems") %>' Width="120px" Font-Size="12" Font-Bold="true"></asp:TableCell>
                                                    <asp:TableCell runat="server" ID="TableCell7" Text='<%# Bind("Score") %>' Width="100px" Font-Size="12" Font-Bold="true"></asp:TableCell>                                    
                                                    <asp:TableCell runat="server" ID="TableCell8" Text='<%# Bind("SubjectScore") %>' Width="100px" Font-Size="12" Font-Bold="true"></asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                    
                                </Columns>
                                <HeaderStyle CssClass="GridBgColr" />
                                <RowStyle CssClass="GridRow" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr style="height:100%">
                        <td  align="center" style="width: 30%; border: 1px solid #000000;">
                           <img alt="" runat="server" id="timg1" src="~/Images/Test_Null.jpg" width="200" height="200"/>
                        </td>
                        <td  align="center" style="width: 30%;  border: 1px solid #000000;" >
                            <img alt="" runat="server" id="timg2"  src="~/Images/Test_Null.jpg" width="200" height="200"/>
                        </td>
                        <td  align="center" style="width: 30%; border: 1px solid #000000;" >
                            <img alt="" runat="server" id="timg3" src="~/Images/Test_Null.jpg" width="200" height="200"/>
                        </td>
                    </tr>
                </table>
               
            </td>
        </tr>
    </table>
    <object id="WebBrowser" width="0" height="0" classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2">
    </object>
    </form>
</body>
</html>
