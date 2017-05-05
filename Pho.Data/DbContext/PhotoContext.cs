using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pho.Data
{
    public class PhotoContext : DbContext
    {
        public PhotoContext ()
        {
            Database.Connection.ConnectionString =
                @"Data Source=BARIS\BARIS;Initial Catalog=PhoWebSite;Integrated Security=true";
                //ConfigurationManager.ConnectionStrings["PhotographerDBConnectionString"].ConnectionString;
        }
        public DbSet<User> Users{ get; set; }
        public DbSet<Content>Contents{get;set;}
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Content_Picture_Mapping> ContentPictureMappings { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<WebSiteUtulity> Descriptions { get; set; }
        public DbSet<PageContext> PageContext { get; set; }
    }
}
