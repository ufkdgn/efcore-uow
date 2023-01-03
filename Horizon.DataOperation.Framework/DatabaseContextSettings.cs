namespace Horizon.DataOperation.Framework
{
    public class DatabaseContextSettings
    {
        public DbProviderType DbProviderType { get; set; }
        public string ConnectionString { get; set; }
        public int Timeout { get; set; } = 60;
    }

    public enum DbProviderType
    {
        MySql,
        Oracle,
        MSSql
    }
}
