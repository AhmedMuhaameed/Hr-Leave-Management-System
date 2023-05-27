using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Identity.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "7f8a1890-2b01-4111-bfda-bed5a3f55668",
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE",
                }, new IdentityRole
                {

                    Id = "c8d67349-d81f-419a-8ffd-26b6c7cf5636",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                });
        }
    }
}
