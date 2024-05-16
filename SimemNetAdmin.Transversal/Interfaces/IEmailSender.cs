using SimemNetAdmin.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimemNetAdmin.Transversal.Interfaces
{
    public interface IEmailSender
    {
        string SendNotificacions(string bodyData);
    }
}
