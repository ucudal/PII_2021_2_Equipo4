using System;

namespace Bot
{
    /// <summary>
    /// interfaz con la firma del metodo que devuelve un reporte.
    /// </summary>
    public interface IReport
    {
        /// <summary>
        /// firma del metodo GiveReport.
        /// </summary>
        /// <returns>Devuelve un reporte como string.</returns>
        String GiveReport();
    }
}