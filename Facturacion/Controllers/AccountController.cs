using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Facturacion.Data.Contracts;
using Facturacion.Data.Models.Account;
using Facturacion.Domain;
using Facturacion.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Facturacion.Controllers
{
    
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountRepository _AccountRepository;
        private TokenService _tokenService;

        public AccountController(IAccountRepository accountRepository, TokenService tokenService)
        {
            _AccountRepository = accountRepository;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginModel login) 
        {
            LoginResponse loginResponse;
            Enums.Account.Login res = await _AccountRepository.ValidateLogin(login);
            Users _user;
            try
            {
                switch (res)
                {
                    case Enums.Account.Login.Success:
                        _user = await _AccountRepository.GetUser(login);
                        string token = _tokenService.GenerarToken(_user);
                        loginResponse = new LoginResponse
                        {
                            token = token,
                            name = _user.Name,
                            isAdmin = _user.IsSuperAdmin,
                            Status = "Ok"
                        };
                        return Ok(loginResponse);
                    case Enums.Account.Login.User_Password_Error:
                        loginResponse = new LoginResponse
                        {
                            token = null,
                            Status = "Ok",
                            Message = "Error de credenciales",
                            name = "-",
                            isAdmin = false
                        };
                        return BadRequest(loginResponse);

                }
            }
            catch (Exception e)
            {

                throw;
            }


            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] UserCreateModel user)
        {
            Enums.Account.Create ret = await _AccountRepository.Create(user);
            UserCreateResponseModel responseModel = new UserCreateResponseModel();
            switch (ret)
            {
                case Enums.Account.Create.Account_Exists:
                    responseModel.Message = "Usuario no disponible.";
                    responseModel.Status = "Ok";
                    break;
                case Enums.Account.Create.Created:
                    responseModel.Message = "Usuario creado correctamente.";
                    responseModel.Status = "Ok";
                    break;
                case Enums.Account.Create.Not_Created:
                    responseModel.Message = "Usuario no creado";
                    responseModel.Status = "Ok";
                    break;
                case Enums.Account.Create.Not_Created_Already_Exists:
                    responseModel.Message = "Usuario ya existe registrado";
                    responseModel.Status = "Ok";
                    break;
                case Enums.Account.Create.Not_Created_Rol_Not_Exists:
                    responseModel.Message = "El rol seleccionado no existe";
                    responseModel.Status = "Ok";
                    break;
                case Enums.Account.Create.Internal_Error:
                    responseModel.Message = "Error interno.";
                    responseModel.Status = "Ok";
                    return BadRequest();
                case Enums.Account.Create.Unknown_Error:
                    responseModel.Message = "Error desconocido";
                    responseModel.Status = "Ok";
                    return BadRequest();
                    
            }
            return Ok(responseModel);
        }

        [HttpDelete]
        [Route("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(DeleteModel user)
        {
            try
            {
                Enums.Account.Delete res = await _AccountRepository.Delete(user.UserID);
                UserDeleteResponse deleteResponse = new UserDeleteResponse();
                switch (res)
                {
                    case Enums.Account.Delete.Deleted:
                        deleteResponse.Message = "Usuario eliminado";
                        deleteResponse.Status = "Ok";
                        break;
                    case Enums.Account.Delete.Not_Deleted:
                        deleteResponse.Message = "Usuario no eliminado";
                        deleteResponse.Status = "Ok";
                        break;
                    case Enums.Account.Delete.Error_Deleting:
                        deleteResponse.Message = "Error eliminando usuario";
                        deleteResponse.Status = "Ok";
                        break;
                    case Enums.Account.Delete.Unknown_error:
                        deleteResponse.Message = "Error desconocido";
                        deleteResponse.Status = "Ok";
                        return BadRequest(deleteResponse);
                    case Enums.Account.Delete.User_Not_Found:
                        deleteResponse.Message = "Usuario no encontrado";
                        deleteResponse.Status = "Ok";
                        break;
                }
                return Ok(deleteResponse);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
        
        [HttpPatch]
        [Route("change_password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Change_Password(ChangePasswordModel passwors )
        {
            try
            {
                ChangePasswordResponse response = new ChangePasswordResponse();
                Enums.Account.ChangePassword res = await  _AccountRepository.ChangePassword(passwors);
                switch (res)
                {
                    case Enums.Account.ChangePassword.Changed:
                        response.Message = "Contraseña cambiada correctamente.";
                        response.Status = "Ok";
                        break;
                    case Enums.Account.ChangePassword.Not_Changed:
                        response.Message = "Contraseña no cambiada.";
                        response.Status = "Ok";
                        break;
                    case Enums.Account.ChangePassword.Password_Dont_Match:
                        response.Message = "Las contraseñas no coinciden.";
                        response.Status = "Ok";
                        break;
                    case Enums.Account.ChangePassword.Unknown_Error:
                        response.Message = "Error desconocido";
                        response.Status = "Ok";
                        return BadRequest(response);
                    case Enums.Account.ChangePassword.Password_Verification_Failure:
                        response.Message = "Error el intentar verificar la contraseña informada";
                        response.Status = "Ok";
                        break;
                    case Enums.Account.ChangePassword.Password_Not_Linked:
                        response.Message = "Contraseñas no se corresponden al usuario informado";
                        response.Status = "Ok";
                        break;
                }
                return Ok(response);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}