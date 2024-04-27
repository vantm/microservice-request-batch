using System.ComponentModel.DataAnnotations;

namespace LocationApi;

public class RabbitMqOptions
{
    public const string Name = "RabbitMq";

    [Required]
    public string Host { get; set; } = "localhost";

    [Required]
    public ushort Port { get; set; } = 5672;

    [Required]
    public string VirtualHost { get; set; } = "/";

    [Required]
    public string Username { get; set; } = "";

    [Required]
    public string Password { get; set; } = "";
}
