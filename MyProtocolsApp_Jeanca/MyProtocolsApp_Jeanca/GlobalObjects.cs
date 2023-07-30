using MyProtocolsApp_Jeanca.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProtocolsApp_Jeanca
{
    public static class GlobalObjects
    {
        //definimos las propiedades de codificación de los json
        //que usaremos en los modelos
        public static string MimeType = "application/json";
        public static string ContentType = "Content-Type";

        //Crear el objeto local (global) de usuario 
        public static UserDTO MyLocalUser = new UserDTO();
    }
}
