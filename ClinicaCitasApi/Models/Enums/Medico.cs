namespace ClinicaCitasApi.Models
{
    public class Medico
    {
        public int Id { get; private set; }
        public string Nombres { get; private set; } = string.Empty;
        public string Apellidos { get; private set; } = string.Empty;
        public string Especialidad { get; private set; } = string.Empty;

        public Medico(int id, string nombres, string apellidos, string especialidad)
        {
            Id = id;
            SetNombres(nombres);
            SetApellidos(apellidos);
            SetEspecialidad(especialidad);
        }

        public void Update(string nombres, string apellidos, string especialidad)
        {
            SetNombres(nombres);
            SetApellidos(apellidos);
            SetEspecialidad(especialidad);
        }

        private void SetNombres(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Nombres es requerido.");
            Nombres = value.Trim();
        }

        private void SetApellidos(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Apellidos es requerido.");
            Apellidos = value.Trim();
        }

        private void SetEspecialidad(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Especialidad es requerida.");
            Especialidad = value.Trim();
        }
    }
}