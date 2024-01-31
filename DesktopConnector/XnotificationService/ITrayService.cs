using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XNotificationService
{
    public interface ITrayService
    {
        void Initialize();
        void Start();

        Action ClickHandler { get; set; }
    }
}
