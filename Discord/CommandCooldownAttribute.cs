using System;
using Qmmands;

namespace GarlicBread.Discord
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class CommandCooldownAttribute : CooldownAttribute
    {
        public CommandCooldownAttribute(int amount, double per, CooldownMeasure cooldownMeasure,
            CooldownType bucketType)
            : base(amount, per, cooldownMeasure, bucketType)
        {
        }
    }
}