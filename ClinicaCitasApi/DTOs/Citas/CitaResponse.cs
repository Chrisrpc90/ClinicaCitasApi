namespace ClinicaCitasApi.Dtos.Citas
{
    public class CitaResponse
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;

        public int PacienteId { get; set; }
        public string PacienteNombre { get; set; } = string.Empty;

        public int MedicoId { get; set; }
        public string MedicoNombre { get; set; } = string.Empty;
        public string MedicoEspecialidad { get; set; } = string.Empty;
    }
}