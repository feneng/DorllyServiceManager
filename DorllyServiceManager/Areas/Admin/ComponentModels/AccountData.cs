using System;
using System.Collections;

namespace DorllyServiceManager.Areas.Admin.ComponentModels
{
    public class AccountData
    {
        public AccountData()
        {
        }

        //IEnumerable<Message>
        //IEnumerable<Notification>
        //IEnumerable<Task>

        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Avatar { get; set; }
    }
}
