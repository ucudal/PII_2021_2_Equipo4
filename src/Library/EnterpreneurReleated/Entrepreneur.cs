using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot
{
    public class RoleEntrepreneur : Role
    {
        private static int entrepreneurAccountant = 0;
        private GeoLocation location;
        public string heading; //rubro
        private List<string> certification;
        private List<string> specializations;
        /// <summary>
        /// Constructor de la clase Entrepreneur, setea los valores de los parámetros 
        /// y suma un valor al contador de emprendedores estático
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="heading"></param>
        /// <param name="geolocation"></param>
        /// <param name="certification"></param>
        /// <param name="specializations"></param>
        public RoleEntrepreneur(string name, int id, string heading, GeoLocation geolocation, string certification, string specialization) : base(name, id)
        {
            this.location = geolocation;
            this.heading = heading;
            AddCertification(certification);
            AddSpecialization(specialization);
            entrepreneurAccountant++;
        }
        /// <summary>
        /// Método para agregarle certificaciones al emprendedor
        /// </summary>
        /// <param name="certification"></param>
        public void AddCertification(string certification)
        {
            this.certification.Add(certification);
        }
        /// <summary>
        /// Método para agregarle espcializaciones al empresario
        /// </summary>
        /// <param name="specializations"></param>
        public void AddSpecialization(string specializations)
        {
            this.specializations.Add(specializations);
        }

        /// <summary>
        /// Método para obtener el reporte del emprendedor 
        /// </summary>
        /// <returns></returns>
        public string GetReport()
        {
            return ($"Nombre: ....");
        }
        public static int EntrepreneurAccountant
        {
            get
            {
                return entrepreneurAccountant;
            }
        }
        /// <summary>
        /// Método para buscar por materiales o por ubicación
        /// </summary>
        /// <returns></returns>
        public List<Publication> SearchingMaterials(string wordToSearch)
        {
            return Search(wordToSearch);
        }
        //VER A QUÉ SEARCH LLAMAR SEGUN SI ES POR UBICACION O POR MATERIAL
    }
}