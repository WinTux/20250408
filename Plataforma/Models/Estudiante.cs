﻿namespace Plataforma.Models
{
    public class Estudiante
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string correo { get; set; }
        public int telefono { get; set; } // string o int ?
    }
}
