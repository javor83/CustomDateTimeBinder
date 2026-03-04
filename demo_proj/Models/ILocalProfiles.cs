using Microsoft.AspNetCore.Identity;

namespace demo_proj.Models
{
    public interface ILocalProfiles
    {
        IQueryable<IdentityUser> Get();

        Task<IdentityResult> Create(DTO_LocalProfile detail);

        Task<IdentityResult> Update(DTO_LocalProfile detail);

        Task<IdentityResult> Delete(string id);

        bool IsLogged();


        Task AssignToRole(string user_id, string role_name);

        Task DeleteFromRole(string user_id, string role_name);

        Task<bool> CheckInRole(string user_id, string role_name);

        IQueryable<IdentityRole> GetRoles();

        Task CreateRole(string role_name);



        Task<IdentityRole> FindRoleByName(string rolename);


        Task DeleteRole(string id);


        Task<string> infoRoles();

    }
}
