namespace Mitrol.Framework.Domain.Core.Models
{
    using Microsoft.AspNetCore.Authorization;
    using Mitrol.Framework.Domain.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class CustomAuthorizationRequirement : IAuthorizationRequirement
    {
        public List<string> ListOfClaims { get; set; }

        public CustomAuthorizationRequirement()
        {
            ListOfClaims = new List<string>();
        }

        public CustomAuthorizationRequirement(string listOfClaims) : this()
        {
            if (!string.IsNullOrEmpty(listOfClaims))
                ListOfClaims = listOfClaims.Split(",").ToList();
        }

        public UserSession CurrentSession { get; internal set; }
    }
}