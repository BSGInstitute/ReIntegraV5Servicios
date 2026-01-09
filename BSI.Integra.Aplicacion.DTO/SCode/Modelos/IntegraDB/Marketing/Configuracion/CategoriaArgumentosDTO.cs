using OfficeOpenXml.Utils;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing.Configuracion
{
    public class ProgramaConfigurado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }

    public class CrearProgramaGeneralConfiguradoDTO
    {
        public int IdProgramaGeneral { get; set; }
        public string Nombre { get; set; }
    }

    public class EditarProgramaConfiguradoDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
    }

    public class ProgramaDetallePlano
    {
        public int ProgramaId { get; set; }
        public int IdProgramaGeneral { get; set; }
        public string NombrePrograma { get; set; }
        public int CategoriaId { get; set; }
        public string NombreCategoria { get; set; }
        public int? ArgumentoId { get; set; }
        public string NombreArgumento { get; set; }
        public string DescripcionArgumento { get; set; }
        public int? PrioridadArgumento { get; set; }
    }
    public class ProgramaConfiguradoDetalleDTO
    {
        public int Id { get; set; }
        public int IdProgramaGeneral { get; set; }
        public string NombrePrograma { get; set; }
        public List<CategoriaArgumentoPorProgramaDTO> CategoriasArgumento { get; set; } = new();
    }
    public class CategoriaArgumentoPorProgramaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<ArgumentoDTO> Argumentos { get; set; } = new();
    }
    public class ArgumentoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Prioridad { get; set; }
    }

    public class CrearArgumentoPorCategoriaDTO
    {
        public int IdProgramaConfigurado { get; set; }
        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public int Prioridad { get; set; }
        public string Descripcion { get; set; }
        public string? UsuarioCreacion { get; set; }
    }
    public class EditarArgumentoPorCategoriaDTO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Prioridad { get; set; }
        public int IdArgumento { get; set; }
        public string? UsuarioModificacion { get; set; }
    }
    public class EliminarArgumentoPorCategoriaDTO
    {
        public int IdProgramaConfigurado { get; set; }
        public int IdArgumento { get; set; }
        public string? UsuarioModificacion { get; set; }
    }

    public class CategoriaArgumento
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }

    public class CrearEditarCategoriaArgumentoDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
    }
}
