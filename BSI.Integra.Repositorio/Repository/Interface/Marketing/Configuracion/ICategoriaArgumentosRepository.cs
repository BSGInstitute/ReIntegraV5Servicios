using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing.Configuracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing.Configuracion
{
    public interface ICategoriaArgumentosRepository
    {
        List<ProgramaConfigurado> ObtenerListadoProgramaConfigurado();
        int CrearProgramaConfigurado(CrearProgramaGeneralConfiguradoDTO request, string usuario);
        bool EliminarProgramaConfigurado(int id, string usuario);

        ProgramaConfiguradoDetalleDTO ObtenerProgramaConfiguradoDetalle(int id);

        bool AgregarArgumentoPorCategoria(CrearArgumentoPorCategoriaDTO request, string usuario);
        bool EditarArgumentoPorCategoria(EditarArgumentoPorCategoriaDTO request, string usuario);
        bool EliminarArgumentoPorCategoria(EliminarArgumentoPorCategoriaDTO request, string usuario);

        List<ProgramaGeneralComboDTO> ObtenerListadoProgramaGeneralValido();

        List<CategoriaArgumento> ObtenerListadoCategoriaArgumento();
        bool CrearCategoriaArgumento(CrearEditarCategoriaArgumentoDTO request, string usuario);
        bool EditarCategoriaArgumento(CrearEditarCategoriaArgumentoDTO request, string usuario);
        bool EliminarCategoriaArgumento(int id, string usuario);
    }
}
