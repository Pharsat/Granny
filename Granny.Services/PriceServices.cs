using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Granny.DAO.EntitiesRepository.Interface;
using Granny.DAO.UnitOfWork.Interface;
using Granny.DataModel;
using Granny.DataTransferObject.Location;
using Granny.DataTransferObject.Price;
using Granny.DataTransferObject.Product;
using Granny.Services.Interfaces;

namespace Granny.Services
{
    public class PriceServices : IPriceServices
    {
        private IUnitOfWork _unitOfWork;
        private ILocationServices _locationServices;
        private IProductServices _productServices;
        private IPriceRepository _priceRepository;
        private IMapper _mapper;

        public PriceServices(
            IUnitOfWork unitOfWork,
            IPriceRepository priceRepository,
            ILocationServices locationServices,
            IProductServices productServices,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _priceRepository = priceRepository;
            _locationServices = locationServices;
            _productServices = productServices;
            _mapper = mapper;
        }

        public async Task<PriceDto> Create(PriceCreateDto priceDto)
        {
            Location location = await _locationServices.GetByName(priceDto.Name).ConfigureAwait(false);
            if (location == null) location.LocationId = await _locationServices.Create(_mapper.Map<LocationDto>(location));

            ProductDto product =  _productServices.GetById(priceDto.PluCode);
            if (product == null) await _productServices.Create(product);

            long productId = _mapper.Map<long>(product.PluCode);
            if (!await _priceRepository.CheckIfExists(productId, priceDto.Price, location.LocationId))
            {
                Price price = new Price();//_mapper.Map<Price>(priceDto).Map(product).Map(location);
                await _priceRepository.Create(price);
                await _unitOfWork.SaveAsync();
                return _mapper.Map<PriceDto>(price);
            }
            return null;
        }

        public async Task<PriceDto> GetBestProductPrice(string pluCode)
        {
            long productId = _mapper.Map<long>(pluCode);
            Price price = await _priceRepository.GetBestProductPrice(productId);
            return _mapper.Map<PriceDto>(price);
        }

        public async Task<IEnumerable<PriceDto>> GetNextProductPrices(string pluCode, decimal value)
        {
            long productId = _mapper.Map<long>(pluCode);
            IEnumerable<Price> priceList = await _priceRepository.GetNextProductPrices(productId, value);
            return _mapper.Map<IEnumerable<PriceDto>>(priceList);
        }

        public async Task<IEnumerable<PriceDto>> GetPricesByLocation(string locationDescription)
        {
            IEnumerable<Price> priceList = await _priceRepository.GetPricesByLocation(locationDescription).ConfigureAwait(false);
            return _mapper.Map<IEnumerable<PriceDto>>(priceList);
        }
    }
}
