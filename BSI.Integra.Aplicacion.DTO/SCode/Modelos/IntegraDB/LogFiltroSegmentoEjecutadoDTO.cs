using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Aplicacion.DTOs;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class LogFiltroSegmentoEjecutadoDTO
    {
        public int Id { get; set; }
        //public int IdCentroCosto { get; set; }
        //public int IdOrigen { get; set; }
        //public int IdTipoDato { get; set; }
        //public int IdFaseOportunidad { get; set; }
        public string NombreCentroCosto { get; set; }
        public string NombreOrigen { get; set; }
        public string NombreTipoDato { get; set; }
        public string NombreFaseOportunidad { get; set; }
        public int TotalOportunidadesCreadas { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }


    public class LogFiltroSegmentoEjecutadosDTO
    {
        public readonly bool TieneErrores;
       


        public int Id { get; set; }

        public int IdFiltroSegmento { get; set; }

        public int IdCentroCosto { get; set; }

        public int IdTipoDato { get; set; }

        public int IdOrigen { get; set; }

        public int IdFaseOportunidad { get; set; }

        public int TotalOportunidadesCreadas { get; set; }

        public bool Estado { get; set; }

        public string UsuarioCreacion { get; set; } = null!;

        public string UsuarioModificacion { get; set; } = null!;

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }

        public byte[] RowVersion { get; set; } = null!;

        public int? IdMigracion { get; set; }

        public List<Error> ListaErrores { get; set; } = new List<Error>();

        public bool HasErrors
        {
            get
            {
                return ListaErrores != null && ListaErrores.Any();
            }
        }
       
    }
    public class ResultFiltroSeg
    {
        public string Message { get; set; }
        public int Count { get; set; }
    }
   

    public class FacebookCuentaPublicitariaDTO
    {
        public int Id { get; set; }
        public string FacebookIdCuentaPublicitaria { get; set; }
        public string Nombre { get; set; }
    }
}

