using Microsoft.AspNetCore.Authorization;

namespace FilmesAPI.Authorization;

public class IdadeMinima: IAuthorizationRequirement
{
    public IdadeMinima(int idade)
    {
        Idade = idade;
    }
    public int Idade { get; set; }
}
