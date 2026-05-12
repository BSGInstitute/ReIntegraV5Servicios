using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: FeriadoService
    /// Autor: aarroyoh
    /// Fecha: 06/05/2026
    /// <summary>
    /// CRUD de feriados (pla.T_Feriado) y consulta combinada por país (pla.V_FeriadoConPais).
    /// </summary>
    public class FeriadoService : IFeriadoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public FeriadoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFeriado, Feriado>(MemberList.None).ReverseMap();
                cfg.CreateMap<TFeriado, FeriadoDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<Feriado, FeriadoDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public IEnumerable<FeriadoDTO> Listar()
        {
            try
            {
                return _unitOfWork.FeriadoRepository.Obtener();
            }
            catch
            {
                throw;
            }
        }

        public FeriadoDTO ObtenerPorId(int id)
        {
            try
            {
                var entidad = _unitOfWork.FeriadoRepository.ObtenerPorId(id);
                if (entidad == null || entidad.Id == 0)
                {
                    throw new BadRequestException($"No existe el feriado con id {id}");
                }
                return _mapper.Map<FeriadoDTO>(entidad);
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<FeriadoConPaisDTO> ListarPorPaises(IEnumerable<int> idsTroncalPais)
        {
            try
            {
                if (idsTroncalPais == null || !idsTroncalPais.Any())
                {
                    throw new BadRequestException("Debe enviar al menos un id de país");
                }
                return _unitOfWork.FeriadoRepository.ObtenerPorPaises(idsTroncalPais);
            }
            catch
            {
                throw;
            }
        }

        public FeriadoDTO Insertar(FeriadoDTO dto, string usuario)
        {
            try
            {
                ValidarDto(dto);

                Feriado entidad = new()
                {
                    Tipo = dto.Tipo,
                    Dia = dto.Dia,
                    Motivo = dto.Motivo,
                    Frecuencia = dto.Frecuencia,
                    IdTroncalCiudad = dto.IdTroncalCiudad,
                    Estado = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario
                };

                var resultado = _unitOfWork.FeriadoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<FeriadoDTO>(resultado);
            }
            catch
            {
                throw;
            }
        }

        public FeriadoDTO Actualizar(FeriadoDTO dto, string usuario)
        {
            try
            {
                if (dto == null || dto.Id <= 0)
                {
                    throw new BadRequestException("El feriado o el id no son validos");
                }
                ValidarDto(dto);

                var existente = _unitOfWork.FeriadoRepository.ObtenerPorId(dto.Id);
                if (existente == null || existente.Id == 0)
                {
                    throw new BadRequestException($"No existe el feriado con id {dto.Id}");
                }

                existente.Tipo = dto.Tipo;
                existente.Dia = dto.Dia;
                existente.Motivo = dto.Motivo;
                existente.Frecuencia = dto.Frecuencia;
                existente.IdTroncalCiudad = dto.IdTroncalCiudad;
                existente.FechaModificacion = DateTime.Now;
                existente.UsuarioModificacion = usuario;

                var resultado = _unitOfWork.FeriadoRepository.Update(existente);
                _unitOfWork.Commit();
                return _mapper.Map<FeriadoDTO>(resultado);
            }
            catch
            {
                throw;
            }
        }

        public bool Eliminar(int id, string usuario)
        {
            try
            {
                var existente = _unitOfWork.FeriadoRepository.ObtenerPorId(id);
                if (existente == null || existente.Id == 0)
                {
                    throw new BadRequestException($"No existe el feriado con id {id}");
                }
                var resultado = _unitOfWork.FeriadoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return resultado;
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<ComboTroncalCiudadDTO> ComboTroncalCiudad()
        {
            try
            {
                return _unitOfWork.FeriadoRepository.ObtenerComboTroncalCiudad();
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<ComboTroncalPaisDTO> ComboTroncalPais()
        {
            try
            {
                return _unitOfWork.FeriadoRepository.ObtenerComboTroncalPais();
            }
            catch
            {
                throw;
            }
        }

        private static void ValidarDto(FeriadoDTO dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("El feriado es nulo");
            }
            if (string.IsNullOrWhiteSpace(dto.Motivo))
            {
                throw new BadRequestException("El motivo es obligatorio");
            }
            if (dto.Frecuencia != 0 && dto.Frecuencia != 1)
            {
                throw new BadRequestException("La frecuencia debe ser 0 (anual) o 1 (unico)");
            }
            if (dto.IdTroncalCiudad <= 0)
            {
                throw new BadRequestException("El id de la ciudad es obligatorio");
            }
        }
    }
}
