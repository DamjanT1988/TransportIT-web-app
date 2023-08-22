using KnowIT_TransportIT_webapp.Data;
using KnowIT_TransportIT_webapp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

public class Service
{
    private readonly BillingContext _billingContext;
    private readonly FreeDayContext _freeDayContext;
    private readonly PassangerContext _passangerContext;
    private readonly TicketsContext _ticketsContext;

    /*
    public Service(BillingContext billingContext,
                          FreeDayContext freeDayContext,
                          PassangerContext passangerContext,
                          TicketsContext ticketsContext)
    {
        _billingContext = billingContext;
        _freeDayContext = freeDayContext;
        _passangerContext = passangerContext;
        _ticketsContext = ticketsContext;
    }*/

    public List<FreeDayClass> GetFreeDays()
    {
        return _freeDayContext.FreeDayClass.ToList();
    }

    public List<TicketsModel> GetTickets()
    {
        return _ticketsContext.TicketsClass.ToList();
    }

}
