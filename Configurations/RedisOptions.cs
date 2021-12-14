using System.Collections.Generic;

namespace AWSServerless1
{
    public class RedisOptions
    {
        public RedisOptions()
        {
            RedisConnect = "localhost";
            Ssl = false;
            AuthToken = "";
        }

        public string RedisConnect { get; set; }
        public bool Ssl { get; set; }
        public string AuthToken { get; set; }
    }
}
