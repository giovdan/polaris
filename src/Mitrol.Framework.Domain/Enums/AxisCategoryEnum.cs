namespace Mitrol.Framework.Domain.Enums
{
    /// <summary>
    /// Tipologia di assi configurabili
    /// N.B. L'assegnazione del valore non deve essere modificata perchè viene utilizzata in APLR per la configurazione degli assi
    /// </summary>
    public enum AxisCategoryEnum
    {
        /// <summary>
        /// CNC Fanuc Gemini
        /// </summary>
        FANUC = 100,

        /// <summary>
        /// CNC Fanuc Robot Antropomorfo
        /// </summary>
        FANUC_ROB = 200,

        /// <summary>
        /// CNC Mitrol: asse di tipo 1
        /// </summary>
        TIPO1 = 1,
        /// <summary>
        /// CNC Mitrol: asse di tipo 2
        /// </summary>
        TIPO2 = 2,
        /// <summary>
        /// CNC Mitrol: asse di tipo 3
        /// </summary>
        TIPO3 = 3,
        /// <summary>
        /// CNC Mitrol: asse di tipo 4
        /// </summary>
        TIPO4 = 4,
        /// <summary>
        /// CNC Mitrol: asse di tipo 5
        /// </summary>
        TIPO5 = 5 
    }

}
