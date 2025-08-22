using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using Microsoft.AspNetCore.Http;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IPlantillaSendinblueDatoService
    {
        bool AgregarPlantillaDatos(PlantillaSendinblueInsertarDTO datos, string usuario);
        public List<PlantillaSendinblueDatoDTO> ObtenerDatosPlantilllaPorId(int IdPlantillaSendinblue);
        bool ActualizarPlantillaDatos(PlantillaSendinblueActualizarDTO datos, string usuario);

    }
}
