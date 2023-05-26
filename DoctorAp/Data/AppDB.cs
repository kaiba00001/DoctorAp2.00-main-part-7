using DoctorAp.Models;
using Microsoft.EntityFrameworkCore;

namespace DoctorAp.Data
{
    public class AppDB:DbContext
    {

        public AppDB(DbContextOptions options) :base(options)
        {


        }
        public DbSet<ScreenLead>  screenLeads { get; set; }
    }
}
