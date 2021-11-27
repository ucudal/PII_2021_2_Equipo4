using System;
using System.Collections.Generic;

namespace Bot
{
    /// <summary>
    /// Clase TokenGenerator.
    /// </summary>
    public class TokenGenerator
    {
        private static TokenGenerator instance;

        /// <summary>
        /// Metodo para generar una instancia de token. Cumple con el patrón Singleton debido a que solamente hay una unica instancia de este.
        /// </summary>
        /// <value>Instancia del Token. </value>
        public static TokenGenerator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TokenGenerator();
                }

                return instance;
            }
        }

        int tkn = 0;

        /// <summary>
        /// Metodo para generar el token. Incrementa la variable tkn y la devuelve por cada token que genera.
        /// </summary>
        /// <returns>El token generado.</returns>
        public int GenerateToken()
        {
            return tkn++;
        }
    }
}