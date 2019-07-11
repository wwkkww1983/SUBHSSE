using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.IO;

namespace FineUIPro.Web.EduTrain
{
    public partial class TrainingUpload : PageBase
    {
        /// <summary>
        /// 主键
        /// </summary>
       public string TrainingItemId
        {
            get
            {
                return (string)ViewState["TrainingItemId"];
            }
            set
            {
                ViewState["TrainingItemId"] = value;
            }
        }

        /// <summary>
        /// 培训教材库id
        /// </summary>
        public string TrainingId
        {
            get
            {
                return (string)ViewState["TrainingId"];
            }
            set
            {
                ViewState["TrainingId"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetButtonPower();
                this.InitTreeMenu();

                this.TrainingId = Request.Params["TrainingId"];
                this.txtCompileMan.Text = this.CurrUser.UserName;
                this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
            }
        }

        #region 初始化树
        /// <summary>
        /// 初始化树
        /// </summary>
        private void InitTreeMenu()
        {
            tvUploadResources.Nodes.Clear();

            ///加载当前人
            TreeNode rootNode = new TreeNode
            {
                Text = this.CurrUser.UserName,
                NodeID = this.CurrUser.UserId,
                Expanded = true
            };
            this.tvUploadResources.Nodes.Add(rootNode);
            ////加载上传资源的状态
            var uploadType = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_UploadResources);
            if (uploadType.Count() > 0)
            {
                var newLaw = BLL.TrainingItemService.GetTrainingItemByCompileMan(this.CurrUser.UserName);
                foreach (var item in uploadType)
                {
                    TreeNode chidNode = new TreeNode
                    {
                        Text = item.ConstText,
                        NodeID = item.ConstValue
                    };
                    rootNode.Nodes.Add(chidNode);
                    this.BoundTree(chidNode.Nodes, item.ConstValue, newLaw);
                }
            }
        }

        /// <summary>
        /// 遍历增加子节点
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="menuId"></param>
        private void BoundTree(TreeNodeCollection nodes, string typeId, List<Model.Training_TrainingItem> trainingItem)
        {
            List<Model.Training_TrainingItem> chidLaw = new List<Model.Training_TrainingItem>();
            if (typeId == BLL.Const.UploadResources_1) ///未审核
            {
                chidLaw = trainingItem.Where(x => x.IsPass == null).ToList();
            }
            if (typeId == BLL.Const.UploadResources_2) ///未采用
            {
                chidLaw = trainingItem.Where(x => x.AuditDate.HasValue && x.IsPass == false).ToList();
            }
            if (typeId == BLL.Const.UploadResources_3) ///已采用
            {
                chidLaw = trainingItem.Where(x => x.AuditDate.HasValue && x.IsPass == true).ToList();
            }
            if (chidLaw.Count() > 0)
            {
                foreach (var item in chidLaw)
                {
                    TreeNode gChidNode = new TreeNode
                    {
                        Text = item.TrainingItemName,
                        NodeID = item.TrainingItemId,
                        EnableClickEvent = true
                    };
                    nodes.Add(gChidNode);
                }
            }
        }
        #endregion

        #region 树节点选择
        /// <summary>
        ///  树节点选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvUploadResources_NodeCommand(object sender, FineUIPro.TreeCommandEventArgs e)
        {
            this.SetTemp();
            this.TrainingItemId = this.tvUploadResources.SelectedNode.NodeID;
            var trainingItem = BLL.TrainingItemService.GetTrainingItemByTrainingItemId(this.TrainingItemId);
            if (trainingItem != null)
            {
                if (trainingItem.AuditDate.HasValue)
                {
                    this.btnDelete.Hidden = true;
                    this.btnSave.Hidden = true;
                }
                else
                {
                    this.btnDelete.Hidden = false;
                    this.btnSave.Hidden = false;
                }

                if (trainingItem != null)
                {
                    txtTrainingItemCode.Text = trainingItem.TrainingItemCode;
                    txtTrainingItemName.Text = trainingItem.TrainingItemName;
                    Model.Sys_User compileMan = BLL.UserService.GetUserByUserId(trainingItem.CompileMan);
                    if (compileMan != null)
                    {
                        txtCompileMan.Text = compileMan.UserName;
                    }
                    if (trainingItem.CompileDate != null)
                    {
                        txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", trainingItem.CompileDate);
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// 增加上传资源按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            this.SetTemp();
        }

        /// <summary>
        /// 删除上传资源按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var trainingItem = BLL.TrainingItemService.GetTrainingItemByTrainingItemId(this.TrainingItemId);
            if (trainingItem != null && !trainingItem.AuditDate.HasValue)
            {
                BLL.TrainingItemService.DeleteTrainingItemsByTrainingItemId(this.TrainingItemId);
                this.SetTemp();
                this.InitTreeMenu();
                ShowNotify("删除成功！");
            }
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
            this.InitTreeMenu();
            ShowNotify("保存成功！");
        }

        private void SaveData()
        {
            Model.Training_TrainingItem trainingItem = new Model.Training_TrainingItem
            {
                TrainingItemCode = this.txtTrainingItemCode.Text.Trim(),
                TrainingItemName = this.txtTrainingItemName.Text.Trim(),
                IsPass = null,
                CompileMan = this.CurrUser.UserName,
                UnitId = CommonService.GetUnitId(this.CurrUser.UnitId),
                CompileDate = Convert.ToDateTime(this.txtCompileDate.Text)
            };
            if (string.IsNullOrEmpty(this.TrainingItemId))
            {
                trainingItem.TrainingId = this.TrainingId;
                this.TrainingItemId = trainingItem.TrainingItemId = SQLHelper.GetNewID(typeof(Model.Training_TrainTestDB));
                BLL.TrainingItemService.AddTrainingItem(trainingItem);
                BLL.LogService.AddSys_Log(this.CurrUser, trainingItem.TrainingItemCode, trainingItem.TrainingItemId, BLL.Const.TrainDBMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                var t = BLL.TrainingItemService.GetTrainingItemByTrainingItemId(this.TrainingItemId);
                if (t != null)
                {
                    trainingItem.TrainingId = t.TrainingId;
                }
                trainingItem.TrainingItemId = this.TrainingItemId;
                BLL.TrainingItemService.UpdateTrainingItem(trainingItem);
                BLL.LogService.AddSys_Log(this.CurrUser, trainingItem.TrainingItemCode, trainingItem.TrainingItemId, BLL.Const.TrainDBMenuId, BLL.Const.BtnModify);
            }
        }        

        #region 界面清空
        /// <summary>
        /// 清空
        /// </summary>
        private void SetTemp()
        {
            this.txtTrainingItemCode.Focus();
            this.TrainingItemId = string.Empty;

            this.txtCompileMan.Text = this.CurrUser.UserName;
            this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);

            this.txtTrainingItemCode.Text = string.Empty;
            this.txtTrainingItemName.Text = string.Empty;
            

            this.lbAttachUrl.Text = string.Empty;

            this.btnDelete.Hidden = false;
            this.btnSave.Hidden = false;
        }
        #endregion

        #region 按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.TrainDBMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnUploadResources))
                {
                    this.btnNew.Hidden = false;
                    this.btnDelete.Hidden = false;
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion

        #region 验证教材名称是否存在
        /// <summary>
        /// 验证教材名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Training_TrainingItem.FirstOrDefault(x => x.TrainingId == this.TrainingId && x.TrainingItemName == this.txtTrainingItemName.Text.Trim() && (x.TrainingItemId != this.TrainingItemId || (this.TrainingItemId == null && x.TrainingItemId != null)));
            if (q != null)
            {
                ShowNotify("输入的教材名称已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        /// <summary>
        /// 上传附件资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUploadResources_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.TrainingItemId))
            {
                SaveData();
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/Training&menuId=9D99A981-7380-4085-84FA-8C3B1AFA6202&type=0", TrainingItemId)));
        }
    }
}