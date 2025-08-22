using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas;
using BSI.Integra.Repositorio.UnitOfWork;
using DocumentFormat.OpenXml.Office2010.Excel;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTOs;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion
{
    public class ExamenTestService : IExamenTestService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public ExamenTestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCriterioEvaluacionProceso, ExamenTest>(MemberList.None).ReverseMap();
                cfg.CreateMap<TCriterioEvaluacionProceso, ExamenTestDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<ExamenTest, ExamenTestDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public IEnumerable<ExamenTestResumidoDTO> Obtener()
        {
            try
            {
                return _unitOfWork.ExamenTestRepository.Obtener();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public (IEnumerable<FormulaPuntajeDTO> , IEnumerable<ComboDTO> , List<FiltroDTO>) ObtenerCombosExamenTest()
        {
            try
            {
                var listaFormula = _unitOfWork.FormulaPuntajeRepository.Obtener();
                var evaluacionCategoria = _unitOfWork.EvaluacionCategoriaRepository.ObtenerCategoriasEvaluacionRegistradas();
                var listaSexo = _unitOfWork.SexoRepository.ObtenerCombo();

                return  (listaFormula,listaSexo,evaluacionCategoria);
            }
            catch (Exception e)
            {
              throw new Exception ("" + e.Message);
            }
        }

        public (List<EvaluacionAgrupadaComponenteDTO>, IEnumerable<ComboDTO> , IEnumerable<ComboDTO> ) ObtenerEvaluacionEditar(int IdEvaluacion)
        {
            try
            {
                var listaGrupo = _unitOfWork.GrupoComponenteEvaluacionRepository.ObtenerComboPorId(IdEvaluacion);  

                var listaComponentes = _unitOfWork.ExamenTestRepository.ObtenerComponentes(IdEvaluacion);

                var listaExamenes = _unitOfWork.ExamenTestRepository.ObtenerEvaluacionAgrupado(IdEvaluacion);

                return (listaExamenes,listaGrupo, listaComponentes);
            }
            catch (Exception e)
            {
                throw new Exception("" + e.Message);
            }
        }

        public List<CentilDTO> ObtenerCentilEvaluacion(int idExamenTest)
        {
            try
            {
                var listaCentil = _unitOfWork.CentilRepository.ObtenerCentilesEvaluacion(idExamenTest);

                return listaCentil;
            }
            catch (Exception e)
            {
                throw new Exception("" + e.Message);
            }
        }
    }
}
