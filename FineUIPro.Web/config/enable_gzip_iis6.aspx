﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="enable_gzip_iis6.aspx.cs" Inherits="FineUIPro.Web.config.enable_gzip_iis6" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        ol li {
            margin-bottom: 5px;
        }

        .title {
            font-size: 14px;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager runat="server"></f:PageManager>
        <div class="title">
            如何在IIS6中启用GZIP压缩
        </div>
        <div style="margin: 10px 0;">
            启用GZIP压缩可以减少网络传输的数据量，加快页面的显示速度。
        </div>
        <ol>
            <li>展开IIS管理器，在网站菜单上点击右键，点击属性菜单，选择服务选项卡，按下图选中启用压缩的复选框。
            <br />
                <img src="../res/images/enable_gzip_iis6_1.png" />
                <br />
                <img src="../res/images/enable_gzip_iis6_2.png" />
                <br />
            </li>
            <li>从<a href="http://www.microsoft.com/downloads/details.aspx?FamilyID=56fc92ee-a71a-4c73-b628-ade629c89499&DisplayLang=en">这里</a>下载IIS
            6.0资源工具包，安装后运行Metabase Explorer。
            <br />
                <img src="../res/images/enable_gzip_iis6_3.png" />
                <br />
            </li>
            <li>展开 LM>W3SVC>Filters>Compression，你需要如下编辑 gzip 和 deflate 两个菜单。
            <br />
                HcScriptFileExtensions:asp,dll,aspx,axd,asmx,php,exe
            <br />
                HcDynamicCompressionLevel:10
            <br />
                HcFileExtensions:htm,html,js,css
            <br />
                <img src="../res/images/enable_gzip_iis6_4.png" />
                <br />
            </li>
            <li>重启IIS。
            <br />
                <img src="../res/images/enable_gzip_iis6_5.png" />
            </li>
        </ol>
        <div>
            原文链接：<a href="http://www.codeproject.com/Articles/31073/Enable-Gzip-compression-in-IIS-6-0-for-ASP-NET-2-0">http://www.codeproject.com/Articles/31073/Enable-Gzip-compression-in-IIS-6-0-for-ASP-NET-2-0</a>
        </div>
    </form>
</body>
</html>
