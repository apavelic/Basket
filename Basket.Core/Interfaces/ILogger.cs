using Basket.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Core.Interfaces
{
    public interface ILogger
    {
        void Log(string message, LogType type);
    }
}
