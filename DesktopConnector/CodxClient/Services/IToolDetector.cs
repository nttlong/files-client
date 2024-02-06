using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodxClient.Services
{
    public interface IToolDetectorService
    {
        IList<Models.OfficeTools> DoDetectOffice();
    }
}
