using Microsoft.EntityFrameworkCore;
using PaymentAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;    


namespace PaymentAPI.Data
{
    public class ApiDbContext : IdentityDbContext
    {
        public DbSet<PaymentData> Payment {get;set;}

        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }
    }
}