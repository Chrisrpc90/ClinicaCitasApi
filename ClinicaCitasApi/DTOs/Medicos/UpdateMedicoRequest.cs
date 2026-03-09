using System.ComponentModel.DataAnnotations;

namespace ClinicaCitasApi.Dtos.Medicos
{
    public class UpdateMedicoRequest
    {
        [Required, StringLength(60, MinimumLength = 2)]
        public string Nombres { get; set; } = string.Empty;

        [Required, StringLength(60, MinimumLength = 2)]
        public string Apellidos { get; set; } = string.Empty;

        [Required, StringLength(60, MinimumLength = 2)]
        public string Especialidad { get; set; } = string.Empty;
    }
}