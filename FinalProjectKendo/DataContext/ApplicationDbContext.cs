using FinalProjectKendo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FinalProjectKendo.DataContext
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext() : base(nameOrConnectionString: "MyConn")
        {

        }
        public virtual DbSet<UserData> UserInfos { get; set; }
        public virtual DbSet<AlbumData> AlbumDatas { get; set; }

    }
}