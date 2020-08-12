using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.Collections.Generic;

namespace APISorteio.Models
{
    public class Administrador : IdentityUser
    {
        public IEnumerable<Sorteio> SorteioLista { get; set; }
    }
}
