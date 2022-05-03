using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myhealthcareapi.Services.ServiceResponses
{
    public enum MedicServiceResponses
    {
        NULLPARAM = 400,
        EXCEPTION = 500,
        SUCCESS = 201,
        EMAILALREADYEXISTS = 409
    }
}
