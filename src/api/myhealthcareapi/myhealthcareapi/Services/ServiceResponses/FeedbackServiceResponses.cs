using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myhealthcareapi.Services.ServiceResponses
{
    
    public enum FeedbackServiceResponses
    {
        SUCCESS = 200,
        EXCEPTION = 500,
        NOTFOUND = 404
    }
}
