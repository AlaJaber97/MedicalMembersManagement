using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace API.Data
{
    public class AppDbContext : IdentityDbContext<BLL.Modals.Identity.User,BLL.Modals.Identity.Role,int>
    {
        public DbSet<BLL.Modals.Member> Members { get; set; }
        public DbSet<BLL.Modals.Logs> Logs { get; set; }
        public DbSet<BLL.Modals.LogTransaction> LogTransactions { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public AppDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<BLL.Modals.Member>().HasKey(member => member.ID);
            builder.Entity<BLL.Modals.Member>().HasIndex(member => member.NotionalID).IsUnique();
        }
        public override EntityEntry<TEntity> Add<TEntity>(TEntity entity)
        {
            if(entity is BLL.Modals.Member Member)
            {
                this.LogTransactions.Add(new BLL.Modals.LogTransaction
                {
                    Action = $"add new member: ({Member.FullName},{Member.NotionalID})",
                    ActionAt = DateTime.UtcNow
                });
            }
            return base.Add(entity);    
        }
        public override EntityEntry<TEntity> Update<TEntity>(TEntity entity)
        {
            if (entity is BLL.Modals.Member Member)
            {
                this.LogTransactions.Add(new BLL.Modals.LogTransaction
                {
                    Action = $"update member: ({Member.FullName},{Member.NotionalID})",
                    ActionAt = DateTime.UtcNow
                });
            }
            return base.Update(entity);
        }
        public override EntityEntry<TEntity> Remove<TEntity>(TEntity entity)
        {
            if (entity is BLL.Modals.Member Member)
            {
                this.LogTransactions.Add(new BLL.Modals.LogTransaction
                {
                    Action = $"delete member: ({Member.FullName},{Member.NotionalID})",
                    ActionAt = DateTime.UtcNow
                });
            }
            return base.Remove(entity);
        }
    }
}
