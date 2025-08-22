using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ConjuntoListaDetalleValorRepository
    /// Autor: Adriana Chipana Ampuero.
    /// Fecha: 12/08/2022
    /// <summary>
    /// Gestión general de T_ConjuntoListaDetalleValor
    /// </summary>
    public class ConjuntoListaDetalleValorRepository : GenericRepository<TConjuntoListaDetalleValor>, IConjuntoListaDetalleValorRepository
    {
        private Mapper _mapper;

        public ConjuntoListaDetalleValorRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TConjuntoListaDetalleValor, ConjuntoListaDetalleValor>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TConjuntoListaDetalleValor MapeoEntidad(ConjuntoListaDetalleValor entidad)
        {
            try
            {

                TConjuntoListaDetalleValor modelo = _mapper.Map<TConjuntoListaDetalleValor>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TConjuntoListaDetalleValor Add(ConjuntoListaDetalleValor entidad)
        {
            try
            {
                var ConjuntoListaDetalleValor = MapeoEntidad(entidad);
                base.Insert(ConjuntoListaDetalleValor);
                return ConjuntoListaDetalleValor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TConjuntoListaDetalleValor Update(ConjuntoListaDetalleValor entidad)
        {
            try
            {
                var ConjuntoListaDetalleValor = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ConjuntoListaDetalleValor.RowVersion = entidadExistente.RowVersion;

                base.Update(ConjuntoListaDetalleValor);
                return ConjuntoListaDetalleValor;
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


        public IEnumerable<TConjuntoListaDetalleValor> Add(IEnumerable<ConjuntoListaDetalleValor> listadoEntidad)
        {
            try
            {
                List<TConjuntoListaDetalleValor> listado = new List<TConjuntoListaDetalleValor>();
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

        public IEnumerable<TConjuntoListaDetalleValor> Update(IEnumerable<ConjuntoListaDetalleValor> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TConjuntoListaDetalleValor> listado = new List<TConjuntoListaDetalleValor>();
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


        public List<FiltroSegmentoValorTipoDTO> ObtenerConjuntoListaDetalleValor(int idConjuntoListaDetalle)
        {
            try
            {
                List<FiltroSegmentoValorTipoDTO> respuesta = new List<FiltroSegmentoValorTipoDTO>();

                var _query = @"SELECT Id, Valor, IdCategoriaObjetoFiltro FROM mkt.T_ConjuntoListaDetalleValor WHERE IdConjuntoListaDetalle = @idConjuntoListaDetalle";

                var query = _dapperRepository.QueryDapper(_query, new { idConjuntoListaDetalle });

                if (!string.IsNullOrEmpty(query))
                {
                    respuesta = JsonConvert.DeserializeObject<List<FiltroSegmentoValorTipoDTO>>(query);
                }

                return respuesta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }





    }
}
