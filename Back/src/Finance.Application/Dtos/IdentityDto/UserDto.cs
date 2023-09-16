using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finance.Application.Attributes;

namespace Finance.Application.Dtos.IdentityDto
{
    public class UserDto
    {
        [FormatterString(WithoutSpace = true)]
        public string UserName { get; set; }

        [FormatterString]
        public string PrimeiroNome { get; set; }
        [FormatterString]
        public string UltimoNome { get; set; }
        public string PhoneNumber { get; set; }

        [FormatterString]
        public string Descricao { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}