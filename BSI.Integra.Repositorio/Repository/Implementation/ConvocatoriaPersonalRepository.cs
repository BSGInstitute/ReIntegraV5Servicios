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
    /// Repositorio: ConvocatoriaPersonalRepository
    /// Autor: Griselberto Huaman
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_ConvocatoriaPersonal
    /// </summary>
    public class ConvocatoriaPersonalRepository : GenericRepository<TConvocatoriaPersonal>, IConvocatoriaPersonalRepository
    {
        private Mapper _mapper;

        public ConvocatoriaPersonalRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TConvocatoriaPersonal, ConvocatoriaPersonal>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TConvocatoriaPersonal MapeoEntidad(ConvocatoriaPersonal entidad)
        {
            try
            {
                //crea la entidad padre
                TConvocatoriaPersonal modelo = _mapper.Map<TConvocatoriaPersonal>(entidad);

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

        public TConvocatoriaPersonal Add(ConvocatoriaPersonal entidad)
        {
            try
            {
                var ConvocatoriaPersonal = MapeoEntidad(entidad);
                base.Insert(ConvocatoriaPersonal);
                return ConvocatoriaPersonal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TConvocatoriaPersonal Update(ConvocatoriaPersonal entidad)
        {
            try
            {
                var ConvocatoriaPersonal = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ConvocatoriaPersonal.RowVersion = entidadExistente.RowVersion;

                base.Update(ConvocatoriaPersonal);
                return ConvocatoriaPersonal;
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


        public IEnumerable<TConvocatoriaPersonal> Add(IEnumerable<ConvocatoriaPersonal> listadoEntidad)
        {
            try
            {
                List<TConvocatoriaPersonal> listado = new List<TConvocatoriaPersonal>();
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

        public IEnumerable<TConvocatoriaPersonal> Update(IEnumerable<ConvocatoriaPersonal> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TConvocatoriaPersonal> listado = new List<TConvocatoriaPersonal>();
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

        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ConvocatoriaPersonal.
        /// </summary>
        /// <returns> List<ConvocatoriaPersonalDTO> </returns>
        public List<ConvocatoriaPersonalDTO> ObtenerConvocatoriasRegistradas()
        {
            try
            {
                List<ConvocatoriaPersonalDTO> listaConvocatoriaPersonal = new List<ConvocatoriaPersonalDTO>();
                var query = @" SELECT
                                   [Id]
                                  ,[Nombre]
                                  ,[NombreProcesoSeleccion]
                                  ,[IdProcesoSeleccion]
                                  ,[Codigo]
                                  ,[FechaInicio]
                                  ,[FechaFin]
                                  ,[CuerpoConvocatoria]
                                  ,[UrlAviso]
                                  ,[IdSedeTrabajo]
                                  ,[SedeTrabajo]
                                  ,[IdPersonal]
                                  ,[IdProveedor]
                                  ,[IdArea]
                                  ,[Area]
                                  ,[Activo]
                                  ,[Proveedor]
                                  ,[PersonalEncargado]
                                  ,[IdEstadoConvocatoria]
                                  ,[NroVacantes]
                                  ,[IdModalidadTrabajo]
                                  ,[IdCategoriaAsignacion]
                                  ,[VerEnPortal]
                                  ,[SoloMatriculado]
                                  ,[InformacionAdicional]
                                  ,[IdTipoContrato]
                                  ,[TipoJornada]
                                  ,[HoraSemanal]
                                  ,[RemIdMoneda]
                                  ,[MontoRemBruta]
                                  ,[VisualizarRem]
                                  ,[AplicaBono]
                                  ,[BonoIdMoneda]
                                  ,[MontoDesdeBono]
                                  ,[MontoHastaBono]
                                  ,[AplicaComision]
                                  ,[ComisionIdMoneda]
                                  ,[MontoDesdeComision]
                                  ,[MontoHastaComision]
                              FROM [gp].[V_TConvocatoriaPersonal_ObtenerProcesoSeleccion] where Estado=1  order by id desc ";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaConvocatoriaPersonal = JsonConvert.DeserializeObject<List<ConvocatoriaPersonalDTO>>(resultado);
                }
                return listaConvocatoriaPersonal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ConvocatoriaPersonal.
        /// </summary>
        /// <returns> List<ConvocatoriaPersonalDTO> </returns>
        public ConvocatoriaPersonalDTO ObtenerConvocatoriasRegistradaById(int Id)
        {
            try
            {
                ConvocatoriaPersonalDTO ConvocatoriaPersonal = new ConvocatoriaPersonalDTO();
                var query = @" SELECT   
                                   [Id]
                                  ,[Nombre]
                                  ,[NombreProcesoSeleccion]
                                  ,[IdProcesoSeleccion]
                                  ,[Codigo]
                                  ,[FechaInicio]
                                  ,[FechaFin]
                                  ,[CuerpoConvocatoria]
                                  ,[UrlAviso]
                                  ,[IdSedeTrabajo]
                                  ,[SedeTrabajo]
                                  ,[IdPersonal]
                                  ,[IdProveedor]
                                  ,[IdArea]
                                  ,[Area]
                                  ,[Activo]
                                  ,[Proveedor]
                                  ,[PersonalEncargado]
                                  ,[IdEstadoConvocatoria]
                                  ,[NroVacantes]
                                  ,[IdModalidadTrabajo]
                                  ,[IdCategoriaAsignacion]
                                  ,[VerEnPortal]
                                  ,[SoloMatriculado]
                                  ,[InformacionAdicional]
                                  ,[IdTipoContrato]
                                  ,[TipoJornada]
                                  ,[HoraSemanal]
                                  ,[RemIdMoneda]
                                  ,[MontoRemBruta]
                                  ,[VisualizarRem]
                                  ,[AplicaBono]
                                  ,[BonoIdMoneda]
                                  ,[MontoDesdeBono]
                                  ,[MontoHastaBono]
                                  ,[AplicaComision]
                                  ,[ComisionIdMoneda]
                                  ,[MontoDesdeComision]
                                  ,[MontoHastaComision]
                              FROM [gp].[V_TConvocatoriaPersonal_ObtenerProcesoSeleccion] where Estado=1 AND Id=@Id order by id desc ";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    ConvocatoriaPersonal = JsonConvert.DeserializeObject<ConvocatoriaPersonalDTO>(resultado);
                }
                return ConvocatoriaPersonal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<int> ObtenerIdsIdioma(int IdConvocatoria)
        {
            try
            {
                List<int> resultadoReturn = new List<int>();
                List<ValorIntDTO> ConvocatoriaPersonal = new List<ValorIntDTO>();
                var query = @" Select  CONCAT(IdIdioma,IdNivelIdioma) as Id from  [gp].T_ConvocatoriaPersonalIdiomaDetalle where IdConvocatoriaPersonal=@IdConvocatoria  AND Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, new { IdConvocatoria });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    ConvocatoriaPersonal = JsonConvert.DeserializeObject<List<ValorIntDTO>>(resultado);
                    resultadoReturn = ConvocatoriaPersonal.Select(dto => dto.Id).ToList();
                }
                return resultadoReturn;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public List<int> ObtenerIdsNivelEstudio(int IdConvocatoria)
        {
            try
            {
                List<int> resultadoReturn = new List<int>();
                List<ValorIntDTO> ConvocatoriaPersonal = new List<ValorIntDTO>();
                var query = @" Select IdNivelEstudio as Id from  [gp].T_ConvocatoriaPersonalNivelEstudioDetalle where IdConvocatoriaPersonal=@IdConvocatoria  AND Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, new { IdConvocatoria });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    ConvocatoriaPersonal = JsonConvert.DeserializeObject<List<ValorIntDTO>>(resultado);
                    resultadoReturn = ConvocatoriaPersonal.Select(dto => dto.Id).ToList();
                }
                return resultadoReturn;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public List<int> ObtenerIdsExperiencia(int IdConvocatoria)
        {
            try
            {
                List<int> resultadoReturn = new List<int>();
                List<ValorIntDTO> ConvocatoriaPersonal = new List<ValorIntDTO>();
                var query = @" Select IdExperiencia as Id from  [gp].T_ConvocatoriaPersonalExperienciaDetalle where IdConvocatoriaPersonal=@IdConvocatoria AND Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, new { IdConvocatoria });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    ConvocatoriaPersonal = JsonConvert.DeserializeObject<List<ValorIntDTO>>(resultado);
                    resultadoReturn = ConvocatoriaPersonal.Select(dto => dto.Id).ToList();
                }
                return resultadoReturn;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public bool InsertarDetalleConvocatorias(int IdConvocatoriaPersonal, string Usuario, string? IdsNivelEstudio, string? IdsExperiencia, string? IdsIdioma)
        {
            var respuesta = false;
            var resultado = _dapperRepository.QuerySPDapper("gp.SP_ActualizarInsertarDetalleConvocatoriaPersonal", new
            {
                IdConvocatoriaPersonal = IdConvocatoriaPersonal,
                Usuario = Usuario,
                IdsNivelEstudio = IdsNivelEstudio,
                IdsExperiencia = IdsExperiencia,
                IdsIdioma = IdsIdioma
            });

            if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
            {
                respuesta = true;
            }
            return respuesta;
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 26/10/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ConvocatoriaPersonal para Combo Postulante
        /// </summary>
        /// <returns> IEnumerable<ConvocatoriaPersonalComboPostulanteDTO> </returns>
        public IEnumerable<ConvocatoriaPersonalComboPostulanteDTO> ObtenerComboComvocatoriaPersonal()
        {
            try
            {
                string queryDapper = @"SELECT
                                            Id                          AS IdConvocatoria,
                                            CONCAT(Codigo, ' - ', Nombre) AS NombreConvocatoria,
                                            IdProcesoSeleccion
                                        FROM
                                            gp.T_ConvocatoriaPersonal
                                        ORDER BY
                                            FechaCreacion DESC;";
                var resultado = _dapperRepository.QueryDapper(queryDapper, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    var respuesta = JsonConvert.DeserializeObject<IEnumerable<ConvocatoriaPersonalComboPostulanteDTO>>(resultado);
                    return respuesta;
                }

                return null;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
