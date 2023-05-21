using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper.Model
{
    class Laboratory
    {
        public string LaboratoryName { get; set; }
        public string? Status { get; set; }
        public string? Location { get; set; }
        public string? ManagerName { get; set; }
        public string? AgentName { get; set; }
        public string? TelephoneNumber { get; set; }
        public string? FaxNumber { get; set; }
        public string? WebsiteAddress { get; set; }
        public string? Email { get; set; }
    }
}
