using System;

namespace Mitrol.Framework.Domain.Core.CustomExceptions
{
    public class FanucResultException : Exception
    {
        public short ResultCode;
        public FanucResultException(short resultCode)
        {
            ResultCode = resultCode;
        }
    }
}