using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Nancy.Json;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: CongelamientoProyeccionFurService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_CongelamientoProyeccionFur
    /// </summary>
    public class CongelamientoProyeccionFurService : ICongelamientoProyeccionFurService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CongelamientoProyeccionFurService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCongelamientoProyeccionFur, CongelamientoProyeccionFur>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        

        #region Metodos Base
        

        public bool Delete(int id, string usuario)
        {
            try
            {
                _unitOfWork.CongelamientoProyeccionFurRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CongelamientoProyeccionFur> Add(List<CongelamientoProyeccionFur> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CongelamientoProyeccionFurRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CongelamientoProyeccionFur>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CongelamientoProyeccionFur> Update(List<CongelamientoProyeccionFur> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CongelamientoProyeccionFurRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CongelamientoProyeccionFur>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(List<int> listadoIds, string usuario)
        {
            try
            {
                _unitOfWork.CongelamientoProyeccionFurRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor Modificacion: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_CongelamientoProyeccionFur
        /// </summary>
        /// <returns> List<CongelamientoProyeccionFurDTO> </returns>
        public CongelamientoProyeccionFurDTO ObtenerCongelamientoProyeccionFur(int idCabecera)
        {
            try
            {
                var respuesta = _unitOfWork.CongelamientoProyeccionFurRepository.ObtenerCongelamientoProyeccionFur(idCabecera);
                CongelamientoProyeccionFurDTO retorno = new CongelamientoProyeccionFurDTO();
                retorno.IdArea= respuesta.IdArea;
                retorno.IdCabeceraSolicitud = respuesta.IdCabeceraSolicitud;
                retorno.ConfiguracionProyeccionFur = JsonConvert.DeserializeObject<ConfiguracionProyeccionFur>(respuesta.ConfiguracionProyeccionFur);
                retorno.DetalleCabeceraProyeccionFur = JsonConvert.DeserializeObject<List<FurConfiguracionAutomaticaVersionDetalleDTO>>(respuesta.DetalleCabeceraProyeccionFur);

                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
