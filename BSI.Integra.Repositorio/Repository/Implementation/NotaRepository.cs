using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: NotaRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 11/11/2022
    /// <summary>
    /// Gestión general de T_Nota
    /// </summary>
    public class NotaRepository : GenericRepository<TNotum>, INotaRepository
    {
        private Mapper _mapper;

        public NotaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TNotum, Notum>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 11/11/2022
        /// <summary>
        /// Obtiene el listado de nota asociados al IdMatriculaCabecera
        /// </summary>
        /// <param name="idMatriculaCabecera"> Id de Matricula Cabecera </param>
        /// <returns> List<NotaPresencialDTO> </returns> 
        public List<NotaPresencialDTO> ListadoNotaPorMatriculaCabecera(int idMatriculaCabecera)
        {
            try
            {
                var query = @"SELECT IdNota,IdEvaluacion,IdPEspecifico,IdMatriculaCabecera,IdAlumno,NombrePEspecifico,
                                     NombreEvaluacion,Porcentaje,Nota,FechaInicio,FechaFin,DuracionCurso  
                              FROM [ope].[T_ObtenerNotaAsistenciaPresencialOnline] WHERE IdMatriculaCabecera=@IdMatriculaCabecera";
                var res = _dapperRepository.QueryDapper(query, new { IdMatriculaCabecera = idMatriculaCabecera });
                return JsonConvert.DeserializeObject<List<NotaPresencialDTO>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 11/11/2022
        /// <summary>
        /// Obtiene el listado de promedio de notas asociados al IdMatriculaCabecera
        /// </summary>
        /// <param name="idMatriculaCabecera"> Id de Matricula Cabecera </param>
        /// <returns> List<NotaPresencialPromedioDTO> </returns> 
        public List<NotaPresencialPromedioDTO> ListadoNotaPorMatriculaCabeceraPromedio(int idMatriculaCabecera)
        {
            try
            {
                var query = "select * from [ope].[T_ObtenerNotaAsistenciaPresencialOnlinePromedio] where IdMatriculaCabecera=@IdMatriculaCabecera";
                var res = _dapperRepository.QueryDapper(query, new { IdMatriculaCabecera = idMatriculaCabecera });
                return JsonConvert.DeserializeObject<List<NotaPresencialPromedioDTO>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Daniel Huaita
        /// Fecha: 30/01/2023
        /// <summary>
        /// Obtiene las notas de los cursos inscritos        
        /// /// </summary>
        /// <param name="IdMatriculaCabecera" name="IdPEspecifico"> Id de Matricula Cabecera, Id pespecifico del alumno</param>
        /// <returns> List<NotaPresencialPromedioDTO> </returns> 
        public List<NotaPresencialPromedioEspecificoDTO> ListadoNotaPorMatriculaCabeceraPromedioIdEspecifico(int IdMatriculaCabecera, int IdPEspecifico)
        {
            try
            {
                var query = "select * from [ope].[T_ObtenerNotaAsistenciaPresencialOnlinePromedioPorIdEspecifico] where IdMatriculaCabecera=@IdMatriculaCabecera AND IdPEspecifico=@IdPEspecifico";
                var res = _dapperRepository.QueryDapper(query, new { IdMatriculaCabecera = IdMatriculaCabecera, IdPEspecifico = IdPEspecifico });
                return JsonConvert.DeserializeObject<List<NotaPresencialPromedioEspecificoDTO>>(res);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
