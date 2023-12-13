using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace FilmesAPI.Authorization;

public class IdadeAuthorization : AuthorizationHandler<IdadeMinima>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IdadeMinima requirement)
    {
        Claim? dataNascimentoClaim = context.User.FindFirst(claim => claim.Type == ClaimTypes.DateOfBirth);

        if (dataNascimentoClaim == null) 
        {  
            return Task.CompletedTask; 
        }

        DateTime dataDeNascimento = Convert.ToDateTime(dataNascimentoClaim.Value);

        int idadeUsuario = DateTime.Today.Year -  dataDeNascimento.Year;

        if(dataDeNascimento > DateTime.Today.AddYears(-idadeUsuario))
        {
            idadeUsuario--;
        }

        if(idadeUsuario >= requirement.Idade)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
