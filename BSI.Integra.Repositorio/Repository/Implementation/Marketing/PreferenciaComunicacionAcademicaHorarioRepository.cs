using AutoMapper;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Marketing;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Marketing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing
{
    public class PreferenciaComunicacionAcademicaHorarioRepository : GenericRepository<TPreferenciaComunicacionAcademicaHorario>, IPreferenciaComunicacionAcademicaHorarioRepository
    {
        private Mapper _mapper;

        public PreferenciaComunicacionAcademicaHorarioRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPreferenciaComunicacionAcademicaHorario, PreferenciaComunicacionAcademicaHorario>(MemberList.None).ReverseMap();
                cfg.CreateMap<PreferenciaComunicacionAcademicaHorario, PreferenciaComunicacionAcademicaHorarioDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PreferenciaComunicacionAcademicaHorario, TPreferenciaComunicacionAcademicaHorario>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPreferenciaComunicacionAcademicaHorario MapeoEntidad(PreferenciaComunicacionAcademicaHorario entidad)
        {
            try
            {
                TPreferenciaComunicacionAcademicaHorario modelo = _mapper.Map<TPreferenciaComunicacionAcademicaHorario>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPreferenciaComunicacionAcademicaHorario Add(PreferenciaComunicacionAcademicaHorario entidad)
        {
            try
            {
                var PreferenciaComunicacionAcademicaHorario = MapeoEntidad(entidad);
                base.Insert(PreferenciaComunicacionAcademicaHorario);
                return PreferenciaComunicacionAcademicaHorario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPreferenciaComunicacionAcademicaHorario Update(PreferenciaComunicacionAcademicaHorario entidad)
        {
            try
            {
                var PreferenciaComunicacionAcademicaHorario = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PreferenciaComunicacionAcademicaHorario.RowVersion = entidadExistente.RowVersion;

                base.Update(PreferenciaComunicacionAcademicaHorario);
                return PreferenciaComunicacionAcademicaHorario;
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


        public IEnumerable<TPreferenciaComunicacionAcademicaHorario> Add(IEnumerable<PreferenciaComunicacionAcademicaHorario> listadoEntidad)
        {
            try
            {
                List<TPreferenciaComunicacionAcademicaHorario> listado = new List<TPreferenciaComunicacionAcademicaHorario>();
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

        public IEnumerable<TPreferenciaComunicacionAcademicaHorario> Update(IEnumerable<PreferenciaComunicacionAcademicaHorario> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPreferenciaComunicacionAcademicaHorario> listado = new List<TPreferenciaComunicacionAcademicaHorario>();
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
        public List<PreferenciaComunicacionAcademicaHorarioDTO> ObtenerPreferenciaHorarioComunicacionByIdAlumno(int IdAlumno)
        {
            try
            {
                List<PreferenciaComunicacionAcademicaHorarioDTO> rpta = new List<PreferenciaComunicacionAcademicaHorarioDTO>();
                var query = @"
                    SELECT Id, IdAlumno, IdBloqueHorarioDetalle
                    FROM mkt.T_PreferenciaComunicacionAcademicaHorario
                    WHERE Estado = 1 AND IdAlumno = @IdAlumno";
                var resultado = _dapperRepository.QueryDapper(query, new { IdAlumno });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PreferenciaComunicacionAcademicaHorarioDTO>>(resultado);
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
