using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nito.AsyncEx;

namespace Bot
{
    /// <summary>
    /// Clase que implementa la búsqueda de una publicación, en este caso la búsqueda por ubicación.
    /// Patrones y prrincipios:
    /// Esta clase cumple con en patrón Expert porque es experta en cómo hacer una búsqueda por ubicación. Además,
    /// cumple con el principio SRP dado que su única razón de cambio es cómo buscar una publicación con la ubicación que se le indica.
    /// </summary>
    public class SearchByLocation : ISearch<Publication>
    {
        private static SearchByLocation instance; 

        /// <summary>
        /// Obtiene una única instancia de esta clase.
        /// </summary>
        /// <value>La única instancia de esta clase.</value>
        public static SearchByLocation Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SearchByLocation();
                }
                
                return instance;
            }
        }

        /// <summary>
        /// Método que búsca todas las publicaciones que contienen la ubicación pasada por parámetro. Recorre todas las
        /// publicaciones y se fija si tiene la misma ubicación recibida. Si es igual, se agrega la publicación a la lista que 
        /// va a devolver y se va a fijar a la siguiente.
        /// </summary>
        /// <param name="wordToSearch">Dirección para buscar.</param>
        /// <returns>Lista de Publicaciones.</returns>
        public string Search(string wordToSearch)
        {
            string publications = string.Empty;
            double distance = 0;
            GeoLocation location = new GeoLocation(wordToSearch, "Montevideo");
            List<Publication> result = new List<Publication>();
            IReadOnlyCollection<Publication> listPublications = PublicationSet.Instance.ListPublications;

            foreach (Publication publication in listPublications)
            {
                distance = location.CalculateDistance(publication.Location);
                if (distance < 3)
                {
                    publications = publications + publication.ReturnPublication();
                }
            }

            return publications;
        }
    }
}