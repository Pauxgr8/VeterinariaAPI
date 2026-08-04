namespace VeterinariaAPI.Models
{
    public class Mascota
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = "";

        public DateTime FechaNacimiento { get; set; }

        public string Sexo { get; set; } = "";

        public decimal Peso { get; set; }

        public string Alergias { get; set; } = "";

        public int PropietarioId { get; set; }

        public DateTime FechaCreacion { get; set; }

        public string UsuarioCreacion { get; set; } = "";

        public DateTime? FechaModificacion { get; set; }

        public string? UsuarioModificacion { get; set; }
    }
}