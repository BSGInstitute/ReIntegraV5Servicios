using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: WhatsAppNumeroValidadoRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 06/12/2022
    /// <summary>
    /// Gestión general de T_WhatsAppNumeroValidado
    /// </summary>
    public class WhatsAppNumeroValidadoRepository : GenericRepository<TWhatsAppNumeroValidado>, IWhatsAppNumeroValidadoRepository
    {
        private Mapper _mapper;

        public WhatsAppNumeroValidadoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TWhatsAppNumeroValidado, WhatsAppNumeroValidado>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TWhatsAppNumeroValidado MapeoEntidad(WhatsAppNumeroValidado entidad)
        {
            try
            {
                //crea la entidad padre
                TWhatsAppNumeroValidado modelo = _mapper.Map<TWhatsAppNumeroValidado>(entidad);

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

        public TWhatsAppNumeroValidado Add(WhatsAppNumeroValidado entidad)
        {
            try
            {
                var WhatsAppNumeroValidado = MapeoEntidad(entidad);
                base.Insert(WhatsAppNumeroValidado);
                return WhatsAppNumeroValidado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TWhatsAppNumeroValidado Update(WhatsAppNumeroValidado entidad)
        {
            try
            {
                var WhatsAppNumeroValidado = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                WhatsAppNumeroValidado.RowVersion = entidadExistente.RowVersion;

                base.Update(WhatsAppNumeroValidado);
                return WhatsAppNumeroValidado;
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


        public IEnumerable<TWhatsAppNumeroValidado> Add(IEnumerable<WhatsAppNumeroValidado> listadoEntidad)
        {
            try
            {
                List<TWhatsAppNumeroValidado> listado = new List<TWhatsAppNumeroValidado>();
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

        public IEnumerable<TWhatsAppNumeroValidado> Update(IEnumerable<WhatsAppNumeroValidado> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TWhatsAppNumeroValidado> listado = new List<TWhatsAppNumeroValidado>();
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
        /// Autor: Gilmer Quispe.
        /// Fecha: 06/12/2022
        /// <summary>
        /// Verifica si existe el numero en la tabla T_WhatsAppNumeroValidado
        /// </summary>
        /// <param name="numero"></param>
        /// <returns> true or false </returns>
        public bool VerificarNumeroValidado(string numero)
        {
            try
            {
                return Exist(w => w.NumeroCelular == numero);

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
