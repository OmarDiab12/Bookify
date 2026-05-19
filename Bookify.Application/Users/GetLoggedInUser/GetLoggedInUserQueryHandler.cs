using Bookify.Application.Abstractions.Authentication;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Application.Users.Login;
using Bookify.Domain.Abstractions;
using MediatR;

namespace Bookify.Application.Users.GetLoggedInUser
{
    public class GetLoggedInUserQueryHandler : IQueryHandler<GetLoggedInUserQuery, GetLoggedInUserResponse>
    {
        private readonly IJwtService _jwtService;
        public GetLoggedInUserQueryHandler(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }
        public async Task<Result<GetLoggedInUserResponse>> Handle(GetLoggedInUserQuery request, CancellationToken cancellationToken)
        {
            return new GetLoggedInUserResponse();
        }

    }
}
