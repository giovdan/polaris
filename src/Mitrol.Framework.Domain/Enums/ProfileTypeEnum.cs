using System;
using System.Collections.Generic;
using System.Text;

namespace Mitrol.Framework.Domain.Enums
{
    public enum ProfileTypeEnum
    {
        X = 0,
        L = 1,
        V = 2,
        B = 3,
        I = 4,
        D = 5,
        T = 6,
        U = 7,
        Q = 8,
        C = 9,
        F = 11,
        N = 12,
        P = 13,
        R = 14,
    }

    public static class ProfileTypeExtensions
    {
        public static EntityTypeEnum ToEntityType(this ProfileTypeEnum profileType)
        {
            return (EntityTypeEnum)profileType;
        }
    }
}
