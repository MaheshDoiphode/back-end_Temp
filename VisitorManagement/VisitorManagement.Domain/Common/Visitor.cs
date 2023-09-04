using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace VisitorManagement.Domain.Common
{
    public class Visitor
    {
        [Key]
        public int VisitorId { get; set; }

        [DefaultValue(false)]
        public bool PreRegisterVisitor { get; set; }

        [DefaultValue(false)]
        public bool OnSiteRegisterVisitor { get; set; }
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string IdType { get; set; }
        public string IdNumber { get; set; }
        public int HostId { get; set; }
        public string HostName { get; set; }
        public string VisitPurpose { get; set; }
        public DateTime ExpectedArrivalTime { get; set; }
        public DateTime ExpectedCheckOutTime { get; set; }
        public TimeSpan VisitDuration { get; set; }

        public byte[] VisitorImage { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
