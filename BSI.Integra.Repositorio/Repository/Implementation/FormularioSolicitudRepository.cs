using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
//using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.FiltroTipoFormularioSolicitudDTO;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: FormularioSolicitudRepository
    /// Autor: Margiory Ramirez Neyra.
    /// Fecha: 12/09/2022
    /// <summary>
    /// Gestión general de T_FormularioSolicitud
    /// </summary>
    public class FormularioSolicitudRepository : GenericRepository<TFormularioSolicitud>, IFormularioSolicitudRepository
    {
        private Mapper _mapper;

        public FormularioSolicitudRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFormularioSolicitud, FormularioSolicitud>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TFormularioSolicitud MapeoEntidad(FormularioSolicitud entidad)
        {
            try
            {
                //crea la entidad padre
                TFormularioSolicitud modelo = _mapper.Map<TFormularioSolicitud>(entidad);

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

        public TFormularioSolicitud Add(FormularioSolicitud entidad)
        {
            try
            {
                var FormularioSolicitud = MapeoEntidad(entidad);
                base.Insert(FormularioSolicitud);
                return FormularioSolicitud;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFormularioSolicitud Update(FormularioSolicitud entidad)
        {
            try
            {
                var FormularioSolicitud = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                FormularioSolicitud.RowVersion = entidadExistente.RowVersion;

                base.Update(FormularioSolicitud);
                return FormularioSolicitud;
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

        public IEnumerable<TFormularioSolicitud> Add(IEnumerable<FormularioSolicitud> listadoEntidad)
        {
            try
            {
                List<TFormularioSolicitud> listado = new List<TFormularioSolicitud>();
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

        public IEnumerable<TFormularioSolicitud> Update(IEnumerable<FormularioSolicitud> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TFormularioSolicitud> listado = new List<TFormularioSolicitud>();
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
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 12/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_FormularioSolicitud para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();

                var query = "SELECT Id, Nombre FROM mkt.T_FormularioSolicitud WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ComboDTO> ObtenerComboFs(InsertarFormulario2DTO nombre)
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();

                var query = "SELECT Id, Nombre  AS Nombre FROM mkt.T_FormularioSolicitud WHERE Estado=1 and Nombre COLLATE Modern_Spanish_CI_AS = '" + nombre.Nombre + "'";
                var query2 = "SELECT Id, Nombre  AS Nombre FROM mkt.T_FormularioSolicitud WHERE Estado=1 and Nombre like '%" + nombre.Nombre + "%'";
                var resultado = _dapperRepository.QueryDapper(query, null);
                var resultado2 = _dapperRepository.QueryDapper(query2, null);
                if (!string.IsNullOrEmpty(resultado2) && !resultado2.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado2);
                }

                else if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 12/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros que contengan los campos de  T_FormularioSolicitud
        /// </summary>
        /// <returns> List<FormularioSolicitudDTO> </returns>
        public IEnumerable<FormularioSolicitudDTO> ObtenerFormularioSolicitud()
        {
            try
            {
                List<FormularioSolicitudDTO> rpta = new List<FormularioSolicitudDTO>();
                var query = @"SELECT Id,IdFormularioRespuesta,FormularioRespuesta,Nombre,Codigo,NombreCampania,IdCampania,Proveedor,IdFormularioSolicitudTextoBoton,TipoSegmento,CodigoSegmento,TipoEvento,URLBotonInvitacionPagina" +
                              " From mkt.V_TFormularioSolicitud_ObtenerTodo WHERE Estado=1 ";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<FormularioSolicitudDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 12/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_FormularioSolicitud
        /// </summary>
        /// <returns> List<FormularioSolicitudDTO> </returns>


        public IEnumerable<FormularioSolicitudCompuestoDTO> ObtenerTodo(FiltroCompuestroGrillaDTO paginador)
        {
            string Condicion = "";
            string Paginacion = "";
            string FormularioRespuesta = "";
            string Nombre = "";
            string Codigo = "";
            string NombreCampania = "";
            string Proveedor = "";
            //int Skip = 0;
            //int Take = 0;
            if (paginador.paginador.take != 0)
            {
                Paginacion = " OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            }

            if (paginador.filter != null)
            {

                foreach (var item in paginador.filter.Filters)
                {
                    if (item.Value.Contains(""))
                    {
                        Condicion += " and " + item.Field + "=@" + item.Field;
                        FormularioRespuesta = item.Value;
                        switch (item.Field.ToLower())
                        {
                            case "formulariorespuesta":
                                FormularioRespuesta = item.Value;
                                break;
                            case "nombre":
                                Nombre = item.Value;
                                break;
                            case "codigo":
                                Codigo = item.Value;
                                break;
                            case "nombrecampania":
                                NombreCampania = item.Value;
                                break;
                            case "proveedor":
                                Proveedor = item.Value;
                                break;
                        }
                    }
                }
            }
            string _queryFormularioSolicitud = "Select Id,IdFormularioRespuesta,FormularioRespuesta,Nombre,Codigo,NombreCampania,IdCampania,Proveedor,IdFormularioSolicitudTextoBoton,TipoSegmento,CodigoSegmento,TipoEvento,URLBotonInvitacionPagina" +
                                                " From mkt.V_TFormularioSolicitud_ObtenerTodo Where Estado=1 " + Condicion + " Order by Id desc " + Paginacion + "";
            var queryFormularioSolicitud = _dapperRepository.QueryDapper(_queryFormularioSolicitud, new { FormularioRespuesta, Nombre, Codigo, NombreCampania, Proveedor, Skip = paginador.paginador.skip, Take = paginador.paginador.take });

            var rpta = JsonConvert.DeserializeObject<List<FormularioSolicitudCompuestoDTO>>(queryFormularioSolicitud);

            if (rpta.Count() == 0) return rpta;

            string _queryFormularioSolicitudTotal = "Select count(*) From mkt.V_TFormularioSolicitud_ObtenerTodo Where Estado=1 " + Condicion + "";
            var queryFormularioSolicitudTotal = _dapperRepository.FirstOrDefault(_queryFormularioSolicitudTotal, new { FormularioRespuesta, Nombre, Codigo, NombreCampania, Proveedor });
            var FormularioSolicitudTotal = JsonConvert.DeserializeObject<Dictionary<string, int>>(queryFormularioSolicitudTotal);

            rpta.FirstOrDefault().Total = FormularioSolicitudTotal.Select(w => w.Value).FirstOrDefault();
            return rpta;
        }

        public IEnumerable<ConjuntoAnuncioFiltroCompuestoDTO> ObtenerConjuntoAnunciosFiltro(string filtro)
        {
            string _queryConjuntoAnuncio = "Select Id,Nombre,IdProveedor,NombreProveedor,Codigo From mkt.V_T_FormularioSolicitud_ObtenerCampania Where Estado=1 and IdFormulario is null and Nombre Like @Filtro Order by Nombre asc";
            var queryConjuntoAnuncio = _dapperRepository.QueryDapper(_queryConjuntoAnuncio, new { Filtro = "%" + filtro + "%" });
            return JsonConvert.DeserializeObject<List<ConjuntoAnuncioFiltroCompuestoDTO>>(queryConjuntoAnuncio);
        }

        public IEnumerable<FiltroDTO> FormularioRespuestaFiltro(string filtro)
        {
            try
            {
                string _queryFormularioRespuesta = "Select Id,Nombre From mkt.V_TFormularioRespuesta_Filtro Where Estado=1 and Nombre like @Filtro";
                var queryFormularioRespuesta = _dapperRepository.QueryDapper(_queryFormularioRespuesta, new { Filtro = "%" + filtro + "%" });
                return JsonConvert.DeserializeObject<List<FiltroDTO>>(queryFormularioRespuesta);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        //public CompuestoConjuntoAnuncioDTO InsertarConjuntoAnuncio(CompuestoConjuntoAnuncioDTO Ob)
        //{
        //    throw new NotImplementedException();
        //}


    }
}









