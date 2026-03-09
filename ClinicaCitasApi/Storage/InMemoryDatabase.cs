using ClinicaCitasApi.Models;

namespace ClinicaCitasApi.Storage
{
    public class InMemoryDatabase
    {
        // Map para IDs + performance
        public Dictionary<int, Paciente> Pacientes { get; } = new();
        public Dictionary<int, Medico> Medicos { get; } = new();
        public Dictionary<int, Cita> Citas { get; } = new();

        private int _pacienteId = 0;
        private int _medicoId = 0;
        private int _citaId = 0;

        public int NextPacienteId() => ++_pacienteId;
        public int NextMedicoId() => ++_medicoId;
        public int NextCitaId() => ++_citaId;

        public InMemoryDatabase()
        {
            // Seed (opcional, ayuda a probar rápido)
            var p1 = new Paciente(NextPacienteId(), "Luis", "Rodriguez", "12345678", "999999999", "luis@mail.com");
            var p2 = new Paciente(NextPacienteId(), "Ana", "Lopez", "87654321", null, null);
            Pacientes[p1.Id] = p1;
            Pacientes[p2.Id] = p2;

            var m1 = new Medico(NextMedicoId(), "Carlos", "Perez", "Cardiología");
            var m2 = new Medico(NextMedicoId(), "Maria", "Gomez", "Pediatría");
            Medicos[m1.Id] = m1;
            Medicos[m2.Id] = m2;
        }
    }
}