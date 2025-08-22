using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: EstructuraEspecificaRepository
    /// Autor: Gilmer Quispe
    /// Fecha: 23/08/2022
    /// <summary>
    /// Gestión general de la tabla T_EstructuraEspecifica
    /// </summary>
    public class EstructuraEspecificaRepository : GenericRepository<TEstructuraEspecifica>, IEstructuraEspecificaRepository
    {
        private Mapper _mapper;

        public EstructuraEspecificaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEstructuraEspecifica, EstructuraEspecifica>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 01/09/2022
        /// Version: 1.0
        /// <summary>
        /// Manda la informacion serializada para al sp para el congelamiento de la estrucutura
        /// </summary>
        /// <param name="datos">Tipo de dato DatosEstructuraCurricularDTO</param>
        /// <param name="usuario">Usuario que esta congelando la estructura</param>
        /// <returns>retorna un objeto booleano</returns>
        public bool CongelarEstructuraAlumno(object datos, string usuario)
        {
            try
            {
                string Json = JsonConvert.SerializeObject(datos);
                var registroDB = _dapperRepository.QuerySPFirstOrDefault("ope.SP_InsertarEstructuraEspecifica", new { Json, usuario });

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Repositorio: TEstructuraEspecificaRepositorioAula
        /// Autor: Daniel Huaita
        /// Fecha: 21/03/2022
        /// <summary>
        /// Obtiene las tareas configuradas en sus estructuras de sus curso
        /// </summary>
        /// <returns> List<RegistroEstructuraCursoTareaDTO> </returns>  
        public List<RegistroEstructuraCursoTareaCalificarDTO> ObtenerRegistroEstructuraTareaCalificarCapitulo(int IdPGeneral, int IdPrincipal, int IdAlumno)
        {
            List<RegistroEstructuraCursoTareaCalificarDTO> rpta = new List<RegistroEstructuraCursoTareaCalificarDTO>();
            try
            {
                var _query = string.Empty;

                _query = "SELECT Id, NombreArchivo, TipoContenido, Carpeta, DireccionUrl, Calificado, Nota, Porcentaje, FechaCreacion, Calica, IdEvaluacion, Tarea  FROM [pw].[V_PW_TareaEvaluacionTareaPorNombre] WHERE IdPGeneral=@IdPGeneral AND IdPrincipal=@IdPrincipal AND Calica=@IdAlumno";
                string queryDB = _dapperRepository.QueryDapper(_query, new { IdPGeneral = IdPGeneral, IdPrincipal = IdPrincipal, IdAlumno = IdAlumno });
                if (!string.IsNullOrEmpty(queryDB) && !queryDB.Contains("[]") && queryDB != "null")
                {
                    rpta = JsonConvert.DeserializeObject<List<RegistroEstructuraCursoTareaCalificarDTO>>(queryDB);
                }
                return rpta;
            }
            catch (Exception e)
            {
                return rpta;
            }
        }
        /// Repositorio: TEstructuraEspecificaRepositorioAula
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 09/05/2023
        /// <summary>
        /// Obtiene la duracion del programa especifico
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de la Matricula Cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns> Total de Duracion </returns>  
        public string ObtenerDuracionTotalPorIdMatriculaCabecera(int idMatriculaCabecera)
        {
            try
            {
                StringDTO rpta = new();
                var query = "SELECT CAST(SUM(ISNULL(Duracion, 0)) AS VARCHAR) AS Valor FROM ope.T_EstructuraEspecifica WHERE IdMatriculaCabecera=@IdMatriculaCabecera AND Estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdMatriculaCabecera = idMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<StringDTO>(resultado)!;
                }
                return rpta.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
