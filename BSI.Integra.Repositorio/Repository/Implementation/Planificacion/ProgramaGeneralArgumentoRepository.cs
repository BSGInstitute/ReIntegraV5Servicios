using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    public class ProgramaGeneralArgumentoRepository : GenericRepository<TPartnerPw>, IProgramaGeneralArgumentoRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralArgumentoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralArgumento, ProgramaGeneralArgumentoDTO>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPartnerPw MapeoEntidad(PartnerPw entidad)
        {
            try
            {
                //crea la entidad padre
                TPartnerPw modelo = _mapper.Map<TPartnerPw>(entidad);

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

        public TPartnerPw Add(PartnerPw entidad)
        {
            try
            {
                var PartnerPw = MapeoEntidad(entidad);
                base.Insert(PartnerPw);
                return PartnerPw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPartnerPw Update(PartnerPw entidad)
        {
            try
            {
                var PartnerPw = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PartnerPw.RowVersion = entidadExistente.RowVersion;

                base.Update(PartnerPw);
                return PartnerPw;
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


        public IEnumerable<TPartnerPw> Add(IEnumerable<PartnerPw> listadoEntidad)
        {
            try
            {
                List<TPartnerPw> listado = new List<TPartnerPw>();
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

        public IEnumerable<TPartnerPw> Update(IEnumerable<PartnerPw> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPartnerPw> listado = new List<TPartnerPw>();
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
        public bool ObtenerPorId(int id)
        {
            try
            {
                //var respuesta = _unitOfWork.CourierRepository.ObtenerCourier();
                return true; // _mapper.Map<List<CourierDTO>>(respuesta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
