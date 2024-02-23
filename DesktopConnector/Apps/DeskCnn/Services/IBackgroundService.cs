using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskCnn.Services
{
    public interface IBackgroundService
    {
        void Start();
        void Stop();
    }
}
