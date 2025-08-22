using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class MaestroCursoComplementarioRepository : GenericRepository<TPuestoTrabajoCursoComplementario>, IMaestroCursoComplementarioRepository
    {
        private Mapper _mapper;
        public MaestroCursoComplementarioRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPuestoTrabajoCursoComplementario, CursoComplementario>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPuestoTrabajoCursoComplementario, CursoComplementarioDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<CursoComplementarioDTO, CursoComplementario>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPuestoTrabajoCursoComplementario, CursoComplementarioDTO>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TPuestoTrabajoCursoComplementario MapeoEntidad(CursoComplementario entidad)
        {
            try
            {
                //crea la entidad padre
                TPuestoTrabajoCursoComplementario modelo = _mapper.Map<TPuestoTrabajoCursoComplementario>(entidad);

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

        public TPuestoTrabajoCursoComplementario Add(CursoComplementario entidad)
        {
            try
            {
                var CursoComplementario = MapeoEntidad(entidad);
                base.Insert(CursoComplementario);
                return CursoComplementario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPuestoTrabajoCursoComplementario Update(CursoComplementario entidad)
        {
            try
            {
                var CursoComplementario = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CursoComplementario.RowVersion = entidadExistente.RowVersion;

                base.Update(CursoComplementario);
                return CursoComplementario;
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


        public IEnumerable<TPuestoTrabajoCursoComplementario> Add(IEnumerable<CursoComplementario> listadoEntidad)
        {
            try
            {
                List<TPuestoTrabajoCursoComplementario> listado = new List<TPuestoTrabajoCursoComplementario>();
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

        public IEnumerable<TPuestoTrabajoCursoComplementario> Update(IEnumerable<CursoComplementario> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPuestoTrabajoCursoComplementario> listado = new List<TPuestoTrabajoCursoComplementario>();
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
