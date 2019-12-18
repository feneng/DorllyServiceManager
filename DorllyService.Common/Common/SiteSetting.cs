using System;
namespace DorllyService.Common
{
    public class SiteSetting
    {
        public string ConfigPath { get; set; }
        public string ApplicationName { get; set; }
        public string Version { get; set; }
        public string DbBackupPath { get; set; }
        public string JumpPage { get; set; }
        public string SsoServer { get; set; }
    }
}
