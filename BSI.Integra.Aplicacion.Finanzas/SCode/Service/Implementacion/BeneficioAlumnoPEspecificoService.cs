using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: BeneficiosAlumnoPEspecificoService
    /// Autor: Jonathan Caipo
    /// Fecha: 20/10/2022
    /// <summary>
    /// Gestión general de T_BeneficiosAlumnoPEspecifico
    /// </summary>
    public class BeneficioAlumnoPEspecificoService : IBeneficioAlumnoPEspecificoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public BeneficioAlumnoPEspecificoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TBeneficiosAlumnoPespecifico, BeneficioAlumnoPEspecifico>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// <summary>
        /// Gestión general de T_TBeneficiosAlumnoPespecifico
        /// </summary>
        /// <param name="oportunidadVerificada"></param>
        /// <returns></returns>
        public BeneficioAlumnoPEspecifico InsertarBeneficios(OportunidadCodigoMatriculaDTO oportunidadVerificada)
        {
            BeneficioAlumnoPEspecifico beneficiosAlumnoPEspecifico = new();

            var datosCodigoMatricula = _unitOfWork.BeneficioAlumnoPEspecificoRepository.ObtenerDatosPorCodigoMatricula(oportunidadVerificada.CodigoMatricula!).FirstOrDefault();

            using (TransactionScope scope = new TransactionScope())
            {
                if (datosCodigoMatricula == null)
                {
                    var datosCodigoMatricula2 = _unitOfWork.BeneficioAlumnoPEspecificoRepository.ObtenerDatosPorCodigoMatriculaSinPaquete(oportunidadVerificada.CodigoMatricula).FirstOrDefault();

                    beneficiosAlumnoPEspecifico.IdAlumno = datosCodigoMatricula2.IdAlumno;
                    beneficiosAlumnoPEspecifico.IdPgeneral = datosCodigoMatricula2.IdPGeneral;
                    beneficiosAlumnoPEspecifico.IdPespecifico = datosCodigoMatricula2.IdPEspecifico;
                    beneficiosAlumnoPEspecifico.IdMatriculaCabecera = datosCodigoMatricula2.IdMatriculaCabecera;
                    beneficiosAlumnoPEspecifico.Beneficios = "Sin beneficio (regularizar)";
                    beneficiosAlumnoPEspecifico.Estado = true;
                    beneficiosAlumnoPEspecifico.UsuarioCreacion = "Regularizado";
                    beneficiosAlumnoPEspecifico.UsuarioModificacion = "Regularizado";
                    beneficiosAlumnoPEspecifico.FechaCreacion = DateTime.Now;
                    beneficiosAlumnoPEspecifico.FechaModificacion = DateTime.Now;
                    _unitOfWork.BeneficioAlumnoPEspecificoRepository.Add(beneficiosAlumnoPEspecifico);
                    _unitOfWork.Commit();
                }
                else if (datosCodigoMatricula.Paquete == null && datosCodigoMatricula.IdPEspecifico >= 0)
                {
                    var beneficiosTipo2 = _unitOfWork.BeneficioAlumnoPEspecificoRepository.ObtenerBeneficiosProgramaTipo2(datosCodigoMatricula.IdPGeneral).FirstOrDefault();

                    beneficiosAlumnoPEspecifico.IdAlumno = datosCodigoMatricula.IdAlumno;
                    beneficiosAlumnoPEspecifico.IdPgeneral = datosCodigoMatricula.IdPGeneral;
                    beneficiosAlumnoPEspecifico.IdPespecifico = datosCodigoMatricula.IdPEspecifico;
                    beneficiosAlumnoPEspecifico.IdMatriculaCabecera = datosCodigoMatricula.IdMatriculaCabecera;
                    beneficiosAlumnoPEspecifico.Beneficios = beneficiosTipo2.Titulo;
                    beneficiosAlumnoPEspecifico.Estado = true;
                    beneficiosAlumnoPEspecifico.UsuarioCreacion = "Regularizado"; //OportunidadVerificada.Usuario;
                    beneficiosAlumnoPEspecifico.UsuarioModificacion = "Regularizado"; //OportunidadVerificada.Usuario;
                    beneficiosAlumnoPEspecifico.FechaCreacion = DateTime.Now;
                    beneficiosAlumnoPEspecifico.FechaModificacion = DateTime.Now;
                    _unitOfWork.BeneficioAlumnoPEspecificoRepository.Add(beneficiosAlumnoPEspecifico);
                    _unitOfWork.Commit();
                }
                else
                {
                    var beneficiosTipo1 = _unitOfWork.BeneficioAlumnoPEspecificoRepository.ObtenerBeneficiosProgramaTipo1(datosCodigoMatricula.IdPGeneral, datosCodigoMatricula.IdPais, datosCodigoMatricula.Paquete);

                    foreach (var beneficio in beneficiosTipo1)
                    {
                        BeneficioAlumnoPEspecifico beneficiosAlumnoPEspecifico2 = new BeneficioAlumnoPEspecifico();
                        beneficiosAlumnoPEspecifico2.IdAlumno = datosCodigoMatricula.IdAlumno;
                        beneficiosAlumnoPEspecifico2.IdPgeneral = datosCodigoMatricula.IdPGeneral;
                        beneficiosAlumnoPEspecifico2.IdPespecifico = datosCodigoMatricula.IdPEspecifico;
                        beneficiosAlumnoPEspecifico2.IdMatriculaCabecera = datosCodigoMatricula.IdMatriculaCabecera;
                        beneficiosAlumnoPEspecifico2.Beneficios = beneficio.Descripcion;
                        beneficiosAlumnoPEspecifico2.Estado = true;
                        beneficiosAlumnoPEspecifico2.UsuarioCreacion = "Regularizado";
                        beneficiosAlumnoPEspecifico2.UsuarioModificacion = "Regularizado";
                        beneficiosAlumnoPEspecifico2.FechaCreacion = DateTime.Now;
                        beneficiosAlumnoPEspecifico2.FechaModificacion = DateTime.Now;
                        _unitOfWork.BeneficioAlumnoPEspecificoRepository.Add(beneficiosAlumnoPEspecifico2);
                        _unitOfWork.Commit();
                    }
                }
                scope.Complete();
            }
            return beneficiosAlumnoPEspecifico;
        }

    }
}
