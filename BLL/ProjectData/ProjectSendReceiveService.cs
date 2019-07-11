using System.Linq;

namespace BLL
{
    /// <summary>
    /// 项目用户转换
    /// </summary>
    public static class ProjectSendReceiveService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取项目用户信息
        /// </summary>
        /// <param name="SendReceiveId"></param>
        /// <returns></returns>
        public static Model.Project_SendReceive GetSendReceiveById(string SendReceiveId)
        {
            return Funs.DB.Project_SendReceive.FirstOrDefault(e => e.SendReceiveId == SendReceiveId);
        }

        /// <summary>
        /// 根据主键获取项目用户信息
        /// </summary>
        /// <param name="SendReceiveId"></param>
        /// <returns></returns>
        public static Model.Project_SendReceive GetNoSendReceiveByPersonId(string userId)
        {
            return Funs.DB.Project_SendReceive.FirstOrDefault(e => e.UserId == userId && e.IsAgree == true);
        }

        /// <summary>
        /// 增加项目用户信息
        /// </summary>
        /// <param name="getSendReceive">人员实体</param>
        public static void AddSendReceive(Model.Project_SendReceive getSendReceive)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Project_SendReceive newSendReceive = new Model.Project_SendReceive
            {
                SendReceiveId = SQLHelper.GetNewID(typeof(Model.Project_SendReceive)),
                UserId = getSendReceive.UserId,
                SendProjectId = getSendReceive.SendProjectId,
                ReceiveProjectId = getSendReceive.ReceiveProjectId,
                SendTime = getSendReceive.SendTime,
                ReceiveTime = getSendReceive.ReceiveTime
            };
            db.Project_SendReceive.InsertOnSubmit(newSendReceive);
            db.SubmitChanges();
        }

        /// <summary>
        /// 项目用户记录
        /// </summary>
        /// <param name="sendReceive"></param>
        public static void UpdateSendReceive(Model.Project_SendReceive sendReceive)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var newSendReceive = db.Project_SendReceive.FirstOrDefault(e => e.SendReceiveId == sendReceive.SendReceiveId);
            if (newSendReceive != null)
            {
                newSendReceive.ReceiveTime = sendReceive.ReceiveTime;
                newSendReceive.IsAgree = sendReceive.IsAgree;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据人员主键删除一个项目用户记录
        /// </summary>
        /// <param name="personId"></param>
        public static void DeleteSendReceiveBySendReceiveId(string sendReceiveId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var sendReceive = db.Project_SendReceive.FirstOrDefault(x=> x.SendReceiveId == sendReceiveId);
            if (sendReceive != null)
            {
                db.Project_SendReceive.DeleteOnSubmit(sendReceive);
                db.SubmitChanges();
            }
        }
    }
}