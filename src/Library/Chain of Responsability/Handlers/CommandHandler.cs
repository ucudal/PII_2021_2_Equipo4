using System.Collections.Generic;
using System;

namespace Bot
{
    /*
    Patrones y principios:
    Debido a que se indentifica una sola razón de cambio, esta clase cumple con SRP, este motivo de cambio podría ser, cambiar el método InternalHandle.
    También cumple con Expert, ya que posee todo lo necesario para cumplir la responsabilidad otorgada a la clase.    
    A su vez, cumple con el patrón Chain of Responsability.
    */
    /// <summary>
    /// Handler para mostrar los comandos que el usuario tiene acceso
    /// </summary>
    public class CommandHandler : AbstractHandler
    {
        /// <summary>
        /// Constructor de la clase CommandHandler
        /// </summary>
        /// <param name="succesor">Condicion que se tiene que cumplir para que se ejecute el handler</param>
        public CommandHandler(AbstractHandler succesor) : base(succesor) { }

        /// <summary>
        /// Metodo que se encarga de atender el handler.
        /// </summary>
        /// <param name="request">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        protected override bool InternalHandle(Message request, out string response)
        {
            if (request.Text.ToLower().Equals("/comandos"))
            {
                response = $"Estos son todos los comandos: \n" + Command.GetCommands(request.UserId);
                return true;
            }

            response = string.Empty;
            return false;
        }
    }
}