using BLL;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.Technique
{
    public partial class ExpertAudit :PageBase
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
                ////权限按钮方法
                this.GetButtonPower();
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                BindGrid();
            }
        }
       
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = "select * from View_Expert where IsPass is null";
            SqlParameter[] parameter = null;
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            // 2.获取当前分页数据
            //var table = this.GetPagedDataTable(Grid1, tb1);
            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

        #region 根据表头信息过滤列表数据
        /// <summary>
        /// 过滤表头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 分页排序
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        /// <summary>
        /// 分页显示条数下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region Grid行点击事件
        /// <summary>
        /// Grid行点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string rowID = Grid1.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "Attach")
            {
                var lawRegulationList = BLL.ExpertService.GetExpertById(rowID);
                if (lawRegulationList != null)
                {
                    PageBase.ShowFileEvent(lawRegulationList.AttachUrl);
                }
            }
        }
        #endregion        

        #region 采用/不采用
        /// <summary>
        /// 右键采用事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPass_Click(object sender, EventArgs e)
        {
            this.SetIsPass(true, BLL.Const.UpState_1);
        }

        /// <summary>
        /// 右键采用并上报事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpPass_Click(object sender, EventArgs e)
        {
            this.SetIsPass(true, BLL.Const.UpState_2);
        }

        /// <summary>
        /// 右键不采用事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNoPass_Click(object sender, EventArgs e)
        {
            this.SetIsPass(false, null);
        }

        /// <summary>
        /// 是否采用方法更新
        /// </summary>
        /// <param name="isPass"></param>
        private void SetIsPass(bool isPass, string upSate)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                var unit = BLL.CommonService.GetIsThisUnit();
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    var expert = BLL.ExpertService.GetExpertById(rowID);
                    if (expert != null)
                    {
                        expert.AuditDate = System.DateTime.Now;
                        expert.AuditMan = this.CurrUser.UserId;
                        expert.IsPass = isPass;
                        expert.UpState = upSate;
                        BLL.ExpertService.UpdateExpertIsPass(expert);
                    }
                    if (upSate == BLL.Const.UpState_2 && unit != null && !string.IsNullOrEmpty(unit.UnitId))
                    {
                        UpExpert(rowID, unit.UnitId);
                    }
                }

                BLL.LogService.AddSys_Log(this.CurrUser, string.Empty, string.Empty, BLL.Const.ExpertMenuId, Const.BtnAuditing);

                BindGrid();
                ShowNotify("操作成功!");
                if (isPass)
                {
                    // 1. 这里放置保存窗体中数据的逻辑
                    // 2. 不关闭窗体，直接回发父窗体
                    PageContext.RegisterStartupScript("F.getActiveWindow().window.reloadGrid();");

                }
            }
        }
        #endregion

        #region 上报到集团公司
        /// <summary>
        /// 上报方法
        /// </summary>
        /// <param name="lawRegulation"></param>
        public void UpExpert(string expertId, string unitId)
        {
            /////创建客户端服务
            var poxy = Web.ServiceProxy.CreateServiceClient();
            poxy.DataInsertTechnique_ExpertTableCompleted += new EventHandler<HSSEService.DataInsertTechnique_ExpertTableCompletedEventArgs>(poxy_DataInsertTechnique_ExpertTableCompleted);
            var expertList = from x in Funs.DB.View_Expert
                             join y in Funs.DB.AttachFile on x.ExpertId equals y.ToKeyId
                                 where x.ExpertId == expertId && x.IsPass == true && (x.UpState == BLL.Const.UpState_2 || x.UpState == BLL.Const.UpState_4)
                             select new HSSEService.Technique_Expert
                                 {
                                     ExpertId = x.ExpertId,
                                     ExpertCode = x.ExpertCode,
                                     ExpertName = x.ExpertName,
                                     Sex = x.SexStr,
                                     Birthday = x.Birthday,
                                     Age = x.Age,
                                     UnitName = x.UnitName,
                                     Marriage = x.Marriage,
                                     Nation = x.Nation,
                                     IdentityCard = x.IdentityCard,
                                     Email = x.Email,
                                     Telephone = x.Telephone,
                                     Education = x.Education,
                                     Hometown = x.Hometown,
                                     UnitId = unitId,
                                     ExpertTypeId = x.ExpertTypeId,
                                     PersonSpecialtyId = x.PersonSpecialtyId,
                                     PostTitleId = x.PostTitleId,
                                     Performance = x.Performance,
                                     EffectiveDate = x.EffectiveDate,
                                     CompileMan = x.CompileMan,
                                     CompileDate = x.CompileDate,
                                     PhotoUrl = x.PhotoUrl,
                                     ExpertTypeName = x.ExpertTypeName,
                                     ExpertTypeCode = x.ExpertTypeCode,
                                     PersonSpecialtyName = x.PersonSpecialtyName,
                                     PersonSpecialtyCode = x.PersonSpecialtyCode,
                                     PostTitleName = x.PostTitleName,
                                     PostTitleCode = x.PostTitleCode,
                                     AttachFileId = y.AttachFileId,
                                     ToKeyId = y.ToKeyId,
                                     AttachSource = y.AttachSource,
                                     AttachUrl = y.AttachUrl,
                                     ////附件转为字节传送
                                     AttachUrlFileContext = FileStructService.GetMoreFileStructByAttachUrl(y.AttachUrl),
                                     PhotoUrlFileContext = BLL.FileStructService.GetFileStructByAttachUrl(x.PhotoUrl),
                                     IsPass = null,
                                 };
            poxy.DataInsertTechnique_ExpertTableAsync(expertList.ToList());
        }

        /// <summary>
        /// 安全专家从子单位上报到集团单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertTechnique_ExpertTableCompleted(object sender, HSSEService.DataInsertTechnique_ExpertTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var expert = BLL.ExpertService.GetExpertById(item);
                    if (expert != null)
                    {
                        expert.UpState = BLL.Const.UpState_3;
                        BLL.ExpertService.UpdateExpertIsPass(expert);
                    }
                }

                BLL.LogService.AddSys_Log(this.CurrUser, "【安全专家】上报到集团公司" + idList.Count.ToString() + "条数据；", string.Empty, BLL.Const.ExpertMenuId, Const.BtnSaveUp);                
            }
            else
            {
                BLL.LogService.AddSys_Log(this.CurrUser, "【安全专家】上报到集团公司失败", string.Empty, BLL.Const.ExpertMenuId, Const.BtnSaveUp);
            }
        }
        #endregion

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ExpertMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAuditing))
                {
                    this.btnPass.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnSaveUp))
                {
                    this.btnUpPass.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnAuditing))
                {
                    this.btnNoPass.Hidden = false;
                }
            }
        }
        #endregion
    }
}