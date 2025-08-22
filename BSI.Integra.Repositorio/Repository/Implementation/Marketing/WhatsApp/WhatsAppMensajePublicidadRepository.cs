using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaGeneralDetalleSubAreaWhatsapp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: WhatsAppMensajePublicidadRepository
    /// Autor: Adriana Chipana Ampuero.
    /// Fecha: 12/08/2022
    /// <summary>
    /// Gestión general de T_WhatsAppMensajePublicidad
    /// </summary>
    public class WhatsAppMensajePublicidadRepository : GenericRepository<TWhatsAppMensajePublicidad>, IWhatsAppMensajePublicidadRepository
    {
        private Mapper _mapper;

        public WhatsAppMensajePublicidadRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TWhatsAppMensajePublicidad, WhatsAppMensajePublicidad>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TWhatsAppMensajePublicidad MapeoEntidad(WhatsAppMensajePublicidadDTO entidad)
        {
            try
            {

                TWhatsAppMensajePublicidad modelo = _mapper.Map<TWhatsAppMensajePublicidad>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TWhatsAppMensajePublicidad Add(WhatsAppMensajePublicidadDTO entidad)
        {
            try
            {
                var WhatsAppMensajePublicidad = MapeoEntidad(entidad);
                base.Insert(WhatsAppMensajePublicidad);
                return WhatsAppMensajePublicidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TWhatsAppMensajePublicidad Update(WhatsAppMensajePublicidadDTO entidad)
        {
            try
            {
                var WhatsAppMensajePublicidad = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                WhatsAppMensajePublicidad.RowVersion = entidadExistente.RowVersion;

                base.Update(WhatsAppMensajePublicidad);
                return WhatsAppMensajePublicidad;
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


        public IEnumerable<TWhatsAppMensajePublicidad> Add(IEnumerable<WhatsAppMensajePublicidadDTO> listadoEntidad)
        {
            try
            {
                List<TWhatsAppMensajePublicidad> listado = new List<TWhatsAppMensajePublicidad>();
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

        public IEnumerable<TWhatsAppMensajePublicidad> Update(IEnumerable<WhatsAppMensajePublicidadDTO> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TWhatsAppMensajePublicidad> listado = new List<TWhatsAppMensajePublicidad>();
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


        public bool InsertarWhatsAppMensajePublicidadMasivoCampaniaGeneral(List<WhatsAppMensajePublicidadDTO> listaNuevoWhatsAppMensajePublicidad)
        {
            try
            {
                string spQuery = "[mkt].[SP_InsertarWhatsAppMensajePublicidadMasivo]";

                var subListasBloque =
                        listaNuevoWhatsAppMensajePublicidad.Select((x, i) => new { Index = i, Value = x })
                        .GroupBy(x => x.Index / 500)
                        .Select(x => x.Select(v => v.Value).ToList())
                        .ToList();

                foreach (var bloque in subListasBloque)
                {
                    _dapperRepository.QuerySPFirstOrDefault(spQuery, new
                    {
                        ListaIdAlumno = string.Join(",", bloque.Select(s => s.IdAlumno)),
                        ListaIdPersonal = string.Join(",", bloque.Select(s => s.IdPersonal)),
                        ListaIdPrioridadMailChimpListaCorreo = string.Join(",", bloque.Select(s => s.IdPrioridadMailChimpListaCorreo)),
                        ListaIdWhatsAppEstadoValidacion = string.Join(",", bloque.Select(s => s.IdWhatsAppEstadoValidacion)),
                        ListaIdWhatsAppConfiguracionEnvio = string.Join(",", bloque.Select(s => s.IdWhatsAppConfiguracionEnvio)),
                        ListaIdPais = string.Join(",", bloque.Select(s => s.IdPais)),
                        ListaCelular = string.Join(",", bloque.Select(s => s.Celular)),
                        ListaEsValido = string.Join(",", bloque.Select(s => s.EsValido ? "1" : "0"))
                    });
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Inserta en mkt.T_WhatsAppMensajePublicidad
        /// </summary>
        /// <param name="filtro">Objeto de tipo WhatsAppMensajePublicidadBO</param>
        /// <returns>Id de la transaccion</returns>
        public int InsertarWhatsAppMensajePublicidad(WhatsAppMensajePublicidadDTO filtro)
        {
            var resultado = new ValorIntDTO();

            string spQuery = "[mkt].[SP_InsertarWhatsAppMensajePublicidadMailingGeneral]";

            var query = _dapperRepository.QuerySPFirstOrDefault(spQuery, new
            {
                filtro.IdAlumno,
                filtro.IdPersonal,
                filtro.IdConjuntoListaResultado,
                filtro.IdPrioridadMailChimpListaCorreo,
                filtro.IdWhatsAppEstadoValidacion,
                filtro.IdWhatsAppConfiguracionEnvio,
                filtro.IdPais,
                filtro.Celular,
                filtro.EsValido,
                filtro.UsuarioCreacion,
                filtro.UsuarioModificacion
            });

            if (!string.IsNullOrEmpty(query))
            {
                resultado = JsonConvert.DeserializeObject<ValorIntDTO>(query);
            }

            return (int)resultado.Valor;
        }

        /// <summary>
        /// Actualiza los contactos del primer preprocesamiento de campania general
        /// </summary>
        /// <param name="preprocesamientoWhatsAppCampaniaGeneral">Objeto de clase PrioridadPreprocesamientoWhatsAppCampaniaGeneralDTO</param>
        /// <returns>Boolean</returns>
        public bool ActualizarContactosConPrimerPreprocesamientoCampaniaGeneral(PrioridadPreprocesamientoWhatsAppCampaniaGeneralDTO preprocesamientoWhatsAppCampaniaGeneral)
        {
            try
            {
                string spQuery = "[mkt].[SP_ActualizarPersonalWhatsAppPublicidadCampaniaGeneral]";

                var query = _dapperRepository.QuerySPFirstOrDefault(spQuery, new
                {
                    IdCampaniaGeneralDetalle = preprocesamientoWhatsAppCampaniaGeneral.IdCampaniaGeneralDetalle,
                    ListaIdPersonal = string.Join(",", preprocesamientoWhatsAppCampaniaGeneral.ListaResponsableReal.Select(s => s.IdResponsable)),
                    ListaCantidad = string.Join(",", preprocesamientoWhatsAppCampaniaGeneral.ListaResponsableReal.Select(s => s.Total)),
                    Usuario = preprocesamientoWhatsAppCampaniaGeneral.Usuario
                });

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene la lista de WhatsApp resultante del primero procesado
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id del detalle de la campania general</param>
        /// <returns>Lista de objetos de clase WhatsAppResultadoCampaniaGeneralDTO</returns>
        public List<WhatsAppResultadoCampaniaGeneralDTO> ObtenerListaWhatsAppPrimerProcesadoCampaniaGeneral(int idCampaniaGeneralDetalle)
        {
            try
            {
                List<WhatsAppResultadoCampaniaGeneralDTO> resultadoFinal = new List<WhatsAppResultadoCampaniaGeneralDTO>();

                string spQuery = "[WHP].[SP_ObtenerDataParaReemplazoDeEtiquetaWhatsApp]";

                var resultado = _dapperRepository.QuerySPDapper(spQuery, new { IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    resultadoFinal = JsonConvert.DeserializeObject<List<WhatsAppResultadoCampaniaGeneralDTO>>(resultado);

                return resultadoFinal;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<WhatsAppMensajePublicidadDTO> ObtenerTodosLosmenajesPorIdCampaniaGeneral(int IdCampaniaGeneral)
        {
            try
            {
                List<WhatsAppMensajePublicidadDTO> resultadoFinal = new List<WhatsAppMensajePublicidadDTO>();

                string spQuery = "WHP.V_ObtenerWhatsAppMenajePublicidadPorCampaniaGeneral WHERE IdCampaniaGeneral=@IdCampaniaGeneral";

                var resultado = _dapperRepository.QueryDapper(spQuery, new { IdCampaniaGeneral });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    resultadoFinal = JsonConvert.DeserializeObject<List<WhatsAppMensajePublicidadDTO>>(resultado);

                return resultadoFinal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DatosALumnoWhatsappDTO ObtenerDatosAlumnoIntegra(string WaFrom, string NumeroEmpresa)
        {
            try
            {
                string _queryInsertar = "mkt.SP_ObtenerAlumnoAsesorPorCelularHookAux";

                var queryInsert = _dapperRepository.QuerySPFirstOrDefault(_queryInsertar, new { celular = WaFrom, numeroEmpresa = NumeroEmpresa });

                DatosALumnoWhatsappDTO aux = new DatosALumnoWhatsappDTO();
                aux = JsonConvert.DeserializeObject<DatosALumnoWhatsappDTO>(queryInsert);
                if (aux == null)
                {
                    aux.IdPersonal = 0;
                    aux.UserName = "fallo";
                    aux.Celular = WaFrom;
                    aux.IdCodigoPais = 51; /* SearchOption obtendra de numero*/
                    aux.IdAlumno = -1;
                    aux.EsMarketing = true;

                    return aux;
                }
                else { return aux; }


            }
            catch (Exception e)
            {

                string parametros = WaFrom + NumeroEmpresa;


                DatosALumnoWhatsappDTO aux = new DatosALumnoWhatsappDTO();
                aux.IdPersonal = 1;
                aux.UserName = "fallo";
                aux.Celular = WaFrom;
                aux.IdCodigoPais = 51;
                aux.IdAlumno = -1;
                aux.EsMarketing = true;


                string _queryInsertar = "mkt.Sp_InsertarLog";
                var queryInsert = _dapperRepository.QuerySPFirstOrDefault(_queryInsertar, new { Usuario = "HookWhats", Ruta = "ObtenerDatosAlumnoIntegra", Parametros = parametros, Mensaje = e.Message, Excepcion = e.Message, Tipo = "VALIDATE" });

                return aux;
            }
        }
    }
}
