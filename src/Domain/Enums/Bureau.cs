using System.ComponentModel;

namespace BlazorHero.CleanArchitecture.Domain.Enums
{
    public enum Bureau
    {
        [Description("GUICHET UNIQUE")]
        GUICHETUNIQUE = 0,
        [Description("DCCFE-LOME")]
        LOME = 1,
        [Description("DCCFE-TSEVIE")]
        TSEVIE = 2,
        [Description("DCCFE-ATAKPAME")]
        ATAKPAME = 3,
        [Description("DCCFE-SOKODE")]
        SOKODE = 4,
        [Description("DCCFE-KARA")]
        KARA = 5,
        [Description("DCCFE-DAPAONG")]
        DAPAONG = 6
    }
}
