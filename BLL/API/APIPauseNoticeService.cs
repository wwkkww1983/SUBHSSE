using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;

namespace BLL
{
    /// <summary>
    ///  工程暂停令
    /// </summary>
    public static class APIPauseNoticeService
    {
        #region 根据PauseNoticeId获取工程暂停令
        /// <summary>
        ///  根据 PauseNoticeId获取工程暂停令
        /// </summary>
        /// <param name="PauseNoticeId"></param>
        /// <returns></returns>
        public static Model.PauseNoticeItem getPauseNoticeById(string PauseNoticeId)
        {
            var getInfo = from x in Funs.DB.Check_PauseNotice
                          where x.PauseNoticeId == PauseNoticeId
                          select new Model.PauseNoticeItem
                          {
                              PauseNoticeId = x.PauseNoticeId,
                              ProjectId = x.ProjectId,
                              PauseNoticeCode = x.PauseNoticeCode,
                              UnitId = x.UnitId,
                              UnitName = Funs.DB.Base_Unit.First(u => u.UnitId == x.UnitId).UnitName,                              
                              CompileManId = x.CompileMan,
                              CompileManName =x.SignPerson,
                              CompileDate = string.Format("{0:yyyy-MM-dd}", x.CompileDate),
                              SignManId = x.SignMan,
                              SignManName = Funs.DB.Sys_User.First(u => u.UserId == x.SignMan).UserName,
                              ApproveManId = x.ApproveMan,
                              ApproveManName = Funs.DB.Sys_User.First(u => u.UserId == x.ApproveMan).UserName,
                              ProjectPlace=x.ProjectPlace,
                              WrongContent=x.WrongContent,
                              PauseTime=string.Format("{0:yyyy-MM-dd HH:mm}", x.PauseTime),
                              PauseContent = x.PauseContent,
                              OneContent=x.OneContent,
                              SecondContent=x.SecondContent,
                              ThirdContent=x.ThirdContent,
                              ProjectHeadConfirm=x.ProjectHeadConfirm,
                              ProjectHeadConfirmId = x.ProjectHeadConfirmId,
                              IsConfirm =x.IsConfirm,
                              IsConfirmName=(x.IsConfirm ==true?"已确认":"待确认"),
                              ConfirmDate = string.Format("{0:yyyy-MM-dd}", x.ConfirmDate),
                              States = x.States,
                              AttachUrl =APIUpLoadFileService.getFileUrl(x.PauseNoticeId,x.AttachUrl),
                              PauseNoticeAttachUrl= APIUpLoadFileService.getFileUrl(x.PauseNoticeId, null),
                          };
            return getInfo.FirstOrDefault();
        }
        #endregion        

        #region 获取工程暂停令列表信息
        /// <summary>
        /// 获取工程暂停令列表信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static List<Model.PauseNoticeItem> getPauseNoticeList(string projectId, string unitId, string strParam, string states)
        {
            var getPauseNotice = from x in Funs.DB.Check_PauseNotice
                                  where x.ProjectId == projectId && (x.UnitId == unitId || unitId == null)
                               && (strParam == null || x.ProjectPlace.Contains(strParam))                                                               
                                  select new Model.PauseNoticeItem
                                  {
                                      PauseNoticeId = x.PauseNoticeId,
                                      ProjectId = x.ProjectId,
                                      PauseNoticeCode = x.PauseNoticeCode,
                                      UnitId = x.UnitId,
                                      UnitName = Funs.DB.Base_Unit.First(u => u.UnitId == x.UnitId).UnitName,
                                      ProjectPlace = x.ProjectPlace,
                                      WrongContent = x.WrongContent,
                                      PauseTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.PauseTime),
                                      PauseContent = x.PauseContent,
                                      CompileManId = x.CompileMan,
                                      CompileManName = x.SignPerson,
                                      CompileDate = string.Format("{0:yyyy-MM-dd}", x.CompileDate),
                                      SignManId = x.SignMan,
                                      SignManName = Funs.DB.Sys_User.First(u => u.UserId == x.SignMan).UserName,
                                      ApproveManId = x.ApproveMan,
                                      ApproveManName = Funs.DB.Sys_User.First(u => u.UserId == x.ApproveMan).UserName,
                                      ProjectHeadConfirm = x.ProjectHeadConfirm,
                                      ProjectHeadConfirmId = x.ProjectHeadConfirmId,
                                      IsConfirm = x.IsConfirm,
                                      IsConfirmName = (x.IsConfirm == true ? "已确认" : "待确认"),
                                      ConfirmDate = string.Format("{0:yyyy-MM-dd}", x.ConfirmDate),
                                      States = x.States,
                                      PauseNoticeAttachUrl = APIUpLoadFileService.getFileUrl(x.PauseNoticeId, null),
                                  };
            if (states == "1")
            {
                getPauseNotice = getPauseNotice.Where(x => !x.IsConfirm.HasValue || x.IsConfirm == false);
            }
            else if (states == "2")
            {
                getPauseNotice = getPauseNotice.Where(x =>  x.IsConfirm == true);
            }

            return getPauseNotice.OrderByDescending(x=> x.PauseNoticeCode).ToList();
        }
        #endregion        

        #region 保存Check_PauseNotice
        /// <summary>
        /// 保存Check_PauseNotice
        /// </summary>
        /// <param name="newItem">工程暂停令</param>
        /// <returns></returns>
        public static void SavePauseNotice(Model.PauseNoticeItem newItem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Check_PauseNotice newPauseNotice = new Model.Check_PauseNotice
            {
                PauseNoticeId = newItem.PauseNoticeId,
                PauseNoticeCode = newItem.PauseNoticeCode,
                ProjectId = newItem.ProjectId,
                UnitId = newItem.UnitId,
                SignPerson = newItem.CompileManName,
                CompileMan = newItem.CompileManId,
                CompileDate = Funs.GetNewDateTime(newItem.CompileDate),
                ProjectPlace = newItem.ProjectPlace,
                WrongContent = newItem.WrongContent,
                PauseTime = Funs.GetNewDateTime(newItem.PauseTime),
                PauseContent=newItem.PauseContent,
                OneContent=newItem.OneContent,
                SecondContent = newItem.SecondContent,
                ThirdContent = newItem.ThirdContent,
                ProjectHeadConfirm =newItem.ProjectHeadConfirm,
                ProjectHeadConfirmId = newItem.ProjectHeadConfirmId,
                ConfirmDate = Funs.GetNewDateTime(newItem.ConfirmDate),
                AttachUrl=newItem.AttachUrl,
                SignMan=newItem.SignManId,
                ApproveMan =newItem.ApproveManId,               
            };

            if (newPauseNotice.ConfirmDate.HasValue)
            {
                newPauseNotice.ConfirmDate = DateTime.Now;
                newPauseNotice.States = Const.State_2;
            } else
            {
                newPauseNotice.States = Const.State_0;
            }

            var updatePauseNotice = Funs.DB.Check_PauseNotice.FirstOrDefault(x => x.PauseNoticeId == newItem.PauseNoticeId);
            if (updatePauseNotice == null)
            {               
                newPauseNotice.PauseNoticeId = SQLHelper.GetNewID();
                newPauseNotice.PauseNoticeCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectPauseNoticeMenuId, newPauseNotice.ProjectId, newPauseNotice.UnitId);
                Check_PauseNoticeService.AddPauseNotice(newPauseNotice);

                //// 回写巡检记录表
                if (!string.IsNullOrEmpty(newItem.HazardRegisterId))
                {
                    List<string> listIds = Funs.GetStrListByStr(newItem.HazardRegisterId, ',');
                    foreach (var item in listIds)
                    {
                        var getHazardRegister = Funs.DB.HSSE_Hazard_HazardRegister.FirstOrDefault(x => x.HazardRegisterId == item);
                        if (getHazardRegister != null)
                        {
                            getHazardRegister.States = "3";
                            getHazardRegister.HandleIdea += "已下发工程暂停令：" + newPauseNotice.PauseNoticeCode;
                            getHazardRegister.ResultId = newPauseNotice.PauseNoticeId;
                            getHazardRegister.ResultType = "3";
                            Funs.SubmitChanges();
                        }
                    }
                }
            }
            else
            {
                Check_PauseNoticeService.UpdatePauseNotice(newPauseNotice);
            }
            if (newPauseNotice.States == "1")
            {
                CommonService.btnSaveData(newPauseNotice.ProjectId, Const.ProjectPauseNoticeMenuId, newPauseNotice.PauseNoticeId, newPauseNotice.CompileMan, true, newPauseNotice.PauseContent, "../Check/PauseNoticeView.aspx?PauseNoticeId={0}");
            }
            ////保存附件
            //SavePauseNoticeUrl(newPauseNotice.PauseNoticeId, newItem.PauseNoticeAttachUrl);
        }
        #endregion

        #region 保存暂停令通知单
        /// <summary>
        /// 保存处罚单-回执单
        /// </summary>
        /// <param name="pauseNoticeId">主键</param>
        /// <param name="attachUrl">路径</param>
        public static void SavePauseNoticeUrl(string pauseNoticeId, string attachUrl)
        {
            var getPauseNotice = Funs.DB.Check_PauseNotice.FirstOrDefault(x => x.PauseNoticeId == pauseNoticeId);
            if (getPauseNotice != null)
            {
                string menuId = Const.ProjectPauseNoticeMenuId;               
                ////保存附件
                if (!string.IsNullOrEmpty(attachUrl))
                {
                    UploadFileService.SaveAttachUrl(UploadFileService.GetSourceByAttachUrl(attachUrl, 10, null), attachUrl, menuId, getPauseNotice.PauseNoticeId);
                }
                else
                {
                    CommonService.DeleteAttachFileById(menuId, getPauseNotice.PauseNoticeId);
                }
            }
        }
        #endregion
    }
}
