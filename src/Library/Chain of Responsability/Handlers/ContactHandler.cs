using System;
using System.Collections.Generic;

namespace Bot
{
    /*
    Patrones y principios:
    También cumple con Expert, ya que posee todo lo necesario para cumplir la responsabilidad otorgada a la clase.    
    A su vez, cumple con el patrón Chain of Responsability.
    */
    /// <summary>
    /// Handler que se encarga del registro de un usuario
    /// </summary>
    public class ContactHandler : AbstractHandler
    {
        /// <summary>
        /// Constructor de la clase RegisterHandler
        /// </summary>
        /// <param name="succesor">Condicion que se tiene que cumplir para que se ejecute el handler</param>
        public ContactHandler(AbstractHandler succesor) : base(succesor)
        {
        }

        /// <summary>
        /// Metodo que se encarga de atender el handler.
        /// </summary>
        /// <param name="request">Mensaje que contiene el texto y el id del usuario.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        protected override bool InternalHandle(Message request, out string response)
        {
            UserInfo user = SessionRelated.Instance.GetUserById(request.UserId);

            if (!user.HasPermission(Permission.ContactCompany))
            {
                response = string.Empty;
                return false;
            }
            
            if (request.Text.Equals("/contacto") && user.HandlerState == Bot.State.Start)
            {
                user.HandlerState = Bot.State.AskingCompanyName;
                response = "Por favor dinos con que empresa te quieres contactar. \nEnvia \"/cancelar\" para cancelar la operación.";
                return true;
            }
            else if (user.HandlerState == Bot.State.AskingCompanyName)
            {
                Company company = SessionRelated.Instance.GetCompanyByName(request.Text);
                if (company != null)
                {
                    response = $"{company.ReturnContact()}";
                    user.HandlerState = Bot.State.Start;
                    return true;
                }
                else
                {
                    response = "Disculpa, no encontramos esa Empresa";
                    user.HandlerState = Bot.State.Start;
                    return true;
                }
            }
            
            response = string.Empty;
            return false;
        }
    }
}