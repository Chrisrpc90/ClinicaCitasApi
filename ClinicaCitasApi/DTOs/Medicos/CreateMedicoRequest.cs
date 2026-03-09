using System.ComponentModel.DataAnnotations;

public class CreateMedicoRequest
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [MinLength(2, ErrorMessage = "El nombre debe tener al menos 2 caracteres.")]
    public string Nombres { get; set; } = string.Empty;

    [Required(ErrorMessage = "El apellido es obligatorio.")]
    [MinLength(2, ErrorMessage = "El apellido debe tener al menos 2 caracteres.")]
    public string Apellidos { get; set; } = string.Empty;

    [Required(ErrorMessage = "La especialidad es obligatoria.")]
    [MinLength(3, ErrorMessage = "La especialidad debe tener al menos 3 caracteres.")]
    public string Especialidad { get; set; } = string.Empty;
}