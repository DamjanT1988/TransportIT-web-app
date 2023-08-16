using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KnowIT_TransportIT_webapp.Models;

namespace KnowIT_TransportIT_webapp.Data
{
    public class TicketsContext : DbContext
    {
        public TicketsContext (DbContextOptions<TicketsContext> options)
            : base(options)
        {
        }

        public DbSet<KnowIT_TransportIT_webapp.Models.TicketsModel> TicketsClass { get; set; } = default!;
    }
}
