<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main_bootstrap_pure.aspx.cs" Inherits="FineUIPro.Web.common.main_bootstrap_pure" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <meta name="sourcefiles" content="~/default.aspx;~/code/PageBase.cs;~/code/DataSourceUtil.cs;~/common/menu.xml;~/res/css/default.css;~/res/css/common.css" />
    <style>
        body {
            padding: 10px;
        }

        .page-header {
            margin: 0 0 10px;
            border-bottom: 1px dotted #e2e2e2;
            padding-bottom: 10px;
        }

            .page-header h1 {
                padding: 0;
                margin: 0 8px;
                font-size: 24px;
                font-weight: lighter;
                color: #2679b5;
            }

                .page-header h1 small {
                    margin: 0 6px;
                    font-size: 14px;
                    font-weight: normal;
                    color: #8089a0;
                }



        .f-tabstrip.f-simple .f-icon-home {
            color: #69aa46!important;
        }

        .badge {
            display: inline-block;
            padding: 0 4px;
            font-size: 12px;
            line-height: 15px;
            color: #fff;
            text-align: center;
            white-space: nowrap;
            vertical-align: baseline;
            background-color: #999;
            border-radius: 10px;
            margin-left: 5px;
        }

        .badge-danger {
            background-color: #d15b47!important;
        }

        .largepanelheader > .f-panel-header {
            background-color: #fff;
        }

            .largepanelheader > .f-panel-header .f-panel-icon,
            .largepanelheader > .f-panel-header .f-panel-title-text {
                color: #478fca;
            }

            .largepanelheader > .f-panel-header .f-panel-title-text {
                font-size: 20px;
                line-height: 28px;
            }

        .largepanelheader.green > .f-panel-header .f-panel-icon,
        .largepanelheader.green > .f-panel-header .f-panel-title-text {
            color: #69aa46;
        }

        .largepanelheader.orange > .f-panel-header .f-panel-icon,
        .largepanelheader.orange > .f-panel-header .f-panel-title-text {
            color: #ff892a;
        }

        .largepanelheader.purple > .f-panel-header .f-panel-icon,
        .largepanelheader.purple > .f-panel-header .f-panel-title-text {
            color: #892e65;
        }



        .accordionpane1 .f-panel-icon,
        .accordionpane1 .f-panel-title-text {
            color: #892e65 !important;
        }

        .accordionpane2 .f-panel-icon,
        .accordionpane2 .f-panel-title-text {
            color: #2E8965 !important;
        }

        .accordionpane3 .f-panel-icon,
        .accordionpane3 .f-panel-title-text {
            color: #F2BB46 !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server"></f:PageManager>
        <f:Panel ID="Panel1" Layout="Region" ShowBorder="false" ShowHeader="false" runat="server" BodyPadding="10px">
            <Items>
                <f:ContentPanel ID="topPanel" RegionPosition="Top" ShowBorder="false" ShowHeader="false" EnableCollapse="true" runat="server" BodyPadding="10px 0">
                    <div class="page-header">
                        <h1>首页
					        <small>
                                <i class="ui-icon f-icon-angle-double-right"></i>
                                基本元素展示
                            </small>
                        </h1>
                    </div>
                </f:ContentPanel>
                <f:Panel runat="server" ShowHeader="false" ShowBorder="false" Layout="Column" RegionPosition="Center" AutoScroll="true">
                    <Items>
                        <f:Panel ID="Panel2" runat="server" ShowHeader="false" ShowBorder="false" ColumnWidth="50%" MarginRight="10px">
                            <Items>
                                <f:TabStrip ID="TabStrip1" CssClass="f-tabstrip-theme-simple" Height="150px" 
                                    ShowBorder="true" TabPosition="Top" MarginBottom="20px"
                                    EnableTabCloseMenu="false" runat="server">
                                    <Tabs>
                                        <f:Tab ID="Tab1" Title="标签一" BodyPadding="5px" Layout="Fit" IconFont="Home"
                                            runat="server">
                                            <Items>
                                                <f:Label ID="Label1" Text="第三个标签的内容" runat="server" />
                                            </Items>
                                        </f:Tab>
                                        <f:Tab ID="Tab2" Title="标签二<span class='badge badge-danger'>4</span>" BodyPadding="5px"
                                            runat="server">
                                            <Items>
                                                <f:Label ID="Label2" Text="第三个标签的内容" runat="server" />
                                            </Items>
                                        </f:Tab>
                                        <f:Tab ID="Tab3" Title="标签三" BodyPadding="5px" runat="server">
                                            <Items>
                                                <f:Label ID="Label3" Text="第三个标签的内容" runat="server" />
                                            </Items>
                                        </f:Tab>
                                    </Tabs>
                                </f:TabStrip>
                                <f:Panel runat="server" CssClass="largepanelheader" ShowBorder="false" IconFont="AlignJustify" Title="手风琴控件" ShowHeader="true" BodyPadding="10px" MarginBottom="20px">
                                    <Items>
                                        <f:Accordion ID="Accordion1" runat="server" Height="180px" ShowHeader="false"
                                            EnableFill="true" EnableActiveOnTop="false" ShowCollapseTool="true"
                                            ShowBorder="true" ActivePaneIndex="1" EnableCollapse="true">
                                            <Panes>
                                                <f:AccordionPane ID="AccordionPane1" CssClass="accordionpane1" runat="server" Title="面板一" IconFont="Tag" BodyPadding="2px 5px">
                                                    <Items>
                                                        <f:Label ID="Label7" Text="面板一中的文本" runat="server">
                                                        </f:Label>
                                                    </Items>
                                                </f:AccordionPane>
                                                <f:AccordionPane ID="AccordionPane2" CssClass="accordionpane2" runat="server" Title="面板二" IconFont="Tag" BodyPadding="2px 5px">
                                                    <Items>
                                                        <f:Label ID="Label8" Text="面板二中的文本" runat="server">
                                                        </f:Label>
                                                    </Items>
                                                </f:AccordionPane>
                                                <f:AccordionPane ID="AccordionPane3" CssClass="accordionpane3" runat="server" Title="面板三" IconFont="Tag" BodyPadding="2px 5px">
                                                    <Items>
                                                        <f:Label ID="Label9" Text="面板三中的文本" runat="server">
                                                        </f:Label>
                                                    </Items>
                                                </f:AccordionPane>
                                            </Panes>
                                        </f:Accordion>
                                    </Items>
                                </f:Panel>
                                <f:ContentPanel ID="Panel4" runat="server" Height="260px" CssClass="largepanelheader green" ShowBorder="false"
                                    IconFont="Bold" Title="按钮控件" ShowHeader="true" BodyPadding="10px" MarginBottom="20px">
                                    <f:Button ID="btnIcon1" Text="图标在左侧" IconFont="Home" runat="server" CssClass="f-btn-gray marginr" />
                                    <f:Button ID="btnIcon2" Text="图标在右侧" IconAlign="Right" IconFont="Car" runat="server" />
                                    <br />
                                    <br />
                                    <f:Button ID="btnIcon3" Text="图标在上面" IconAlign="Top" IconFont="Camera" runat="server" CssClass="f-btn-info marginr" />
                                    <f:Button ID="btnIcon4" Text="图标在下面" IconAlign="Bottom" IconFont="Phone" runat="server" CssClass="f-btn-success" />
                                    <br />
                                    <br />
                                    <f:Button ID="btnCustomIcon" Text="点击修改图标（在三个图标之前切换）" CssClass="f-btn-warning" IconFont="VolumeUp" runat="server" OnClick="btnCustomIcon_Click" />
                                    <br />
                                    <br />
                                    <f:Button ID="Button1" IconFont="Android" CssClass="f-btn-danger marginr" runat="server" />
                                    <f:Button ID="Button2" IconFont="Apple" CssClass="f-btn-inverse" runat="server" />
                                </f:ContentPanel>
                            </Items>
                        </f:Panel>
                        <f:Panel ID="Panel3" runat="server" ShowHeader="false" ShowBorder="false" ColumnWidth="50%">
                            <Items>
                                <f:TabStrip ID="TabStrip2" CssClass="f-tabstrip-theme-simple" Height="150px" ShowBorder="true" TabPosition="Bottom" MarginBottom="20px"
                                    EnableTabCloseMenu="false" ActiveTabIndex="1" runat="server">
                                    <Tabs>
                                        <f:Tab ID="Tab4" Title="标签一" BodyPadding="5px" Layout="Fit" IconFont="Home"
                                            runat="server">
                                            <Items>
                                                <f:Label ID="Label4" Text="第三个标签的内容" runat="server" />
                                            </Items>
                                        </f:Tab>
                                        <f:Tab ID="Tab5" Title="标签二<span class='badge badge-danger'>4</span>" BodyPadding="5px"
                                            runat="server">
                                            <Items>
                                                <f:Label ID="Label5" Text="第三个标签的内容" runat="server" />
                                            </Items>
                                        </f:Tab>
                                        <f:Tab ID="Tab6" Title="标签三" BodyPadding="5px" runat="server">
                                            <Items>
                                                <f:Label ID="Label6" Text="第三个标签的内容" runat="server" />
                                            </Items>
                                        </f:Tab>
                                    </Tabs>
                                </f:TabStrip>
                                <f:Panel ID="Panel5" runat="server" CssClass="largepanelheader orange" ShowBorder="false" IconFont="AlignJustify" Title="手风琴控件" ShowHeader="true" BodyPadding="10px" MarginBottom="20px">
                                    <Items>
                                        <f:Accordion ID="Accordion2" CssClass="f-accordion-theme-simple" runat="server" Height="180px" ShowHeader="false"
                                            EnableFill="false" EnableActiveOnTop="false" ShowCollapseTool="true"
                                            ShowBorder="false" ActivePaneIndex="1" EnableCollapse="false">
                                            <Panes>
                                                <f:AccordionPane ID="AccordionPane4" CssClass="accordionpane1" runat="server" Title="面板一" IconFont="Tag" BodyPadding="2px 5px">
                                                    <Content>
                                                        面板一xxxx中的文本<br />
                                                        <br />
                                                        <br />
                                                        <br />
                                                    </Content>
                                                </f:AccordionPane>
                                                <f:AccordionPane ID="AccordionPane5" CssClass="accordionpane2" runat="server" Title="面板二" IconFont="Tag" BodyPadding="2px 5px">
                                                    <Content>
                                                        面板二中的文本<br />
                                                        <br />
                                                    </Content>
                                                </f:AccordionPane>
                                                <f:AccordionPane ID="AccordionPane6" CssClass="accordionpane3" runat="server" Title="面板三" IconFont="Tag" BodyPadding="2px 5px">
                                                    <Content>
                                                        面板三中的文本<br />
                                                        <br />
                                                    </Content>
                                                </f:AccordionPane>
                                            </Panes>
                                        </f:Accordion>
                                    </Items>
                                </f:Panel>
                                <f:Panel ID="Panel6" runat="server" Height="260px" CssClass="largepanelheader purple" ShowBorder="false" IconFont="User"
                                    Title="登陆面板" ShowHeader="true" BodyPadding="10px" MarginBottom="20px" EnableIFrame="true" IFrameUrl="~/common/main_bootstrap_pure_login.aspx">
                                </f:Panel>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:Panel>
            </Items>
        </f:Panel>

    </form>
</body>
</html>
