namespace VeterinariaAPI.Models
{
    public class HojaClinica
    {
        public int Id { get; set; }

        public DateTime FechaAtencion { get; set; }

        public string Sintomas { get; set; } = "";

        public string Diagnostico { get; set; } = "";

        public string Tratamiento { get; set; } = "";

        public int MascotaId { get; set; }

        public DateTime FechaCreacion { get; set; }

        public string UsuarioCreacion { get; set; } = "";

        public DateTime? FechaModificacion { get; set; }

        public string? UsuarioModificacion { get; set; }
    }
}