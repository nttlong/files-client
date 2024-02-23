using DeskCnn.Models;
using DeskCnn.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskCnn.ServiceFactory
{
    public class ToolDetectorService : IToolDetectorService
    {
        

        IList<OfficeTools> IToolDetectorService.DoDetectOffice()
        {
            throw new NotImplementedException();
        }
    }
}
