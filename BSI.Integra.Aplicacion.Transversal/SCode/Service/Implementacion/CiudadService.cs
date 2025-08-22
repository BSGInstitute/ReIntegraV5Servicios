using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Configuracion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: CiudadService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_Ciudad
    /// </summary>
    public class CiudadService : ICiudadService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CiudadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TCiudad, Ciudad>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        public Ciudad Insertar(CiudadEnvioDTO data, string usuario)
        {
            try
            {
                var repCiudad = _unitOfWork.CiudadRepository;
                Ciudad entidad = new Ciudad();
                entidad.Id = 0;
                entidad.Codigo = data.Codigo;
                entidad.Nombre = data.Nombre;
                entidad.IdPais = data.IdPais;
                entidad.LongCelular = data.LongCelular;
                entidad.LongTelefono = data.LongTelefono;
                entidad.LongCelularAlterno = data.LongCelularAlterno;
                entidad.FechaCreacion = DateTime.Now;
                entidad.FechaModificacion = DateTime.Now;
                entidad.UsuarioCreacion = usuario;
                entidad.UsuarioModificacion = usuario;
                entidad.Estado = true;


                var modelo = _unitOfWork.CiudadRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Ciudad>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Ciudad Actualizar(CiudadEnvioDTO data, string usuario)
        {
            try
            {
                Ciudad entidad = new Ciudad();
                entidad = _unitOfWork.CiudadRepository.ObtenerPorId(data.Id);
                entidad.Codigo = data.Codigo;
                entidad.Nombre = data.Nombre;
                entidad.IdPais = data.IdPais;
                entidad.LongCelular = data.LongCelular;
                entidad.LongTelefono = data.LongTelefono;
                entidad.LongCelularAlterno = data.LongCelularAlterno;
                entidad.FechaCreacion = DateTime.Now;
                entidad.FechaModificacion = DateTime.Now;
                entidad.UsuarioCreacion = usuario;
                entidad.UsuarioModificacion = usuario;
                entidad.Estado = true;

                var modelo = _unitOfWork.CiudadRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Ciudad>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ActualizarCiudadMultiple(CiudadMultipleDTO dtos, string usuario)
        {
            try
            {
                if (dtos != null && dtos.IdsCiudades.Count() > 0)
                {
                    var idsUnidos = String.Join(",", dtos.IdsCiudades);
                    List<Ciudad> entidades = new List<Ciudad>();
                    var lista = _unitOfWork.CiudadRepository.ObtenerPorIds(idsUnidos);

                    if (lista != null && lista.Count() > 0)
                    {
                        foreach (var item in dtos.IdsCiudades)
                        {
                            var entidad = lista.FirstOrDefault(x => x.Id == item);
                            if (entidad != null && entidad.Id != 0)
                            {
                                entidad.IdPais = dtos.IdPais;
                                entidad.LongCelular = dtos.LongCelular;
                                entidad.LongTelefono = dtos.LongTelefono;
                                entidad.UsuarioModificacion = usuario;
                                entidad.FechaModificacion = DateTime.Now;
                                entidades.Add(entidad);
                            }
                        }
                        var respuesta = _unitOfWork.CiudadRepository.Update(entidades);
                        _unitOfWork.Commit();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #region Metodos Base

        public bool Eliminar(int id, string usuario)
        {
            try
            {
                var ciudad = _unitOfWork.CiudadRepository.ObtenerPorId(id);
                if (ciudad != null && ciudad.Codigo != 0)
                {
                    var resultado = _unitOfWork.CiudadRepository.Delete(id, usuario);
                    _unitOfWork.Commit();
                    return resultado;
                }
                else
                {
                    throw new BadRequestException($"No se encontro la entidad con el id {id}");
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Ciudad> Insertar(List<Ciudad> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CiudadRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Ciudad>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Ciudad> Actualizar(List<Ciudad> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CiudadRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Ciudad>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Eliminar(List<int> listadoIds, string usuario)
        {
            try
            {
                _unitOfWork.CiudadRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Ciudad para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<CiudadComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.CiudadRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Ciudad
        /// </summary>
        /// <returns> List<CiudadDTO> </returns>
        public IEnumerable<CiudadDTO> ObtenerCiudad()
        {
            try
            {
                return _unitOfWork.CiudadRepository.ObtenerCiudad();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 07/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Nombre de la Ciudad asociado a un Id
        /// </summary>
        /// <param name="idCiudad">Id de la Ciudad</param>
        /// <returns> ValorStringDTO </returns>
        public StringDTO ObtenerNombreCiudadPorId(int idCiudad)
        {
            try
            {
                return _unitOfWork.CiudadRepository.ObtenerNombreCiudadPorId(idCiudad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Griselberto Huaman
        /// Fecha: 07/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_RegionCiudad para mostrarse en combo.
        /// </summary>
        /// <returns> RegionCiudadComboDTO </returns>
        public IEnumerable<RegionCiudadComboDTO> ObtenerComboRegionCiudad()
        {
            try
            {
                return _unitOfWork.CiudadRepository.ObtenerComboRegionCiudad();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 07/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las ciudades de cada sede BSG, para mostrarse en combo.
        /// </summary>
        /// <returns> RegionCiudadComboDTO </returns>
        public IEnumerable<DTO.ComboDTO> ObtenerCiudadesDeSedesExistentes()
        {
            try
            {
                return _unitOfWork.CiudadRepository.ObtenerCiudadesDeSedesExistentes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public IEnumerable<CiudadEnvioDTO> ObtenerNombreCiudadPorIdPais(int idPais)
        {
            try
            {
                return _unitOfWork.CiudadRepository.ObtenerNombreCiudadPorIdPais(idPais);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO.ComboDTO> ObtenerCiudadesPorPais(string idPais)
        {
            try
            {
                return _unitOfWork.CiudadRepository.ObtenerCiudadesPorPais(idPais);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Christian Quispe
        /// Fecha: 24/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Ciudad para mostrarse en grilla.
        /// </summary>
        /// <returns> List<CiudadAlternoDTO> </returns>
        public IEnumerable<CiudadAlternoDTO> ObtenerTodoCiudades()
        {
            try
            {
                return _unitOfWork.CiudadRepository.ObtenerTodoCiudades();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Max Mantilla.
        /// Fecha: 20/07/2023
        /// Version: 1.0
        /// <summary>
		/// Obtiene lista de ciudades con denominacion BS para programa especifico
		/// </summary>
		/// <returns>List<FiltroDTO></returns>
		public List<FiltroDTO> ObtenerListaCiudadesBs()
        {
            try
            {
                return _unitOfWork.CiudadRepository.ObtenerListaCiudadesBs();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 14/05/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de municipios por ciudad
        /// </summary>
        /// <returns>List<MunicipioDTO></returns>
        public List<ComboDTO> ObtenerMunicipioPorCiudad(int idCiudadRef)
        {
            try
            {
                return _unitOfWork.MunicipioRepository.ObtenerMunicipioPorCiudad(idCiudadRef);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 14/05/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de municipios por ciudad
        /// </summary>
        /// <returns>List<MunicipioDTO></returns>
        public List<ComboDTO> ObtenerMunicipioPorEstadoyCiudad(int idCiudadRef, int? idCiudadMexico)
        {
            try
            {
                return _unitOfWork.MunicipioRepository.ObtenerMunicipioPorEstadoyCiudad(idCiudadRef, idCiudadMexico);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 14/05/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de municipios por ciudad
        /// </summary>
        /// <returns>List<MunicipioDTO></returns>
        public List<AsentamientoMunicipioDTO> ObtenerAsentamientoPorMunicipio(int idCiudadRef, int idMunicipioMexico)
        {
            try
            {
                return _unitOfWork.AsentamientoMunicipioRepository.ObtenerAsentamientoPorMunicipio(idCiudadRef, idMunicipioMexico);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 14/05/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de municipios por ciudad
        /// </summary>
        /// <returns>List<MunicipioDTO></returns>
        public List<AsentamientoMunicipioDTO> ObtenerAsentamientoPorMunicipioyCiudadMexico(int idCiudadRef, int idMunicipioMexico, int? idCiudadMexico)
        {
            try
            {
                return _unitOfWork.AsentamientoMunicipioRepository.ObtenerAsentamientoPorMunicipioyCiudadMexico(idCiudadRef, idMunicipioMexico, idCiudadMexico);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 04/06/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de ciudadMexico del Estado Mexico
        /// </summary>
        /// <returns>List<MunicipioDTO></returns>
        public List<ComboDTO> ObtenerCiudadMexicoByIdEstadoMexico(int idCiudadRef)
        {
            try
            {
                return _unitOfWork.CiudadRepository.ObtenerCiudadMexicoByIdEstadoMexico(idCiudadRef);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 14/05/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de municipios por ciudad
        /// </summary>
        /// <returns>List<MunicipioDTO></returns>
        public List<DatosCodigoPostalDTO> BusquedaPorCodigoPostal(string codigoPostal)
        {
            try
            {
                return _unitOfWork.AsentamientoMunicipioRepository.BusquedaPorCodigoPostal(codigoPostal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
