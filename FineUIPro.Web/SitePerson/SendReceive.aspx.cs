using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FineUIPro.Web.SitePerson
{
    public partial class SendReceive : PageBase
    {
        #region 自定义变量
        /// <summary>
        /// 项目id
        /// </summary>
        public string ProjectId
        {
            get
            {
                return (string)ViewState["ProjectId"];
            }
            set
            {
                ViewState["ProjectId"] = value;
            }
        }
        #endregion

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
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.ProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }
                ////权限按钮方法
                this.GetButtonPower();
                if (this.CurrUser.PageSize.HasValue)
                {
                    Grid1.PageSize = this.CurrUser.PageSize.Value;
                } 
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                this.BindGrid();
                this.btnReceive.Hidden = true;
                this.btnNoReceive.Hidden = true;
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @" SELECT sendReceive.SendReceiveId,sendReceive.PersonId,sendReceive.SendProjectId,SendProject.ProjectName AS SendProjectName,sendReceive.ReceiveProjectId,(CASE WHEN sendReceive.IsAgree =1 THEN '接收' WHEN  sendReceive.IsAgree =0 THEN '拒收' ELSE '待接收' END) AS IsAgreeName"
                     + @" ,ReceiveProject.ProjectName AS ReceiveProjectName,sendReceive.SendTime,sendReceive.ReceiveTime,person.UnitId,unit.UnitName,person.CardNo,person.PersonName,person.IdentityCard,person.ProjectId,Project.ProjectName"
                     + @" FROM dbo.SitePerson_SendReceive AS sendReceive"
                     + @" LEFT JOIN dbo.SitePerson_Person AS person ON sendReceive.PersonId =person.PersonId"
                     + @" LEFT JOIN dbo.Base_Unit AS unit ON unit.UnitId=person.UnitId"
                     + @" LEFT JOIN dbo.Base_Project AS SendProject ON SendProject.ProjectId=sendReceive.SendProjectId"
                     + @" LEFT JOIN dbo.Base_Project AS ReceiveProject ON ReceiveProject.ProjectId=sendReceive.ReceiveProjectId"
                     + @" LEFT JOIN dbo.Base_Project AS Project ON Project.ProjectId=person.ProjectId"
                     + @" WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (this.cbIsSend.SelectedValue == "0")
            {
                strSql += " AND sendReceive.SendProjectId =@ProjectId ";
            }
            else
            {
                strSql += " AND sendReceive.ReceiveProjectId=@ProjectId ";
            }

            listStr.Add(new SqlParameter("@ProjectId", this.ProjectId));
            if (!string.IsNullOrEmpty(this.txtPersonName.Text.Trim()))
            {
                strSql += " AND person.PersonName LIKE @PersonName";
                listStr.Add(new SqlParameter("@PersonName", "%" + this.txtPersonName.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtUnitName.Text.Trim()))
            {
                strSql += " AND unit.UnitName LIKE @UnitName";
                listStr.Add(new SqlParameter("@UnitName", "%" + this.txtUnitName.Text.Trim() + "%"));
            }
            if (this.ckbShow.Checked)
            {
                strSql += " AND sendReceive.ReceiveTime IS NULL";
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                string sendReceiveId = Grid1.Rows[i].DataKeys[0].ToString();
                var isNull = BLL.ProjectSendReceiveService.GetSendReceiveById(sendReceiveId);
                if (isNull != null && !isNull.IsAgree.HasValue) ////未参加过培训的人员
                {
                    Grid1.Rows[i].RowCssClass = "Red";
                }
            }
        }
        #endregion

        #region Gv事件
        #region 页索引改变事件
        /// <summary>
        /// 页索引改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 排序
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {           
            BindGrid();
        }
        #endregion

        #region 分页选择下拉改变事件
        /// <summary>
        /// 分页选择下拉改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }
        #endregion
        #endregion
        
        #region 关闭弹出窗事件
        /// <summary>
        /// 关闭弹出框事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 事件
        /// <summary>
        /// 送出事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSend_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SendPerson.aspx", "人员项目批量转换 - ")));            
        }

        /// <summary>
        /// 接收事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReceive_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string mess = string.Empty;
            foreach (int rowIndex in Grid1.SelectedRowIndexArray)
            {
                string sendReceiveId = Grid1.DataKeys[rowIndex][0].ToString();
                var sendReceive = BLL.SendReceiveService.GetSendReceiveById(sendReceiveId);
                if (sendReceive != null && !sendReceive.ReceiveTime.HasValue)
                {
                    sendReceive.ReceiveTime = System.DateTime.Now;
                    sendReceive.IsAgree = true;
                    BLL.SendReceiveService.UpdateSendReceive(sendReceive);
                    var getPerson = BLL.PersonService.GetPersonById(sendReceive.PersonId);
                    if (getPerson != null)
                    {
                        var isPerson = BLL.PersonService.GetPersonByIdentityCard(sendReceive.ReceiveProjectId, getPerson.IdentityCard);
                        if (isPerson == null)
                        {
                            var pUnit = BLL.ProjectUnitService.GetProjectUnitByUnitIdProjectId(sendReceive.ReceiveProjectId, getPerson.UnitId);
                            if (pUnit == null)
                            {
                                ///人员项目单位是否存在
                                Model.Project_ProjectUnit newProjectUnit = new Model.Project_ProjectUnit
                                {
                                    ProjectId = sendReceive.ReceiveProjectId,
                                    UnitId = getPerson.UnitId,
                                    InTime = System.DateTime.Now
                                };
                                BLL.ProjectUnitService.AddProjectUnit(newProjectUnit);
                            }

                            Model.SitePerson_Person newPerson = new Model.SitePerson_Person
                            {
                                PersonId = SQLHelper.GetNewID(typeof(Model.SitePerson_Person)),
                                CardNo = getPerson.CardNo,
                                PersonName = getPerson.PersonName,
                                Sex = getPerson.Sex,
                                IdentityCard = getPerson.IdentityCard,
                                Address = getPerson.Address,
                                ProjectId = sendReceive.ReceiveProjectId,
                                UnitId = getPerson.UnitId,
                                TeamGroupId = null,
                                WorkAreaId = null,
                                WorkPostId = getPerson.WorkPostId,
                                InTime = System.DateTime.Now,
                                Telephone = getPerson.Telephone,
                                PositionId = getPerson.PositionId,
                                PostTitleId = getPerson.PostTitleId,
                                PhotoUrl = getPerson.PhotoUrl,
                                IsUsed = true,
                                IsCardUsed = getPerson.IsCardUsed,
                                DepartId = getPerson.DepartId,
                            };

                            ///插入到接收项目人员表
                            BLL.PersonService.AddPerson(newPerson);
                        }
                    }
                }
                else
                {
                    mess += "行:" + (rowIndex + 1).ToString() + "已处理单据！";
                }
            }

            BindGrid();
            if (!string.IsNullOrEmpty(mess))
            {
                Alert.ShowInTop(mess, MessageBoxIcon.Warning);
            }
            else
            {
                ShowNotify("接收成功!", MessageBoxIcon.Success);
            }
        }
        
        /// <summary>
        /// 拒收事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNoReceive_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string mess = string.Empty;
            foreach (int rowIndex in Grid1.SelectedRowIndexArray)
            {
                string sendReceiveId = Grid1.DataKeys[rowIndex][0].ToString();
                var sendReceive = BLL.SendReceiveService.GetSendReceiveById(sendReceiveId);
                if (sendReceive != null && !sendReceive.ReceiveTime.HasValue)
                {
                    sendReceive.ReceiveTime = System.DateTime.Now;
                    sendReceive.IsAgree = false;
                    BLL.SendReceiveService.UpdateSendReceive(sendReceive);

                    var person = BLL.PersonService.GetPersonById(sendReceive.PersonId);
                    if(person != null)
                    {
                        person.OutTime = null;
                        person.IsUsed = true;
                        BLL.PersonService.UpdatePerson(person); ///在原来项目出场更改
                    }
                }
                else
                {
                    mess += "行:" + (rowIndex + 1).ToString() + "已处理单据！";
                }
            }

            BindGrid();
            if (!string.IsNullOrEmpty(mess))
            {
                Alert.ShowInTop(mess, MessageBoxIcon.Warning);
            }
            else
            {
                ShowNotify("拒收成功!", MessageBoxIcon.Success);
            }
        }

        /// <summary>
        /// 查看事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnView_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }

            var sendReceive = BLL.SendReceiveService.GetSendReceiveById(Grid1.SelectedRowID);
            if (sendReceive != null)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PersonListEdit.aspx?PersonId={0}&value=0", sendReceive.PersonId, "查看 - ")));
            }           
        }

        #region 删除方法
        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            this.DeleteData();
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        private void DeleteData()
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string mess = string.Empty;
            foreach (int rowIndex in Grid1.SelectedRowIndexArray)
            {
                string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                var sendReceive = BLL.SendReceiveService.GetSendReceiveById(rowID);
                if (sendReceive != null)
                {
                    if (sendReceive.SendProjectId != this.ProjectId && !sendReceive.ReceiveTime.HasValue)
                    {
                        mess += "行:" + (rowIndex + 1).ToString() + "非本项目发送且未接收，不能删除！";
                    }
                    else
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, "删除人员项目转换信息", rowID, BLL.Const.PersonListMenuId, BLL.Const.BtnModify);
                        BLL.SendReceiveService.DeleteSendReceiveBySendReceiveId(rowID);
                    }
                }
            }

            BindGrid();
            if (!string.IsNullOrEmpty(mess))
            {
                Alert.ShowInTop(mess, MessageBoxIcon.Warning);
            }
            else
            {
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
            
           
        }
        #endregion
        #endregion

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            if (Request.Params["value"] == "0")
            {
                return;
            }
            var buttonList = BLL.CommonService.GetAllButtonList(this.ProjectId, this.CurrUser.UserId, BLL.Const.SendReceiveMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDelete.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnReceive.Hidden = false;
                    this.btnNoReceive.Hidden = false;
                    this.btnSend.Hidden = false;
                }
            }
        }
        #endregion
        
        #region 导出按钮
        ///  导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("人员项目转换" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = this.Grid1.Rows.Count();
            BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
            this.GetButtonPower();
            if (this.cbIsSend.SelectedValue == "0")
            {                              
                this.btnReceive.Hidden = true;
                this.btnNoReceive.Hidden = true;
            }
            else
            {
                this.btnSend.Hidden = true;
                this.btnMenuDelete.Hidden = true;
            }
        }
        
        protected void ckbShow_CheckedChanged(object sender, CheckedEventArgs e)
        {
            this.BindGrid();
        }
        #endregion
    }
}