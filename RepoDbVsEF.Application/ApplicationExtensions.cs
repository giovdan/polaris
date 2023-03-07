using RepoDbVsEF.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoDbVsEF.Application
{
    public static class ApplicationExtensions
    {
        public static Entity ToEntity(this EntityListItem entityListItem)
        {
            return new Entity
            {
                DisplayName = entityListItem.DisplayName,
                EntityType = entityListItem.EntityType,
                Id = entityListItem.Id
            };
        }
    }
}
