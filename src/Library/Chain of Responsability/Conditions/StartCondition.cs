namespace Bot
{
    /// <summary>
    /// 
    /// </summary>
    public class StartCondition : ICondition
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public bool VerifyCondition(Message condition)
        {
            return condition.Text == "/hola";
        }
    }
}