


namespace Mitrol.Framework.MachineManagement.Application
{
    using Mitrol.Framework.MachineManagement.Application.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Mitrol.Framework.Domain.Extensions;
    using Mitrol.Framework.Domain.Enums;

    public static class MachineManagementExtensions
    {
        ///// <summary>
        ///// Costruisce il codice in funzione dei code generators
        ///// </summary>
        ///// <param name="codeGenerators"></param>
        ///// <param name="separator">Separatore</param>
        ///// <returns></returns>
        public static string ToString(this IEnumerable<CodeGeneratorItem> codeGenerators
            , string separator = "-")
        {
            if (codeGenerators == null)
            {
                throw new ArgumentNullException(nameof(codeGenerators));
            }

            var orderedIdentifiers = codeGenerators.OrderBy(x => x.Order);
            string code = string.Empty;
            foreach (var identifier in orderedIdentifiers)
            {
                if (identifier.Value == null)
                    continue;

                string value = identifier.Value.ToString();
                if (code.IsNullOrEmpty())
                {
                    code = value;
                }
                else if (!string.IsNullOrEmpty(value))
                    code += $" {separator} {value}";
            }

            return code.Trim();
        }
    }

}
