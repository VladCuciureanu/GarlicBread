using System;

namespace GarlicBread.Discord
{
    [Flags]
    public enum RuntimeFlags
    {
        None,
        DryRun,
        Verbose,
        RunAsOther
    }
}