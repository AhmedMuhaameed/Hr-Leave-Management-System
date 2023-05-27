using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Identity.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "c8d67349-d81f-419a-8ffd-26b6c7cf5636",
                    UserId = "4fde68ce-20d0-4680-a322-6da4cd06716e",
                }, new IdentityUserRole<string>
                {
                    RoleId = "7f8a1890-2b01-4111-bfda-bed5a3f55668",
                    UserId = "be9794bd-a828-431c-a05a-213442f74c7a",
                });
        }
    }
}
