using ClinicaCitasApi.Models.Enums;

namespace ClinicaCitasApi.Models
{
    public class Cita
    {
        public int Id { get; private set; }
        public DateTime FechaHora { get; private set; }
        public string Motivo { get; private set; } = string.Empty;
        public EstadoCita Estado { get; private set; } = EstadoCita.Programada;

        public int PacienteId { get; private set; }
        public int MedicoId { get; private set; }

        public Cita(int id, DateTime fechaHora, string motivo, int pacienteId, int medicoId)
        {
            Id = id;
            SetFechaHora(fechaHora);
            SetMotivo(motivo);
            PacienteId = pacienteId;
            MedicoId = medicoId;
        }

        public void Update(DateTime fechaHora, string motivo, int pacienteId, int medicoId)
        {
            EnsureNotCanceled();
            SetFechaHora(fechaHora);
            SetMotivo(motivo);
            PacienteId = pacienteId;
            MedicoId = medicoId;
        }

        public void Confirmar()
        {
            EnsureNotCanceled();
            Estado = EstadoCita.Confirmada;
        }

        public void Cancelar()
        {
            Estado = EstadoCita.Cancelada;
        }

        private void SetMotivo(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Motivo es requerido.");
            if (value.Trim().Length > 120) throw new ArgumentException("Motivo máximo 120 caracteres.");
            Motivo = value.Trim();
        }

        private void SetFechaHora(DateTime value)
        {
            if (value <= DateTime.Now) throw new ArgumentException("La fecha/hora debe ser futura.");
            FechaHora = value;
        }

        private void EnsureNotCanceled()
        {
            if (Estado == EstadoCita.Cancelada)
                throw new InvalidOperationException("No se puede modificar una cita cancelada.");
        }
    }
}