using MoneyPlanner.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyPlanner.Service.Interfaces
{
    public interface IUserContext
    {
        UserDTO CurrentUser { get; set; }
    }
}
