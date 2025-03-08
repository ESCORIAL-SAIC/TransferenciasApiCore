using AutoMapper;
using TransferenciasApiCore.Models;
using TransferenciasApiCore.Models.SolicitudTransferencia;
using TransferenciasApiCore.Models.Transferencia;
using TransferenciasApiCore.Models.Usuario;

namespace TransferenciasApiCore
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EscoTxSolicitudTransferenciaInsertDto, EscoTxSolicitudTransferencia>();
            CreateMap<EscoTxItemSolicitudTransferenciaInsertDto, EscoTxItemSolicitudTransferencia>();
            CreateMap<EscoTxItemSolicitudTransferenciaUpdatePickeadoDto, EscoTxItemSolicitudTransferencia>();
            CreateMap<EscoUsuarioAppLoginDto, EscoUsuarioApp>();
            CreateMap<EscoTxTransferenciaInsertDto, EscoTxTransferencia>();
            CreateMap<EscoTxItemTransferenciaInsertDto, EscoTxItemTransferencia>();
        }
    }
}
