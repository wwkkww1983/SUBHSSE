<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckSpecialPrint.aspx.cs" Inherits="FineUIPro.Web.Check.CheckSpecialPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/Style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
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
    <table id="Table2" runat="server" width="100%" cellpadding="0" cellspacing="0" align="center">
        <tr>
            <td style="width: 100%; background: url('../Images/bg-1.gif'); border-width: 0px;
                border-width: 0px;">
                <div id="div1">
                    <table id="tabbtn" runat="server" width="100%" style="background: url('../Images/bg-1.gif')"
                        cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left" valign="middle" style="width: 50%; font-size: 11pt; font-weight: bold;
                                border-width: 0px;">
                                &nbsp;
                            </td>
                            <td align="right" valign="middle" style="width: 50%; height: 30px; border-width: 0px;
                                border-width: 0px;">
                                <img src="../Images/PageSetup.gif" runat="server" id="Img2" onclick="document.all.WebBrowser.ExecWB(8,1)"
                                    alt="页面设置" style="cursor: pointer" />
                                <img src="../Images/PrintSetup.gif" runat="server" id="btnPrint" onclick="document.all.WebBrowser.ExecWB(6,1)"
                                    alt="打印设置" style="cursor: pointer" />
                                <img src="../Images/PrintPreview.gif" runat="server" id="Img1" onclick="printpr()"
                                    alt="打印预览" style="cursor: pointer" />
                                <img src="../Images/Print.gif" runat="server" id="Img3" onclick="print()" alt="打印"
                                    style="cursor: pointer" />
                            </td>
                            <td align="right" valign="middle" style="width: 50%; height: 30px; border-width: 0px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <table id="Table5" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0"
        bordercolor="#000000">
        <tr>
            <td>
                <table id="Table11" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0"
                    bordercolor="#000000">
                    <tr>
                        <td align="center" colspan="3">
                            <asp:Label ID="Label1" runat="server" Font-Size="22px" Font-Bold="true" Text="HSE检查表"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 33%; height: 45px;">
                            <asp:Label ID="Label4" runat="server" Font-Size="17px" Text="检查类型：HSE专项检查"></asp:Label>
                        </td>
                        <td align="left" style="width: 33%; height: 45px;">
                            <asp:Label ID="txtCheckTime" runat="server" Font-Size="17px"></asp:Label>
                        </td>
                        <td align="left" style="width: 33%; height: 45px;">
                            <asp:Label ID="txtCheckType" runat="server" Font-Size="17px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="left" style="width: 33%; height: 45px;">
                            <asp:Label ID="txtCheckPerson" runat="server" Font-Size="17px"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table id="Table1" runat="server" width="100%" cellpadding="0" cellspacing="0" border="0"
                    >
                    <tr style="height: 32px">
                        <td colspan="6" align="left" style="border:none;">
                            <div runat="server" id="div3">
                            </div>
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
