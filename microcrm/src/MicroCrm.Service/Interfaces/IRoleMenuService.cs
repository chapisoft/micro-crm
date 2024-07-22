using System;
using System.Data;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MicroCrm.Domain.Models;
using URF.Core.Abstractions.Services;
using System.Collections.Generic;
using MicroCrm.Dto;

namespace MicroCrm.Service
{
  public interface IRoleMenuService : IService<RoleMenu>
  {

    Task<IEnumerable<RoleMenu>> GetByMenuIdAsync(int menuid);

    Task<IEnumerable<RoleMenu>> GetByRoleNameAsync(string roleName);
    Task AuthorizeAsync(RoleMenusView[] list);

    Task<IEnumerable<MenuItem>> RenderMenus(string[] roleNames);

    Task<IEnumerable<ListItem>> NavDataSource(string[] roles);


  }
}
