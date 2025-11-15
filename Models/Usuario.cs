using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace FinControl.API.Models
{
    public class Usuario : IdentityUser
    {

        public required string Nome { get; set; }


    }

}
