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
    /// Repositorio: ContenidoCertificadoIrcaRepository
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 27/07/2023
    /// <summary>
    /// Gestión general de T_ContenidoCertificadoIrca
    /// </summary>
    public class ContenidoCertificadoIrcaRepository : GenericRepository<TContenidoCertificadoIrca>, IContenidoCertificadoIrcaRepository
    {
        private Mapper _mapper;

        public ContenidoCertificadoIrcaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TContenidoCertificadoIrca, ContenidoCertificadoIrca>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TContenidoCertificadoIrca MapeoEntidad(ContenidoCertificadoIrca entidad)
        {
            try
            {
                TContenidoCertificadoIrca modelo = _mapper.Map<TContenidoCertificadoIrca>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private IEnumerable<TContenidoCertificadoIrca> MapeoEntidad(IEnumerable<ContenidoCertificadoIrca> entidad)
        {
            try
            {
                IEnumerable<TContenidoCertificadoIrca> modelo = _mapper.Map<IEnumerable<TContenidoCertificadoIrca>>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TContenidoCertificadoIrca Add(ContenidoCertificadoIrca entidad)
        {
            try
            {
                var CajaEgresoAprobado = MapeoEntidad(entidad);
                base.Insert(CajaEgresoAprobado);
                return CajaEgresoAprobado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TContenidoCertificadoIrca Update(ContenidoCertificadoIrca entidad)
        {
            try
            {
                var ContenidoCertificadoIrca = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ContenidoCertificadoIrca.RowVersion = entidadExistente.RowVersion;

                base.Update(ContenidoCertificadoIrca);
                return ContenidoCertificadoIrca;
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
                base.Delete(id, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<TContenidoCertificadoIrca> Add(IEnumerable<ContenidoCertificadoIrca> listadoEntidad)
        {
            try
            {
                IEnumerable<TContenidoCertificadoIrca> listado = MapeoEntidad(listadoEntidad);
                base.Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<TContenidoCertificadoIrca> Update(IEnumerable<ContenidoCertificadoIrca> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                IEnumerable<TContenidoCertificadoIrca> listado = MapeoEntidad(listadoEntidad);

                var infoExistente = base.GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                base.Update(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(IEnumerable<int> listadoIds, string usuario)
        {
            try
            {
                base.Delete(listadoIds, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 27/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Entidad - ContenidoCertificadoIrca </returns>
        public ContenidoCertificadoIrca? ObtenerPorId(int id)
        {
            try
            {
                var query = $@"
                    SELECT Id,
		                IdMatriculaCabecera,
		                CursoIrcaId,
		                NombreCurso,
		                CodigoCurso,
		                FechaInicio,
		                FechaFin,
		                DuracionCurso,
		                ResultadoCurso,
		                IdCentroCosto_Irca AS IdCentroCostoIrca,
		                Procesado,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion 
	                FROM ope.T_ContenidoCertificadoIrca
                    WHERE
                        Estado = 1 AND Id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ContenidoCertificadoIrca>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#CCI-OPI-001@Error en ObtenerPorId() {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 27/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name="idContenidoCertificadoIrca"></param>
        /// <returns> Resultado IRCA </returns>
        public string? ObtenerDescripcionResultadoIrca(int idContenidoCertificadoIrca)
        {
            string query = "SELECT Resultado AS Valor FROM ope.V_ObtenerDescripcionResultadoIrca WHERE Id=@IdContenidoCertificadoIrca";
            string resultado = _dapperRepository.FirstOrDefault(query, new { idContenidoCertificadoIrca });
            if (!string.IsNullOrEmpty(resultado) && resultado != "null")
            {
                return JsonConvert.DeserializeObject<StringDTO>(query)!.Valor;
            }
            return null;
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 27/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name="idPgeneral"></param>
        /// <returns></returns>
        public VistaPreviaCertificadoIrcaDTO? ObtenerValoresVistaPreviaIrca(int idPgeneral)
        {
            string query = "SELECT Id, IdPespecifico FROM ope.T_ObtenerContenidoIrcaporIdPgeneral WHERE IdPgeneral=@idPgeneral";
            string resultado = _dapperRepository.FirstOrDefault(query, new { idPgeneral });
            if (!string.IsNullOrEmpty(resultado) && resultado != "null")
            {
                return JsonConvert.DeserializeObject<VistaPreviaCertificadoIrcaDTO>(resultado)!;
            }
            return null;
        }

        public void InsertarListaContenidoCertificadoIrca(List<ContenidoCertificadoIrcaDTO> objs)
        {
            foreach (var item in objs)
            {
                TContenidoCertificadoIrca contenidoIrca = new TContenidoCertificadoIrca();

                contenidoIrca.IdMatriculaCabecera = item.IdMatriculaCabecera;
                contenidoIrca.CursoIrcaId = item.CursoIrcaId;
                contenidoIrca.NombreCurso = item.NombreCurso;
                contenidoIrca.CodigoCurso = item.CodigoCurso;
                contenidoIrca.FechaInicio = item.FechaInicio;
                contenidoIrca.FechaFin = item.FechaFin;
                contenidoIrca.DuracionCurso = item.DuracionCurso;
                contenidoIrca.ResultadoCurso = item.ResultadoCurso;
                contenidoIrca.IdCentroCostoIrca = item.IdCentroCostoIrca;
                contenidoIrca.Procesado = false;
                contenidoIrca.Estado = true;
                contenidoIrca.UsuarioCreacion = item.Usuario;
                contenidoIrca.UsuarioModificacion = item.Usuario;
                contenidoIrca.FechaCreacion = DateTime.Now;
                contenidoIrca.FechaModificacion = DateTime.Now;

                //change for mapeo

                base.Insert(contenidoIrca);
            }
            
        }
    }
}
