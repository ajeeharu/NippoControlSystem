using NippoControlSystem.Application.Dtos;
using NippoControlSystem.Domain.Tables;
using NippoControlSystem.Infrastructure.Dtos;

namespace NippoControlSystem.Domain.Services
{
    public interface IItemTableProvider
    {
        ItemTable<CheckDatDto> CheckDat { get; }
        ItemTable<PortDatDto> PortDat { get; }
        void SetCheckDat(IEnumerable<CheckDatDto> rows);
        void SetPortDat(IEnumerable<PortDatDto> rows);
    }

    public class ItemTableProvider : IItemTableProvider
    {
        public ItemTable<CheckDatDto> CheckDat { get; private set; } = new();
        public ItemTable<PortDatDto> PortDat { get; private set; } = new();

        public void SetCheckDat(IEnumerable<CheckDatDto> rows)
            => CheckDat = new ItemTable<CheckDatDto>(rows);

        public void SetPortDat(IEnumerable<PortDatDto> rows)
            => PortDat = new ItemTable<PortDatDto>(rows);
    }
}