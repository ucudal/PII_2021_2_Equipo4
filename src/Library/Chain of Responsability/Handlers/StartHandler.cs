namespace Bot
{
    /// <summary>
    /// 
    /// </summary>
    public class StartHandler : AbstractHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>

        public StartHandler(StartCondition condition) : base(condition) {}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        protected override void handleRequest(Message request)
        {
            Commands commands = new Commands();
            UserRelated userData = new SessionRelated().ReturnInfo(request.UserId);
            userData.Channel.SendMessage("¡Bienvenido al bot del equipo 4!");
            userData.Channel.SendMessage("¿Qué desea hacer?:\n" + commands.ReturnCommands("Consola"));
            userData.Channel.SendMessage("Si deseas salir, solo escribe Exit. Si quieres ver los comandos, escribe Comandos");
        }
    }
}