// Services/BillingService.cs

using KnowIT_TransportIT_webapp.Data;
using KnowIT_TransportIT_webapp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


//create a service to connect cross-controllers and cross-contexts
public class BillingService
{
    private readonly BillingContext _billingContext;
    private readonly FreeDayContext _freeDayContext;

    public BillingService(BillingContext billingContext, FreeDayContext freeDayContext)
    {
        _billingContext = billingContext;
        _freeDayContext = freeDayContext;
    }

    // Example method that uses both contexts
    public List<BillingModel> GetBillingsOnFreeDays()
    {
        var freeDays = _freeDayContext.FreeDayClass.ToList();
        // logic to filter or combine data from both contexts
        return new List<BillingModel>(); // return appropriate data
    }

    // Add more methods as required
    public List<FreeDayClass> GetFreeDays()
    {
        return _freeDayContext.FreeDayClass.ToList();
    }
}
