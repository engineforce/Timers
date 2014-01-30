using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomTimers.Common
{
    public interface IStatusReporter
    {
        void ReportStatus(string status);
    }
}
