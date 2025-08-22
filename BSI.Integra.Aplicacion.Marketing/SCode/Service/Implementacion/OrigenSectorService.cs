using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion
{
    /// Service: OrigenSectorService
    /// Autor: Edson Daniel Mayta Escobedo
    /// Fecha: 26/08/2022
    /// <summary>
    /// Gestión general de T_OrigenSector
    /// </summary>
    public class OrigenSectorService : IOrigenSectorService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;


        public OrigenSectorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TOrigenSector, OrigenSector>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public OrigenSector Add(OrigenSector entidad)
        {
            try
            {
                var modelo = _unitOfWork.OrigenSectorRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<OrigenSector>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OrigenSector Update(OrigenSector entidad)
        {
            try
            {
                var modelo = _unitOfWork.OrigenSectorRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<OrigenSector>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int id, string usuario)
        {
            try
            {
                _unitOfWork.OrigenSectorRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OrigenSector> Add(List<OrigenSector> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.OrigenSectorRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<OrigenSector>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OrigenSector> Update(List<OrigenSector> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.OrigenSectorRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<OrigenSector>>(modelo);
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
                _unitOfWork.OrigenSectorRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los proveedores de origenes asignados y no asignados
        /// </summary>
        /// <returns>List<ActividadAgendaDTO></returns>
        public OrigenSectorContadoDTO ObtenerAsignacionOrigen()
        {
            try
            {
                OrigenSectorContadoDTO listaOrigenContado = new OrigenSectorContadoDTO();
                listaOrigenContado.ListaOrigenSectorConfigurado = _unitOfWork.OrigenSectorRepository.ObtenerOrigenConfigurado();
                listaOrigenContado.ContadorConfigurado = listaOrigenContado.ListaOrigenSectorConfigurado.Count();
                listaOrigenContado.ListaOrigenSectorNoConfigurado = _unitOfWork.OrigenSectorRepository.ObtenerOrigenNoConfigurado();
                listaOrigenContado.ContadorNoConfigurado = listaOrigenContado.ListaOrigenSectorNoConfigurado.Count();
                return listaOrigenContado;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los proveedores de origenes asignados y no asignados
        /// </summary>
        /// <returns>List<ActividadAgendaDTO></returns>
        public List<OrigenSectorConfiguradoDTO> ObtenerOrigenSector()
        {
            try
            {
                List<OrigenSectorConfiguradoDTO> listaOrigenSectorConfigurado = new List<OrigenSectorConfiguradoDTO>();
                listaOrigenSectorConfigurado = _unitOfWork.OrigenSectorRepository.ObtenerOrigenSectorConfigurado();
                return listaOrigenSectorConfigurado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// Elimina las configuraciones de un sector y asigna las configuraciones por defecto
        /// </summary>
        /// <returns>List<ActividadAgendaDTO></returns>
        public bool EliminarSector(int IdOrigenSector, string UsuarioModificacion)
        {
            bool flagOrigenSector = false;
            try
            {
                try
                {
                    flagOrigenSector = _unitOfWork.OrigenSectorRepository.EliminarOrigenSector(IdOrigenSector, UsuarioModificacion);
                    if (flagOrigenSector == true)
                    {
                        _unitOfWork.Commit();
                        _unitOfWork.OrigenSectorRepository.EliminarOportunidadConfiguracion();
                    }

                }
                catch
                {
                }

                return flagOrigenSector;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flagOrigenSector;
        }


        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza La configuracion de las categoria origen
        /// </summary>
        /// <returns>List<ActividadAgendaDTO></returns>
        public bool ActualizarDatosDeConfiguracion(List<ActualizarDatosDeConfiguracionDTO> ListaConfiguracionActualizada)
        {
            bool flagOrigenSector = false;
            try
            {
                try
                {
                    flagOrigenSector = _unitOfWork.OrigenSectorRepository.ActualizarDatosDeConfiguracion(ListaConfiguracionActualizada);

                    if (flagOrigenSector == true)
                    {
                        _unitOfWork.Commit();
                        _unitOfWork.OrigenSectorRepository.EliminarOportunidadConfiguracion();
                    }
                }
                catch
                {
                }

                return flagOrigenSector;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flagOrigenSector;
        }
        public bool ActualizarDatosDeConfiguracionAgrupados(ActualizarDatosDeConfiguracionAgrupadoDTO ActualizarConfiguracionDatosAgrupados)
        {
            bool flagOrigenSector = false;
            try
            {

                List<ActualizarDatosDeConfiguracionDTO> ListaDeActualizacion = new List<ActualizarDatosDeConfiguracionDTO>();
                var configOrigenSector = new MapperConfiguration(cfg => cfg.CreateMap<ActualizarDatosDeConfiguracionAgrupadoDTO, ActualizarDatosDeConfiguracionDTO>(MemberList.None).ReverseMap());
                _mapper = new Mapper(configOrigenSector);
                List<ListaIdCategoriaOrigenDTO> rpta = new List<ListaIdCategoriaOrigenDTO>();
                rpta = _unitOfWork.OrigenSectorRepository.ObtenerOrigenDatoCalidadDetalle(ActualizarConfiguracionDatosAgrupados.idOrigenSector);
                foreach (var idOrigenDatoCalidadDetalle in rpta)
                {
                    ActualizarDatosDeConfiguracionDTO ConfiguracionCategoriaOirgen = new ActualizarDatosDeConfiguracionDTO();

                    ConfiguracionCategoriaOirgen.idorigendatoCalidad = idOrigenDatoCalidadDetalle.Id;
                    ConfiguracionCategoriaOirgen.DatosCalidad = ActualizarConfiguracionDatosAgrupados.DatosCalidad;
                    ConfiguracionCategoriaOirgen.MuyAltaAd = ActualizarConfiguracionDatosAgrupados.MuyAltaAd;
                    ConfiguracionCategoriaOirgen.MuyAltaAr = ActualizarConfiguracionDatosAgrupados.MuyAltaAr;
                    ConfiguracionCategoriaOirgen.AltaAd = ActualizarConfiguracionDatosAgrupados.AltaAd;
                    ConfiguracionCategoriaOirgen.AltaAr = ActualizarConfiguracionDatosAgrupados.AltaAr;
                    ConfiguracionCategoriaOirgen.MediaAd = ActualizarConfiguracionDatosAgrupados.MediaAd;
                    ConfiguracionCategoriaOirgen.MediaAr = ActualizarConfiguracionDatosAgrupados.MediaAr;
                    ConfiguracionCategoriaOirgen.UsuarioModificacion = ActualizarConfiguracionDatosAgrupados.UsuarioModificacion;


                    ListaDeActualizacion.Add(ConfiguracionCategoriaOirgen);
                }
                flagOrigenSector = ActualizarDatosDeConfiguracion(ListaDeActualizacion);
                _unitOfWork.OrigenSectorRepository.EliminarOportunidadConfiguracion();

                return flagOrigenSector;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flagOrigenSector;
        }


        ///// Autor: Edson Daniel Mayta Escobedo
        ///// Fecha: 19/09/2022
        ///// Version: 1.0
        ///// <summary>
        ///// Actualiza La configuracion de las categoria origen
        ///// </summary>
        ///// <returns>List<ActividadAgendaDTO></returns>
        //public bool InsertarOrigenSector(string Nombre, string Descripcion, int Orden, String UsuarioCreacion)
        //{
        //    bool? RespuestaBool = new bool();
        //    try
        //    {
        //        RespuestaBool = _unitOfWork.OrigenSectorRepository.InsertarOrigenSector(Nombre, Descripcion, Orden, UsuarioCreacion);
        //        return RespuestaBool;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return RespuestaBool;
        //}

    }
}
