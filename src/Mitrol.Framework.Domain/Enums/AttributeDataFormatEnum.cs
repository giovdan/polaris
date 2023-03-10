namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.ComponentModel;

    //[TypeConverter(typeof(EnumCustomNameTypeConverter))]
    public enum AttributeDataFormatEnum
    {
        /// <summary>
        /// Velocità lineare [mm/min - inch/min]
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 7,1)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 7,3)]
        //[DatabaseDisplayName("VEL")]
        [Description("Velocità")]
        LinearSpeed = 1,

        /// <summary>
        /// Velocità di rotazione [rpm]
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 0, 0)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 0, 0)]
        //[DatabaseDisplayName("RPM")]
        [Description("Velocità di rotazione")]
        RotationalSpeed = 2,

        /// <summary>
        /// Velocità di avanzamento al giro [mm/rev - inch/rev]
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 7,3)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 7,4)]
        //[DatabaseDisplayName("REV")]
        [Description("Velocità di rivoluzione")]
        RevolutionSpeed = 3,

        /// <summary>
        /// Velocità periferica [mt/min - feet/min]
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 7,1)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 7,3)]
        //[DatabaseDisplayName("VP")]
        [Description("Velocità periferica")]
        PeripheralSpeed = 4,

        /// <summary>
        /// Angolo [grad]
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 7, 2)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 7, 2)]
        //[EnumSerializationName("Angle")]
        //[DatabaseDisplayName("ANG")]
        [Description("Angolo")]
        Angle = 5,

        /// <summary>
        /// Peso [kg - pound]
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 7,1)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 7,1)]
        //[DatabaseDisplayName("WGT")]
        [Description("Peso")]
        Weight = 6,

        /// <summary>
        /// Peso lineare [kg/mt - pound/foot]
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 7,3)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 7,3)]
        //[DatabaseDisplayName("LWG")]
        [Description("Peso lineare")]
        LinearWeight = 7,

        /// <summary>
        /// Peso specifico [Kg/dm³ - Pound/feet³]
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 7,2)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 7,4)]
        //[DatabaseDisplayName("SWG")]
        [Description("Peso Specifico")]
        SpecificWeight = 8,

        /// <summary>
        /// Superficie [mt² - feet²]
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 7,2)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 7,3)]
        //[DatabaseDisplayName("SUR")]
        [Description("Superficie")]
        Surface = 9,

        /// <summary>
        /// Superficie lineare [mt²/mt - feet²/feet]
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 7,3)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 7,3)]
        //[DatabaseDisplayName("LSU")]
        [Description("Superficie")]
        LinearSurface = 10,

        /// <summary>
        /// Superficie per peso [mt²/ton] Non utilizzata !
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 7,3)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 7,3)]
        //[DatabaseDisplayName("WSU")]
        [Description("Superficie su peso")]
        WeightSurface = 11,

        /// <summary>
        /// Pressione [bar]
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 0, 0)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 0, 0)]
        //[DatabaseDisplayName("PRS")]
        [Description("Pressione")]
        Pressure = 12,

        /// <summary>
        /// Pressione per apparecchiatura plasma [psi]
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 0,0)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 0,0)]
        //[DatabaseDisplayName("PPS")]
        [Description("Pressione per apparecchiatura Plasma")]
        PlasmaPressure = 13,

        /// <summary>
        /// Lunghezza [mm - inch]
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 7, 2)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 7, 5)]
        //[EnumSerializationName("Length")]
        //[DatabaseDisplayName("LEN")]
        [Description("Lunghezza")]
        Length = 14,

        /// <summary>
        /// Diametro [mm - inch]
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 7,1)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 7,5)]
        //[DatabaseDisplayName("DMR")]
        [Description("Diametro")]
        Diameter = 15,

        /// <summary>
        /// Distanza [mt - inch]
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 7,3)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 7,3)]
        //[DatabaseDisplayName("DST")]
        [Description("Distanza")]
        Distance = 16,

        /// <summary>
        /// Tipologia senza alcuna conversione (numeri, stringhe ..) [n]
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 0)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 0)]
        //[EnumSerializationName("AsIs")]
        //[DatabaseDisplayName("ASIS")]
        [Description("Valore senza conversione")]
        AsIs = 17,

        /// <summary>
        /// Sezione [cm² - inch²]
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 7,1)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 7,3)]
        //[DatabaseDisplayName("SCT")]
        [Description("Sezione")]
        Section = 18,

        /// <summary>
        /// Tensione elettrica [V]
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 1,1)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 1,1)]
        //[DatabaseDisplayName("VOLT")]
        [Description("Tensione")]
        ElectricalPotential = 19,

        /// <summary>
        /// Corrente elettrica [A]
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 0, 0)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 0, 0)]
        //[DatabaseDisplayName("AMP")]
        [Description("Corrente Elettrica")]
        ElectricCurrent = 20,

        /// <summary>
        /// Tempo [sec]
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 1,1)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 1,1)]
        //[DatabaseDisplayName("sec")]
        [Description("Tempo in secondi")]
        TimeInSeconds = 21,

        /// <summary>
        /// Tempo [hh:mm:ss]
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 0)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 0)]
        //[DatabaseDisplayName("TEM")]
        [Description("Tempo")]
        Time = 22,

        /// <summary>
        /// Area [mm² - inch²]
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 2,2)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 3,3)]
        //[DatabaseDisplayName("AR")]
        [Description("Area")]
        Area = 23,

        /// <summary>
        /// Tempo [ms]
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 1,1)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 1,1)]
        //[DatabaseDisplayName("ms")]
        [Description("Tempo in millisecondi")]
        TimeInMilliseconds = 24,

        /// <summary>
        /// Tempo [min]
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 0,0)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 0,0)]
        //[DatabaseDisplayName("min")]
        [Description("Tempo in minuti")]
        TimeInMinutes = 25,

        /// <summary>
        /// Displacement (parametri assi) [dis]
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 3)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 3)]
        //[DatabaseDisplayName("DIS")]
        [Description("dis")]
        PaxeDis = 26,

        /// <summary>
        /// Velocità (parametri assi) [dis/s]
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 3)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 3)]
        //[DatabaseDisplayName("DISS")]
        [Description("dis/s")]
        PaxeLinearSpeed = 27,

        /// <summary>
        /// Accelerazione (parametri assi) [dis/s²]
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 3)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 3)]
        //[DatabaseDisplayName("DISSS")]
        [Description("dis/s²")]
        PaxeAcceleration = 28,

        /// <summary>
        /// Guadagno (parametri assi) [1/s]
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 3)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 3)]
        //[DatabaseDisplayName("GAIN")]
        [Description("1/s")]
        PaxeGain = 29,

        /// <summary>
        /// Impulsi encoder per giro (parametri assi) [i/rev]
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 3)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 3)]
        //[DatabaseDisplayName("IMPE")]
        [Description("i/rev")]
        PaxeImpe = 30,

        /// <summary>
        /// Accoppiamento meccanico (parametri assi) [rev]
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 3)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 3)]
        //[DatabaseDisplayName("REVE")]
        [Description("rev")]
        PaxeReve = 31,

        /// <summary>
        /// Scala azionamento (parametri assi) [dis/8V]
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 3)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 3)]
        //[DatabaseDisplayName("VEL8")]
        [Description("dis/8V")]
        PaxeScaleDrive = 32,

        /// <summary>
        /// Percentuale [%]
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 3)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 3)]
        //[DatabaseDisplayName("PERC")]
        [Description("%")]
        Percentage = 33,

        /// <summary>
        /// MilliVolts [mV]
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 3)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 3)]
        //[DatabaseDisplayName("MV")]
        [Description("mV")]
        MilliVolts = 34,

        /// <summary>
        /// Scansioni [sc]
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 3)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 3)]
        //[DatabaseDisplayName("SCANS")]
        [Description("sc")]
        Scans = 35,

        /// <summary>
        /// Bit [bit]
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 3)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 3)]
        //[DatabaseDisplayName("BIT")]
        [Description("Valore bit")]
        Bit = 36,

        /// <summary>
        /// Guadagno di un traduttore [µ/bit]
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 3)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 3)]
        //[DatabaseDisplayName("TGAIN")]
        [Description("Guadagno del trasduttore")]
        TransducerGain = 37,

        /// <summary>
        /// Booleano [0/1]
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 3)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 3)]
        //[DatabaseDisplayName("BOOL")]
        [Description("Valore booleano")]
        Bool = 38,

        /// <summary>
        /// Coefficiente di accelerazione [bit/sc]
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 3)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 3)]
        //[DatabaseDisplayName("BITSC")]
        [Description("Coefficiente di accelerazione")]
        AccelerationCoefficient = 39,

        /// <summary>
        /// Velocità angolare [grad/sec]
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 7)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 7)]
        //[EnumSerializationName("AngularSpeed")]
        //[DatabaseDisplayName("ANGSPEED")]
        [Description("Velocità angolare")]
        AngularSpeed = 40,

        /// <summary>
        /// Accelerazione angolare [grad/s²]
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 3)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 3)]
        //[DatabaseDisplayName("ANGACC")]
        [Description("Accelerazione angolare")]
        AngularAcceleration = 41,
        /// <summary>
        /// Velocità lineare in sec [mm/sec - inch/sec]
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 3)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 3)]
        //[DatabaseDisplayName("VELSEC")]
        [Description("Velocità lineare in sec")]
        LinearSpeedSec = 42,

        /// <summary>
        /// Velocità di avanzamento mm/n
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 3)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 7)]
        //[DatabaseDisplayName("LinearForwardSpeed")]
        [Description("Velocità di avanzamento")]
        LinearForwardSpeed = 43,

        /// <summary>
        /// Velocità di avanzamento cm²/min
        /// </summary>
        //[DecimalPrecision(MeasurementSystemEnum.MetricSystem, 3)]
        //[DecimalPrecision(MeasurementSystemEnum.ImperialSystem, 7)]
        //[DatabaseDisplayName("SectionForwardSpeed")]
        [Description("Velocità di avanzamento")]
        SectionForwardSpeed = 44,
    }
}
