using System;
namespace DorllyService.Common
{
    /// <summary>
    ///  类，属性名称必须与appsettings.json的文件名字一致
    /// </summary>
    public class ConnectionService
    {
        /// <summary>
        /// SqlService连接字符串
        /// </summary>
        public string SqlServiceConnection { get; set; }
        public string TheOtherSqlServiceConnection { get; set; }
        /// <summary>
        /// Redis连接字符串
        /// </summary>
        public string RedisConnection { get; set; }

    }
}
