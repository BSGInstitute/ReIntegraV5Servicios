using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ComprobantePagoOportunidadRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 12/08/2022
    /// <summary>
    /// Gestión general de T_ComprobantePagoOportunidad
    /// </summary>
    public class ComprobantePagoOportunidadRepository : GenericRepository<TComprobantePagoOportunidad>, IComprobantePagoOportunidadRepository
    {
        private Mapper _mapper;

        public ComprobantePagoOportunidadRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TComprobantePagoOportunidad, ComprobantePagoOportunidad>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TComprobantePagoOportunidad MapeoEntidad(ComprobantePagoOportunidad entidad)
        {
            try
            {
                //crea la entidad padre
                TComprobantePagoOportunidad modelo = _mapper.Map<TComprobantePagoOportunidad>(entidad);

                //mapea los hijos
                //if (entidad.ListadoHijoNivel1 != null && entidad.ListadoHijoNivel1.Count > 0)
                //{
                //    var listadoHijoNivel1 = _mapper.Map<List<THijo>>(entidad.ListadoHijoNivel1);
                //    foreach (var hijoNivel1 in listadoHijoNivel1)
                //    {
                //        modelo.THijo.Add(hijoNivel1);
                //    }
                //}

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TComprobantePagoOportunidad Add(ComprobantePagoOportunidad entidad)
        {
            try
            {
                var ComprobantePagoOportunidad = MapeoEntidad(entidad);
                base.Insert(ComprobantePagoOportunidad);
                return ComprobantePagoOportunidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TComprobantePagoOportunidad AddAsync(ComprobantePagoOportunidad entidad)
        {
            try
            {
                var ComprobantePagoOportunidad = MapeoEntidad(entidad);
                base.InsertAsync(ComprobantePagoOportunidad);
                return ComprobantePagoOportunidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TComprobantePagoOportunidad Update(ComprobantePagoOportunidad entidad)
        {
            try
            {
                var ComprobantePagoOportunidad = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ComprobantePagoOportunidad.RowVersion = entidadExistente.RowVersion;

                base.Update(ComprobantePagoOportunidad);
                return ComprobantePagoOportunidad;
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


        public IEnumerable<TComprobantePagoOportunidad> Add(IEnumerable<ComprobantePagoOportunidad> listadoEntidad)
        {
            try
            {
                List<TComprobantePagoOportunidad> listado = new List<TComprobantePagoOportunidad>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                base.Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TComprobantePagoOportunidad> Update(IEnumerable<ComprobantePagoOportunidad> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TComprobantePagoOportunidad> listado = new List<TComprobantePagoOportunidad>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

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
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 12/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ComprobantePagoOportunidad.
        /// </summary>
        /// <returns> List<ComprobantePagoOportunidadDTO> </returns>
        public IEnumerable<ComprobantePagoOportunidadDTO> ObtenerComprobantePagoOportunidad()
        {
            try
            {
                List<ComprobantePagoOportunidadDTO> rpta = new List<ComprobantePagoOportunidadDTO>();
                var query = @"
                    SELECT Id,IdContacto,Nombres,Apellidos,Celular,Dni,Correo,NombrePais,IdPais,NombreCiudad,TipoComprobante,NroDocumento,NombreRazonSocial,
	                    Direccion,BitComprobante,IdOcurrencia,IdAsesor,IdOportunidad,Comentario,UsuarioCreacion,UsuarioModificacion,FechaCreacion,
	                    FechaModificacion,IdClasificacionPersona
                    FROM fin.T_ComprobantePagoOportunidad
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComprobantePagoOportunidadDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 12/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ComprobantePagoOportunidad.
        /// </summary>
        /// <returns> List<ComprobantePagoAlumnoDTO> </returns>
        public List<ComprobantePagoAlumnoDTO> ObtenerReporteComprobanteAlumno(filtroReporteComprobanteDTO filtro)
        {
            try
            {
                if (filtro.IdFormaPago == "") { filtro.IdFormaPago = "_"; }
                if (filtro.CodigoMatricula == "") { filtro.CodigoMatricula = "_"; }
                if (filtro.Alumno == "") { filtro.Alumno = "_"; }
                if (filtro.CentroCosto == "") { filtro.CentroCosto = "_"; }
                if (filtro.Comprobante == "") { filtro.Comprobante = "_"; }
                List<ComprobantePagoAlumnoDTO> items = new List<ComprobantePagoAlumnoDTO>();

                var query = _dapperRepository.QuerySPDapper("[fin].[SP_ComprobantePagoAlumno]", new { filtro.IdFormaPago, filtro.CodigoMatricula, filtro.Alumno, filtro.CentroCosto, filtro.Comprobante, filtro.FechaInicial, filtro.FechaFin });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ComprobantePagoAlumnoDTO>>(query);
                }
                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
