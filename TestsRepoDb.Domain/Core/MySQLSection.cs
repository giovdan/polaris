﻿namespace RepoDbVsEF.Domain.Models.Core
{
    public class MySQLSection
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
    }
}
