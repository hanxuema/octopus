using System.Collections.Generic;

namespace AWSServerless1
{
    public class SessionViewViewModel
    {
        public string SessionId { get; set; }
        public string CurrentIdentifyProvider { get; set; }
        public bool Internal { get; set; }
        public string IsfAuthenticationType { get; set; }
        public string LastLoggedInDateTime { get; set; }
        public string DisplayGiveName { get; set; }
        public string DisplayFamilyName { get; set; }
        public Dictionary<string, string> Identifiers { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, List<string>> ExternalIdentifiers { get; set; }
    }
}
