using Microsoft.Owin.Security.DataHandler.Encoder;
using System;
using System.Collections.Concurrent;
using System.Security.Cryptography;

namespace SMSIdentityApi
{
    public class AudiencesStore
    {
        public static ConcurrentDictionary<string, Audience> AudiencesList = new ConcurrentDictionary<string, Audience>();

        static AudiencesStore()
        {
            AudiencesList.TryAdd("099153c2625149bc8ecb3e85e03f0022",
                                new Audience
                                {
                                    ClientId = "099153c2625149bc8ecb3e85e03f0022",
                                    Base64Secret = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==",
                                    Name = "ResourceServer.Api 1"
                                });
        }

        public static Audience AddAudience(string name)
        {
            var clientId = Guid.NewGuid().ToString("N");

            var key = new byte[32];
            RNGCryptoServiceProvider.Create().GetBytes(key);
            var base64Secret = TextEncodings.Base64Url.Encode(key);

            Audience newAudience = new Audience { ClientId = clientId, Base64Secret = base64Secret, Name = name };
            AudiencesList.TryAdd(clientId, newAudience);
            return newAudience;
        }

        public static Audience FindAudience(string clientId)
        {
            Audience audience = null;
            if (AudiencesList.TryGetValue(clientId, out audience))
            {
                return audience;
            }
            return null;
        }
    }
}