using System;
using System.Linq;
using DorllyService.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DorllyService.Domain
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider provider)
        {
            using var context = new DorllyServiceManagerContext(
                provider.GetRequiredService<DbContextOptions<DorllyServiceManagerContext>>());

            context.Database.EnsureCreated();

            if (context.User.Any())
            {
                return;   //数据已经初始化
            }

            //以下数据初始化顺序不能变，因为存在外键约束
            //也不能最后统一调用SaveChanges，还是因为存在外键约束
            //初始化园区列表
            var gardens = new Garden[]
            {
                new Garden { Name = "福田园区", Address = "深圳市福田区", State = 1 },
                new Garden { Name = "广州园区", Address = "广州市天河区", State = 1 }
            };
            context.Garden.AddRange(gardens);
            context.SaveChanges();

            //初始化用户列表
            var salt = Utils.GetCheckCode(8);
            var users = new User[]
            {
               new User
               {
                   Name="Carson",
                   Account="Alexander",
                   CreateDate=DateTime.Parse("2005-09-01"),
                   Password=DesEncrypt.Encrypt("888888",salt),
                   Birthday=DateTime.Parse("1988-10-17"),
                   Sex=1,
                   BelongGardenId=null,
                   ContactPhone="13751045901",
                   LastLoginTime=default,
                   Email="354658353@qq.com",
                   WorkTel="",
                   Avatar=null,
                   Salt=salt,
                   UserType=1,
                   State=1
               }
            };
            context.User.AddRange(users);
            context.SaveChanges();

            //初始化子系统列表
            var systems = new SubSystem[]
            {
                new SubSystem{ Name="服务商系统管理端", Code="SSM", Description="服务商系统的后台管理端，供集团和园区工作人员管理服务商系统。", State=1},
                new SubSystem{ Name="服务商系统供应商端", Code="SSS", Description="服务商系统的供应商管理端，供供应商使用系统。", State=1}
            };
            context.SubSystem.AddRange(systems);
            context.SaveChanges();

            //初始化模块列表
            context.Module.Add(new Module
            {
                Name = "系统管理",
                BelongSystemId = context.SubSystem.Single(s => s.Code == "SSM").Id,
                Level = 1,
                Status = true,
                Type = 1,
                ParentId = null,
                Order = 9
            });
            context.SaveChanges();
            context.Module.Add(new Module
            {
                Name = "菜单管理",
                BelongSystemId = context.SubSystem.Single(s => s.Code == "SSM").Id,
                Level = 2,
                Status = true,
                Type = 2,
                ParentId = context.Module.Single(m => m.Name == "系统管理").Id,
                Order = 4
            });
            context.SaveChanges();

            //初始化角色列表
            var roles = new Role[]
            {
                new Role { Code="DorllyAdmin", Name="多丽后台管理员", Description="拥有全部权限的多丽后台管理员", Status=true},
            };
            context.Role.AddRange(roles);
            context.SaveChanges();

            //初始化用户角色
            var userRoles = new UserRole[]
            {
                new UserRole{UserId=context.User.Single(u => u.Account == "Alexander").Id,RoleId=context.Role.Single(r => r.Code == "DorllyAdmin").Id }
            };
            context.UserRole.AddRange(userRoles);
            context.SaveChanges();

            //初始化权限列表
            var permissions =new Permission[]
            {
                new Permission { Name = "菜单管理-主页", Code = "Module-Index", BelongModuleId = context.Module.Single(m => m.Name == "菜单管理").Id, Status = true, Type = 1, Path = "/Module/Index" },
                new Permission { Name = "菜单管理-详情", Code = "Module-Details", BelongModuleId = context.Module.Single(m => m.Name == "菜单管理").Id, Status = true, Type = 1, Path = "/Module/Details" },
                new Permission { Name = "菜单管理-编辑", Code = "Module-Edit", BelongModuleId = context.Module.Single(m => m.Name == "菜单管理").Id, Status = true, Type = 1, Path = "/Module/Edit" }

            };
            context.Permission.AddRange(permissions);
            context.SaveChanges();

            var rolePermissions = new RolePermission[]
            {
                new RolePermission { RoleId = context.Role.Single(r=>r.Code=="DorllyAdmin").Id, PermissionId = context.Permission.Single(p=>p.Code=="Module-Index").Id },
                new RolePermission { RoleId = context.Role.Single(r=>r.Code=="DorllyAdmin").Id, PermissionId = context.Permission.Single(p=>p.Code=="Module-Details").Id },
                new RolePermission { RoleId = context.Role.Single(r=>r.Code=="DorllyAdmin").Id, PermissionId = context.Permission.Single(p=>p.Code=="Module-Edit").Id }
            };
            context.RolePermission.AddRange(rolePermissions);
            context.SaveChanges();
        }
    }
}