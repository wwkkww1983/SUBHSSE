using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BLL;

namespace FineUIPro.Web.common
{
    public partial class mainI : PageBase
    {
        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {              
                BindGridToDoMatter("close");
                BindGridNotice("close");
                BindGridNewDynamic("close");
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
                if (GetRequestEventArgument() == "reloadGrid")
                {
                    BindGridToDoMatter("close");
                }
            }
        }
        #endregion

        #region 待办事项
        /// <summary>
        /// 绑定数据(待办事项)
        /// </summary>
        private void BindGridToDoMatter(string type)
        {
            var q = from x in Funs.DB.View_ToDoMatter where x.UserId == this.CurrUser.UserId select x;
            List<string> lawRegulationButtonList = CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.LawRegulationListMenuId);
            if (!lawRegulationButtonList.Contains(Const.BtnAuditing))
            {
                q = q.Where(e => e.Type != "法律法规");
            }
            List<string> standardButtonList = CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HSSEStandardListMenuId);
            if (!standardButtonList.Contains(Const.BtnAuditing))
            {
                q = q.Where(e => e.Type != "标准规范");
            }
            List<string> rulesRegulationsButtonList = CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.RulesRegulationsMenuId);
            if (!rulesRegulationsButtonList.Contains(Const.BtnAuditing))
            {
                q = q.Where(e => e.Type != "规章制度");
            }
            List<string> manageRuleButtonList = CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.ManageRuleMenuId);
            if (!manageRuleButtonList.Contains(Const.BtnAuditing))
            {
                q = q.Where(e => e.Type != "管理规定");
            }
            List<string> trainingItemButtonList = CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.TrainDBMenuId);
            if (!trainingItemButtonList.Contains(Const.BtnAuditing))
            {
                q = q.Where(e => e.Type != "培训教材");
            }
            List<string> trainTestItemButtonList = CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.TrainTestDBMenuId);
            if (!trainTestItemButtonList.Contains(Const.BtnAuditing))
            {
                q = q.Where(e => e.Type != "安全试题");
            }
            List<string> accidentCaseItemButtonList = CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.AccidentCaseMenuId);
            if (!accidentCaseItemButtonList.Contains(Const.BtnAuditing))
            {
                q = q.Where(e => e.Type != "事故案例");
            }
            List<string> knowledgeItemButtonList = CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.KnowledgeDBMenuId);
            if (!knowledgeItemButtonList.Contains(Const.BtnAuditing))
            {
                q = q.Where(e => e.Type != "应知应会");
            }
            List<string> hazardButtonList = CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HazardListMenuId);
            if (!hazardButtonList.Contains(Const.BtnAuditing))
            {
                q = q.Where(e => e.Type != "危险源");
            }
            List<string> rectifyItemButtonList = CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.RectifyMenuId);
            if (!rectifyItemButtonList.Contains(Const.BtnAuditing))
            {
                q = q.Where(e => e.Type != "安全隐患");
            }
            List<string> HAZOPButtonList = CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HAZOPMenuId);
            if (!HAZOPButtonList.Contains(Const.BtnAuditing))
            {
                q = q.Where(e => e.Type != "HAZOP");
            }
            List<string> expertButtonList = CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.ExpertMenuId);
            if (!expertButtonList.Contains(Const.BtnAuditing))
            {
                q = q.Where(e => e.Type != "安全专家");
            }
            List<string> emergencyButtonList = CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.EmergencyMenuId);
            if (!emergencyButtonList.Contains(Const.BtnAuditing))
            {
                q = q.Where(e => e.Type != "应急预案");
            }
            List<string> specialSchemeButtonList = CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.SpecialSchemeMenuId);
            if (!specialSchemeButtonList.Contains(Const.BtnAuditing))
            {
                q = q.Where(e => e.Type != "专项方案");
            }
            var toDoMatterList = q.ToList();
            var dataIdList = (from x in Funs.DB.Sys_FlowOperate
                              where x.OperaterId == this.CurrUser.UserId && (x.IsClosed == false || !x.IsClosed.HasValue)
                              select x).ToList();
            if (dataIdList.Count() > 0)
            {
                foreach (var item in dataIdList)
                {
                    var isClosedData = Funs.DB.Sys_FlowOperate.FirstOrDefault(x => x.DataId == item.DataId && x.IsClosed == true && x.State== BLL.Const.State_2);
                    if (isClosedData == null)
                    {
                        Model.View_ToDoMatter newTodo = new Model.View_ToDoMatter
                        {
                            Id = item.DataId
                        };
                        var menu = BLL.SysMenuService.GetSysMenuByMenuId(item.MenuId);
                        if (menu != null)
                        {
                            newTodo.Type = menu.MenuName;
                            if (!string.IsNullOrEmpty(item.Url))
                            {
                                string newUrl = item.Url.Replace("View.aspx", "Edit.aspx");
                                newTodo.Url = String.Format(newUrl, item.DataId, "审核 - ");
                            }

                            string userName = BLL.UserService.GetUserNameByUserId(item.OperaterId);
                            var project = BLL.ProjectService.GetProjectByProjectId(item.ProjectId);
                            if (project != null)
                            {
                                newTodo.Name = project.ProjectName + ":待" + userName + "处理";
                            }
                            else
                            {
                                newTodo.Name = "本部系统：待" + userName + "处理";
                            }
                            newTodo.Date = item.OperaterTime;
                            newTodo.UserId = item.OperaterId;
                            toDoMatterList.Add(newTodo);
                        }
                    }
                }
            }
            //危险观察责任单位人员整改提醒
            List<Model.Hazard_Registration> registrationHandles = (from x in Funs.DB.Hazard_Registration
                                                                   where x.ResponsibilityManId == this.CurrUser.UserId && x.States == BLL.Const.State_2 && x.IsEffective == true
                                                                       && (x.State == BLL.Const.RegistrationState_0 || x.State == BLL.Const.RegistrationState_3)
                                                                   select x).ToList();
            foreach (var item in registrationHandles)
            {
                Model.View_ToDoMatter newTodo = new Model.View_ToDoMatter
                {
                    Id = item.RegistrationId,
                    Type = "危险观察整改"
                };
                var project = BLL.ProjectService.GetProjectByProjectId(item.ProjectId);
                if (project != null)
                {
                    newTodo.Name = project.ProjectName + ":待整改";
                }
                newTodo.Url = String.Format("~/Hazard/RegistrationHandleEdit.aspx?RegistrationId={0}", item.RegistrationId, "整改 - ");
                newTodo.Date = item.CheckTime;
                newTodo.UserId = this.CurrUser.UserId;
                toDoMatterList.Add(newTodo);
            }
            //危险观察本单位人员确认提醒
            List<Model.Hazard_Registration> registrationConfirms = (from x in Funs.DB.Hazard_Registration
                                                                    where x.CheckManId == this.CurrUser.UserId && x.States == BLL.Const.State_2 && x.IsEffective == true
                                                                        && x.State == BLL.Const.RegistrationState_1
                                                                    select x).ToList();
            foreach (var item in registrationConfirms)
            {
                Model.View_ToDoMatter newTodo = new Model.View_ToDoMatter
                {
                    Id = item.RegistrationId,
                    Type = "危险观察整改"
                };
                var project = BLL.ProjectService.GetProjectByProjectId(item.ProjectId);
                if (project != null)
                {
                    newTodo.Name = project.ProjectName + ":待确认";
                }
                newTodo.Url = String.Format("~/Hazard/RegistrationHandleEdit.aspx?RegistrationId={0}", item.RegistrationId, "确认 - ");
                newTodo.Date = item.CheckTime;
                newTodo.UserId = this.CurrUser.UserId;
                toDoMatterList.Add(newTodo);
            }

            if (this.CurrUser.UserId == BLL.Const.sysglyId)
            {
                if (type != "oper")
                {
                    toDoMatterList = toDoMatterList.Take(5).ToList();
                }
                var sysVer = (from x in Funs.DB.Sys_Version where x.IsSub == true orderby x.VersionDate descending select x).FirstOrDefault();
                if (sysVer != null && sysVer.VersionName != Funs.SystemVersion && sysVer.VersionName.Length > 27 && Funs.SystemVersion.Length > 27)
                {
                    DateTime? vdate = Funs.GetNewDateTime(sysVer.VersionName.Substring(17, 10));
                    DateTime? fdate = Funs.GetNewDateTime(Funs.SystemVersion.Substring(17, 10));
                    string vNum = sysVer.VersionName.Substring(27);
                    string fNum = Funs.SystemVersion.Substring(27);
                    if (vdate > fdate || (vdate == fdate && vNum != fNum))
                    {
                        Model.View_ToDoMatter newTodo = new Model.View_ToDoMatter
                        {
                            Id = sysVer.AttachUrl,
                            Type = "系统版本",
                            Name = "新版本:" + sysVer.VersionName,
                            Date = sysVer.VersionDate,
                            Url = "ShowUpFile.aspx?fileUrl=" + sysVer.AttachUrl
                        };
                        toDoMatterList.Add(newTodo);
                    }
                }
            }
            else
            {
                if (type != "oper")
                {
                    toDoMatterList = toDoMatterList.Take(6).ToList();
                }
            }

            DataTable tb = this.LINQToDataTable(toDoMatterList);
            // 2.获取当前分页数据
            //var table = this.GetPagedDataTable(GridNewDynamic, tb1);
            GridToDoMatter.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(GridToDoMatter.FilteredData, tb);
            var table = this.GetPagedDataTable(GridToDoMatter, tb);

            GridToDoMatter.DataSource = table;
            GridToDoMatter.DataBind();
        }

        /// <summary>
        /// Grid行双击事件(待办事项)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridToDoMatter_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            if (GridToDoMatter.SelectedRow != null)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format(GridToDoMatter.SelectedRow.Values[3].ToString()), "待办事项：" + GridToDoMatter.SelectedRow.Values[0].ToString()));
            }
        }

        /// <summary>
        /// 右键展开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuOpen3_Click(object sender, EventArgs e)
        {
            this.BindGridToDoMatter("oper");
        }

        /// <summary>
        /// 右键收起事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuClose3_Click(object sender, EventArgs e)
        {
            this.BindGridToDoMatter("close");
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
                        Url = String.Format("~/InformationProject/NoticeView.aspx?NoticeId={0}", item.NoticeId)
                    };
                    toDoMatterList.Add(newTodo);
                }
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

        /// <summary>
        /// Grid行双击事件(待办事项)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridNotice_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format(GridNotice.SelectedRow.Values[3].ToString()),  GridNotice.SelectedRow.Values[0].ToString()));
        }

        /// <summary>
        /// 右键展开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuOpen2_Click(object sender, EventArgs e)
        {
            this.BindGridNotice("oper");
        }

        /// <summary>
        /// 右键收起事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuClose2_Click(object sender, EventArgs e)
        {
            this.BindGridNotice("close");
        }
        #endregion

        #region 资质预警
        /// <summary>
        /// 绑定数据(资质预警)
        /// </summary>
        private void BindGridNewDynamic(string type)
        {
            string strSql = string.Empty;
            if (type == "oper")
            {
                if (BLL.CommonService.IsThisUnitLeaderOrManage(this.CurrUser.UserId))
                {
                    strSql = "SELECT * FROM View_NewDynamic";
                }
                else
                {
                    strSql = @"SELECT * FROM View_NewDynamic A"
                              + @" LEFT JOIN Project_ProjectUser B ON A.ProjectId =B.ProjectId"
                              + @" WHERE B.UserId='" + this.CurrUser.UserId + "'"
                              + @" ORDER BY A.Date DESC";
                    if (!string.IsNullOrEmpty(this.CurrUser.LoginProjectId))
                    {
                        strSql = "SELECT Id,Name,Date,Url FROM View_NewDynamic WHERE ProjectId='" + this.CurrUser.LoginProjectId + "' ORDER BY Date DESC";
                    }
                } 
               
            }
            else
            {
                if (BLL.CommonService.IsThisUnitLeaderOrManage(this.CurrUser.UserId))
                {
                    strSql = "SELECT TOP 5 * FROM View_NewDynamic";
                }
                else
                {
                    strSql = @"SELECT TOP 5 * FROM View_NewDynamic A"
                              + @" LEFT JOIN Project_ProjectUser B ON A.ProjectId =B.ProjectId"
                              + @" WHERE B.UserId='" + this.CurrUser.UserId + "'"
                              + @" ORDER BY A.Date DESC";
                    if (!string.IsNullOrEmpty(this.CurrUser.LoginProjectId))
                    {
                        strSql = "SELECT TOP 5 * FROM View_NewDynamic WHERE ProjectId='" + this.CurrUser.LoginProjectId + "' ORDER BY Date DESC";
                    }
                } 
            }

            DataTable tb = SQLHelper.GetDataTableRunText(strSql, null); 
            GridNewDynamic.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(GridNewDynamic.FilteredData, tb);
            var table = this.GetPagedDataTable(GridNewDynamic, tb);
            GridNewDynamic.DataSource = table;
            GridNewDynamic.DataBind();
        }

        /// <summary>
        /// Grid行双击事件(资质预警)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridNewDynamic_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            var view = Funs.DB.View_NewDynamic.FirstOrDefault(x => x.Id == GridNewDynamic.SelectedRowID);   
            if (view != null)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format(GridNewDynamic.SelectedRow.Values[3].ToString(), GridNewDynamic.SelectedRowID), "资质预警:" + view.Type));
            }
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

        #region 项目图片
        public string pics, links, texts;

        /// <summary>
        /// 项目图片显示
        /// </summary>
        public void ProjectPic()
        {
            this.picContent.Visible = false;
            string strSql = @"SELECT DISTINCT TOP 5 A.PictureId,A.ProjectId,A.Title,A.ContentDef,A.PictureType,A.UploadDate,A.States,A.AttachUrl,A.CompileMan"
                + @" FROM dbo.InformationProject_Picture A"
                + @" LEFT JOIN  AttachFile AS B ON A.PictureId = B.ToKeyId WHERE B.AttachFileId IS NOT NULL";
            //bool isLeaderManage =BLL.CommonService.IsThisUnitLeaderOrManage(this.CurrUser.UserId);
            //if (!isLeaderManage)
            //{
            //    strSql += " LEFT JOIN Project_ProjectUser B ON A.ProjectId =B.ProjectId";
            //    strSql += " WHERE B.UserId='" + this.CurrUser.UserId + "'";
            //}
            strSql += " ORDER BY A.UploadDate DESC";
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
                        var urls = Funs.GetStrListByStr(q.AttachUrl, ',');
                        foreach (var item in urls)
                        {
                            if (!string.IsNullOrEmpty(item))
                            {
                                links += "../InformationProject/PictureView.aspx?PictureId=" + dv.Table.Rows[i]["PictureId"].ToString() + "|";
                                pics += "../" + item + "|";
                                texts += dv.Table.Rows[i]["Title"].ToString() + "|";
                            }
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

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, EventArgs e)
        {
            this.BindGridNewDynamic("close");
            this.BindGridToDoMatter("close");
        }
    }
}
