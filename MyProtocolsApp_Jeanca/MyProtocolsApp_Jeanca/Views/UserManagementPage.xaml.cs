using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyProtocolsApp_Jeanca.ViewModels;
using MyProtocolsApp_Jeanca.Models;

namespace MyProtocolsApp_Jeanca.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserManagementPage : ContentPage
    {
        UserViewModel viewModel;

        public UserManagementPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new UserViewModel(); 

            LoadUserInfo();
        }


        private void LoadUserInfo()
        {
            TxtID.Text = GlobalObjects.MyLocalUser.IDUsuario.ToString();
            TxtEmail.Text = GlobalObjects.MyLocalUser.Correo;
            TxtName.Text = GlobalObjects.MyLocalUser.Nombre;
            TxtPhoneNumber.Text = GlobalObjects.MyLocalUser.Telefono;
            TxtBackUpEmail.Text = GlobalObjects.MyLocalUser.CorreoRespaldo;
            TxtAddress.Text = GlobalObjects.MyLocalUser.Direccion;
        }

        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            //Primero hacemos validacion de campos requeridos

            if (ValidateFields())
            {
                //Sacar un respaldo del usuario global tal y como está en este momento
                //por sí aldo sale mal en el proceso
                UserDTO BackupLocalUser = new UserDTO();
                BackupLocalUser = GlobalObjects.MyLocalUser;

                try
                {
                    GlobalObjects.MyLocalUser.Nombre = TxtName.Text.Trim();
                    GlobalObjects.MyLocalUser.CorreoRespaldo = TxtBackUpEmail.Text.Trim();
                    GlobalObjects.MyLocalUser.Telefono = TxtPhoneNumber.Text.Trim();
                    GlobalObjects.MyLocalUser.Direccion = TxtAddress.Text.Trim();

                    var answer = await DisplayAlert("???", "Are you sure to continue updating user info?", "Yes", "No");

                    if (answer)
                    {
                        bool R = await viewModel.UpdateUser(GlobalObjects.MyLocalUser);

                        if (R)
                        {
                            await DisplayAlert(":)", "User Updated!!!", "OK");
                            await Navigation.PopAsync();
                        }
                        else
                        {
                            await DisplayAlert(":(", "Something went wrong...", "OK");
                            await Navigation.PopAsync();
                        }
                    }

                }
                catch (Exception)
                {

                    //Si algo sale mal reversamos los cambios
                    GlobalObjects.MyLocalUser = BackupLocalUser;
                    throw;
                }

            }
        }

        private bool ValidateFields()
        {
            bool R = false;

            if (TxtName.Text != null && !string.IsNullOrEmpty(TxtName.Text.Trim()) &&
                TxtBackUpEmail != null && !string.IsNullOrEmpty(TxtBackUpEmail.Text.Trim()) &&
                TxtPhoneNumber.Text !=null && !string.IsNullOrEmpty(TxtPhoneNumber.Text.Trim()))
            {
                //En este caso estan todos lod datos requeridos

                R = true;
            }
            else
            {
                //Si falta algun dato obligatorio
                if (TxtName.Text == null || string.IsNullOrEmpty(TxtName.Text.Trim()))
                {
                    DisplayAlert("Validation Failed", "The Name is required", "OK");
                    TxtName.Focus();
                    return false;
                }

                if (TxtBackUpEmail.Text == null || string.IsNullOrEmpty(TxtBackUpEmail.Text.Trim()))
                {
                    DisplayAlert("Validation Failed", "The BackUp Email is required", "OK");
                    TxtBackUpEmail.Focus();
                    return false;
                }

                if (TxtPhoneNumber.Text == null || string.IsNullOrEmpty(TxtPhoneNumber.Text.Trim()))
                {
                    DisplayAlert("Validation Failed", "The Phone Number is required", "OK");
                    TxtPhoneNumber.Focus();
                    return false;
                }

            }

            return R;
        }

        private async void BtnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}