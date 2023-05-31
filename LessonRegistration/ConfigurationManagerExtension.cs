using Microsoft.Extensions.Configuration;
using System;
using System.Runtime.CompilerServices;

namespace LessonRegistration
{
    public static class ConfigurationManagerExtension
    {
        public static string GetBaseUri(this ConfigurationManager configurationManager, string name)
        {
            var value = configurationManager.GetSection("BaseUri").GetValue<string>(name);
            if (value == null)
            {
                throw new Exception("hasn't variable");
            }

            return value;
        }

        public static string GetKeyCloakSetting(this ConfigurationManager configurationManager, string name)
        {
            var value = configurationManager.GetSection("KeyCloak").GetValue<string>(name);
            if (value == null)
            {
                throw new Exception("hasn't variable");
            }

            return value;
        }
    }
}
