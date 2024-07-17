using FinBetApi.Infrastructure.DataAccess.SqlServer.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinBetApi.Infrastructure.DataAccess.SqlServer
{
    public class FinBetDbContext : DbContext
    {
        public FinBetDbContext(DbContextOptions<FinBetDbContext> options) : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientContact> ClientContacts { get; set; }
        public DbSet<DateItem> DateItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>()
                .Property(x => x.ClientName)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<ClientContact>()
                .Property(x => x.ContractType)
                .IsRequired()
                .HasMaxLength(255);
            modelBuilder.Entity<ClientContact>()
                .Property(x => x.ContractValue)
                .IsRequired()
                .HasMaxLength(255);
            modelBuilder.Entity<ClientContact>()
                .HasOne(x => x.Client)
                .WithMany(x => x.Contacts)
                .HasForeignKey(x => x.ClientId)
                .IsRequired();
            modelBuilder.Entity<Client>()
                .HasData(new Client()
                {
                    Id = 1,
                    ClientName = "client",
                });
            modelBuilder.Entity<ClientContact>()
                .HasData(
                    new ClientContact() { Id = 1, ClientId = 1, ContractType = "type1", ContractValue = "value" },
                    new ClientContact() { Id = 2, ClientId = 1, ContractType = "type2", ContractValue = "value" },
                    new ClientContact() { Id = 3, ClientId = 1, ContractType = "type3", ContractValue = "value" }
                );

            modelBuilder.Entity<DateItem>()
                .HasOne(x => x.Item)
                .WithMany()
                .HasForeignKey(x => x.ItemId)
                .IsRequired();

            modelBuilder.Entity<Item>().HasData(new Item() { Id = 1, Code = 1, Value = "value"});
            modelBuilder.Entity<DateItem>()
                .HasData(
                new DateItem { Id= 1, ItemId = 1, Date = new DateTime(2021, 1, 1) },
                new DateItem { Id= 2, ItemId = 1, Date = new DateTime(2021, 1, 10) },
                new DateItem { Id= 3, ItemId = 1, Date = new DateTime(2021, 1, 30) },
                new DateItem { Id= 4, ItemId = 2, Date = new DateTime(2021, 1, 15) },
                new DateItem { Id= 5, ItemId = 2, Date = new DateTime(2021, 1, 30) }
            );
        }
    }
}
