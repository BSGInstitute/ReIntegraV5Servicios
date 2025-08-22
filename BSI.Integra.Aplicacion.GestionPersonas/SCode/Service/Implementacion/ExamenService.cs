using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Web.WebPages.Html;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion
{
    public class ExamenService : IExamenService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public ExamenService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TExaman, Examen>(MemberList.None).ReverseMap();
                cfg.CreateMap<TExaman, ExamenDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<Examen, ExamenDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public List<int> ObtenerIdGruposPorEvaluacion(int idEvaluacion)
        {
            var idGrupos = _unitOfWork.ExamenRepository.ObtenerIdGruposPorEvaluacion(idEvaluacion);
            return idGrupos;
        }
        public List<FactorComponenteDTO> ObtenerComponentePorEvaluacion(int idEvaluacion)
        {
            var componentes = _unitOfWork.ExamenRepository.ObtenerComponentePorEvalauacion(idEvaluacion);
            return componentes;
        }
        public bool ActualizarFactorComponente(FactorComponenteDTO Json)
        {
            try
            {
                int idBusqueda = Json.Id == 0 ? Json.IdExamen : Json.Id;
                var examen = _unitOfWork.ExamenRepository.FirstById(idBusqueda);
                examen.Factor = Json.Factor;
                examen.UsuarioModificacion = Json.Usuario ?? "System";
                examen.FechaModificacion = DateTime.Now;
                var estado = _unitOfWork.ExamenRepository.Update(examen);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<ExamenVDTO> ObtenerComponentesPorEvaluacion(int idEvaluacion)
        {
            try
            {
                List<ExamenVDTO> listaExamen;
                if (idEvaluacion > 0)
                {
                    listaExamen = _unitOfWork.ExamenRepository.ObtenerComponentesAsociadosEvaluacion(idEvaluacion);
                }
                else
                {
                    listaExamen = new List<ExamenVDTO>();
                }
                return listaExamen;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
