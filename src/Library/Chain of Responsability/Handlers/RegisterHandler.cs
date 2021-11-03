namespace Bot
{
    /// <summary>
    /// Handler que se encarga del registro de un usuario
    /// </summary>
    public class RegisterHandler : AbstractHandler
    {
        /// <summary>
        /// Indica los diferentes estados que puede tener el comando RegisterHandler.
        /// - Start: El estado inicial del comando. En este estado el comando pide el token de registro
        /// - ConfirmingToken: Luego de pedir el token. En este estado el comando valida si el token ingresado existe y vuelve al estado Start.
        /// </summary>
        public enum RegisterState
        {
            Start,
            ConfirmingToken,
        }

        /// <summary>
        /// Los datos que va obteniendo el comando en los diferentes estados.
        /// </summary>
        public RegisterData Data { get; private set; } = new RegisterData();

        /// <summary>
        /// El estado del comando.
        /// </summary>
        public RegisterState State { get; private set; }

        /// <summary>
        /// Constructor de la clase RegisterHandler
        /// </summary>
        /// <param name="condition">Condicion que se tiene que cumplir para que se ejecute el handler</param>
        public RegisterHandler(AbstractHandler succesor) : base(succesor)
        {
            State = RegisterState.Start;
        }

        /// <summary>
        /// Metodo que se encarga de atender el handler.
        /// </summary>
        /// <param name="request">Mensaje que contiene el texto y el id del usuario.</param>
        protected override bool InternalHandle(Message request, out string response)
        {
            UserRelated userData = SessionRelated.Instance.ReturnInfo(request.UserId);

            if ((State == RegisterState.Start) && (request.Text.Equals("/registro")))
            {
                this.State = RegisterState.ConfirmingToken;
                response = "Inserta tu token de usuario empresa: ";
                return true;
            }
            else if (State == RegisterState.ConfirmingToken)
            {
                if (SessionRelated.DiccUserTokens.ContainsKey(request.Text))
                {
                    this.State = RegisterState.Start;
                    this.Data.Token = request.Text;
                    userData.ChangeRoleToUserCompany(SessionRelated.Instance.ReturnCompany(request.Text));
                    response = "Token verificado, ahora eres un usuario empresa! :)";
                    return true;
                }
                else
                {
                    this.Data.Token = request.Text;
                    this.State = RegisterState.Start;
                    response = "Disculpa, no hemos encontrado ese token :(";
                    return true;
                }
            }

            response = string.Empty;
            return false;
        }

        public class RegisterData
        {
            /// <summary>
            /// El token que se ingresa en el estado ConfirmingToken
            /// </summary>
            public string Token { get; set; }
        }
    }
}