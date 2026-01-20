using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing.Configuracion;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface.Marketing.Configuracion;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.Configuracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion.Marketing.Configuracion
{
    public class CategoriaArgumentosService : ICategoriaArgumentosService
    {
        private readonly ICategoriaArgumentosRepository categoriaArgumentoReporitory;

        public CategoriaArgumentosService(ICategoriaArgumentosRepository _categoriaArgumentoReporitory)
        {
            categoriaArgumentoReporitory = _categoriaArgumentoReporitory;
        }

        public List<ProgramaConfigurado> ObtenerListadoProgramaConfigurado()
        {
            return categoriaArgumentoReporitory.ObtenerListadoProgramaConfigurado();
        }

        public int CrearProgramaConfigurado(CrearProgramaGeneralConfiguradoDTO request, string usuario)
        {
            return categoriaArgumentoReporitory.CrearProgramaConfigurado(request, usuario);
        }

        public bool EliminarProgramaConfigurado(int id, string usuario)
        {
            return categoriaArgumentoReporitory.EliminarProgramaConfigurado(id, usuario);
        }

        public ProgramaConfiguradoDetalleDTO ObtenerProgramaConfiguradoDetalle(int id)
        {
            return categoriaArgumentoReporitory.ObtenerProgramaConfiguradoDetalle(id);
        }

        public bool AgregarArgumentoPorCategoria(CrearArgumentoPorCategoriaDTO request, string usuario)
        {
            return categoriaArgumentoReporitory.AgregarArgumentoPorCategoria(request, usuario);
        }

        public bool EditarArgumentoPorCategoria(EditarArgumentoPorCategoriaDTO request, string usuario)
        {
            return categoriaArgumentoReporitory.EditarArgumentoPorCategoria(request, usuario);
        }

        public bool EliminarArgumentoPorCategoria(EliminarArgumentoPorCategoriaDTO request, string usuario)
        {
            return categoriaArgumentoReporitory.EliminarArgumentoPorCategoria(request, usuario);
        }

        public List<ProgramaGeneralComboDTO> ObtenerListadoProgramaGeneralValido()
        {
            return categoriaArgumentoReporitory.ObtenerListadoProgramaGeneralValido();
        }

        public List<CategoriaArgumento> ObtenerListadoCategoriaArgumento()
        {
            return categoriaArgumentoReporitory.ObtenerListadoCategoriaArgumento();
        }

        public bool CrearCategoriaArgumento(CrearEditarCategoriaArgumentoDTO request, string usuario)
        {
            return categoriaArgumentoReporitory.CrearCategoriaArgumento(request, usuario);
        }

        public bool EditarCategoriaArgumento(CrearEditarCategoriaArgumentoDTO request, string usuario)
        {
            return categoriaArgumentoReporitory.EditarCategoriaArgumento(request, usuario);
        }

        public bool EliminarCategoriaArgumento(int id, string usuario)
        {
            return categoriaArgumentoReporitory.EliminarCategoriaArgumento(id, usuario);
        }
    }
}
