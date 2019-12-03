using BLL;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.Technique
{
    public partial class EmergencyAudit : PageBase
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
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                BindGrid();
                ////权限按钮方法
                this.GetButtonPower();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = "select * from View_Technique_Emergency where IsPass is null";
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
                    var emergency = BLL.EmergencyService.GetEmergencyListById(rowID);
                    if (emergency != null)
                    {
                        emergency.AuditDate = System.DateTime.Now;
                        emergency.AuditMan = this.CurrUser.UserId;
                        emergency.IsPass = isPass;
                        emergency.UpState = upSate;
                        BLL.EmergencyService.UpdateEmergencyList(emergency);
                    }

                    if (upSate == BLL.Const.UpState_2 && unit != null && !string.IsNullOrEmpty(unit.UnitId))
                    {
                        UpEmergency(rowID, unit.UnitId);
                    }
                }

                BLL.LogService.AddSys_Log(this.CurrUser, string.Empty,string.Empty, BLL.Const.EmergencyMenuId, Const.BtnAuditing);

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

        private void UpEmergency(string emergencyId, string unitId)
        {
            ////创建客户端服务
            var poxy = Web.ServiceProxy.CreateServiceClient();
            poxy.DataInsertTechnique_EmergencyTableCompleted += new EventHandler<HSSEService.DataInsertTechnique_EmergencyTableCompletedEventArgs>(poxy_DataInsertTechnique_EmergencyTableCompleted);
            var emergency = from x in Funs.DB.View_Technique_Emergency
                            join y in Funs.DB.AttachFile on x.EmergencyId equals y.ToKeyId
                            where x.IsPass == true && (x.UpState == BLL.Const.UpState_2 || x.UpState == BLL.Const.UpState_4) && (x.IsBuild == false || x.IsBuild == null)
                            select new HSSEService.Technique_Emergency
                            {
                                EmergencyId = x.EmergencyId,
                                EmergencyTypeId = x.EmergencyTypeId,
                                EmergencyCode = x.EmergencyCode,
                                EmergencyName = x.EmergencyName,
                                Summary = x.Summary,
                                Remark = x.Remark,
                                CompileMan = x.CompileMan,
                                CompileDate = x.CompileDate,
                                IsPass = null,
                                UnitId = unitId,
                                AttachFileId = y.AttachFileId,
                                ToKeyId = y.ToKeyId,
                                AttachSource = y.AttachSource,
                                AttachUrl = y.AttachUrl,
                                ////附件转为字节传送
                                FileContext = FileStructService.GetMoreFileStructByAttachUrl(y.AttachUrl),

                            };
            poxy.DataInsertTechnique_EmergencyTableAsync(emergency.ToList());
        }
        #endregion

        #region 应急预案上报到集团公司
        /// <summary>
        /// 应急预案上报到集团公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertTechnique_EmergencyTableCompleted(object sender, HSSEService.DataInsertTechnique_EmergencyTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var emergency = BLL.EmergencyService.GetEmergencyListById(item);
                    if (emergency != null)
                    {
                        emergency.UpState = BLL.Const.UpState_3;
                        BLL.EmergencyService.UpdateEmergencyList(emergency);
                    }
                }
                BLL.LogService.AddSys_Log(this.CurrUser, "【应急预案】上报到集团公司" + idList.Count.ToString() + "条数据；",
                    string.Empty, BLL.Const.EmergencyMenuId, Const.BtnUploadResources);
            }
            else
            {
                BLL.LogService.AddSys_Log(this.CurrUser, "【应急预案】上报到集团公司失败；", string.Empty, BLL.Const.EmergencyMenuId, Const.BtnUploadResources);
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.EmergencyMenuId);
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