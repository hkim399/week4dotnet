using HealthAPI.Models;
using Microsoft.EntityFrameworkCore;

public class HealthContext : DbContext {
    // this constructor is important
    // will be used in program.cs
    public HealthContext(DbContextOptions options) : base(options) { }
    

    // when you create it, it sets the limit, ocnstraint
    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);

        builder.Entity<Ailment>().Property(p => p.Name).HasMaxLength(40);
        builder.Entity<Medication>().Property(p => p.Name).HasMaxLength(40);
        builder.Entity<Patient>().Property(p => p.Name).HasMaxLength(40);

        // specific name for specific entity
        builder.Entity<Ailment>().ToTable("Ailment");
        builder.Entity<Medication>().ToTable("Medication");
        builder.Entity<Patient>().ToTable("Patient");

        // check if it is populated and if it is empty, populate it, else do nothing
        builder.Entity<Patient>().HasData(SampleData.GetPatients());
        builder.Entity<Medication>().HasData(SampleData.GetMedication());
        builder.Entity<Ailment>().HasData(SampleData.GetAilments());
    }


    public DbSet<Ailment>? Ailments { get; set; }
    public DbSet<Medication>? Medications { get; set; }
    public DbSet<Patient>? Patients { get; set; }
}
