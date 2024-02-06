using CodxClient.Models;
using CodxClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodxClient.ServiceFactory
{
    public class ToolDetectorService : IToolDetectorService
    {
        

        IList<OfficeTools> IToolDetectorService.DoDetectOffice()
        {
            throw new NotImplementedException();
        }
    }
}
