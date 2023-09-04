namespace VisitorManagement.Application.DTOs
{
    public class VisitorDTO
    {
        public int Id { get; set; }
        public bool IsPreRegistered { get; set; }
        public bool IsOnSiteRegistered { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string ContactNumber { get; set; }
        public string StreetAddress { get; set; }
        public string IdentificationType { get; set; }
        public string IdentificationNumber { get; set; }
        public int HostUserId { get; set; }
        public string HostUserName { get; set; }
        public string PurposeOfVisit { get; set; }
        public DateTime ExpectedArrival { get; set; }
        public DateTime ExpectedCheckOut { get; set; }
        public TimeSpan VisitDuration { get; set; }
        public byte[] VisitorImage { get; set; }

    }
}