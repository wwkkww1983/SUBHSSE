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
    public partial class TrainTestDBUpload : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string TrainTestItemId
        {
            get
            {
                return (string)ViewState["TrainTestItemId"];
            }
            set
            {
                ViewState["TrainTestItemId"] = value;
            }
        }

        public string TrainTestId
        {
            get
            {
                return (string)ViewState["TrainTestId"];
            }
            set
            {
                ViewState["TrainTestId"] = value;
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
                this.GetButtonPower();
                this.InitTreeMenu();

                this.TrainTestId = Request.Params["TrainTestId"];

                this.txtCompileMan.Text = this.CurrUser.UserName;
                this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
            }
        }
        #endregion

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
                var trainTestDBItem = BLL.TrainTestDBItemService.GetTrainTestDBItemByCompile(this.CurrUser.UserName);
                foreach (var item in uploadType)
                {
                    TreeNode chidNode = new TreeNode
                    {
                        Text = item.ConstText,
                        NodeID = item.ConstValue
                    };
                    rootNode.Nodes.Add(chidNode);
                    this.BoundTree(chidNode.Nodes, item.ConstValue, trainTestDBItem);
                }
            }
        }

        /// <summary>
        /// 遍历增加子节点
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="menuId"></param>
        private void BoundTree(TreeNodeCollection nodes, string typeId, List<Model.Training_TrainTestDBItem> trainTestDBItem)
        {
            List<Model.Training_TrainTestDBItem> chidLaw = new List<Model.Training_TrainTestDBItem>();
            if (typeId == BLL.Const.UploadResources_1) ///未审核
            {
                chidLaw = trainTestDBItem.Where(x => x.IsPass == null).ToList();
            }
            if (typeId == BLL.Const.UploadResources_2) ///未采用
            {
                chidLaw = trainTestDBItem.Where(x => x.AuditDate.HasValue && x.IsPass == false).ToList();
            }
            if (typeId == BLL.Const.UploadResources_3) ///已采用
            {
                chidLaw = trainTestDBItem.Where(x => x.AuditDate.HasValue && x.IsPass == true).ToList();
            }
            if (chidLaw.Count() > 0)
            {
                foreach (var item in chidLaw)
                {
                    TreeNode gChidNode = new TreeNode
                    {
                        Text = item.TraiinTestItemName,
                        NodeID = item.TrainTestItemId,
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
            this.TrainTestItemId = this.tvUploadResources.SelectedNode.NodeID;
            var trainTestDBItem = BLL.TrainTestDBItemService.GetTrainTestDBItemById(this.TrainTestItemId);
            if (trainTestDBItem != null)
            {
                if (trainTestDBItem.AuditDate.HasValue)
                {
                    this.btnDelete.Hidden = true;
                    this.btnSave.Hidden = true;
                }
                else
                {
                    this.btnDelete.Hidden = false;
                    this.btnSave.Hidden = false;
                }
                if (trainTestDBItem != null)
                {
                    this.txtTrainTestItemCode.Text = trainTestDBItem.TrainTestItemCode;
                    this.txtTrainTestItemName.Text = trainTestDBItem.TraiinTestItemName;
                    if (!string.IsNullOrEmpty(trainTestDBItem.AttachUrl))
                    {
                        this.lbAttachUrl.Text = trainTestDBItem.AttachUrl.Substring(trainTestDBItem.AttachUrl.IndexOf("~") + 1);
                    }
                }                  
            }
        }
        #endregion        

        #region 增加
        /// <summary>
        /// 增加上传资源按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            this.SetTemp();
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除上传资源按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var trainTestDBItem = BLL.TrainTestDBItemService.GetTrainTestDBItemById(this.TrainTestItemId);
            if (trainTestDBItem != null && !trainTestDBItem.AuditDate.HasValue)
            {
                BLL.TrainTestDBItemService.DeleteTrainTestDBItemById(this.TrainTestItemId);
                this.SetTemp();
                this.InitTreeMenu();
                ShowNotify("删除成功！");
            }
        }
        #endregion

        #region 保存
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
            Model.Training_TrainTestDBItem trainTestDBItem = new Model.Training_TrainTestDBItem
            {
                TrainTestItemCode = this.txtTrainTestItemCode.Text.Trim(),
                TraiinTestItemName = this.txtTrainTestItemName.Text.Trim(),
                //trainTestDBItem.AttachUrl = this.FullAttachUrl;
                IsPass = null,
                CompileMan = this.CurrUser.UserName,
                UnitId = CommonService.GetUnitId(this.CurrUser.UnitId),
                CompileDate = Convert.ToDateTime(this.txtCompileDate.Text)
            };
            if (string.IsNullOrEmpty(this.TrainTestItemId))
            {
                trainTestDBItem.TrainTestId = this.TrainTestId;
                this.TrainTestItemId = trainTestDBItem.TrainTestItemId = SQLHelper.GetNewID(typeof(Model.Training_TrainTestDBItem));
                BLL.TrainTestDBItemService.AddTrainTestDBItem(trainTestDBItem);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "添加安全试题库");
            }
            else
            {
                var t = BLL.TrainTestDBItemService.GetTrainTestDBItemById(this.TrainTestItemId);
                if (t != null)
                {
                    trainTestDBItem.TrainTestId = t.TrainTestId;
                }
                trainTestDBItem.TrainTestItemId = this.TrainTestItemId;
                BLL.TrainTestDBItemService.UpdateTrainTestDBItem(trainTestDBItem);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "修改安全试题库");
            }
        }
        #endregion

        #region 界面清空
        /// <summary>
        /// 清空
        /// </summary>
        private void SetTemp()
        {
            this.txtTrainTestItemCode.Focus();
            this.TrainTestItemId = string.Empty;

            this.txtCompileMan.Text = this.CurrUser.UserName;
            this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);

            this.txtTrainTestItemCode.Text = string.Empty;
            this.txtTrainTestItemName.Text = string.Empty;


            //this.fuAttachUrl.Reset();
            this.lbAttachUrl.Text = string.Empty;
            //this.FullAttachUrl = string.Empty;

            this.btnDelete.Hidden = false;
            this.btnSave.Hidden = false;
            //this.btnUpFile.Hidden = false;
            //this.btnDeleteFile.Hidden = false;
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.TrainTestDBMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSaveUp))
                {
                    this.btnNew.Hidden = false;
                    this.btnDelete.Hidden = false;
                    this.btnSave.Hidden = false;
                    //this.btnUpFile.Hidden = false;
                    //this.btnDeleteFile.Hidden = false;
                }
            }
        }
        #endregion

        #region 验证试题名称是否存在
        /// <summary>
        /// 验证试题名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Training_TrainTestDBItem.FirstOrDefault(x => x.TrainTestId == this.TrainTestId && x.TraiinTestItemName == this.txtTrainTestItemName.Text.Trim() && (x.TrainTestItemId != this.TrainTestItemId || (this.TrainTestItemId == null && x.TrainTestItemId != null)));
            if (q != null)
            {
                ShowNotify("输入的试题名称已存在！", MessageBoxIcon.Warning);
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
            if (string.IsNullOrEmpty(this.TrainTestItemId))
            {
                SaveData();
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/TrainTestDB&menuId=F58EE8ED-9EB5-47C7-9D7F-D751EFEA44CA&type=0", TrainTestItemId)));
        }
    }
}