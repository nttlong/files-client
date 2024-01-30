using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodxDesk.Services
{
    public interface INotificationService
    {
        void ShowNotification(string title, string body);
    }
}
