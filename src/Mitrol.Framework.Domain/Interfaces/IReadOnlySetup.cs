namespace Mitrol.Framework.Domain.Interfaces
{
    using Mitrol.Framework.Domain.Configuration;
    using Mitrol.Framework.Domain.Enums;
    using System.Collections.Generic;
    using System.Linq;

    public interface IReadOnlyToolUnit
    {
        int CurrentToolId { get; }
        int RequiredToolId { get; }
        SetupActionEnum ToolSetupAction { get; }
    }

    public interface IReadOnlyHeadSlot
    {
        int CurrentToolId { get; }
        ushort CurrentCoolingDownTime { get; }
        float MaxLength { get; }// TODO: Rename. Is not clear that it's the max tool length allowed
        bool IsEnabled { get; }
        short SlotPosition { get; }
        int CurrentWorkType { get; }
    }

    public interface IReadOnlyDrillHead : IReadOnlyUnit
    {
        //short SpindlesOverride { get; }
        IEnumerable<IReadOnlyHeadSlot> Slots { get; }
        IReadOnlyHeadSlot ManualSlot { get; }
        short SlotInSpindle { get; }
    }

    public interface IReadOnlyPlaTorch : IReadOnlyToolUnit, IReadOnlyUnit
    {
        float ArchVoltage { get; }
        decimal Current { get; }
        float DTorchPla { get; }
        string Electrode { get; }
        int MixGas1 { get; }
        int MixGas2 { get; }
        string Nozzle { get; }
        string NozzleRetCap { get; }
        byte NumTorce { get; }
        short Path { get; }
        int PlasmaCutflowPressure { get; }
        GasTypeXprEnum PlasmaGas { get; }
        int PlasmaPiercePressure { get; }
        int ProcessID { get; }
        string Shield { get; }
        int ShieldCutflowPressure { get; }
        GasTypeXprEnum ShieldGas { get; }
        int ShieldPiercePressure { get; }
        string ShieldRetCap { get; }
        string SwirlRing { get; }
        long TableIdBevel { get; }
        long TableIdMark { get; }
        long TableIdTrueHole { get; }
        float Thickness { get; }
        TorchTypeEnum TorchType { get; }
        string Watertube { get; }
    }

    public interface IReadOnlyOxyTorch : IReadOnlyToolUnit, IReadOnlyUnit
    {
        float AngleOxyBevelBottom { get; }
        float AngleOxyBevelTop { get; }
        float DTorchOxy1 { get; }
        float DTorchOxy2 { get; }
        float DTorchOxy3 { get; }
        float HeightOxyBevelBottom { get; }
        float HeightOxyBevelTop { get; }
        float LastLifeTu { get; }
        byte NumTorce { get; }
        short Path { get; }
        decimal Pco { get; }
        decimal Pg { get; }
        decimal Ppg { get; }
        decimal Ppo { get; }
        decimal Ppoa { get; }
        long TableIdBevel { get; }
        TorchTypeEnum TorchType { get; }
    }

    public interface IReadOnlyUnit
    {
        UnitEnum Unit { get; }
    }

    public interface IReadOnlyStation
    {
        int Id { get; }
        bool IsOriginValid { get; }
        double NA { get; }
        double NX { get; }
        double NY { get; }
    }

    public interface IReadOnlySetup
    {
        IReadOnlyGeneralSetup General { get; }
        IEnumerable<IReadOnlyDrillHead> DrillHeads { get; }
        IEnumerable<IReadOnlyPlaTorch> PlaTorches { get; }
        IEnumerable<IReadOnlyOxyTorch> OxyTorches { get; }
        IEnumerable<IReadOnlyStation> Stations { get; }
        IEnumerable<IReadOnlySaw> Saws { get; }
    }

    public interface IReadOnlySaw : IReadOnlyToolUnit, IReadOnlyUnit
    {
        GridPosition GridPosition { get; }
        SawingMachineTypeEnum Type { get; }
        BladeTypeEnum BladeType { get; }
        BladeForwardSpeedTypeEnum BladeForwardSpeedType { get; }
    }

    public interface IReadOnlyGeneralSetup
    {
        float Angle { get; }
        bool Bended { get; }
        float Dsa { get;  }
        float Dsb { get;}
        float Lb { get;  }
        float Lbr { get;  }
        float Lbri { get;  }
        float LinearWeight { get; }
        string MaterialCode { get; }
        MaterialTypeEnum MaterialType { get; }
        float Oc { get; }
        float Ocx { get; }
        float OffPz { get; }
        float Op { get; }
        float Pl { get; }
        float PosCa { get; }
        float PosCb { get; }
        float PosCc { get; }
        float PosPzA { get; }
        float PosPzB { get; }
        float PosPzC { get; }
        string ProfileCode { get; }
        ProfileTypeEnum ProfileType { get; }
        bool Pu { get; }
        float Radius { get;}
        float Rbf { get;}
        float Rf { get;  }
        float Ri { get;  }
        float Sa { get;  }
        float Sab { get;}
        float Sba { get; }
        float Sbb { get;  }
        float Section { get; }
        float SpecificWeight { get;}
        float Ta { get;}
        float Tba { get; }
        float Tbb { get; }
    }


    public static class SetupExtensions
    {
        public static bool TryGetValue<TUnit>(this IEnumerable<TUnit> units, UnitEnum unitKey, out TUnit value) where TUnit : IReadOnlyUnit
        {
            value = units.SingleOrDefault(unit => unit.Unit == unitKey);
            return value != null;
        }

        public static bool TryGetValue<TUnit>(this IEnumerable<TUnit> units, int id, out TUnit value) where TUnit : IReadOnlyStation
        {
            value = units.SingleOrDefault(unit => unit.Id == id);
            return value != null;
        }

        public static bool TryGetStation(this IEnumerable<IReadOnlyStation> units, int id, out IReadOnlyStation value)
        {
            value = units.SingleOrDefault(unit => unit.Id == id);
            return value != null;
        }
        
    }
}
