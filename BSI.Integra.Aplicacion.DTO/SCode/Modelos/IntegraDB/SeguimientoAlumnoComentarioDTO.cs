using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class SeguimientoAlumnoComentarioDTO
    {
        public int Id { get; set; }
        public int? IdMatriculaCabecera { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        //public int IdSeguimientoAlumnoCategoria { get; set; }
        public int? IdSeguimientoAlumnoCategoriaPago { get; set; }
        public int? IdSeguimientoAlumnoCategoriaAcademico { get; set; }
        public int IdPersonal { get; set; }
        public int IdOportunidad { get; set; }
        //public string Comentario { get; set; }
        public string ComentarioPago { get; set; }
        public string ComentarioAcademico { get; set; }
        //public DateTime FechaCompromiso { get; set; }
        public string Usuario { get; set; }
    }


    public class TipoSeguimientoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        //public DateTime FechaCompromiso { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        //public DateTime FechaCreacion { get;set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class TipoSeguimientoEntradaDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public string Usuario { get; set; }
    }
}
