using System.Threading;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public partial class DBContext : DbContext
{
    public DBContext()
    {
    }
    public DBContext(DbContextOptions<DBContext> options): base(options) { }

    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<SupportAgent> SupportAgents { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<ActionLog> ActionLog { get; set; }

    public override int SaveChanges()
    {

        if (ChangeTracker.HasChanges())
        {
            var res = base.SaveChanges();
            return res;

        }
        else
            return 2000;
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {

        if (ChangeTracker.HasChanges())
        {
            var res = base.SaveChanges(acceptAllChangesOnSuccess);
            return res;

        }
        else
            return 2000;
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        if (ChangeTracker.HasChanges())
        {
            var res = base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            //_logger.AddMiddleLog("SaveChanges(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)", res.Result.ToString());
            return res;
        }
        else
            return Task.FromResult(2000);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        if (ChangeTracker.HasChanges())
        {
            var res = base.SaveChangesAsync(cancellationToken);
            //_logger.AddMiddleLog("SaveChanges(CancellationToken cancellationToken = default)", res.Result.ToString());
            return res;

        }
        else
            return Task.FromResult(2000);
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
        OnModelCreatingPartial(modelBuilder);

        // مثال: تعریف رابطه بین Ticket و Customer
        modelBuilder.Entity<Ticket>().HasOne(t => t.User).WithMany(u => u.Tickets).HasForeignKey(t => t.UserId).OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Ticket>().HasOne(t => t.SupportAgent).WithMany(sa => sa.AssignedTickets).HasForeignKey(t => t.SupportAgentId).OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Ticket>().HasOne(t => t.Department).WithMany(d => d.Tickets).HasForeignKey(t => t.DepartmentId).OnDelete(DeleteBehavior.Restrict);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
