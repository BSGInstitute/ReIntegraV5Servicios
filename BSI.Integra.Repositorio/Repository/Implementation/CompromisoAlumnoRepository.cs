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
    /// Repositorio: CompromisoAlumnoRepository
    /// Autor:  Gilmer uispe.
    /// Fecha: 12/12/2022
    /// <summary>
    /// Gestión general de T_CompromisoAlumno
    /// </summary>
    public class CompromisoAlumnoRepository : GenericRepository<TCompromisoAlumno>, ICompromisoAlumnoRepository
    {
        private Mapper _mapper;

        public CompromisoAlumnoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCompromisoAlumno, CompromisoAlumno>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TCompromisoAlumno MapeoEntidad(CompromisoAlumno entidad)
        {
            try
            {
                //crea la entidad padre
                TCompromisoAlumno modelo = _mapper.Map<TCompromisoAlumno>(entidad);

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

        public TCompromisoAlumno Add(CompromisoAlumno entidad)
        {
            try
            {
                var CompromisoAlumno = MapeoEntidad(entidad);
                base.Insert(CompromisoAlumno);
                return CompromisoAlumno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCompromisoAlumno Update(CompromisoAlumno entidad)
        {
            try
            {
                var CompromisoAlumno = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CompromisoAlumno.RowVersion = entidadExistente.RowVersion;

                base.Update(CompromisoAlumno);
                return CompromisoAlumno;
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


        public IEnumerable<TCompromisoAlumno> Add(IEnumerable<CompromisoAlumno> listadoEntidad)
        {
            try
            {
                List<TCompromisoAlumno> listado = new List<TCompromisoAlumno>();
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

        public IEnumerable<TCompromisoAlumno> Update(IEnumerable<CompromisoAlumno> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCompromisoAlumno> listado = new List<TCompromisoAlumno>();
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
    }
}
