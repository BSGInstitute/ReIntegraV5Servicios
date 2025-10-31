using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICampoFormularioOpcionRepository : IGenericRepository<TCampoFormularioOpcion>
    {
        TCampoFormularioOpcion Add(CampoFormularioOpcion entidad);
        IEnumerable<TCampoFormularioOpcion> Add(IEnumerable<CampoFormularioOpcion> listadoEntidad);
    }
}

