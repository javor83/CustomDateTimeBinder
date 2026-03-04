using Microsoft.AspNetCore.Identity;
using System.Text;

namespace demo_proj.Models
{
    /*
     * 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>
    (
    options => 
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequiredLength = 3;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireDigit = false;

    }
    ).AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();



builder.Services.AddScoped<ILocalProfiles, LocalProfiles>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication(); app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
     */


    public class LocalProfiles : ILocalProfiles
    {
        private UserManager<IdentityUser> _list = null;
        private RoleManager<IdentityRole> _roleManager = null;
        private readonly IHttpContextAccessor _httpContextAccessor;
        //********************************************************************************
        public LocalProfiles(UserManager<IdentityUser> list, RoleManager<IdentityRole> rm, IHttpContextAccessor httpContextAccessor)
        {
            this._list = list;
            this._roleManager = rm;
            this._httpContextAccessor = httpContextAccessor;
        }
        #region управление на ролите

        //*****************************************************************************************
        IQueryable<IdentityRole> ILocalProfiles.GetRoles()
        {
            return
                this._roleManager.Roles;
        }
        //*****************************************************************************************
        async Task ILocalProfiles.CreateRole(string role_name)
        {
            IdentityRole role = new IdentityRole()
            {
                Name = role_name
            };
            await this._roleManager.CreateAsync(role);
        }
        //*****************************************************************************************
        async Task<IdentityRole> ILocalProfiles.FindRoleByName(string rolename)
        {
            IdentityRole find = await this._roleManager.FindByNameAsync(rolename);
            return find;
        }
        //*****************************************************************************************

        async Task ILocalProfiles.DeleteRole(string id)
        {
            IdentityRole rl = await this._roleManager.FindByIdAsync(id);
            if (rl != null)
            {
                await this._roleManager.DeleteAsync(rl);
            }
        }
        #endregion

        //********************************************************************************
        async Task<string> ILocalProfiles.infoRoles()
        {
            StringBuilder sb = new StringBuilder();

            IdentityUser[] registerd = (this as ILocalProfiles).Get().ToArray();

            foreach (var k in registerd)
            {
                bool is_admin = await (this as ILocalProfiles).CheckInRole(k.Id, enum_AppRoles.Administrator.ToString());
                bool is_user = await (this as ILocalProfiles).CheckInRole(k.Id, enum_AppRoles.User.ToString());
                sb.Append($"User {k.Id} | {k.UserName} admin {is_admin} | User {is_user}").AppendLine();

            }


            return sb.ToString();
        }

        //********************************************************************************
        bool ILocalProfiles.IsLogged()
        {
            /*
             * admin123@abv.bg pascal
homeuser@abv.bg	pascal
             */
            bool result =
            this._httpContextAccessor.HttpContext.User.Identity != null
            &&
            this._httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
            return result;
        }
        //********************************************************************************
        IQueryable<IdentityUser> ILocalProfiles.Get()
        {
            return this._list.Users;
        }
        //********************************************************************************
        async Task<IdentityResult> ILocalProfiles.Create(DTO_LocalProfile detail)
        {
            var result = await this._list.CreateAsync
                 (
                     new IdentityUser()
                     {
                         UserName = detail.UserName,
                         Email = detail.Email
                     },
                     detail.Password
                 );
            return result;
        }
        //********************************************************************************
        async Task<IdentityResult> ILocalProfiles.Delete(string id)
        {
            IdentityResult result = null;
            IdentityUser tuser = await this._list.FindByIdAsync(id);
            if (tuser != null)
            {
                result = await this._list.DeleteAsync(tuser);
            }
            return result;
        }

        //********************************************************************************
        async Task<IdentityResult> ILocalProfiles.Update(DTO_LocalProfile detail)
        {
            IdentityResult result = null;
            IdentityUser find = await this._list.FindByIdAsync(detail.ID);
            if (find != null)
            {
                find.UserName = detail.UserName;
                find.Email = detail.Email;

                result = await this._list.UpdateAsync(find);

                result = await this._list.RemovePasswordAsync(find);
                result = await this._list.AddPasswordAsync(find, detail.Password);

            }

            return result;
        }
        //********************************************************************************

        async Task ILocalProfiles.AssignToRole(string user_id, string role_name)
        {
            IdentityUser local_user = await this._list.FindByIdAsync(user_id);
            if (local_user != null)
            {
                IdentityRole role = await (this as ILocalProfiles).FindRoleByName(role_name);
                if (role != null)
                {
                    await this._list.AddToRoleAsync(local_user, role_name);
                }
            }
        }
        //********************************************************************************

        async Task ILocalProfiles.DeleteFromRole(string user_id, string role_name)
        {
            IdentityUser local_user = await this._list.FindByIdAsync(user_id);
            if (local_user != null)
            {
                await this._list.RemoveFromRoleAsync(local_user, role_name);
            }
        }
        //********************************************************************************
        async Task<bool> ILocalProfiles.CheckInRole(string user_id, string role_name)
        {
            bool result = false;
            IdentityUser local_user = await this._list.FindByIdAsync(user_id);
            if (local_user != null)
            {
                result = await this._list.IsInRoleAsync(local_user, role_name);
            }
            return result;
        }
        //********************************************************************************

    }
}
