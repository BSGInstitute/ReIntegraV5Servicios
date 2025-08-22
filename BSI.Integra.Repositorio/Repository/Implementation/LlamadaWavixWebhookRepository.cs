using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    public class LlamadaWavixWebhookRepository : GenericRepository<TLlamadaWavixWebhook>, ILlamadaWavixWebhookRepository
    {
        private Mapper _mapper;
        public LlamadaWavixWebhookRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TLlamadaWavixWebhook, LlamadaWavixWebhook>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TLlamadaWavixWebhook MapeoEntidad(LlamadaWavixWebhook entidad)
        {
            try
            {
                //crea la entidad padre
                TLlamadaWavixWebhook modelo = _mapper.Map<TLlamadaWavixWebhook>(entidad);

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

        public TLlamadaWavixWebhook Add(LlamadaWavixWebhook entidad)
        {
            try
            {
                var LlamadaWavixWebhook = MapeoEntidad(entidad);
                base.Insert(LlamadaWavixWebhook);
                return LlamadaWavixWebhook;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TLlamadaWavixWebhook Update(LlamadaWavixWebhook entidad)
        {
            try
            {
                var LlamadaWavixWebhook = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                LlamadaWavixWebhook.RowVersion = entidadExistente.RowVersion;

                base.Update(LlamadaWavixWebhook);
                return LlamadaWavixWebhook;
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


        public IEnumerable<TLlamadaWavixWebhook> Add(IEnumerable<LlamadaWavixWebhook> listadoEntidad)
        {
            try
            {
                List<TLlamadaWavixWebhook> listado = new List<TLlamadaWavixWebhook>();
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

        public IEnumerable<TLlamadaWavixWebhook> Update(IEnumerable<LlamadaWavixWebhook> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TLlamadaWavixWebhook> listado = new List<TLlamadaWavixWebhook>();
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

        /// Autor: Carlos Crispin
        /// Fecha: 26/11/2024
        /// Version: 1.0
        /// <summary>
        /// Inserta los datos del webhook a LlamadaWavixWebHook
        /// </summary>
        /// <returns> List<TipoContratoComboDTO> </returns>
        public bool? GuardarLlamadaWebhook(LlamadaWavixWebHookDTO llamada)
        {
            try
            {
                EstadoActualizacionDTO rpta = new EstadoActualizacionDTO();
                rpta.Valor = false;
                var query = string.Empty;
                var usuarioCreacion = "Webhook";
                var tipo = "Saliente";

                var resultado = _dapperRepository.QuerySPFirstOrDefault("mkt.SP_InsertarLlamadaWavixWebhook", 
                    new {
                        Uuid = llamada.Uuid,
                        TroncalSIP=llamada.TroncalSIP,
                        EstadoLlamada=llamada.EstadoLlamada,
                        OcurrenciaLlamada = llamada.OcurrenciaLlamada,
                        Origen=llamada.Origen,
                        Destino = llamada.Destino,
                        UsuarioCreacion= usuarioCreacion, 
                        Tipo= tipo
                    });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<EstadoActualizacionDTO>(resultado);
                    return rpta.Valor;
                }
                return rpta.Valor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Carlos Crispin
        /// Fecha: 26/11/2024
        /// Version: 1.0
        /// <summary>
        /// Inserta los datos del webhook a LlamadaWavixWebHook
        /// </summary>
        /// <returns> List<TipoContratoComboDTO> </returns>
        public bool? GuardarLlamadaEntranteWebhook(LlamadaWavixEntranteDTO llamada)
        {
            try
            {
                EstadoActualizacionDTO rpta = new EstadoActualizacionDTO();
                rpta.Valor = false;
                var query = string.Empty;
                var usuarioCreacion = "Webhook";
                var tipo = "Entrante";

                var resultado = _dapperRepository.QuerySPFirstOrDefault("mkt.SP_InsertarLlamadaWavixWebhook",
                    new
                    {
                        Uuid = llamada.Uuid,
                        TroncalSIP = llamada.TroncalSIP,
                        EstadoLlamada = "",
                        OcurrenciaLlamada = "",
                        Origen = llamada.Origen,
                        Destino = llamada.Destino,
                        UsuarioCreacion = usuarioCreacion,
                        Tipo = tipo
                    });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<EstadoActualizacionDTO>(resultado);
                    return rpta.Valor;
                }
                return rpta.Valor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
