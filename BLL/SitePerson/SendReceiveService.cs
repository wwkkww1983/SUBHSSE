using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 人员项目转换
    /// </summary>
    public static class SendReceiveService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取人员项目转换信息
        /// </summary>
        /// <param name="SendReceiveId"></param>
        /// <returns></returns>
        public static Model.SitePerson_SendReceive GetSendReceiveById(string SendReceiveId)
        {
            return Funs.DB.SitePerson_SendReceive.FirstOrDefault(e => e.SendReceiveId == SendReceiveId);
        }

        /// <summary>
        /// 根据主键获取人员项目转换信息
        /// </summary>
        /// <param name="SendReceiveId"></param>
        /// <returns></returns>
        public static Model.SitePerson_SendReceive GetNoSendReceiveByPersonId(string PersonId)
        {
            return Funs.DB.SitePerson_SendReceive.FirstOrDefault(e => e.PersonId == PersonId && e.IsAgree == true);
        }

        /// <summary>
        /// 增加人员项目转换信息
        /// </summary>
        /// <param name="getSendReceive">人员实体</param>
        public static void AddSendReceive(Model.SitePerson_SendReceive getSendReceive)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SitePerson_SendReceive newSendReceive = new Model.SitePerson_SendReceive
            {
                SendReceiveId = SQLHelper.GetNewID(typeof(Model.SitePerson_SendReceive)),
                PersonId = getSendReceive.PersonId,
                SendProjectId = getSendReceive.SendProjectId,
                ReceiveProjectId = getSendReceive.ReceiveProjectId,
                SendTime = getSendReceive.SendTime,
                ReceiveTime = getSendReceive.ReceiveTime
            };
            db.SitePerson_SendReceive.InsertOnSubmit(newSendReceive);
            db.SubmitChanges();
        }

        /// <summary>
        /// 人员项目转换记录
        /// </summary>
        /// <param name="sendReceive"></param>
        public static void UpdateSendReceive(Model.SitePerson_SendReceive sendReceive)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var newSendReceive = db.SitePerson_SendReceive.FirstOrDefault(e => e.SendReceiveId == sendReceive.SendReceiveId);
            if (newSendReceive != null)
            {
                newSendReceive.ReceiveTime = sendReceive.ReceiveTime;
                newSendReceive.IsAgree = sendReceive.IsAgree;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据人员主键删除一个人员项目转换记录
        /// </summary>
        /// <param name="personId"></param>
        public static void DeleteSendReceiveBySendReceiveId(string sendReceiveId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var sendReceive = db.SitePerson_SendReceive.FirstOrDefault(x=> x.SendReceiveId == sendReceiveId);
            if (sendReceive != null)
            {
                db.SitePerson_SendReceive.DeleteOnSubmit(sendReceive);
                db.SubmitChanges();
            }
        }
    }
}