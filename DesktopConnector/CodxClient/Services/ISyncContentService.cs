using CodxClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodxClient.Services
{
    internal interface ISyncContentService
    {
        void DoSync(RequestInfo requestInfo);
    }
}
