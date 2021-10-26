using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerZ.Common.Providers
{
    public interface IDateTimeProvider
    {
        public DateTime Now { get; }
        public DateTime UtcNow { get; }
    }
}
