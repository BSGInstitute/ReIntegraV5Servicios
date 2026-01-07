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
