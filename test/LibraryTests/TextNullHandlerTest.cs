using System;
using NUnit.Framework;
using Bot;

namespace BotTests
{

    /// <summary>
    /// Clase TextNullHandlerTest la cual se encarga de testear las funcionalidades de la clase TextNullHandler, se declaran las variables globales que se van a utilizar.
    /// </summary>
    public class TextNullHandlerTest
    {
        IRole role;
        UserInfo user1;
        Message testMessage;
        String response;
        IHandler result;

        [Test]
        public void TextNullHandlerTest1()
        {
            role = new RoleDefault();
            user1 = new UserInfo("name1", 5433261, role);
            user1.HandlerState = Bot.State.ConfirmingHeadingEntrepreneur;
            TextNullHandler textNullHandler = new TextNullHandler(null);
            testMessage = new Message(5433261, null);
            result = textNullHandler.Handle(testMessage, out response);

            //Assert.That(result, Is.Not.Null);
            //Assert.That(result, Is.EqualTo("El mensaje no puede estar vacio, ni ser una imagen o video"));
        }
    }
}