using MyEcom.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEcom.Core.DatabaseContext;
using System.Data.Entity;
using MyEcom.Domain;
using System.Data.Entity.Migrations;
using MyEcom.Common.RabbitmqDto;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using MyEcom.Core.Service;

namespace MyEcom.Core.Repository
{
    public abstract class AbstractBaseRepository<T> : IDisposable where T : class, IBaseEntity<int>
    {
        internal   MyEcomDbContext context = null;

        public DbSet<T> Entity { get { return context.Set<T>(); } }

        public AbstractBaseRepository()
        {
            context = new MyEcomDbContext();
        }

        public virtual bool Add(T entity)
        {
            Entity.Add(entity);

            return context.SaveChanges() > 0;
        }

        public virtual T Find(int Id)
        {
            return Entity.FirstOrDefault(x => x.Id == Id);
        }

        public virtual bool Delete(T entity)
        {
            if (entity != null && entity.Id != default(int))
            {
                var record = Find(entity.Id);
                if (record == null)
                {
                    throw new NullReferenceException(nameof(entity.Id));
                }
                record.IsDeleted = true;
                return context.SaveChanges() > 0;
            }
            throw new ArgumentOutOfRangeException(nameof(entity.Id));
        }

        public virtual bool Update(T entity)
        {
            context.Set<T>().AddOrUpdate(entity);
            context.SaveChanges();

            return context.SaveChanges() > 0;
        }

        public virtual IQueryable<IBaseEntity<int>> ListAll()
        {
            return Entity;
        }

        public void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
            }
        }
        public IQueryable<E> Query<E>() where E : class
        {
            return context.Set<E>();
        }
        public IEnumerable<ReservationViewDto> GetAllReservation()
        {
             string _connString = ConfigurationManager.ConnectionStrings["MyEcomConStr"].ToString();

             var reservations = new List<Reservation>();
            var reservationDto = new List<ReservationViewDto>();

            using (var connection = new SqlConnection(_connString))
            {
                connection.Open();


                using (var command = new SqlCommand(@"SELECT [Id], [hotel_id], [customerid], [roomid],[checkin],[checkout],[CreatTime],[IsDeleted] FROM [dbo].[Reservations]", connection))
                {

                    command.Notification = null;

                    var dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(Dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();



                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        reservations.Add(item: new Reservation
                        {
                            Id = (int)reader["Id"],
                            hotel_id = (int)reader["hotel_id"],
                            customerid = (int)reader["customerid"],
                            roomid = (int)reader["roomid"],
                            checkin = Convert.ToDateTime(reader["checkin"]),
                            checkout = Convert.ToDateTime(reader["checkout"]),
                            CreateTime = Convert.ToDateTime(reader["CreateTime"]),
                            IsDeleted = (bool)reader["IsDeleted"]
                        });

                    }
                
                }
                reservationDto=Services.ReservationService.AllReservation();

                return reservationDto;
            }

        }
        private void Dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                ReservationHub.Send();
            }
        }

    }
    public class BaseRepository<T> : AbstractBaseRepository<T>
        where T : class, IBaseEntity<int>
    {
        
       
    }
}
