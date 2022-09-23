using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;
using System.Security.AccessControl;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> options) : base(options)
        {

        }

        // SEED DATA

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Region>().HasData(
        //        new Region
        //        {
        //            Id = Guid.NewGuid(),
        //            Code = "FSS",
        //            Name = "Romance",
        //            Area = 12345,
        //            Lat = 1.025,
        //            Long = 222.001,
        //            Population = 1,
        //            ImageUrl = "https://channel.mediacdn.vn/thumb_w/640/prupload/879/2017/10/img20171016213119549.jpg"
        //        },
        //        new Region
        //        {
        //            Id = Guid.NewGuid(),
        //            Code = "WAIKO",
        //            Name = "Waikato Region",
        //            Area = 8970,
        //            Lat = -37.5144584,
        //            Long = 174.5405128,
        //            Population = 496701,
        //            ImageUrl = "http://vyctravel.com/libs/upload/ckfinder/images/New%20Zealand/NewZealand_Uc/Shutterstock/Skyline%20Queenstown%20Gondola.jpg"
        //        },
        //        new Region
        //        {
        //            Id = Guid.NewGuid(),
        //            Code = "NRTHL",
        //            Name = "Northland Region",
        //            Area = 13789,
        //            Lat = -35.3708304,
        //            Long = 172.5717825,
        //            Population = 194600,
        //            ImageUrl = "https://duhocinec.com/static/assets/uploads/du-hoc-new-zealand/hinh-minh-hoa/du-hoc-newzealand-minh-hoa-15.jpg"
        //        },
        //        new Region
        //        {
        //            Id = Guid.NewGuid(),
        //            Code = "AUCK",
        //            Name = "Auckland Region",
        //            Area = 4894,
        //            Lat = -36.5253207,
        //            Long = 173.7785704,
        //            Population = 1718982,
        //            ImageUrl = "https://dafulbrightteachers.org/wp-content/uploads/2019/05/New-Zealand-o-dau-1-1.jpg"
        //        },
        //        new Region
        //        {
        //            Id = Guid.NewGuid(),
        //            Code = "BAYP",
        //            Name = "Bay Of Plenty Region",
        //            Area = 12230,
        //            Lat = -37.5328259,
        //            Long = 175.7642701,
        //            Population = 345400,
        //            ImageUrl = "https://bestour.com.vn/uploads/thu-do-WELLINGTON-new-zealand-bestour3.jpg"
        //        }
        //    );

        //    modelBuilder.Entity<WalkDifficulty>().HasData(
        //        new WalkDifficulty
        //        {
        //            Id = Guid.NewGuid(),
        //            Code = "Easy"
        //        },
        //        new WalkDifficulty
        //        {
        //            Id = Guid.NewGuid(),
        //            Code = "Medium"
        //        },
        //        new WalkDifficulty
        //        {
        //            Id = Guid.NewGuid(),
        //            Code = "Hard"
        //        }
        //        );

        
        //}

        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<WalkDifficulty> WalkDifficulties { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
