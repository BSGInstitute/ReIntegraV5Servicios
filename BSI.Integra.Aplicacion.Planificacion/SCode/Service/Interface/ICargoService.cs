using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface ICargoService
    {
        //#region Metodos Base
        //Cargo Add(Cargo entidad);
        //Cargo Update(Cargo entidad);
        //bool Delete(int id, string usuario);

        //List<Cargo> Add(List<Cargo> listadoEntidad);
        //List<Cargo> Update(List<Cargo> listadoEntidad);
        //bool Delete(List<int> listadoIds, string usuario);
        //#endregion

        CargoDTO Actualizar(CargoDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
        CargoDTO Insertar(CargoDTO dto, string usuario);
        CargoDTO ObtenerPorId(int id);
        List<CargoDTO> Obtener();
        IEnumerable<ComboDTO> ObtenerCombo();

    }
}
