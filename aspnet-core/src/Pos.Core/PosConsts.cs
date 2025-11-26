using Pos.Debugging;

namespace Pos;

public class PosConsts
{
    public const string LocalizationSourceName = "Pos";

    public const string ConnectionStringName = "Default";

    public const bool MultiTenancyEnabled = true;


    /// <summary>
    /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
    /// </summary>
    public static readonly string DefaultPassPhrase =
        DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "6cdb13ea39bc44ceaf3d7d2b4b4747ea";
}
