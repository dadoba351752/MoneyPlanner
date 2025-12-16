using MoneyPlanner.Service.DTO;
using MoneyPlanner.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyPlanner.Service.Context
{
    public class UserContext : IUserContext
    {
        public UserDTO CurrentUser { get; set; }
    }
}
