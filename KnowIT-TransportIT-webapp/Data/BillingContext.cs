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
        private readonly FreeDayContext _freeDayContext;

        public BillingContext(DbContextOptions<BillingContext> options /*FreeDayContext freeDayContext*/)
            : base(options)
        {
            //_freeDayContext = freeDayContext;
        }

        //public DbSet<KnowIT_TransportIT_webapp.Models.FreeDayClass> FreeDayClass { get; set; }

        public DbSet<KnowIT_TransportIT_webapp.Models.BillingModel> BillingModel { get; set; } = default!;

        // Use _freeDayContext whenever you need to query or manipulate data in the FreeDayContext
    }

}
