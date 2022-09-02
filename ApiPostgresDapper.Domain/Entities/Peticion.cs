using System;

namespace ApiPostgresDapper.Domain.Entities
{
    public class Peticion
    {
        public int Id { get; set; }
        public string Request { get; set; }
        public DateTime Fecha_Request { get; set; }
        public string Response { get; set; }
        public DateTime Fecha_Response { get; set; }
        public string Username { get; set; }
    }
}
