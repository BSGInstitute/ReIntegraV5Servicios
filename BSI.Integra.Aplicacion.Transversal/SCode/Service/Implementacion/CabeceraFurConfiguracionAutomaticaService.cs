using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: CabeceraFurConfiguracionAutomaticaService
    /// Autor: Griselberto Huaman.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_CabeceraFurConfiguracionAutomatica
    /// </summary>
    public class CabeceraFurConfiguracionAutomaticaService  
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CabeceraFurConfiguracionAutomaticaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCabeceraFurConfiguracionAutomatica, CabeceraFurConfiguracionAutomatica>(MemberList.None).ReverseMap();
                cfg.CreateMap<CabeceraFurConfiguracionAutomaticaFrontDTO, CabeceraFurConfiguracionAutomatica>(MemberList.None).ReverseMap();
            }
           );

         
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public CabeceraFurConfiguracionAutomatica Add(CabeceraFurConfiguracionAutomaticaFrontDTO data)
        {
            try
            {
                var rep = _unitOfWork.CabeceraFurConfiguracionAutomaticaRepository;
                var entidad = _mapper.Map<CabeceraFurConfiguracionAutomatica>(data);
                entidad.Id = 0;
                entidad.IdEstadoProyeccionFur = 1;// Estado Creado
                entidad.UsuarioCreacion = data.Usuario;
                entidad.UsuarioModificacion = data.Usuario;
                entidad.FechaCreacion = DateTime.Now;
                entidad.FechaModificacion = DateTime.Now;
                entidad.Estado = true;

                var modelo = rep.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CabeceraFurConfiguracionAutomatica>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CabeceraFurConfiguracionAutomatica Update(CabeceraFurConfiguracionAutomaticaFrontDTO data)
        {
            try
            {
                var rep = _unitOfWork.CabeceraFurConfiguracionAutomaticaRepository;
                var entidad = _mapper.Map<CabeceraFurConfiguracionAutomatica>(rep.ObtenerCabeceraFurConfiguracionAutomaticaById(data.Id));
                entidad.FechaModificacion = DateTime.Now;
                entidad.UsuarioModificacion = data.Usuario;
                entidad.Nombre = data.Nombre;
                entidad.Codigo = data.Codigo;
                entidad.Observacion = data.Observacion;

                var modelo = _unitOfWork.CabeceraFurConfiguracionAutomaticaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CabeceraFurConfiguracionAutomatica>(modelo);
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
                _unitOfWork.CabeceraFurConfiguracionAutomaticaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CabeceraFurConfiguracionAutomatica> Add(List<CabeceraFurConfiguracionAutomatica> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CabeceraFurConfiguracionAutomaticaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CabeceraFurConfiguracionAutomatica>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CabeceraFurConfiguracionAutomatica> Update(List<CabeceraFurConfiguracionAutomatica> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CabeceraFurConfiguracionAutomaticaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CabeceraFurConfiguracionAutomatica>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:Griselberto huamanc
        /// Fecha:08/03/2023
        /// Version: 1.0
        /// <summary>
        /// elimina un registro
        /// </summary>
        /// <returns> bool </returns>
        public bool Delete(List<int> listadoIds, string usuario)
        {
            try
            {
                _unitOfWork.CabeceraFurConfiguracionAutomaticaRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// Autor:Griselberto huamanc
        /// Fecha:08/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro por id
        /// </summary>
        /// <returns> CabeceraFurConfiguracionAutomatica </returns>
        public IEnumerable<CabeceraFurConfiguracionAutomaticaDTO> ObtenerCabeceraFurConfiguracionAutomatica(FiltroBusquedaCabeceraFCADTO filtro)
        {
            try
            {
                return _unitOfWork.CabeceraFurConfiguracionAutomaticaRepository.ObtenerCabeceraFurConfiguracionAutomatica(filtro);
            } 
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor:Griselberto huamanc
        /// Fecha:08/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro por idArea que este en proceso
        /// </summary>
        /// <returns> CabeceraFurConfiguracionAutomatica </returns>
        public bool ValidarCabeceraFurConfiguracionAutomaticaEnProcesoByIdArea(int IdArea)
        {
            try
            {
                return _unitOfWork.CabeceraFurConfiguracionAutomaticaRepository.ValidarCabeceraFurConfiguracionAutomaticaEnProcesoByIdArea(IdArea);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor:Griselberto huamanc
        /// Fecha:08/03/2023
        /// Version: 1.0
        /// <summary>
        /// Cambia el estado de Solicitud de Cabecera
        /// </summary>
        /// <returns> CabeceraFurConfiguracionAutomatica </returns>
        public bool CambiarEstadoAEnRevision(CambioDeEstadoDTO data,string Usuario)
        {
            try
            {
                var repCabecera = _unitOfWork.CabeceraFurConfiguracionAutomaticaRepository;
                var repFurConfiguracion = _unitOfWork.FurConfiguracionAutomaticaRepository;

                var respuestaActivo = repFurConfiguracion.CambiarActivoFurConfiguracionAutomatica(data.idSeleccion, Usuario);
                if (respuestaActivo)
                {
                    var entidad = repCabecera.ObtenerCabeceraFurConfiguracionAutomaticaById(data.IdCabecera);

                    entidad.FechaModificacion = DateTime.Now;
                    entidad.UsuarioModificacion = Usuario;
                    entidad.IdEstadoProyeccionFur = 2;// En Revision

                    repCabecera.Update(entidad);
                    _unitOfWork.Commit();
                    return true;
                }
                else throw new Exception("!Ocurrio un error al tratar de Enviar la solicitud");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor:Griselberto huamanc
        /// Fecha:08/03/2023
        /// Version: 1.0
        /// <summary>
        /// Cambia el estado de Solicitud de Cabecera
        /// </summary>
        /// <returns> CabeceraFurConfiguracionAutomatica </returns>
        public bool CambiarEstadoArechazado(RechazoProyeccionDTO data, string Usuario)
        {
            try
            {
                var repCabecera = _unitOfWork.CabeceraFurConfiguracionAutomaticaRepository;
                var repFurConfiguracion = _unitOfWork.FurConfiguracionAutomaticaRepository;

                var entidad = repCabecera.ObtenerCabeceraFurConfiguracionAutomaticaById(data.Id);

                var respuestaDesactivar = repFurConfiguracion.DesactivarFurConfiguracionAutomatica(entidad.IdArea, Usuario);
                if(respuestaDesactivar)
                {
                    entidad.FechaModificacion = DateTime.Now;
                    entidad.UsuarioModificacion = Usuario;
                    entidad.IdEstadoProyeccionFur = 4;// Rechazado
                    entidad.Observacion = data.observacion;
                    entidad.IdConfiguracionProyeccionFur = data.IdConfiguracion;

                    repCabecera.Update(entidad);
                    _unitOfWork.Commit();
                    return true;
                }
                else throw new Exception("!Ocurrio un error al tratar de Rechazar la solicitud");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// Autor:Griselberto huamanc
        /// Fecha:08/03/2023
        /// Version: 1.0
        /// <summary>
        /// Crea una cabecera de solicitud y cambia las fechas de Inicio , Fin configuracion , y el estado Activo=0
        /// </summary>
        /// <returns> CabeceraFurConfiguracionAutomatica </returns>
        public CabeceraFurConfiguracionAutomatica CrearCebecarYModificarFechasActivo(CabeceraFurConfiguracionAutomaticaFrontDTO data ,int idConfiguracion)
        {
            try
            {
                CabeceraFurConfiguracionAutomatica respuesta = new CabeceraFurConfiguracionAutomatica();
                var repConfiguracion = _unitOfWork.CabeceraFurConfiguracionAutomaticaRepository;
                var resultadoPreparacion = repConfiguracion.PrepararConfiguracionFurProyeccion(idConfiguracion, data.IdArea, data.Usuario);
                if (resultadoPreparacion) respuesta = this.Add(data);
                else throw new Exception("Ocurrio un error al preparar el detalle para la proyección, consultarlo con TI!");
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
