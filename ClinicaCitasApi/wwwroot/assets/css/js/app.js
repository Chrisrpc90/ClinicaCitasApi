function getBaseUrl(){
  // usa el mismo host/puerto donde corre la API
  return window.location.origin;
}

async function fetchJson(path){
  const res = await fetch(getBaseUrl() + path);
  if(!res.ok){
    const text = await res.text();
    throw new Error(`Error ${res.status}: ${text}`);
  }
  return res.json();
}

function setText(id, value){
  const el = document.getElementById(id);
  if(el) el.textContent = value;
}

function formatDateTime(iso){
  // iso => "2026-02-26T12:30:00"
  const d = new Date(iso);
  if(Number.isNaN(d.getTime())) return iso;
  return d.toLocaleString();
}

// ---- Dashboard KPIs ----
async function loadDashboard(){
  try{
    const [pacientes, medicos, citas] = await Promise.all([
      fetchJson("/api/pacientes"),
      fetchJson("/api/medicos"),
      fetchJson("/api/citas")
    ]);

    setText("kpiPacientes", pacientes.length);
    setText("kpiMedicos", medicos.length);
    setText("kpiCitas", citas.length);

    const confirmadas = citas.filter(c => (c.estado || "").toLowerCase() === "confirmada").length;
    const programadas = citas.filter(c => (c.estado || "").toLowerCase() === "programada").length;
    const canceladas = citas.filter(c => (c.estado || "").toLowerCase() === "cancelada").length;

    setText("kpiConfirmadas", confirmadas);
    setText("kpiProgramadas", programadas);
    setText("kpiCanceladas", canceladas);
  }catch(err){
    console.error(err);
    setText("dashboardError", "No se pudo cargar datos. Verifica que la API esté corriendo.");
  }
}

// ---- Tablas ----
async function loadPacientes(){
  const tbody = document.getElementById("pacientesBody");
  try{
    const data = await fetchJson("/api/pacientes");
    setText("countPacientes", data.length);
    tbody.innerHTML = data.map(p => `
      <tr>
        <td>${p.id}</td>
        <td><strong>${p.nombres} ${p.apellidos}</strong><div class="muted">DNI: ${p.dni}</div></td>
        <td>${p.telefono ?? "<span class='muted'>—</span>"}</td>
        <td>${p.email ?? "<span class='muted'>—</span>"}</td>
      </tr>
    `).join("");
  }catch(err){
    console.error(err);
    tbody.innerHTML = `<tr><td colspan="4" class="text-danger">Error cargando pacientes</td></tr>`;
  }
}

async function loadMedicos(){
  const tbody = document.getElementById("medicosBody");
  try{
    const data = await fetchJson("/api/medicos");
    setText("countMedicos", data.length);
    tbody.innerHTML = data.map(m => `
      <tr>
        <td>${m.id}</td>
        <td><strong>${m.nombres} ${m.apellidos}</strong></td>
        <td><span class="badge text-bg-primary">${m.especialidad}</span></td>
      </tr>
    `).join("");
  }catch(err){
    console.error(err);
    tbody.innerHTML = `<tr><td colspan="3" class="text-danger">Error cargando médicos</td></tr>`;
  }
}

function estadoBadge(estado){
  const e = (estado || "").toLowerCase();
  if(e === "confirmada") return `<span class="badge-status badge-confirmada">Confirmada</span>`;
  if(e === "cancelada") return `<span class="badge-status badge-cancelada">Cancelada</span>`;
  return `<span class="badge-status badge-programada">Programada</span>`;
}

async function loadCitas(){
  const tbody = document.getElementById("citasBody");
  try{
    const data = await fetchJson("/api/citas");
    setText("countCitas", data.length);
    tbody.innerHTML = data.map(c => `
      <tr>
        <td>${c.id}</td>
        <td><strong>${formatDateTime(c.fechaHora)}</strong><div class="muted">${c.motivo}</div></td>
        <td>
          <div><strong>${c.pacienteNombre ?? c.pacienteId}</strong></div>
          <div class="muted">Paciente ID: ${c.pacienteId}</div>
        </td>
        <td>
          <div><strong>${c.medicoNombre ?? c.medicoId}</strong></div>
          <div class="muted">${c.medicoEspecialidad ?? ""}</div>
        </td>
        <td>${estadoBadge(c.estado)}</td>
      </tr>
    `).join("");
  }catch(err){
    console.error(err);
    tbody.innerHTML = `<tr><td colspan="5" class="text-danger">Error cargando citas</td></tr>`;
  }
}