using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IModuloSistemaPaqueteService
    {
        bool Insertar(ModuloSistemaPaqueteV5DTO dto, string usuario);
        bool Actualizar(ModuloSistemaPaqueteV5DTO dto, string usuario);
        bool Eliminar(int id, string usuario);
        IEnumerable<ModuloSistemaPaqueteV5DTO> Obtener();
        IEnumerable<ModuloSistemaPaqueteModulosV5DTO> ObtenerModulos(int idPaquete);
        IEnumerable<ModuloSistemaPaqueteModulosV5DTO> ObtenerListaModulos(int idPaquete);
    }
}
