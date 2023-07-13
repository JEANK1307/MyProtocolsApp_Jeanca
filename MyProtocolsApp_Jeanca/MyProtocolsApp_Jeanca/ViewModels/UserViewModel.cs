using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyProtocolsApp_Jeanca.Models;

namespace MyProtocolsApp_Jeanca.ViewModels
{
    public class UserViewModel: BaseViewModel
    {
        //El VM funciona como puente entre el modelo y la vista
        //en sentido teorico el VM "siente" los cambios de la vista
        //y los pasa al modelo de forma automática, o viceversa
        //según se use en uno o dos sentidos.

        //También se puede usar (como en este caso particular,
        //simplemente como mediador de procesos. Más adelante se usará
        //commands y bindings en 2 sentidos.
        
        //primero en formato de funciones clásicas

        public User MyUser { get; set; }


        public UserViewModel() 
        {
            MyUser = new User();
        }

        //Funciones

        //Función para validar el ingreso de l usuario al app por medio
        //del login

        public async Task<bool> UserAccessValidation(string pEmail,string pPassword)
        {
            //debemos poder controlar que no se ejecute la operación más de una vez
            //en este caso hay una funcionalidad pensada para eso en BaseViewModel que 
            //fue heredada al definir esta clase.
            //Se usará una propiedad llamada "IsBusy" para indicar que está en proceso
            //de ejecución mientras su valor sea verdadero

            //control de bloqueo de funcionalidad
            if(IsBusy) return false;
            IsBusy = true;

            try
            {
                MyUser.Email = pEmail;
                MyUser.Password = pPassword;

                bool R = await MyUser.ValidateUserLogin();

                return R;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;

                return false;

                throw;
            }
            finally 
            { 
                IsBusy = false; 
            }

        }



    }
}
