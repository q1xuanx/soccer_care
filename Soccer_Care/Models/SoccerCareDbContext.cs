using Microsoft.EntityFrameworkCore;

namespace Soccer_Care.Models
{
    public class SoccerCareDbContext : DbContext
    {
        public SoccerCareDbContext(DbContextOptions options) : base(options) { 
            
        }

        public DbSet<BillModel> BillSet { get; set; }
        public DbSet<DetailsOrderModel> DetailsOrder { get; set; }  
        public DbSet<FieldLikeModel> FieldLike { get; set; }
        public DbSet<FootBallFieldModel> FootBallFields { get; set; }
        public DbSet<HistoryOrderModel> HistoryOrders { get; set; }
        public DbSet<OrderFieldModel> OrderField { get; set; }
        public DbSet<RatingModel> Ratings { get; set; }
        public DbSet<TypeFieldModel> TypeFields { get; set; }
        public DbSet<UserModel> User { get; set; }
        public DbSet<ListFieldModel> listFields { get; set; }

    }
}
