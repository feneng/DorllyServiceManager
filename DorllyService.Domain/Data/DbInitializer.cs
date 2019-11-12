using System;
using System.Linq;

namespace DorllyService.Domain
{
    public class DbInitializer
    {
        public static void Initialize(DorllyServiceManagerContext context)
        {
            context.Database.EnsureCreated();
            if (context.User.Any())
            {
                return;   // DB has been seeded
            }

            //初始化用户列表
            var users = new User[]
            {
                new User{Name="Carson",Account="Alexander",CreateDate=DateTime.Parse("2005-09-01"),
                Password="888888",Birthday=DateTime.Parse("1988-10-17"),Sex=1,BelongGardenId=1,ContactPhone="13751045901",
                LastLoginTime=default,Email="354658353@qq.com",WorkTel="",Avatar=null,State=1}
            };
            foreach (User s in users)
            {
                context.User.Add(s);
            }

            //初始化子系统列表
            var systems = new SubSystem[] {
                new SubSystem{ Name="服务商系统管理端", Code="SSM", Description="服务商系统的后台管理端，供集团和园区工作人员管理服务商系统。", State=1},
                new SubSystem{ Name="服务商系统供应商端", Code="SSS", Description="服务商系统的供应商管理端，供供应商使用系统。", State=1}

            };
            foreach (var s in systems)
            {
                context.SubSystem.Add(s);
            }

            //初始化模块列表
            var modules = new Module[] { 
                new Module { Name="系统管理", BelongSystemId=1, Level =1, Status=true, Type=1,Order=9 },
                new Module { Name="菜单管理", BelongSystemId=1, Level =1, Status=true, Type=2,ParentId=1,Order=4 }
            };
            foreach (var s in modules)
            {
                context.Module.Add(s);
            }

            //初始化园区列表
            var gardens = new Garden[] {
                new Garden { Name="福田园区", Address="深圳市福田区", State=1},
                new Garden { Name="广州园区", Address="广州市天河区", State=1}
            };
            foreach (var s in gardens)
            {
                context.Garden.Add(s);
            }

            //初始化角色列表
            var roles = new Role[] {
                new Role { Code="DorllyAdmin", Name="多丽后台管理员", Description="拥有全部权限的多丽后台管理员", Status=true},
            };
            foreach (var s in roles)
            {
                context.Role.Add(s);
            }

            //初始化权限列表
            var permissions = new Permission[] {
                new Permission { Name="菜单管理-主页", Code="Module-Index", BelongModuleId=2, Status=true, Type=1,Path="/Module/Index" },
                new Permission { Name="菜单管理-详情", Code="Module-Details", BelongModuleId=2, Status=true, Type=1,Path="/Module/Details" },
                new Permission { Name="菜单管理-编辑", Code="Module-Edit", BelongModuleId=2, Status=true, Type=1,Path="/Module/Edit" },
            };
            foreach (var s in permissions)
            {
                context.Permission.Add(s);
            }


            context.SaveChanges();
        }
    }
}
