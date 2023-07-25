using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Domain
{
    public class BudgetConfiguration
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public int? RetryCount { get; set; }
    }

    public class ConnectionStrings
    {
        public string SqlServerConnectionString { get; set; }
        public string EventBusHostname { get; set; }
        public string EventBusUsername { get; set; }
        public string EventBusPassword { get; set; }
    }
}
