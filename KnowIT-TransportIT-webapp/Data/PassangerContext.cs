using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KnowIT_TransportIT_webapp.Models;

namespace KnowIT_TransportIT_webapp.Data
{
    public class PassangerContext : DbContext
    {
        public PassangerContext (DbContextOptions<PassangerContext> options)
            : base(options)
        {
        }

        public DbSet<KnowIT_TransportIT_webapp.Models.PassangerModel> PassangerModel { get; set; } = default!;
    }
}
