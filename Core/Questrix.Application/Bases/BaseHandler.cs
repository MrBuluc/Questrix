using Questrix.Application.Interfaces.AutoMapper;
using Questrix.Application.Interfaces.UnitOfWorks;

namespace Questrix.Application.Bases
{
    public class BaseHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        public readonly IMapper mapper = mapper;
        public readonly IUnitOfWork unitOfWork = unitOfWork;
    }
}
