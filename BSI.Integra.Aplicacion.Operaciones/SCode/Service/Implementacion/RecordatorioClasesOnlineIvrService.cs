using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Implementacion
{
    /// Service: RecordatorioClasesOnlineIvrService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_RecordatorioClasesOnlineIvr
    /// </summary>
    public class RecordatorioClasesOnlineIvrService : IRecordatorioClasesOnlineIvrService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public RecordatorioClasesOnlineIvrService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TRecordatorioClasesOnlineIvr, RecordatorioClasesOnlineIvr>(MemberList.None).ReverseMap();
                //cfg.CreateMap<RecordatorioClasesOnlineIvrRecibidoDTO, RecordatorioClasesOnlineIvr>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }



        #region Metodos Base
        public RecordatorioClasesOnlineIvr Add(RecordatorioClasesOnlineIvr data, string Usuario)
        {
            try
            {
                RecordatorioClasesOnlineIvr entidad = _mapper.Map<RecordatorioClasesOnlineIvr>(data);
                entidad.Id = 0;
                entidad.UsuarioCreacion = Usuario;
                entidad.UsuarioModificacion = Usuario;
                entidad.FechaModificacion = DateTime.Now;
                entidad.FechaCreacion = DateTime.Now;
                entidad.Estado = true;

                var modelo = _unitOfWork.RecordatorioClasesOnlineIvrRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<RecordatorioClasesOnlineIvr>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RecordatorioClasesOnlineIvr Update(RecordatorioClasesOnlineIvr data)
        {
            try
            {
                var repositorioRecordatorioClasesOnlineIvr = _unitOfWork.RecordatorioClasesOnlineIvrRepository;
                var modelo = _unitOfWork.RecordatorioClasesOnlineIvrRepository.Update(data);
                _unitOfWork.Commit();
                return _mapper.Map<RecordatorioClasesOnlineIvr>(modelo);
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
                _unitOfWork.RecordatorioClasesOnlineIvrRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RecordatorioClasesOnlineIvr> Add(List<RecordatorioClasesOnlineIvr> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.RecordatorioClasesOnlineIvrRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<RecordatorioClasesOnlineIvr>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RecordatorioClasesOnlineIvr> Update(List<RecordatorioClasesOnlineIvr> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.RecordatorioClasesOnlineIvrRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<RecordatorioClasesOnlineIvr>>(modelo);
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
                _unitOfWork.RecordatorioClasesOnlineIvrRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2023
        /// Version: 1.0
        /// <summary>
        /// Actualiza el intento del registro.
        /// </summary>
        /// <returns></returns>
        public bool ActualizarIntento(int Id)
        {
            try
            {
                var entidad = _unitOfWork.RecordatorioClasesOnlineIvrRepository.ObtenerRecordatorioClasesOnlineIvrById(Id);
                if (entidad != null)
                {
                    entidad.Intento = entidad.Intento + 1;
                    entidad.UsuarioModificacion = "Ivr-Procceso";
                    entidad.FechaModificacion = DateTime.Now;
                    var data = this.Update(entidad);
                    return true;
                }
                else return false;


            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2023
        /// Version: 1.0
        /// <summary>
        /// Actualiza el intento y concluido del registro.
        /// </summary>
        /// <returns></returns>
        public bool ActualizarIntentoConcluido(int Id)
        {
            try
            {
                var entidad = _unitOfWork.RecordatorioClasesOnlineIvrRepository.ObtenerRecordatorioClasesOnlineIvrById(Id);
                if (entidad != null)
                {
                    entidad.Concluido = true;
                    entidad.Intento = entidad.Intento + 1;
                    entidad.UsuarioModificacion = "Ivr-Procceso";
                    entidad.FechaModificacion = DateTime.Now;
                    var data = this.Update(entidad);
                    return true;
                }
                else return false;


            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2023
        /// Version: 1.0
        /// <summary>
        /// Actualiza el concluido del registro.
        /// </summary>
        /// <returns></returns>
        public bool ActualizarConcluido(int Id)
        {
            try
            {
                var entidad = _unitOfWork.RecordatorioClasesOnlineIvrRepository.ObtenerRecordatorioClasesOnlineIvrById(Id);
                if (entidad != null)
                {
                    entidad.Concluido = true;
                    entidad.UsuarioModificacion = "Ivr-Procceso";
                    entidad.FechaModificacion = DateTime.Now;
                    var data = this.Update(entidad);
                    return true;
                }
                else return false;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de T_RecordatorioClasesOnlineIvr de Id enviado.
        /// </summary>
        /// <returns> RecordatorioClasesOnlineIvr </returns>
        public RecordatorioClasesOnlineIvr ObtenerRecordatorioClasesOnlineIvrById(int Id)
        {
            try
            {
                return _unitOfWork.RecordatorioClasesOnlineIvrRepository.ObtenerRecordatorioClasesOnlineIvrById(Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro para llamada.
        /// </summary>
        /// <returns> List<RecordatorioClasesOnlineIvrDTO> </returns>
        public DatoLLamadaRecordatorioClasesOnlineDTO ObtenerDatoLlamadaRecordatorioClasesOnline()
        {
            try
            {
                return _unitOfWork.RecordatorioClasesOnlineIvrRepository.ObtenerDatoLlamadaRecordatorioClasesOnline();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro para llamada.
        /// </summary>
        /// <returns> List<RecordatorioClasesOnlineIvrDTO> </returns>
        public DatoLLamadaRecordatorioClasesOnlineDTO ObtenerDatoLlamadaRecordatorioClasesOnlineById(int Id)
        {
            try
            {
                return _unitOfWork.RecordatorioClasesOnlineIvrRepository.ObtenerDatoLlamadaRecordatorioClasesOnlineById(Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
