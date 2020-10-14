using System;
using System.Linq;
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
            var salt = "4Z8P8TP2";//Utils.GetCheckCode(8);
            var users = new User[]
            {
               new User
               {
                   Name="Carson",
                   Account="Admin",
                   CreateDate=DateTime.Parse("2005-09-01"),
                   Password="CE81EE216D5E78D9",//DesEncrypt.Encrypt("888888",salt),
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
                Icon = "glyphicon glyphicon-cog",
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
                Path = "/Admin/Module/Index",
                Status = true,
                Type = 2,
                ParentId = context.Module.Single(m => m.Name == "系统管理").Id,
                Order = 4
            });
            context.SaveChanges();
            context.Module.Add(new Module
            {
                Name = "用户管理",
                BelongSystemId = context.SubSystem.Single(s => s.Code == "SSM").Id,
                Level = 2,
                Path = "/Admin/User/Index",
                Status = true,
                Type = 2,
                ParentId = context.Module.Single(m => m.Name == "系统管理").Id,
                Order = 4
            });
            context.SaveChanges();
            context.Module.Add(new Module
            {
                Name = "权限管理",
                BelongSystemId = context.SubSystem.Single(s => s.Code == "SSM").Id,
                Level = 2,
                Path = "/Admin/Permission/Index",
                Status = true,
                Type = 2,
                ParentId = context.Module.Single(m => m.Name == "系统管理").Id,
                Order = 4
            });
            context.SaveChanges();
            context.Module.Add(new Module
            {
                Name = "角色管理",
                BelongSystemId = context.SubSystem.Single(s => s.Code == "SSM").Id,
                Level = 2,
                Path = "/Admin/Role/Index",
                Status = true,
                Type = 2,
                ParentId = context.Module.Single(m => m.Name == "系统管理").Id,
                Order = 4
            });
            context.SaveChanges();
            context.Module.Add(new Module
            {
                Name = "子系统管理",
                BelongSystemId = context.SubSystem.Single(s => s.Code == "SSM").Id,
                Level = 2,
                Path = "/Admin/SubSystem/Index",
                Status = true,
                Type = 2,
                ParentId = context.Module.Single(m => m.Name == "系统管理").Id,
                Order = 4
            });
            context.SaveChanges();
            context.Module.Add(new Module
            {
                Name = "基础资料管理",
                BelongSystemId = context.SubSystem.Single(s => s.Code == "SSM").Id,
                Level = 1,
                Status = true,
                Icon = "glyphicon glyphicon-list-alt",
                Type = 1,
                ParentId = null,
                Order = 8
            });
            context.SaveChanges();
            context.Module.Add(new Module
            {
                Name = "园区管理",
                BelongSystemId = context.SubSystem.Single(s => s.Code == "SSM").Id,
                Level = 2,
                Path = "/Admin/Garden/Index",
                Status = true,
                Type = 2,
                ParentId = context.Module.Single(m => m.Name == "基础资料管理").Id,
                Order = 4
            });
            context.SaveChanges();
            context.Module.Add(new Module
            {
                Name = "服务属性管理",
                BelongSystemId = context.SubSystem.Single(s => s.Code == "SSM").Id,
                Level = 2,
                Path = "/Admin/ServiceProperty/Index",
                Status = true,
                Type = 2,
                ParentId = context.Module.Single(m => m.Name == "基础资料管理").Id,
                Order = 4
            });
            context.SaveChanges();
            context.Module.Add(new Module
            {
                Name = "服务分类管理",
                BelongSystemId = context.SubSystem.Single(s => s.Code == "SSM").Id,
                Level = 2,
                Path = "/Admin/ServiceCategory/Index",
                Status = true,
                Type = 2,
                ParentId = context.Module.Single(m => m.Name == "基础资料管理").Id,
                Order = 4
            });
            context.SaveChanges();
            context.Module.Add(new Module
            {
                Name = "服务等级管理",
                BelongSystemId = context.SubSystem.Single(s => s.Code == "SSM").Id,
                Level = 2,
                Path = "/Admin/ServiceLevel/Index",
                Status = true,
                Type = 2,
                ParentId = context.Module.Single(m => m.Name == "基础资料管理").Id,
                Order = 4
            });
            context.SaveChanges();
            context.Module.Add(new Module
            {
                Name = "服务订单管理",
                BelongSystemId = context.SubSystem.Single(s => s.Code == "SSM").Id,
                Level = 1,
                Status = true,
                Icon = "glyphicon glyphicon-th-list",
                Type = 1,
                ParentId = null,
                Order = 7
            });
            context.SaveChanges();
            context.Module.Add(new Module
            {
                Name = "订单管理",
                BelongSystemId = context.SubSystem.Single(s => s.Code == "SSM").Id,
                Level = 2,
                Path = "/Admin/ServiceOrder/Index",
                Status = true,
                Type = 2,
                ParentId = context.Module.Single(m => m.Name == "服务订单管理").Id,
                Order = 4
            });
            context.SaveChanges();
            context.Module.Add(new Module
            {
                Name = "服务管理",
                BelongSystemId = context.SubSystem.Single(s => s.Code == "SSM").Id,
                Level = 1,
                Status = true,
                Icon = "glyphicon glyphicon-leaf",
                Type = 1,
                ParentId = null,
                Order = 6
            });
            context.SaveChanges();
            context.Module.Add(new Module
            {
                Name = "服务信息管理",
                BelongSystemId = context.SubSystem.Single(s => s.Code == "SSM").Id,
                Level = 2,
                Path = "/Admin/ServiceInfo/Index",
                Status = true,
                Type = 2,
                ParentId = context.Module.Single(m => m.Name == "服务管理").Id,
                Order = 4
            });
            context.SaveChanges();
            context.Module.Add(new Module
            {
                Name = "服务商管理",
                BelongSystemId = context.SubSystem.Single(s => s.Code == "SSM").Id,
                Level = 1,
                Status = true,
                Icon = "glyphicon glyphicon-user",
                Type = 1,
                ParentId = null,
                Order = 5
            });
            context.SaveChanges();
            context.Module.Add(new Module
            {
                Name = "服务商信息管理",
                BelongSystemId = context.SubSystem.Single(s => s.Code == "SSM").Id,
                Level = 2,
                Path = "/Admin/ServiceProvider/Index",
                Status = true,
                Type = 2,
                ParentId = context.Module.Single(m => m.Name == "服务商管理").Id,
                Order = 4
            });
            context.SaveChanges();
            context.Module.Add(new Module
            {
                Name = "合同管理",
                BelongSystemId = context.SubSystem.Single(s => s.Code == "SSM").Id,
                Level = 2,
                Path = "/Admin/Contract/Index",
                Status = true,
                Type = 2,
                ParentId = context.Module.Single(m => m.Name == "服务商管理").Id,
                Order = 3
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
                new UserRole{UserId=context.User.Single(u => u.Account == "Admin").Id,RoleId=context.Role.Single(r => r.Code == "DorllyAdmin").Id }
            };
            context.UserRole.AddRange(userRoles);
            context.SaveChanges();

            //初始化权限列表
            var permissions = new Permission[]
            {
                new Permission { Name = "菜单管理-主页", Code = "Module-Index", BelongModuleId = context.Module.Single(m => m.Name == "菜单管理").Id, Status = true, Type = 1, Path = "/Module/Index" },
                new Permission { Name = "菜单管理-详情", Code = "Module-Details", BelongModuleId = context.Module.Single(m => m.Name == "菜单管理").Id, Status = true, Type = 1, Path = "/Module/Details" },
                new Permission { Name = "菜单管理-编辑", Code = "Module-Edit", BelongModuleId = context.Module.Single(m => m.Name == "菜单管理").Id, Status = true, Type = 1, Path = "/Module/Edit" },
                new Permission { Name = "用户管理-主页", Code = "User-Index", BelongModuleId = context.Module.Single(m => m.Name == "用户管理").Id, Status = true, Type = 1, Path = "/User/Index" },
                new Permission { Name = "用户管理-详情", Code = "User-Details", BelongModuleId = context.Module.Single(m => m.Name == "用户管理").Id, Status = true, Type = 1, Path = "/User/Details" },
                new Permission { Name = "用户管理-编辑", Code = "User-Edit", BelongModuleId = context.Module.Single(m => m.Name == "用户管理").Id, Status = true, Type = 1, Path = "/User/Edit" },
                new Permission { Name = "权限管理-主页", Code = "Permission-Index", BelongModuleId = context.Module.Single(m => m.Name == "权限管理").Id, Status = true, Type = 1, Path = "/Permission/Index" },
                new Permission { Name = "权限管理-详情", Code = "Permission-Details", BelongModuleId = context.Module.Single(m => m.Name == "权限管理").Id, Status = true, Type = 1, Path = "/Permission/Details" },
                new Permission { Name = "权限管理-编辑", Code = "Permission-Edit", BelongModuleId = context.Module.Single(m => m.Name == "权限管理").Id, Status = true, Type = 1, Path = "/Permission/Edit" },
                new Permission { Name = "角色管理-主页", Code = "Role-Index", BelongModuleId = context.Module.Single(m => m.Name == "角色管理").Id, Status = true, Type = 1, Path = "/Role/Index" },
                new Permission { Name = "角色管理-详情", Code = "Role-Details", BelongModuleId = context.Module.Single(m => m.Name == "角色管理").Id, Status = true, Type = 1, Path = "/Role/Details" },
                new Permission { Name = "角色管理-编辑", Code = "Role-Edit", BelongModuleId = context.Module.Single(m => m.Name == "角色管理").Id, Status = true, Type = 1, Path = "/Role/Edit" },
                new Permission { Name = "园区管理-主页", Code = "Garden-Index", BelongModuleId = context.Module.Single(m => m.Name == "园区管理").Id, Status = true, Type = 1, Path = "/Garden/Index" },
                new Permission { Name = "园区管理-详情", Code = "Garden-Details", BelongModuleId = context.Module.Single(m => m.Name == "园区管理").Id, Status = true, Type = 1, Path = "/Garden/Details" },
                new Permission { Name = "园区管理-编辑", Code = "Garden-Edit", BelongModuleId = context.Module.Single(m => m.Name == "园区管理").Id, Status = true, Type = 1, Path = "/Garden/Edit" },
                new Permission { Name = "服务属性管理-主页", Code = "ServiceProperty-Index", BelongModuleId = context.Module.Single(m => m.Name == "服务属性管理").Id, Status = true, Type = 1, Path = "/ServiceProperty/Index" },
                new Permission { Name = "服务属性管理-详情", Code = "ServiceProperty-Details", BelongModuleId = context.Module.Single(m => m.Name == "服务属性管理").Id, Status = true, Type = 1, Path = "/ServiceProperty/Details" },
                new Permission { Name = "服务属性管理-编辑", Code = "ServiceProperty-Edit", BelongModuleId = context.Module.Single(m => m.Name == "服务属性管理").Id, Status = true, Type = 1, Path = "/ServiceProperty/Edit" },
                new Permission { Name = "服务分类管理-主页", Code = "ServiceCategory-Index", BelongModuleId = context.Module.Single(m => m.Name == "服务分类管理").Id, Status = true, Type = 1, Path = "/ServiceCategory/Index" },
                new Permission { Name = "服务分类管理-详情", Code = "ServiceCategory-Details", BelongModuleId = context.Module.Single(m => m.Name == "服务分类管理").Id, Status = true, Type = 1, Path = "/ServiceCategory/Details" },
                new Permission { Name = "服务分类管理-编辑", Code = "ServiceCategory-Edit", BelongModuleId = context.Module.Single(m => m.Name == "服务分类管理").Id, Status = true, Type = 1, Path = "/ServiceCategory/Edit" },
                new Permission { Name = "服务等级管理-主页", Code = "ServiceLevel-Index", BelongModuleId = context.Module.Single(m => m.Name == "服务等级管理").Id, Status = true, Type = 1, Path = "/ServiceLevel/Index" },
                new Permission { Name = "服务等级管理-详情", Code = "ServiceLevel-Details", BelongModuleId = context.Module.Single(m => m.Name == "服务等级管理").Id, Status = true, Type = 1, Path = "/ServiceLevel/Details" },
                new Permission { Name = "服务等级管理-编辑", Code = "ServiceLevel-Edit", BelongModuleId = context.Module.Single(m => m.Name == "服务等级管理").Id, Status = true, Type = 1, Path = "/ServiceLevel/Edit" },
                new Permission { Name = "订单管理-主页", Code = "ServiceOrder-Index", BelongModuleId = context.Module.Single(m => m.Name == "订单管理").Id, Status = true, Type = 1, Path = "/ServiceOrder/Index" },
                new Permission { Name = "订单管理-详情", Code = "ServiceOrder-Details", BelongModuleId = context.Module.Single(m => m.Name == "订单管理").Id, Status = true, Type = 1, Path = "/ServiceOrder/Details" },
                new Permission { Name = "订单管理-编辑", Code = "ServiceOrder-Edit", BelongModuleId = context.Module.Single(m => m.Name == "订单管理").Id, Status = true, Type = 1, Path = "/ServiceOrder/Edit" },
                new Permission { Name = "服务信息管理-主页", Code = "ServiceInfo-Index", BelongModuleId = context.Module.Single(m => m.Name == "服务信息管理").Id, Status = true, Type = 1, Path = "/ServiceInfo/Index" },
                new Permission { Name = "服务信息管理-详情", Code = "ServiceInfo-Details", BelongModuleId = context.Module.Single(m => m.Name == "服务信息管理").Id, Status = true, Type = 1, Path = "/ServiceInfo/Details" },
                new Permission { Name = "服务信息管理-编辑", Code = "ServiceInfo-Edit", BelongModuleId = context.Module.Single(m => m.Name == "服务信息管理").Id, Status = true, Type = 1, Path = "/ServiceInfo/Edit" },
                new Permission { Name = "服务商信息管理-主页", Code = "ServiceProvider-Index", BelongModuleId = context.Module.Single(m => m.Name == "服务商信息管理").Id, Status = true, Type = 1, Path = "/ServiceProvider/Index" },
                new Permission { Name = "服务商信息管理-详情", Code = "ServiceProvider-Details", BelongModuleId = context.Module.Single(m => m.Name == "服务商信息管理").Id, Status = true, Type = 1, Path = "/ServiceProvider/Details" },
                new Permission { Name = "服务商信息管理-编辑", Code = "ServiceProvider-Edit", BelongModuleId = context.Module.Single(m => m.Name == "服务商信息管理").Id, Status = true, Type = 1, Path = "/ServiceProvider/Edit" },
                new Permission { Name = "合同管理-主页", Code = "Contract-Index", BelongModuleId = context.Module.Single(m => m.Name == "合同管理").Id, Status = true, Type = 1, Path = "/Contract/Index" },
                new Permission { Name = "合同管理-详情", Code = "Contract-Details", BelongModuleId = context.Module.Single(m => m.Name == "合同管理").Id, Status = true, Type = 1, Path = "/Contract/Details" },
                new Permission { Name = "合同管理-编辑", Code = "Contract-Edit", BelongModuleId = context.Module.Single(m => m.Name == "合同管理").Id, Status = true, Type = 1, Path = "/Contract/Edit" },
            };
            context.Permission.AddRange(permissions);
            context.SaveChanges();

            var rolePermissions = new RolePermission[]
            {
                new RolePermission { RoleId = context.Role.Single(r=>r.Code=="DorllyAdmin").Id, PermissionId = context.Permission.Single(p=>p.Code=="Module-Index").Id },
                new RolePermission { RoleId = context.Role.Single(r=>r.Code=="DorllyAdmin").Id, PermissionId = context.Permission.Single(p=>p.Code=="Module-Details").Id },
                new RolePermission { RoleId = context.Role.Single(r=>r.Code=="DorllyAdmin").Id, PermissionId = context.Permission.Single(p=>p.Code=="Module-Edit").Id },
                new RolePermission { RoleId = context.Role.Single(r=>r.Code=="DorllyAdmin").Id, PermissionId = context.Permission.Single(p=>p.Code=="User-Index").Id },
                new RolePermission { RoleId = context.Role.Single(r=>r.Code=="DorllyAdmin").Id, PermissionId = context.Permission.Single(p=>p.Code=="User-Details").Id },
                new RolePermission { RoleId = context.Role.Single(r=>r.Code=="DorllyAdmin").Id, PermissionId = context.Permission.Single(p=>p.Code=="User-Edit").Id },
                new RolePermission { RoleId = context.Role.Single(r=>r.Code=="DorllyAdmin").Id, PermissionId = context.Permission.Single(p=>p.Code=="Role-Index").Id },
                new RolePermission { RoleId = context.Role.Single(r=>r.Code=="DorllyAdmin").Id, PermissionId = context.Permission.Single(p=>p.Code=="Role-Details").Id },
                new RolePermission { RoleId = context.Role.Single(r=>r.Code=="DorllyAdmin").Id, PermissionId = context.Permission.Single(p=>p.Code=="Role-Edit").Id },
                new RolePermission { RoleId = context.Role.Single(r=>r.Code=="DorllyAdmin").Id, PermissionId = context.Permission.Single(p=>p.Code=="Permission-Index").Id },
                new RolePermission { RoleId = context.Role.Single(r=>r.Code=="DorllyAdmin").Id, PermissionId = context.Permission.Single(p=>p.Code=="Permission-Details").Id },
                new RolePermission { RoleId = context.Role.Single(r=>r.Code=="DorllyAdmin").Id, PermissionId = context.Permission.Single(p=>p.Code=="Permission-Edit").Id },
                new RolePermission { RoleId = context.Role.Single(r=>r.Code=="DorllyAdmin").Id, PermissionId = context.Permission.Single(p=>p.Code=="Garden-Index").Id },
                new RolePermission { RoleId = context.Role.Single(r=>r.Code=="DorllyAdmin").Id, PermissionId = context.Permission.Single(p=>p.Code=="Garden-Details").Id },
                new RolePermission { RoleId = context.Role.Single(r=>r.Code=="DorllyAdmin").Id, PermissionId = context.Permission.Single(p=>p.Code=="Garden-Edit").Id },
                new RolePermission { RoleId = context.Role.Single(r=>r.Code=="DorllyAdmin").Id, PermissionId = context.Permission.Single(p=>p.Code=="ServiceProperty-Index").Id },
                new RolePermission { RoleId = context.Role.Single(r=>r.Code=="DorllyAdmin").Id, PermissionId = context.Permission.Single(p=>p.Code=="ServiceProperty-Details").Id },
                new RolePermission { RoleId = context.Role.Single(r=>r.Code=="DorllyAdmin").Id, PermissionId = context.Permission.Single(p=>p.Code=="ServiceProperty-Edit").Id },
                new RolePermission { RoleId = context.Role.Single(r=>r.Code=="DorllyAdmin").Id, PermissionId = context.Permission.Single(p=>p.Code=="ServiceCategory-Index").Id },
                new RolePermission { RoleId = context.Role.Single(r=>r.Code=="DorllyAdmin").Id, PermissionId = context.Permission.Single(p=>p.Code=="ServiceCategory-Details").Id },
                new RolePermission { RoleId = context.Role.Single(r=>r.Code=="DorllyAdmin").Id, PermissionId = context.Permission.Single(p=>p.Code=="ServiceCategory-Edit").Id },
                new RolePermission { RoleId = context.Role.Single(r=>r.Code=="DorllyAdmin").Id, PermissionId = context.Permission.Single(p=>p.Code=="ServiceLevel-Index").Id },
                new RolePermission { RoleId = context.Role.Single(r=>r.Code=="DorllyAdmin").Id, PermissionId = context.Permission.Single(p=>p.Code=="ServiceLevel-Details").Id },
                new RolePermission { RoleId = context.Role.Single(r=>r.Code=="DorllyAdmin").Id, PermissionId = context.Permission.Single(p=>p.Code=="ServiceLevel-Edit").Id },
                new RolePermission { RoleId = context.Role.Single(r=>r.Code=="DorllyAdmin").Id, PermissionId = context.Permission.Single(p=>p.Code=="ServiceOrder-Index").Id },
                new RolePermission { RoleId = context.Role.Single(r=>r.Code=="DorllyAdmin").Id, PermissionId = context.Permission.Single(p=>p.Code=="ServiceOrder-Details").Id },
                new RolePermission { RoleId = context.Role.Single(r=>r.Code=="DorllyAdmin").Id, PermissionId = context.Permission.Single(p=>p.Code=="ServiceOrder-Edit").Id },
                new RolePermission { RoleId = context.Role.Single(r=>r.Code=="DorllyAdmin").Id, PermissionId = context.Permission.Single(p=>p.Code=="ServiceInfo-Index").Id },
                new RolePermission { RoleId = context.Role.Single(r=>r.Code=="DorllyAdmin").Id, PermissionId = context.Permission.Single(p=>p.Code=="ServiceInfo-Details").Id },
                new RolePermission { RoleId = context.Role.Single(r=>r.Code=="DorllyAdmin").Id, PermissionId = context.Permission.Single(p=>p.Code=="ServiceInfo-Edit").Id },
                new RolePermission { RoleId = context.Role.Single(r=>r.Code=="DorllyAdmin").Id, PermissionId = context.Permission.Single(p=>p.Code=="ServiceProvider-Index").Id },
                new RolePermission { RoleId = context.Role.Single(r=>r.Code=="DorllyAdmin").Id, PermissionId = context.Permission.Single(p=>p.Code=="ServiceProvider-Details").Id },
                new RolePermission { RoleId = context.Role.Single(r=>r.Code=="DorllyAdmin").Id, PermissionId = context.Permission.Single(p=>p.Code=="ServiceProvider-Edit").Id },
                new RolePermission { RoleId = context.Role.Single(r=>r.Code=="DorllyAdmin").Id, PermissionId = context.Permission.Single(p=>p.Code=="Contract-Index").Id },
                new RolePermission { RoleId = context.Role.Single(r=>r.Code=="DorllyAdmin").Id, PermissionId = context.Permission.Single(p=>p.Code=="Contract-Details").Id },
                new RolePermission { RoleId = context.Role.Single(r=>r.Code=="DorllyAdmin").Id, PermissionId = context.Permission.Single(p=>p.Code=="Contract-Edit").Id },
            };
            context.RolePermission.AddRange(rolePermissions);
            context.SaveChanges();
        }
    }
}