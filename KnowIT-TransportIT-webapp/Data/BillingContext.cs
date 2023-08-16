using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KnowIT_TransportIT_webapp.Models;

namespace KnowIT_TransportIT_webapp.Data
{
    public class BillingContext : DbContext
    {
        public BillingContext (DbContextOptions<BillingContext> options)
            : base(options)
        {
        }

        public DbSet<KnowIT_TransportIT_webapp.Models.BillingModel> BillingModel { get; set; } = default!;
    }
}
