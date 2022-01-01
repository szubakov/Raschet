using System.Threading.Tasks;
using _2021_ZubakovSemon.Models;
using Microsoft.AspNetCore.Identity;

namespace _2021_ZubakovSemon.Initializer
{
	public class RoleInitializer
	{
		public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
		{
            #region ��������� ������
            if (await roleManager.FindByNameAsync("Admin") == null)	// ���������, ��������� �� ���� Admin
			{
				await roleManager.CreateAsync(new IdentityRole() { Name = "Admin", NormalizedName = "Admin" });
			}

			if (await userManager.FindByNameAsync("root@mail.ru") == null)	// ������ ��������� ���������� �� ����� ������������
			{
				User admin = new ()
				{
					Email = "root@mail.ru",
					UserName = "root@mail.ru",
					EmailConfirmed = false,
					PhoneNumberConfirmed = false,
					TwoFactorEnabled = false,
					AccessFailedCount = 0,
					LockoutEnabled = false,
					PhoneNumber = "+79212000000"
				};
				IdentityResult result = await userManager.CreateAsync(admin, "1234567890");
				if (result.Succeeded)
				{
					await userManager.AddToRoleAsync(admin, "Admin");
				}
			}
			#endregion
			#region  ��������� ������������
			if (await roleManager.FindByNameAsync("User") == null) // ���������, ��������� �� ���� Admin
			{
				await roleManager.CreateAsync(new IdentityRole() { Name = "User", NormalizedName = "User" });
			}

			if (await userManager.FindByNameAsync("user@mail.ru") == null)  // ������ ��������� ���������� �� ����� ������������
			{
				User user = new()
				{
					Email = "user@mail.ru",
					UserName = "user@mail.ru",
					EmailConfirmed = false,
					PhoneNumberConfirmed = false,
					TwoFactorEnabled = false,
					AccessFailedCount = 0,
					LockoutEnabled = false,
					PhoneNumber = "+79212000000"
				};
				IdentityResult result = await userManager.CreateAsync(user, "1234567890");
				if (result.Succeeded)
				{
					await userManager.AddToRoleAsync(user, "User");
				}
			}
			#endregion
		}
	}
}
