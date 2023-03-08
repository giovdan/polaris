namespace Mitrol.Framework.Domain.Core.Enums
{
    public enum LoginResultTypeEnum : byte
    {
        Successfull = 1,
        InvalidUserName,
        InvalidPassword,
        UserDisabled,
        InvalidRefreshToken,
        RefreshTokenExpired,
        InvalidGrantType,
        GenericError
    }
}