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

        public UserRole MyUserRole { get; set; }

        public UserDTO MyUserDTO { get; set; }

        public UserViewModel() 
        {
            MyUser = new User();
            MyUserRole = new UserRole();
            MyUserDTO = new UserDTO();
        }

        //Funciones

        //Funcion que carga los datos del objeto de usuario global
        public async Task<UserDTO>GetUserDataAsync(string pEmail)
        {
            if (IsBusy) return null; 
            IsBusy = true;

            try
            {
                UserDTO userDTO = new UserDTO();

                userDTO = await MyUserDTO.GetUserInfo(pEmail);

                if (userDTO == null) return null;
                return userDTO;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
            finally { IsBusy = false; }
        }


        public async Task<bool> UpdateUser(UserDTO pUser)
        {
            if (IsBusy) return false;
            IsBusy = true;

            try
            {
                MyUserDTO = pUser;

                bool R = await MyUserDTO.UpdateUserAsync();

                return R;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            finally { IsBusy = false; }
        }

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

        //Carga la lista de roles, que se usaran por ejemplo en el picker de roles en la 
        //creación de un usuario nuevo
        public async Task<List<UserRole>> GetUserRolesAsync()
        {
            try
            {
                List<UserRole> roles = new List<UserRole>();

                roles = await MyUserRole.GetAllUserRolesAsync();

                if(roles == null)
                {
                    return null;
                }

                return roles;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Función de creación de usuario nuevo
        public async Task<bool> AddUserAsync(string pEmail,
                                             string pPassword,
                                             string pName,
                                             string pBackUpEmail,
                                             string pPhoneNumber,
                                             string pAddress,
                                             int pUserRoleID)
        {
            if (IsBusy) return false;
            IsBusy = true;

            try
            {

                MyUser = new User();

                MyUser.Email = pEmail;
                MyUser.Password = pPassword;
                MyUser.Name = pName;
                MyUser.BackUpEmail = pBackUpEmail;
                MyUser.PhoneNumber = pPhoneNumber;
                MyUser.UserRoleId = pUserRoleID;

                bool R = await MyUser.AddUserAsync();

                return R;

            }
            catch (Exception)
            {

                throw;
            }
            finally 
            { 
                IsBusy = false; 
            }


        }

    }
}
