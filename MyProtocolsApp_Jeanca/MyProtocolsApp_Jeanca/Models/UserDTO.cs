using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyProtocolsApp_Jeanca.Models
{
    public class UserDTO
    {
        [Newtonsoft.Json.JsonIgnore]
        public RestRequest Request { get; set; }

        public int IDUsuario { get; set; }
        public string Correo { get; set; } = null!;
        public string Contrasennia { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string CorreoRespaldo { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string? Direccion { get; set; }
        public bool? Activo { get; set; }
        public bool? EstaBloqueado { get; set; }
        public int IDRol { get; set; }
        public string DescripcionRol { get; set; } = null!;

        //FUNCIONES
        public async Task<UserDTO> GetUserInfo(string PEmail)
        {
            try
            {
                //Usaremos el prefijo de la ruta URL del API que se indica en
                //services\APIConnection para agregar el sufijo y lograr la ruta
                //completa de consumo del end point que se quiere usar.

                string RouteSufix = string.Format("Users/GetUserInfoByEmail?Pemail={0}", PEmail);


                //armamos la ruta completa al endpoint en el API

                string URL = Services.APIConnection.ProductionPrefixURL + RouteSufix;

                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL, Method.Get);

                //agregamos mecanismo de seguridad, en este caso API key

                Request.AddHeader(Services.APIConnection.ApiKeyName, Services.APIConnection.ApiKeyValue);
                Request.AddHeader(GlobalObjects.ContentType, GlobalObjects.MimeType);

                //ejecutar la llamada al API
                RestResponse response = await client.ExecuteAsync(Request);

                //saber si las cosas salieron bien
                HttpStatusCode statusCode = response.StatusCode;

                if (statusCode == HttpStatusCode.OK)
                {
                    //En el API diseñamos el end point de forma que retorne un list<UserDTO>
                    //Pero esta funcion retorna solo UN objeto de UserDTO, por lo tanto
                    //debemos seleccionar de la lista el item con el index 0.

                    //esto puede llegar a servir para muchos propósitos donde un API retorna uno o muchos registros
                    //pero necesitamos solo 1 de ellos

                    var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserDTO>>(response.Content);

                    var item = list[0];

                    return item;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                throw;
            }
        }


        public async Task<bool> UpdateUserAsync()
        {
            try
            {
                //Usaremos el prefijo de la ruta URL del API que se indica en
                //services\APIConnection para agregar el sufijo y lograr la ruta
                //completa de consumo del end point que se quiere usar.

                string RouteSufix = string.Format("Users/{0}", this.IDUsuario);

                //armamos la ruta completa al endpoint en el API

                string URL = Services.APIConnection.ProductionPrefixURL + RouteSufix;

                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL, Method.Put);

                //agregamos mecanismo de seguridad, en este caso API key

                Request.AddHeader(Services.APIConnection.ApiKeyName, Services.APIConnection.ApiKeyValue);

                Request.AddHeader(GlobalObjects.ContentType, GlobalObjects.MimeType);

                //En el caso de una operación POST debemos serializar el objeto para pasarlo como
                //json al API
                string SerializedModelObject = JsonConvert.SerializeObject(this);
                //Agregamos el objeto serializado al cuerpo del request.
                Request.AddBody(SerializedModelObject, GlobalObjects.MimeType);


                //ejecutar la llamada al API
                RestResponse response = await client.ExecuteAsync(Request);

                //saber si las cosas salieron bien
                HttpStatusCode statusCode = response.StatusCode;

                if (statusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                throw;
            }
        }




    }
}
