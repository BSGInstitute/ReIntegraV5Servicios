using AutoMapper;
using BSI.Integra.Aplicacion.Base.Classes;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Helper
{
    public class ErrorSistema
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public ErrorSistema(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TError, Error>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }
        private static readonly Lazy<ErrorSistema> instance = new Lazy<ErrorSistema>(() => new ErrorSistema());
        protected static Dictionary<int, ErrorBase> Errores;
        public static ErrorSistema Instance
        {
            get
            {
                return instance.Value;
            }
        }
        private ErrorSistema()
        {
            Errores = new Dictionary<int, ErrorBase>();
            var errores = _unitOfWork.ErrorRepository.ObtenerTodosErroresSistema();
            CargarErrores(errores);
        }
        private void CargarErrores(List<ErrorDTO> errores)
        {
            foreach (var error in errores)
            {
                var mensajeReal = error.Descripcion;
                if (!string.IsNullOrEmpty(error.DescripcionPersonalizada))
                    mensajeReal = error.DescripcionPersonalizada;
                ErrorBase newError = new ErrorBase();
                newError.Codigo = error.Codigo;
                newError.Mensaje = mensajeReal;
                newError.Tipo = error.IdErrorTipo;
                Errores.Add(error.Codigo, newError);
            }
        }
        public string MensajeError(int codigo)
        {
            return Errores[codigo].Mensaje;
        }
    }
}
