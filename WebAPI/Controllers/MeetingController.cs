using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL;

namespace WebAPI.Controllers
{
    /// <summary>
    /// 会议接口
    /// </summary>
    public class MeetingController : ApiController
    {
        #region 根据MeetingId获取会议详细信息
        /// <summary>
        /// 根据MeetingId获取会议详细信息
        /// </summary>
        /// <param name="meetingId">会议ID</param>
        /// <param name="meetingType">会议类型(C-班前会；W-周例会；M-例会；S-专题例会；A-其他会议)</param>
        /// <returns>会议详细</returns>
        public Model.ResponeData getMeetingByMeetingId(string meetingId, string meetingType)
        {
            var responeData = new Model.ResponeData();
            try
            {
                responeData.data = BLL.APIMeetingService.getMeetingByMeetingId(meetingId, meetingType);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 根据projectId\meetingType\states获取会议列表
        /// <summary>
        /// 根据projectId、meetingType获取会议列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="meetingType">会议类型(C-班前会；W-周例会；M-例会；S-专题例会；A-其他会议)</param>
        /// <param name="states">状态（0-待提交；1-已提交）</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public Model.ResponeData getMeetingByProjectIdStates(string projectId, string meetingType, string states, int pageIndex)
        {
            var responeData = new Model.ResponeData();
            try
            {
                var getDataList = BLL.APIMeetingService.getMeetingByProjectIdStates(projectId, meetingType, states);
                int pageCount = getDataList.Count();
                if (pageCount > 0 && pageIndex > 0)
                {
                    getDataList = getDataList.OrderByDescending(u => u.MeetingDate).Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize).ToList();

                }
                responeData.data = new { pageCount, getDataList };
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion

        #region 保存Meeting
        /// <summary>
        /// 保存Meeting
        /// </summary>
        /// <param name="meeting">会议信息</param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData SaveMeeting([FromBody] Model.MeetingItem meeting)
        {
            var responeData = new Model.ResponeData();
            try
            {
                BLL.APIMeetingService.SaveMeeting(meeting);
            }
            catch (Exception ex)
            {
                responeData.code = 0;
                responeData.message = ex.Message;
            }

            return responeData;
        }
        #endregion
    }
}
