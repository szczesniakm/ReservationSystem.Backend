using ReservationSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
