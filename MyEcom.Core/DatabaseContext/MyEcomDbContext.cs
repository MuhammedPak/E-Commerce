using MyEcom.Domain;
using MyEcom.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyEcom.Core.DatabaseContext
{
    public class MyEcomDbContext : DbContext
    {
        public MyEcomDbContext() : base("MyEcomConStr")
        {

        }
        public DbSet<Hotel_Image> Hotel_Image { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<Hotel> Hotel { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<HotelContent> HotelContent { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<RoomContent> RoomContent { get; set; }
    }
}
