using System;
using System.Text;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;

namespace Bot
{
    /// <summary>
    /// Conjunto de Empresas, clase estática que administra la lista de Empresas en general.
    /// </summary>
    public static class CompanySet
    {
        private const string Path = @"..\..\..\..\..\docs\CompanyDataBase.json";
        /// <summary>
        /// Obtiene la lista de Empresas, esto para que la clase Búsqueda pueda manipular eficientemente las Publicaciones.
        /// </summary>
        /// <value>Clase Empresa.</value>
        public static IReadOnlyCollection<Company> ListCompany
        {
            get
            {
                List<Company> listCompanies = new List<Company>();
                Company company;
                try
                {
                    using (StreamReader txtReader = new StreamReader(Path))
                    {
                        
                        string line = txtReader.ReadLine();
                        string name;
                        string item;
                        GeoLocation location;
                        string contact;
                        IReadOnlyList<Publication> listHistorialPublications;
                        IReadOnlyList<Publication> ListOwnPublications;
                        IReadOnlyList<User> ListUsers;

                        while (line != null)
                        {
                            name = JsonSerializer.Deserialize<Company>(line).Name;
                            item = JsonSerializer.Deserialize<Company>(line).Item;
                            contact = JsonSerializer.Deserialize<Company>(line).Contact;
                            location = JsonSerializer.Deserialize<Company>(line).Location;

                            listHistorialPublications = JsonSerializer.Deserialize<Company>(line).ListHistorialPublications;
                            ListOwnPublications = JsonSerializer.Deserialize<Company>(line).ListOwnPublications;
                            ListUsers = JsonSerializer.Deserialize<Company>(line).ListUsers;
                            
                            company = new Company(name, item, location, contact);
                            company.AddListHistorialPublication(listHistorialPublications);
                            company.AddOwnPublication(ListOwnPublications);
                            company.AddUser(ListUsers);

                            listCompanies.Add(company);
                            //Read the next line
                            line = txtReader.ReadLine();
                        }
                        txtReader.Close();
                        txtReader.Dispose();
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return listCompanies.AsReadOnly();
            }
        }

        /// <summary>
        /// Método que se encarga de agregar una Empresa a la lista de Empresas del sistema.
        /// </summary>
        /// <param name="name">Nombre de Empresa.</param>
        /// <param name="item">Rubro de Empresa.</param>
        /// <param name="location">Ubicación de Empresa.</param>
        /// <param name="contact">Contacto de Empresa.</param>
        /// <returns><c>True</c> en caso de que se pueda agregar y <c>False</c> en caso 
        /// contrario.</returns>
        public static bool AddCompany(string name, string item, GeoLocation location, string contact, List<User> listUsers, List<Publication> listOwnPublications, List<Publication> listHistorialPublications)
        {
            Company company = new Company(name, item, location, contact);
            company.AddUser(listUsers);
            company.AddOwnPublication(listOwnPublications);
            company.AddListHistorialPublication(listHistorialPublications);

            if(!ContainsCompanyInListCompanies(company))
            {
                string jsonCompany = JsonSerializer.Serialize(company);

                using(StreamWriter txtWrite = new StreamWriter(Path, true))
                {
                    txtWrite.WriteLine(jsonCompany);
                    txtWrite.Close();
                    txtWrite.Dispose();
                    return true;
                }
            }
            else return false;
        }

        /// <summary>
        /// Sobrecarga del método AddCompany que permite ingresar un objeto Empresa como parámetro
        /// para ser ingresado al sistema.
        /// </summary>
        /// <param name="company">Empresa.</param>
        /// <returns><c>True</c> en caso de que se pueda agregar y <c>False</c> en caso 
        /// contrario.</returns>
        public static bool AddCompany(Company company)
        {
            if(!ContainsCompanyInListCompanies(company))
            {
                string jsonCompany = JsonSerializer.Serialize(company);

                using(StreamWriter txtWrite = new StreamWriter(Path, true))
                {
                    txtWrite.WriteLine(jsonCompany);
                    txtWrite.Close();
                    txtWrite.Dispose();
                    return true;
                }
            }
            else return false;
        }

        /// <summary>
        /// Método que se encarga de eliminar una Empresa de la lista de Empresas del sistema.
        /// </summary>
        /// <param name="company">Empresa.</param>
        /// <returns><c>True</c> en caso de que se haya eliminado correctamente y <c>False</c> en caso 
        /// contrario.</returns>
        public static bool DeleteCompany(Company company)
        {
            if(ContainsCompanyInListCompanies(company))
            {
                string nameCompanyToDelete = company.Name;
                List<Company> listCompaniesEdit = new List<Company>();

                foreach(Company item in ListCompany)
                {
                    if(company.Name != item.Name)
                    {
                        listCompaniesEdit.Add(item);  
                    }
                }
                File.WriteAllText(Path, "");
                
                foreach(Company companyToAdd in listCompaniesEdit)
                {
                    AddCompany(companyToAdd);
                }
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Método que retorna la lista completa de Empresas en un string con sus respectivos
        /// índices.
        /// </summary>
        /// <returns>String con el nombre de la Empresa y sus indices.</returns>
        public static string ReturnListCompanies()
        {
            StringBuilder result = new StringBuilder("Empresas: \n");

            foreach (Company company in ListCompany)
            {
                result.Append($"{company.Name} \n");
            }
            return result.ToString();
        }

        /// <summary>
        /// Método simple que se encarga de comprobar si una clase Empresa se encuentra
        /// en el sistema de Empresas.
        /// </summary>
        /// <param name="company">Empresa.</param>
        /// <returns><c>True</c> en caso de encontrarse en el sistema y <c>False</c> en caso 
        /// contrario.</returns>
        public static bool ContainsCompanyInListCompanies(Company company)
        {
            foreach(Company item in ListCompany)
            {
                if(item.Name == company.Name) return true;
            }
            return false;
        }
    }
}