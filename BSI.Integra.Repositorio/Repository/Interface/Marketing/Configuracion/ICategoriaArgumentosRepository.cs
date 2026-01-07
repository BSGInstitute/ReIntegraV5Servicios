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
        List<CategoriaArgumento> ObtenerListadoCategoriaArgumento();
        bool CrearCategoriaArgumento(CrearEditarCategoriaArgumentoDTO request, string usuario);
        bool EditarCategoriaArgumento(CrearEditarCategoriaArgumentoDTO request, string usuario);
        bool EliminarCategoriaArgumento(int id, string usuario);
    }
}
