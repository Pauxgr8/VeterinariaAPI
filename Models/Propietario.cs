namespace VeterinariaAPI.Models

{

    public class Propietario

    {

        public int Id { get; set; }

        public string Identificacion { get; set; } = string.Empty;

        public string PrimerNombre { get; set; } = string.Empty;

        public string SegundoNombre { get; set; } = string.Empty;

        public string PrimerApellido { get; set; } = string.Empty;

        public string SegundoApellido { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;

        public string Correo { get; set; } = string.Empty;

        public DateTime FechaCreacion { get; set; }

        public string UsuarioCreacion { get; set; } = string.Empty;

        public DateTime? FechaModificacion { get; set; }

        public string? UsuarioModificacion { get; set; }

    }

}
