using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion
{
    public class GestionDocenteFlujoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdGestionDocenteEstado { get; set; }
        public int IdGestionDocenteCategoria { get; set; }
        public bool Estado { get; set; }
        public string Usuario { get; set; }
        public int? IdGestionDocenteActividadCabecera { get; set; }
        /// <summary>
        /// IDs de actividades a asociar al flujo. El servicio las procesa tras guardar.
        /// Si está vacío o null, no se realizan asociaciones.
        /// </summary>
        public List<int> ActividadesIds { get; set; }
    }

    public class GestionDocenteActividadCabeceraListaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdGestionDocenteEstado { get; set; }
        public int IdGestionDocenteCategoria { get; set; }
    }

    public class GestionDocenteActividadCabeceraFlujoDTO
    {
        public int Id { get; set; }
        public int IdGestionDocenteFlujo { get; set; }
        public int IdGestionDocenteActividadCabecera { get; set; }
        public bool Estado { get; set; }
        public string Usuario { get; set; }
    }

    public class GestionDocenteEstadoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class GestionDocenteCategoriaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

    }

    public class GestionDocenteSesionDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public class GestionDocenteFlujoOutputDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdGestionDocenteEstado { get; set; }
        public string NombreEstado { get; set; }
        public int IdGestionDocenteCategoria { get; set; }
        public string NombreCategoria { get; set; }
    }

    public class FlujoCompletoDTO
    {
        public GestionDocenteFlujoOutputDTO Flujo { get; set; }
        public List<ActividadCabeceraCompletaDTO> Actividades { get; set; }
    }
}
