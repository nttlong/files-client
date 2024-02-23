using DeskCnn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskCnn.Services
{
    public interface IObserveContentService
    {
        void RegisterRequestInfo(RequestInfo info);
        void Start(string LocalStorageDir);
    }
}
