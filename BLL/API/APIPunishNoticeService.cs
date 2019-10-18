using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;

namespace BLL
{
    /// <summary>
    /// 处罚通知单
    /// </summary>
    public static class APIPunishNoticeService
    {
        #region 根据PunishNoticeId获取处罚通知单
        /// <summary>
        ///  根据 PunishNoticeId获取处罚通知单
        /// </summary>
        /// <param name="PunishNoticeId"></param>
        /// <returns></returns>
        public static Model.PunishNoticeItem getPunishNoticeById(string PunishNoticeId)
        {
            var getInfo = from x in Funs.DB.Check_PunishNotice
                          where x.PunishNoticeId == PunishNoticeId
                          select new Model.PunishNoticeItem
                          {
                              PunishNoticeId = x.PunishNoticeId,
                              ProjectId = x.ProjectId,
                              PunishNoticeCode = x.PunishNoticeCode,
                              UnitId = x.UnitId,
                              UnitName = Funs.DB.Base_Unit.First(u => u.UnitId == x.UnitId).UnitName,
                              ContractNum = x.ContractNum,
                              PunishNoticeDate = string.Format("{0:yyyy-MM-dd}", x.PunishNoticeDate),
                              BasicItem = x.BasicItem,
                              IncentiveReason = x.IncentiveReason,
                              PunishMoney = x.PunishMoney ?? 0,
                              Currency = x.Currency,
                              FileContents = System.Web.HttpUtility.HtmlDecode(x.FileContents),
                              CompileManId = x.CompileMan,
                              CompileManName = Funs.DB.Sys_User.First(u => u.UserId == x.CompileMan).UserName,
                              CompileDate = string.Format("{0:yyyy-MM-dd}", x.CompileDate),
                              SignManId = x.SignMan,
                              SignManName = Funs.DB.Sys_User.First(u => u.UserId == x.SignMan).UserName,
                              ApproveManId = x.ApproveMan,
                              ApproveManName = Funs.DB.Sys_User.First(u => u.UserId == x.ApproveMan).UserName,
                              States = x.States,
                              PunishUrl = Funs.DB.AttachFile.FirstOrDefault(z => z.MenuId == Const.ProjectPunishNoticeStatisticsMenuId && z.ToKeyId == x.PunishNoticeId).AttachUrl.Replace('\\', '/'),
                              ReceiptUrl = Funs.DB.AttachFile.FirstOrDefault(z => z.MenuId == Const.ProjectPunishNoticeMenuId && z.ToKeyId == x.PunishNoticeId).AttachUrl.Replace('\\', '/'),
                          };
            return getInfo.FirstOrDefault();
        }
        #endregion        

        #region 获取处罚通知单列表信息
        /// <summary>
        /// 获取处罚通知单列表信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static List<Model.PunishNoticeItem> getPunishNoticeList(string projectId, string unitId, string strParam, string states)
        {
            var getPunishNotice = from x in Funs.DB.Check_PunishNotice
                                  where x.ProjectId == projectId && (x.UnitId == unitId || unitId == null)
                               //&& (strParam == null || x.PunishNoticeName.Contains(strParam)) 
                                && (states == null || (states == "0" && (x.States == "0" || x.States == null)) || (states == "1" && (x.States == "1" || x.States == "2")))
                                  orderby x.PunishNoticeCode descending
                                  select new Model.PunishNoticeItem
                                  {
                                      PunishNoticeId = x.PunishNoticeId,
                                      ProjectId = x.ProjectId,
                                      PunishNoticeCode = x.PunishNoticeCode,
                                      UnitId = x.UnitId,
                                      UnitName = Funs.DB.Base_Unit.First(u => u.UnitId == x.UnitId).UnitName,
                                      ContractNum = x.ContractNum,
                                      PunishNoticeDate = string.Format("{0:yyyy-MM-dd}", x.PunishNoticeDate),
                                      BasicItem = x.BasicItem,
                                      IncentiveReason = x.IncentiveReason,
                                      PunishMoney = x.PunishMoney ?? 0,
                                      Currency = x.Currency,
                                      //FileContents = System.Web.HttpUtility.HtmlDecode(x.FileContents),
                                      CompileManId = x.CompileMan,
                                      CompileManName = Funs.DB.Sys_User.First(u => u.UserId == x.CompileMan).UserName,
                                      CompileDate = string.Format("{0:yyyy-MM-dd}", x.CompileDate),
                                      //SignManId = x.SignMan,
                                      //SignManName = Funs.DB.Sys_User.First(u => u.UserId == x.SignMan).UserName,
                                      //ApproveManId = x.ApproveMan,
                                      //ApproveManName = Funs.DB.Sys_User.First(u => u.UserId == x.ApproveMan).UserName,
                                      States = x.States,
                                      //PunishUrl = Funs.DB.AttachFile.FirstOrDefault(z => z.MenuId == Const.ProjectPunishNoticeStatisticsMenuId && z.ToKeyId == x.PunishNoticeId).AttachUrl.Replace('\\', '/'),
                                      //ReceiptUrl = Funs.DB.AttachFile.FirstOrDefault(z => z.MenuId == Const.ProjectPunishNoticeMenuId && z.ToKeyId == x.PunishNoticeId).AttachUrl.Replace('\\', '/'),
                                  };
            return getPunishNotice.ToList();
        }
        #endregion        

        #region 保存Check_PunishNotice
        /// <summary>
        /// 保存Check_PunishNotice
        /// </summary>
        /// <param name="newItem">处罚通知单</param>
        /// <returns></returns>
        public static void SavePunishNotice(Model.PunishNoticeItem newItem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Check_PunishNotice newPunishNotice = new Model.Check_PunishNotice
            {
                PunishNoticeId = newItem.PunishNoticeId,
                PunishNoticeCode = newItem.PunishNoticeCode,
                ProjectId = newItem.ProjectId,
                PunishNoticeDate = Funs.GetNewDateTime(newItem.PunishNoticeDate),
                UnitId = newItem.UnitId,
                ContractNum = newItem.ContractNum,
                IncentiveReason = newItem.IncentiveReason,
                BasicItem = newItem.BasicItem,
                PunishMoney = newItem.PunishMoney,
                Currency = newItem.Currency,
                SignMan = newItem.SignManId,
                ApproveMan = newItem.ApproveManId,
                FileContents = System.Web.HttpUtility.HtmlEncode(newItem.FileContents),
                CompileMan = newItem.CompileManId,
                States = Const.State_2,
            };
            if (newItem.States != "1")
            {
                newPunishNotice.States = Const.State_0;
            }

            var updatePunishNotice = Funs.DB.Check_PunishNotice.FirstOrDefault(x => x.PunishNoticeId == newItem.PunishNoticeId);
            if (updatePunishNotice == null)
            {
                newPunishNotice.CompileDate = DateTime.Now;
                newPunishNotice.PunishNoticeId = SQLHelper.GetNewID();
                newPunishNotice.PunishNoticeCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectPunishNoticeMenuId, newPunishNotice.ProjectId, newPunishNotice.UnitId);
                PunishNoticeService.AddPunishNotice(newPunishNotice);
            }
            else
            {
                PunishNoticeService.UpdatePunishNotice(newPunishNotice);
            }
            if (newPunishNotice.States == "1")
            {
                CommonService.btnSaveData(newPunishNotice.ProjectId, Const.ProjectPunishNoticeMenuId, newPunishNotice.PunishNoticeId, newPunishNotice.CompileMan, true, newPunishNotice.PunishNoticeCode, "../Solution/PunishNoticeView.aspx?PunishNoticeId={0}");
            }
            ////保存附件
            if (!string.IsNullOrEmpty(newItem.PunishUrl))
            {
                UploadFileService.SaveAttachUrl(UploadFileService.GetSourceByAttachUrl(newItem.PunishUrl, 10, null), newItem.PunishUrl, Const.ProjectPunishNoticeStatisticsMenuId, newPunishNotice.PunishNoticeId);
            }
            else
            {
                CommonService.DeleteAttachFileById(Const.ProjectPunishNoticeStatisticsMenuId,newPunishNotice.PunishNoticeId);
            }
        }
        #endregion

        #region 保存处罚单-回执单
        /// <summary>
        /// 保存处罚单-回执单
        /// </summary>
        /// <param name="punishNoticeId">主键</param>
        /// <param name="attachUrl">回执单路径</param>
        public static void SavePunishNoticeReceiptUrl(string punishNoticeId,string attachUrl)
        {
            var getPunishNotice = Funs.DB.Check_PunishNotice.FirstOrDefault(x => x.PunishNoticeId == punishNoticeId);
            if (getPunishNotice != null)
            {
                ////保存附件
                if (!string.IsNullOrEmpty(attachUrl))
                {
                    UploadFileService.SaveAttachUrl(UploadFileService.GetSourceByAttachUrl(attachUrl, 10, null), attachUrl, Const.ProjectPunishNoticeMenuId, getPunishNotice.PunishNoticeId);
                }
                else
                {
                    CommonService.DeleteAttachFileById(Const.ProjectPunishNoticeMenuId,getPunishNotice.PunishNoticeId);
                }
            }
        }
        #endregion
    }
}
