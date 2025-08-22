using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion
{
    /// Service:LogFiltroSegmentoEjecutadoService
    /// Autor:  Gilmer Quispe.
    /// Fecha:  21/06/2022
    /// <summary>
    /// Gestión general de T_LogFiltroSegmentoEjecutado
    /// </summary>
    public class LogFiltroSegmentoEjecutadoService : ILogFiltroSegmentoEjecutadoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public LogFiltroSegmentoEjecutadoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>{
            cfg.CreateMap<TLogFiltroSegmentoEjecutado, LogFiltroSegmentoEjecutado>(MemberList.None).ReverseMap();
           
            cfg.CreateMap<LogFiltroSegmentoEjecuDTO, LogFiltroSegmentoEjecutado>(MemberList.None).ReverseMap();
           
            }
            );


            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public LogFiltroSegmentoEjecutado Add(LogFiltroSegmentoEjecuDTO entidad, string Usuario)
        {
            try
            {

                LogFiltroSegmentoEjecutado data = _mapper.Map<LogFiltroSegmentoEjecutado>(entidad);
                data.Id = 0;
                data.UsuarioModificacion = Usuario;
                data.UsuarioCreacion = Usuario;
                data.FechaCreacion = DateTime.Now;
                data.FechaModificacion = DateTime.Now;
                data.Estado = true;
                var modelo = _unitOfWork.LogFiltroSegmentoEjecutadoRepository.Add(data);
                _unitOfWork.Commit();
                return _mapper.Map<LogFiltroSegmentoEjecutado>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        public List<Error> ListaErrores { get; set; } = new List<Error>();

        public bool HasErrors
        {
            get
            {
                return ListaErrores != null && ListaErrores.Any();
            }
        }
        public List<LogFiltroSegmentoEjecutadoDTO> ObtenerPorIdFiltroSegmento(int idFiltroSegmento)
        {
            try
            {

                return _unitOfWork.LogFiltroSegmentoEjecutadoRepository.ObtenerPorIdFiltroSegmento(idFiltroSegmento);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
