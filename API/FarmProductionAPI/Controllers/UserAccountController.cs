using FarmProductionAPI.Core.Commands.ExportCommand;
using FarmProductionAPI.Core.Commands.UserAccountCommand;
using FarmProductionAPI.Core.Queries.UserAccountQuery;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.ExportModels;
using FarmProductionAPI.Domain.Response;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FarmProductionAPI.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    public class UserAccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _config;

        public UserAccountController(IMediator mediator, IConfiguration config)
        {
            _mediator = mediator;
            _config = config;
        }

        [HttpPost]
        [Route("get-by-request")]
        public async Task<ResponseResultAPI<List<UserAccountDTO>>> GetByRequest([FromBody] GetListUserAccountQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return result;
        }

        [HttpPost]
        public async Task<ResponseResultAPI<UserAccountDTO>> Save([FromBody] SaveUserAccountCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result;
        }

        [HttpDelete("{id:guid}")]
        public async Task<ResponseResultAPI<UserAccountDTO>> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteUserAccountCommand(id));
            return result;
        }

        [HttpPost]
        [Route("update-info")]
        public async Task<ResponseResultAPI<UserAccountDTO>> UpdateInfo([FromBody] UpdateInfoCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result;
        }

        [HttpPost]
        [Route("change-pass")]
        public async Task<ResponseResultAPI<UserAccountDTO>> ChangePass([FromBody] ChangePasswordCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<ResponseResultAPI<UserAccountDTO>> Login([FromBody] LoginCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            if (result.Data != null)
            {
                result.Data.Token = GenerateToken(result.Data);
            }
            return result;
        }

        private string GenerateToken(UserAccountDTO user)
        {
            var _key = _config["Jwt:Key"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var role = user.Role != null ? user.Role.Code : "";
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserName),
                new Claim(ClaimTypes.Role, role)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpGet]
        [Route("export")]
        public async Task<ResponseResultAPI<byte[]>> ExportUserAccount([FromQuery] ExportExcelCommand<UserAccountExport> command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result;
        }
    }
}
