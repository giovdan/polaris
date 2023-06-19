namespace Mitrol.Framework.Domain.Production.Enums
{
    public enum MacroSectionEnum:byte 
    {
        HEAD=1,
        POS_X=2,
        HOL=4,
        PALP=8,
        CUT=16,

        ALL=HEAD|POS_X|HOL|PALP|CUT

    }
}
