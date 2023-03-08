namespace Mitrol.Framework.Domain.Licensing
{
    using System.Runtime.InteropServices;
    using System.Text;

    public static class LicensingAPI
    {
        private const string s_vendorCode = "5SCiFp0ecjKBBlWSw1RNoJ6kCqvNKppwg8O9miZn4chHNBuwrMXPOdDd/7gL2z5mqINDCqsvPRAaFVeJxH5sgq/+iIJa/rrMv53Z3Tlz7bpGyeLZxkmMbymFh2ON4jXk4DKONjKRF7fNRXYZiEH8Zx8Ef+/afq8Gr0ykgrDziwszzVGYPZp+64fFAfZAh5GWYxUrGz/OGztLmK3jDD8owY4zuXDt7fKarFdd2mChyQId6/M+dhechgtstvcZ431mj/xbNXDp4VUCEWJcCssIP2IeDuV8WwkaoZ1Rjf0lUg7SE9UNWV13G1id/Ku+GPAn2UE4z5v9IB0anIgHtw5xXjBH22qE7CiYFCIOX1PMwi2DHYHahk3oLJp+A2ddaWCbwdVdiDf2ryJcF0+EHEe4n3aXvRnX/hvKYlMyI+d2Mc4FUiIRf0iX4DhR6u1L+ZX2ytxg9MgxZgOI1f06OVpBG/NEAtUS0mW3sf6blqXU8EJ992ncrDJRZFBrQpNGR7+72Jx+CIlcX2LM4w9iwnHF3C97xRK9/99gGFurybXvyE86A0QT3pkA5ndkVIZG8gI+tDvVd9oaQBDl3Ve6pw5rx4b4nF/30E4NzwRsHtv7s3hFXelMoTJFp1ZDPBXA9x/naVhGRNm9mm2W7LxTQUKCi+nQtZ5SBLTOs1gKLjHjB5mfzrs6J+kyjXVKT/J53IoP2UYeWAz6x8jmS/c97JN/7bDUsi7y+WP2qTodHje5RilDK5kIF2E3twFl3+1rJwHrqDtZfIIinV+eF+ud8V9di5p7jXlW9rO5iLa+2lywI7LrJ1dRBjbr475faj3S9OKdobUGgM2jbViJZXKhv3ZF/zExKHZLJEEUmOJbA6sj3OFtgSLBejYf36Zu5bfYfkSQsgwttZMPFkyF24myxleDHmePWxpErkorMvxZqbVYuQUcoYPIpXP8qxICLW9B/zvKRMxS2KravAaFsl/PM8wzSA==";

        private const string s_dllName = "apidsp_windows_x64.dll";
        private const CharSet s_charSet = CharSet.Ansi;

        #region < P/Invoke >

        private class Native
        {
            [DllImport(s_dllName, CharSet = s_charSet)]
            public static extern HaspStatusEnum hasp_login(int feature_id, string vendor_code, ref int handle);

            [DllImport(s_dllName, CharSet = s_charSet)]
            public static extern HaspStatusEnum hasp_login(int feature_id, byte[] vendor_code, ref int handle);

            [DllImport(s_dllName, CharSet = s_charSet)]
            public static extern HaspStatusEnum hasp_logout(int handle);

            [DllImport(s_dllName, CharSet = s_charSet)]
            public static extern HaspStatusEnum hasp_read(int handle, int fileid, int offset, int length, [Out] byte[] buffer);

            [DllImport(s_dllName, CharSet = s_charSet)]
            public static extern HaspStatusEnum hasp_read(int handle, int fileid, int offset, int length, [MarshalAs(UnmanagedType.U1)] ref bool buffer);

            [DllImport(s_dllName, CharSet = s_charSet)]
            public static extern HaspStatusEnum hasp_read(int handle, int fileid, int offset, int length, ref byte buffer);

            [DllImport(s_dllName, CharSet = s_charSet)]
            public static extern HaspStatusEnum hasp_read(int handle, int fileid, int offset, int length, ref char buffer);

            [DllImport(s_dllName, CharSet = s_charSet)]
            public static extern HaspStatusEnum hasp_read(int handle, int fileid, int offset, int length, ref double buffer);

            [DllImport(s_dllName, CharSet = s_charSet)]
            public static extern HaspStatusEnum hasp_read(int handle, int fileid, int offset, int length, ref short buffer);

            [DllImport(s_dllName, CharSet = s_charSet)]
            public static extern HaspStatusEnum hasp_read(int handle, int fileid, int offset, int length, ref int buffer);

            [DllImport(s_dllName, CharSet = s_charSet)]
            public static extern HaspStatusEnum hasp_read(int handle, int fileid, int offset, int length, ref long buffer);

            [DllImport(s_dllName, CharSet = s_charSet)]
            public static extern HaspStatusEnum hasp_read(int handle, int fileid, int offset, int length, ref ushort buffer);

            [DllImport(s_dllName, CharSet = s_charSet)]
            public static extern HaspStatusEnum hasp_read(int handle, int fileid, int offset, int length, ref uint buffer);

            [DllImport(s_dllName, CharSet = s_charSet)]
            public static extern HaspStatusEnum hasp_read(int handle, int fileid, int offset, int length, ref ulong buffer);

            [DllImport(s_dllName, CharSet = s_charSet)]
            public static extern HaspStatusEnum hasp_read(int handle, int fileid, int offset, int length, ref float buffer);

            [DllImport(s_dllName, CharSet = s_charSet)]
            public static extern HaspStatusEnum hasp_read(int handle, int fileid, int offset, int length, ref string buffer);

        }

        #endregion < P/Invoke >

        public static HaspStatusEnum CheckUsbDongle(int feature)
        {
            var handle = 0; // HASP_INVALID_HANDLE_VALUE;

            var result = Native.hasp_login(feature, s_vendorCode, ref handle);

            Native.hasp_logout(handle);

            return result;
        }

        public static HaspStatusEnum GetSerialNumber(out string serialNumber)
        {
            const int HASP_FILEID_RW = 0xfff4;
            var handle = 0; // HASP_INVALID_HANDLE_VALUE;
            var feature = 0;

            var result = Native.hasp_login(feature, s_vendorCode, ref handle);

            var buffer = new byte[50];

            if (result == HaspStatusEnum.StatusOk)
            {
                result = Native.hasp_read(handle, HASP_FILEID_RW, 0, 50, buffer);
            }

            serialNumber = Encoding.Default.GetString(buffer);

            Native.hasp_logout(handle);

            return result;
        }
    }
}
