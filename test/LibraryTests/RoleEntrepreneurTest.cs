using Bot;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BotTests
{   
    /// <summary>
    /// Tests de los métodos de la clase RoleEntrepreneur
    /// </summary>
    public class RoleEntrepreneurTests
    {
        RoleEntrepreneur emprendedor; 
        GeoLocation ubicacionParaPruebas;
        string name;
        int id;
        string heading;
        string certification;
        string specialization;

        [SetUp]
        public void Setup()
        {
            name = "Alejandra";
            id = 1;
            heading = "";
            GeoLocation ubicacionParaPruebas = new GeoLocation("8 de OCtubre", "Blanqueada");
            certification = "";
            specialization = "";

            emprendedor = new RoleEntrepreneur(name, id, heading, ubicacionParaPruebas, certification, specialization);
        }

        [Test]
        public void AddCertificationTest()
        {
            certification = "Manejar explosivos";
            emprendedor.AddCertification(certification);
            Assert.AreEqual("Manejar explosivos", emprendedor.ReturnCertification()[1]);
        }

        [Test]
        public void AddSpecializationTest()
        {
            specialization = "Quimica";
            emprendedor.AddSpecialization(specialization);
            Assert.AreEqual("Quimica", emprendedor.ReturnSpecialization()[1]);
        }
        
        [Test]
        public void AddListHistorialPublicationsTest1()
        {
            RoleEntrepreneur emprendedor2 = new RoleEntrepreneur("Juan", 2, "Industria", ubicacionParaPruebas, "Uso de corrosivos", "");
            Company empresaDeVidrios = new Company("vidrioglass", "vidrio", ubicacionParaPruebas, "vidrioglas@correo.com");
            Material vidrio = new Material("Vidrio", 100, 850);
            Publication publicacion = new Publication("Publicación de vidrio", empresaDeVidrios, ubicacionParaPruebas, vidrio);
            emprendedor.AddHistorialPublication(publicacion);
            Assert.IsNotEmpty(emprendedor.listHistorialPublications);
        }
        [Test]
        public void AddHistorialPublicationTest2()
        {
            RoleEntrepreneur emprendedor3 = new RoleEntrepreneur("Juan", 2, "Construcción", ubicacionParaPruebas, "", "");
            Company pvcCompany = new Company("pvCompany", "plasticos", ubicacionParaPruebas, "pvcventas@correo.com");
            Material pvc = new Material("PVC", 200, 500);
            Publication publicacion = new Publication("Publicación de plástico", pvcCompany, ubicacionParaPruebas, pvc);
            emprendedor.AddHistorialPublication(publicacion);
            List<Publication> listaPublicacionesEsperada = new List<Publication>();
            listaPublicacionesEsperada.Add(publicacion);
            Assert.AreEqual(listaPublicacionesEsperada, emprendedor.ReturnListHistorialPublications());
        }
        
        [Test]
        public void ContactCompanyTest()
        {
            Company pvcCompany = new Company("pvCompany", "plasticos", ubicacionParaPruebas, "pvcventas@correo.com");
            Material pvc = new Material("PVC", 200, 500);
            Publication publicacion = new Publication("Publicación de plástico", pvcCompany, ubicacionParaPruebas, pvc);
            Assert.AreEqual(pvcCompany.ReturnContact(), emprendedor.ContactCompany(publicacion));
        }
    }
}