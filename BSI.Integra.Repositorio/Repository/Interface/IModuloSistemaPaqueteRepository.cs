using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IModuloSistemaPaqueteRepository : IGenericRepository<TModuloSistemaPaqueteV5>
    {
        TModuloSistemaPaqueteV5 Add(TModuloSistemaPaqueteV5 entidad);
        TModuloSistemaPaqueteV5 Update(TModuloSistemaPaqueteV5 entidad);
        public bool Delete(int id, string usuario);
        public TModuloSistemaPaqueteV5 ObtenerPorId(int id);
        IEnumerable<ModuloSistemaPaqueteV5DTO> Obtener();
        IEnumerable<ModuloSistemaPaqueteModulosV5DTO> ObtenerModulos(int idPaquete);
        IEnumerable<ModuloSistemaPaqueteModulosV5DTO> ObtenerListaModulos(int idPaquete);
    }
}
