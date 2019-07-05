using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using BLL;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Text;

namespace FineUIPro.Web.common
{
    public partial class main : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (this.CurrUser.UserId == BLL.Const.sysglyId)
                //{
                //    //this.Panel10.Hidden = false;
                //}
                BindGridNewDynamic("close");
                BindGridNewContent("close");
                BindGridNotice("close");
                this.ProjectPic();
            }
            else
            {
                if (GetRequestEventArgument() == "reloadGridNewDynamic")
                {
                    BindGridNewDynamic("close");
                }
                if (GetRequestEventArgument() == "reloadGridNotice")
                {
                    BindGridNotice("close");
                }
            }
        }

        #region 最新动态
        /// <summary>
        /// 绑定数据(最新动态)
        /// </summary>
        private void BindGridNewDynamic(string type)
        {
            List<Model.View_NewDynamic> newDynamic = new List<Model.View_NewDynamic>();
            if (this.CurrUser.UserId == BLL.Const.sysglyId)
            {
                var sysVer = (from x in Funs.DB.Sys_Version where x.IsSub == true orderby x.VersionDate descending select x).FirstOrDefault();
                if (sysVer != null && sysVer.VersionName != Funs.SystemVersion && sysVer.VersionName.Length > 27 && Funs.SystemVersion.Length > 27)
                {
                    DateTime? vdate = Funs.GetNewDateTime(sysVer.VersionName.Substring(17, 10));
                    DateTime? fdate = Funs.GetNewDateTime(Funs.SystemVersion.Substring(17, 10));
                    string vNum = sysVer.VersionName.Substring(27);
                    string fNum = Funs.SystemVersion.Substring(27);
                    if (vdate > fdate || (vdate == fdate && vNum != fNum))
                    {
                        Model.View_NewDynamic vDynamic = new Model.View_NewDynamic
                        {
                            Id = sysVer.AttachUrl,
                            Type = "系统版本",
                            Name = "有新版本：" + sysVer.VersionName,
                            Date = sysVer.VersionDate,
                            Url = "~/common/ShowUpFile.aspx?fileUrl={0}"
                        };
                        newDynamic.Add(vDynamic);
                    }
                }
            }
            else
            {
                var pDynamic = (from x in Funs.DB.View_NewDynamic select x).ToList();
                if (pDynamic.Count() > 0)
                {
                    newDynamic.AddRange(pDynamic);
                }
            }
            newDynamic = newDynamic.OrderBy(x => x.Type).ToList();
            if (type == "oper")
            {
            }
            else
            {
                newDynamic = newDynamic.Take(18).ToList();
            }

            DataTable tb = this.LINQToDataTable(newDynamic);
            // 2.获取当前分页数据
            //var table = this.GetPagedDataTable(GridNewDynamic, tb1);
            GridNewDynamic.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(GridNewDynamic.FilteredData, tb);
            var table = this.GetPagedDataTable(GridNewDynamic, tb);

            GridNewDynamic.DataSource = table;
            GridNewDynamic.DataBind();
        }

        /// <summary>
        /// Grid行双击事件(最新动态)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridNewDynamic_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format(GridNewDynamic.SelectedRow.Values[3].ToString(), GridNewDynamic.SelectedRowID, "查看 - ")));
        }

        /// <summary>
        /// 右键展开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuOpen1_Click(object sender, EventArgs e)
        {
            this.BindGridNewDynamic("oper");
        }

        /// <summary>
        /// 右键收起事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuClose1_Click(object sender, EventArgs e)
        {
            this.BindGridNewDynamic("close");
        }
        #endregion

        #region 最新话题
        /// <summary>
        /// 绑定数据(最新动态)
        /// </summary>
        private void BindGridNewContent(string type)
        {
            //string strSql = string.Empty;
            //if (type == "oper")
            //{
            //    strSql = "select * from View_Exchange_Content where dateadd(month,1,CompileDate)>=getdate()";
            //}
            //else
            //{
            //    strSql = "select Top 18 * from View_Exchange_Content where dateadd(month,1,CompileDate)>=getdate()";
            //}
            //DataTable tb = SQLHelper.GetDataTableRunText(strSql);

            //// 2.获取当前分页数据
            ////var table = this.GetPagedDataTable(GridNewDynamic, tb1);
            //GridNewContent.RecordCount = tb.Rows.Count;
            //tb = GetFilteredTable(GridNewContent.FilteredData, tb);
            //var table = this.GetPagedDataTable(GridNewContent, tb);

            //GridNewContent.DataSource = table;
            //GridNewContent.DataBind();

            List<Model.View_NewDynamic> newDynamic = new List<Model.View_NewDynamic>();
            var sub = from x in Funs.DB.QualityAudit_SubUnitQuality
                      //where x.ProjectId == this.CurrUser.LoginProjectId 
                      select x;
            foreach (var item in sub)
            {
                string name = string.Empty;
                if (item.BL_EnableDate > System.DateTime.Now)
                {
                    name += "营业执照已过期;";
                }
                if (item.O_EnableDate > System.DateTime.Now)
                {
                    name += "机构代码已过期;";
                }
                if (item.C_EnableDate > System.DateTime.Now)
                {
                    name += "资质证书已过期;";
                }
                if (item.QL_EnableDate > System.DateTime.Now)
                {
                    name += "质量体系认证证书已过期;";
                }
                if (item.H_EnableDate > System.DateTime.Now)
                {
                    name += "HSE体系认证证书已过期;";
                }
                if (item.SL_EnableDate > System.DateTime.Now)
                {
                    name += "安全生产许可证已过期;";
                }

                if (!string.IsNullOrEmpty(name))
                {
                    Model.View_NewDynamic vDynamic = new Model.View_NewDynamic
                    {
                        Id = item.SubUnitQualityId,
                        Type = BLL.UnitService.GetUnitNameByUnitId(item.UnitId),
                        Name = name,
                        Date = item.CompileDate
                    };
                    newDynamic.Add(vDynamic);
                }
            }
            string projectName = string.Empty;
            Model.Base_Project project = BLL.ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId);
            if (project != null)
            {
                projectName = project.ProjectName;
            }
            List<Model.View_NewDynamic> list = (from x in Funs.DB.View_NewDynamic
                                                where (x.Type == "特岗人员资质" || x.Type == "特种设备资质") && x.Name.Contains(projectName)
                                                select x).ToList();
            newDynamic.AddRange(list);
            newDynamic = newDynamic.OrderBy(x => x.Type).ToList();
            if (type == "oper")
            {
            }
            else
            {
                newDynamic = newDynamic.Take(18).ToList();
            }

            DataTable tb = this.LINQToDataTable(newDynamic);
            // 2.获取当前分页数据
            //var table = this.GetPagedDataTable(GridNewDynamic, tb1);
            GridNewContent.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(GridNewContent.FilteredData, tb);
            var table = this.GetPagedDataTable(GridNewContent, tb);

            GridNewContent.DataSource = table;
            GridNewContent.DataBind();
        }

        /// <summary>
        /// Grid行双击事件(最新话题)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridNewContent_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            //PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("~/Exchange/Content.aspx", "查看 - ")));
        }

        /// <summary>
        /// 右键展开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuOpen2_Click(object sender, EventArgs e)
        {
            this.BindGridNewContent("oper");
        }

        /// <summary>
        /// 右键收起事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuClose2_Click(object sender, EventArgs e)
        {
            this.BindGridNewContent("close");
        }
        #endregion

        #region 项目图片
        public string pics, links, texts;

        /// <summary>
        /// 项目图片显示
        /// </summary>
        public void ProjectPic()
        {
            string strSql = "SELECT TOP 5 * FROM dbo.InformationProject_Picture ORDER BY UploadDate DESC";
            if (!string.IsNullOrEmpty(this.CurrUser.LoginProjectId))
            {
                strSql = "SELECT TOP 5 * FROM dbo.InformationProject_Picture WHERE ProjectId='" + this.CurrUser.LoginProjectId + "' ORDER BY UploadDate DESC";
            }
            DataSet ds = BLL.SQLHelper.RunSqlString(strSql, "InformationProject_Picture");
            DataView dv = ds.Tables[0].DefaultView;
            if (dv.Table.Rows.Count != 0)
            {
                for (int i = 0; i < dv.Table.Rows.Count; i++)
                {
                    var q = Funs.DB.AttachFile.FirstOrDefault(e => e.ToKeyId == dv.Table.Rows[i]["PictureId"].ToString());

                    if (q != null && q.AttachUrl != null)
                    {
                        links += "../InformationProject/PictureView.aspx?PictureId=" + dv.Table.Rows[i]["PictureId"].ToString() + "|";
                        var urls = Funs.GetStrListByStr(q.AttachUrl, ',');
                        foreach (var item in urls)
                        {
                            pics += "../" + item + "|";
                            texts += dv.Table.Rows[i]["Title"].ToString() + "|";
                        }
                    }
                }
                if (!string.IsNullOrEmpty(links) && !string.IsNullOrEmpty(links) && !string.IsNullOrEmpty(links))
                {
                    this.picContent.Visible = true;
                    links = links.Substring(0, links.LastIndexOf("|"));
                    pics = pics.Substring(0, pics.LastIndexOf("|")).Replace("\\", "/");
                    texts = texts.Substring(0, texts.LastIndexOf("|"));
                }
                else
                {
                    this.picContent.Visible = false;
                }
            }
        }
        #endregion

        #region 通知通过
        /// <summary>
        /// 绑定数据(通知通过)
        /// </summary>
        private void BindGridNotice(string type)
        {
            List<Model.View_ToDoMatter> toDoMatterList = new List<Model.View_ToDoMatter>();
            ///近三个发布的通知
            var noticeList = from x in Funs.DB.InformationProject_Notice
                             where x.ReleaseDate >= System.DateTime.Now.AddMonths(-3)
                             select x;
            if (noticeList.Count() > 0)
            {
                List<Model.InformationProject_Notice> getNotices = new List<Model.InformationProject_Notice>();
                var projectId = from x in Funs.DB.Project_ProjectUser where x.UserId == this.CurrUser.UserId select x.ProjectId;
                if (projectId.Count() > 0)
                {
                    foreach (var item in projectId)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            noticeList = noticeList.Where(x => x.AccessProjectId.Contains(item));
                            if (noticeList.Count() > 0)
                            {
                                getNotices.AddRange(noticeList.ToList());
                            }
                        }
                    }
                }
                else
                {
                    getNotices = noticeList.Where(x => x.AccessProjectId.Contains("#")).ToList();
                }

                foreach (var item in getNotices.Distinct().OrderByDescending(x => x.ReleaseDate).ToList())
                {
                    Model.View_ToDoMatter newTodo = new Model.View_ToDoMatter
                    {
                        Id = item.NoticeId,
                        Type = "通知通告",
                        Name = "[" + item.NoticeCode + "]" + item.NoticeTitle,
                        Date = item.ReleaseDate,
                        Url = String.Format("~/InformationProject/NoticeView.aspx?NoticeId={0}", item.NoticeId, "查看 - ")
                    };
                    toDoMatterList.Add(newTodo);
                }
                DataTable tb = this.LINQToDataTable(toDoMatterList);
                // 2.获取当前分页数据
                //var table = this.GetPagedDataTable(GridNewDynamic, tb1);
                GridNotice.RecordCount = tb.Rows.Count;
                tb = GetFilteredTable(GridNotice.FilteredData, tb);
                var table = this.GetPagedDataTable(GridNotice, tb);

                GridNotice.DataSource = table;
                GridNotice.DataBind();
            }
        }

        /// <summary>
        /// Grid行双击事件(待办事项)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridNotice_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format(GridNotice.SelectedRow.Values[3].ToString(), "查看 - ")));
        }

        /// <summary>
        /// 右键展开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuOpen3_Click(object sender, EventArgs e)
        {
            this.BindGridNotice("oper");
        }

        /// <summary>
        /// 右键收起事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuClose3_Click(object sender, EventArgs e)
        {
            this.BindGridNotice("close");
        }
        #endregion

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGridNewDynamic("close");
        }
    }
}
