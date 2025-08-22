using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: CategoriaAlumnoRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 11/11/2022
    /// <summary>
    /// Gestión general de T_CategoriaAlumno
    /// </summary>
    public class CategoriaAlumnoRepository : GenericRepository<TCategoriaAlumno>, ICategoriaAlumnoRepository
    {
        private Mapper _mapper;

        public CategoriaAlumnoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCategoriaAlumno, CategoriaAlumno>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 11/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Categoria Alumno
        /// </summary>
        /// <returns></returns>
        public List<CategoriaAlumnoDTO> ObtenerCategoriaAlumno()
        {
            try
            {
                List<CategoriaAlumnoDTO> salidaCategorias = new List<CategoriaAlumnoDTO>();
                List<CategoriaAlumnoDTO> categoriaAlumnos = new List<CategoriaAlumnoDTO>();
                var query = @"SELECT
                                Id, Nombre, Descripcion, EstadoCategoria, Descuento, AmpliacionFechaFinPrograma, CantidadDiasVencimiento, 
                                Estados, IdEstados, SubEstados, IdSubEstados
                            FROM 
                                ope.V_ObtenerCategoriaAlumnoEspecifica ORDER BY Id asc";
                var documentoBD = _dapperRepository.QueryDapper(query, null);

                if (!documentoBD.Contains("[]") && !string.IsNullOrEmpty(documentoBD))
                {
                    categoriaAlumnos = JsonConvert.DeserializeObject<List<CategoriaAlumnoDTO>>(documentoBD)!;
                }
                salidaCategorias = categoriaAlumnos
                   .GroupBy(x => new { x.Id, x.Nombre, x.Descripcion, x.EstadoCategoria, x.Descuento, x.AmpliacionFechaFinPrograma, x.CantidadDiasVencimiento, x, x.Estados, x.IdEstados, x.SubEstados, x.IdSubEstados })
                   .Select(g =>
                   new CategoriaAlumnoDTO
                   {
                       Id = g.Key.Id,
                       Nombre = g.Key.Nombre,
                       Descripcion = g.Key.Descripcion,
                       EstadoCategoria = g.Key.EstadoCategoria,
                       Descuento = g.Key.Descuento,
                       AmpliacionFechaFinPrograma = g.Key.AmpliacionFechaFinPrograma,
                       CantidadDiasVencimiento = g.Key.CantidadDiasVencimiento,
                       Estados = g.Key.Estados,
                       IdEstados = g.Key.IdEstados,
                       SubEstados = g.Key.SubEstados,
                       IdSubEstados = g.Key.IdSubEstados
                   }).ToList();
                return salidaCategorias;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 14/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la fecha de pago por matriculaCabecera
        /// </summary>
        /// <param name="matriculaCabecera"></param>
        /// <returns></returns>
        public List<FechaPagoDTO> ObtenerFechaPago(int matriculaCabecera)
        {
            List<FechaPagoDTO> salidaCombos = new List<FechaPagoDTO>();
            List<FechaPagoDTO> combosEstado = new List<FechaPagoDTO>();
            var query = $"EXECUTE OPE.SP_ObtenerUltimaFechaPagada @CodeMatri={matriculaCabecera}";
            var documentoBD = _dapperRepository.QueryDapper(query, null);
            if (!documentoBD.Contains("[]") && !string.IsNullOrEmpty(documentoBD))
            {
                combosEstado = JsonConvert.DeserializeObject<List<FechaPagoDTO>>(documentoBD)!;
            }
            salidaCombos = combosEstado
                   .GroupBy(x => new { x.IdMatriculaCabecera, x.IdEstado_matricula, x.IdSubEstadoMatricul, x.IdCategoriaAlumno, x.FechaVencimiento, x.FechaPago })
                   .Select(g =>
                   new FechaPagoDTO
                   {
                       IdMatriculaCabecera = g.Key.IdMatriculaCabecera,
                       IdEstado_matricula = g.Key.IdEstado_matricula,
                       IdSubEstadoMatricul = g.Key.IdSubEstadoMatricul,
                       IdCategoriaAlumno = g.Key.IdCategoriaAlumno,
                       FechaVencimiento = g.Key.FechaVencimiento,
                       FechaPago = g.Key.FechaPago
                   }).ToList();
            return salidaCombos;
        }
    }
}
