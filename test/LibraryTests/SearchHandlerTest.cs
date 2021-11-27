using System;
using NUnit.Framework;
using Bot;

namespace BotTests
{

    /// <summary>
    /// Clase TextNullHandlerTest la cual se encarga de testear las funcionalidades de la clase TextNullHandler, se declaran las variables globales que se van a utilizar.
    /// </summary>
    public class SearchHandlerTest
    {
        IRole role;
        UserInfo user1;
        Message testMessage;
        String response;
        IHandler result;
        Company company;


        [Test]
        public void SearchHandlerNoHasPermissionTest()
        {
            role = new RoleDefault();
            user1 = new UserInfo("name1", 5433261, role);
            SessionRelated.Instance.AddNewUser(user1);
            SearchHandler searchHandler = new SearchHandler(null);
            testMessage = new Message(5433261, "");

            result = searchHandler.Handle(testMessage, out response);
            Assert.That(result, Is.Null);
        }
        [Test]
        public void SearchHandlerCommandTest()
        {
            GeoLocation entrepreneurLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo");
            role = new RoleEntrepreneur("", entrepreneurLocation);
            user1 = new UserInfo("name1", 5433261, role);
            SessionRelated.Instance.AddNewUser(user1);
            SearchHandler searchHandler = new SearchHandler(null);
            user1.HandlerState = Bot.State.Start;
            testMessage = new Message(5433261, "/busqueda");

            result = searchHandler.Handle(testMessage, out response);
            Assert.That(response, Is.EqualTo("Por favor dinos por qué quieres buscar. \n Envía \"/pormaterial\" para buscar por material. \nEnvia \"/porubicacion\" para buscar por ubicación. \nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public void SearchHandlerWrongCommandTest()
        {
            GeoLocation PruebaLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo");
            role = new RoleEntrepreneur("", PruebaLocation);
            user1 = new UserInfo("name1", 5433261, role);
            SessionRelated.Instance.AddNewUser(user1);
            user1.HandlerState = Bot.State.Start;
            SearchHandler searchHandler = new SearchHandler(null);
            testMessage = new Message(5433261, "NoCompany");

            result = searchHandler.Handle(testMessage, out response);
            Assert.That(result, Is.Null);
            Assert.That(response, Is.Empty);
        }
    }
}