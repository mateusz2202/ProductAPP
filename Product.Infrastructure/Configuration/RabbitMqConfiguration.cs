using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.Configuration;

public class RabbitMqConfiguration
{
    public string HostName { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}
