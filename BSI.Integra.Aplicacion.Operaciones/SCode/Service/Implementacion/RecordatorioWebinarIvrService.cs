using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Implementacion
{
    /// Service: RecordatorioWebinarIvrIvrService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_RecordatorioWebinarIvr
    /// </summary>
    public class RecordatorioWebinarIvrService : IRecordatorioWebinarIvrService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public RecordatorioWebinarIvrService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TRecordatorioWebinarIvr, RecordatorioWebinarIvr>(MemberList.None).ReverseMap();
                //cfg.CreateMap<RecordatorioWebinarIvrRecibidoDTO, RecordatorioWebinarIvr>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        

        #region Metodos Base
        public RecordatorioWebinarIvr Add(RecordatorioWebinarIvr data,string Usuario)
        {
            try
            {
                RecordatorioWebinarIvr entidad = _mapper.Map<RecordatorioWebinarIvr>(data);
                entidad.Id = 0;
                entidad.UsuarioCreacion = Usuario;
                entidad.UsuarioModificacion = Usuario;
                entidad.FechaModificacion = DateTime.Now;
                entidad.FechaCreacion = DateTime.Now;
                entidad.Estado = true;

                var modelo = _unitOfWork.RecordatorioWebinarIvrRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<RecordatorioWebinarIvr>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RecordatorioWebinarIvr Update(RecordatorioWebinarIvr data)
        {
            try
            {
                var repositorioRecordatorioWebinarIvr = _unitOfWork.RecordatorioWebinarIvrRepository;
                var modelo = _unitOfWork.RecordatorioWebinarIvrRepository.Update(data);
                _unitOfWork.Commit();
                return _mapper.Map<RecordatorioWebinarIvr>(modelo);
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
                _unitOfWork.RecordatorioWebinarIvrRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RecordatorioWebinarIvr> Add(List<RecordatorioWebinarIvr> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.RecordatorioWebinarIvrRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<RecordatorioWebinarIvr>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RecordatorioWebinarIvr> Update(List<RecordatorioWebinarIvr> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.RecordatorioWebinarIvrRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<RecordatorioWebinarIvr>>(modelo);
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
                _unitOfWork.RecordatorioWebinarIvrRepository.Delete(listadoIds, usuario);
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
                var entidad = _unitOfWork.RecordatorioWebinarIvrRepository.ObtenerRecordatorioWebinarIvrPorId(Id);
                if(entidad != null)
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
                var entidad = _unitOfWork.RecordatorioWebinarIvrRepository.ObtenerRecordatorioWebinarIvrPorId(Id);
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
                var entidad = _unitOfWork.RecordatorioWebinarIvrRepository.ObtenerRecordatorioWebinarIvrPorId(Id);
                if(entidad != null)
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
        /// Obtiene el registro de T_RecordatorioWebinarIvr de Id enviado.
        /// </summary>
        /// <returns> RecordatorioWebinarIvr </returns>
        public RecordatorioWebinarIvr ObtenerRecordatorioWebinarIvrPorId(int Id)
        {
            try
            {
                return _unitOfWork.RecordatorioWebinarIvrRepository.ObtenerRecordatorioWebinarIvrPorId(Id);
            }
            catch(Exception ex)
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
        /// <returns> List<RecordatorioWebinarIvrDTO> </returns>
        public DatoLLamadaRecordatorioWebinarDTO ObtenerDatoLlamadaRecordatorioWebinar()
        {
            try
            {
                return _unitOfWork.RecordatorioWebinarIvrRepository.ObtenerDatoLlamadaRecordatorioWebinar();
            }
            catch(Exception ex)
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
        /// <returns> List<RecordatorioWebinarIvrDTO> </returns>
        public DatoLLamadaRecordatorioWebinarDTO ObtenerDatoLlamadaRecordatorioWebinarPorId(int Id)
        {
            try
            {
                return _unitOfWork.RecordatorioWebinarIvrRepository.ObtenerDatoLlamadaRecordatorioWebinarPorId(Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }

}
