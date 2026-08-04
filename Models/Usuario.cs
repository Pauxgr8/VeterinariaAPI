namespace VeterinariaAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public string UsuarioLogin { get; set; }

        public string Clave { get; set; }

        public string Estado { get; set; }

        public DateTime FechaCreacion { get; set; }

        public string UsuarioCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public string? UsuarioModificacion { get; set; }
    }
}