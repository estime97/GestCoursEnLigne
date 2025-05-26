using System.ComponentModel;

namespace BlazorHero.CleanArchitecture.Domain.Enums
{
    public enum Bureau
    {
        [Description("GUICHET UNIQUE")]
        GUICHETUNIQUE,
        [Description("DCCF LOME")]
        LOME,
        [Description("DCCF TSEVIE")]
        TSEVIE,
        [Description("DCCF ATAKPAME")]
        ATAKPAME,
        [Description("DCCF SOKODE")]
        SOKODE,
        [Description("DCCF KARA")]
        KARA,
        [Description("DCCF DAPAONG")]
        DAPAONG
    }
}
