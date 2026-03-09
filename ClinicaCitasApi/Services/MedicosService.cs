using ClinicaCitasApi.Dtos.Medicos;
using ClinicaCitasApi.Models;
using ClinicaCitasApi.Storage;

namespace ClinicaCitasApi.Services
{
    public class MedicosService : Services.Interfaces.IMedicosService
    {
        private readonly InMemoryDatabase _db;

        public MedicosService(InMemoryDatabase db) => _db = db;

        public IEnumerable<MedicoResponse> GetAll()
            => _db.Medicos.Values.Select(ToResponse).OrderBy(x => x.Id);

        public MedicoResponse GetById(int id)
        {
            if (!_db.Medicos.TryGetValue(id, out var m))
                throw new KeyNotFoundException($"Médico {id} no existe.");
            return ToResponse(m);
        }

        public MedicoResponse Create(CreateMedicoRequest request)
        {
            var medico = new Medico(_db.NextMedicoId(), request.Nombres, request.Apellidos, request.Especialidad);
            _db.Medicos[medico.Id] = medico;
            return ToResponse(medico);
        }

        public MedicoResponse Update(int id, UpdateMedicoRequest request)
        {
            if (!_db.Medicos.TryGetValue(id, out var medico))
                throw new KeyNotFoundException($"Médico {id} no existe.");

            medico.Update(request.Nombres, request.Apellidos, request.Especialidad);
            return ToResponse(medico);
        }

        public void Delete(int id)
        {
            if (!_db.Medicos.Remove(id))
                throw new KeyNotFoundException($"Médico {id} no existe.");
        }

        private static MedicoResponse ToResponse(Medico m) => new()
        {
            Id = m.Id,
            Nombres = m.Nombres,
            Apellidos = m.Apellidos,
            Especialidad = m.Especialidad
        };
    }
}