using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class PersonItem
    {
        /// <summary>
        /// 人员主键ID
        /// </summary>
        public string PersonId { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string PersonName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IdentityCard { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 项目id
        /// </summary>
        public string ProjectId { get; set; }
        /// <summary>
        /// 项目号
        /// </summary>
        public string ProjectCode { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 单位ID
        /// </summary>
        public string UnitId { get; set; }
        /// <summary>
        /// 单位代号
        /// </summary>
        public string UnitCode { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// 班组ID
        /// </summary>
        public string TeamGroupId { get; set; }
        /// <summary>
        /// 班组名称
        /// </summary>
        public string TeamGroupName { get; set; }
        /// <summary>
        /// 区域ID
        /// </summary>
        public string WorkAreaId { get; set; }
        /// <summary>
        /// 区域代码
        /// </summary>
        public string WorkAreaCode { get; set; }
        /// <summary>
        /// 区域名称
        /// </summary>
        public string WorkAreaName { get; set; }
        /// <summary>
        /// 岗位ID
        /// </summary>
        public string WorkPostId { get; set; }
        /// <summary>
        /// 岗位名称
        /// </summary>
        public string WorkPostName { get; set; }
        /// <summary>
        /// 入场时间
        /// </summary>
        public DateTime? InTime { get; set; }
        /// <summary>
        /// 出场时间
        /// </summary>
        public DateTime? OutTime { get; set; }
        /// <summary>
        /// 出场原因
        /// </summary>
        public string OutResult { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 职务ID
        /// </summary>
        public string PositionId { get; set; }
        /// <summary>
        /// 职务名称
        /// </summary>
        public string PositionName { get; set; }
        /// <summary>
        /// 职称id
        /// </summary>
        public string PostTitleId { get; set; }
        /// <summary>
        /// 职称
        /// </summary>
        public string PostTitleName { get; set; }
        /// <summary>
        /// 照片路径
        /// </summary>
        public string PhotoUrl { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public string IsUsed { get; set; }
        /// <summary>
        /// 考勤卡是否启用
        /// </summary>
        public string IsCardUsed { get; set; }
        /// <summary>
        /// 发卡序号
        /// </summary>
        public int PersonIndex { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartName { get; set; }
        /// <summary>
        /// 岗位类型
        /// </summary>
        public string PostType { get; set; }
        /// <summary>
        /// 是否安全人员
        /// </summary>
        public bool? IsHsse { get; set; }
        /// <summary>
        /// 培训次数
        /// </summary>
        public int TrainCount { get; set; }
    }
}
