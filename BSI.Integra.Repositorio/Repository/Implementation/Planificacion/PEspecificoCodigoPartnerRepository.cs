using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    public class PEspecificoCodigoPartnerRepository : GenericRepository<TPespecificoCodigoPartner>, IPEspecificoCodigoPartnerRepository
    {
        private Mapper _mapper;

        public PEspecificoCodigoPartnerRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPespecificoCodigoPartner, PespecificoCodigoPartner>(MemberList.None).ReverseMap();
                cfg.CreateMap<PespecificoCodigoPartner, PespecificoCodigoPartnerDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PespecificoCodigoPartner, TPespecificoCodigoPartner>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPespecificoCodigoPartner MapeoEntidad(PespecificoCodigoPartner entidad)
        {
            try
            {
                //crea la entidad padre
                TPespecificoCodigoPartner modelo = _mapper.Map<TPespecificoCodigoPartner>(entidad);

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

        public TPespecificoCodigoPartner Add(PespecificoCodigoPartner entidad)
        {
            try
            {
                var PespecificoCodigoPartner = MapeoEntidad(entidad);
                base.Insert(PespecificoCodigoPartner);
                return PespecificoCodigoPartner;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPespecificoCodigoPartner Update(PespecificoCodigoPartner entidad)
        {
            try
            {
                var PespecificoCodigoPartner = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PespecificoCodigoPartner.RowVersion = entidadExistente.RowVersion;

                base.Update(PespecificoCodigoPartner);
                return PespecificoCodigoPartner;
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


        public IEnumerable<TPespecificoCodigoPartner> Add(IEnumerable<PespecificoCodigoPartner> listadoEntidad)
        {
            try
            {
                List<TPespecificoCodigoPartner> listado = new List<TPespecificoCodigoPartner>();
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

        public IEnumerable<TPespecificoCodigoPartner> Update(IEnumerable<PespecificoCodigoPartner> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPespecificoCodigoPartner> listado = new List<TPespecificoCodigoPartner>();
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


        /// Autor: Marco Jose Villanueva Torres.
        /// Fecha: 14-03-2025
        /// <param name="id"> (PK) </param> 
        /// <summary>
        /// Obtiene el registro por el Primary Key
        /// </summary>
        /// <returns >PespecificoCodigoPartner || null</returns>
        public PespecificoCodigoPartner? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT Id,
                           IdPEspecifico,
                           Codigo,
                           Pdu,
                           FechaInicio,
                           Estado,
                           UsuarioCreacion,
                           UsuarioModificacion,
                           FechaCreacion,
                           FechaModificacion,
                           RowVersion FROM pla.T_PEspecificoCodigoPartner
                    WHERE Id=@id AND estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PespecificoCodigoPartner>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }


        /// Autor:  Marco Jose Villanueva Torres.
        /// Fecha: 14-03-2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de PespecificoCodigoPartner.
        /// </summary>
        /// <returns> List<PespecificoCodigoPartnerdDTO> </returns>
        public List<PespecificoCodigoPartnerDTO> ObtenerPEspecificoCodigoPartner(int idPGeneral)
        {
            try
            {
                List<PespecificoCodigoPartnerDTO> rpta = new List<PespecificoCodigoPartnerDTO>();
                var query = @"SELECT IdPEspecificoCodigoPartner AS Id,
                                   IdPEspecifico,
                                   Codigo,
                                   Pdu,
                                   FechaInicio,
                                   IdPGeneral 
                            FROM pla.V_ObtenerCodigoPartnerPEspecifico 
                            where IdPGeneral=@idPGeneral";
                var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PespecificoCodigoPartnerDTO>>(resultado);

                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
