using ReservationSystem.Domain.Repositories;

namespace ReservationSystem.Application.Services
{
    public class OSsService
    {
        private readonly IOSRepository _osRepository;

        public OSsService(IOSRepository osRepository)
        {
            _osRepository = osRepository;
        }

        public async Task<IEnumerable<string>> GetDictionary()
            => await _osRepository.GetDictionary();
    }
}
