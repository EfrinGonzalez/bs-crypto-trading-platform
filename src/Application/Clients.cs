using MediatR;

namespace BsCryptoTrading.Application.Clients;

public sealed record OnboardClientCommand(string ExternalRef, string LegalName) : IRequest<Guid>;

public sealed class OnboardClientHandler : IRequestHandler<OnboardClientCommand, Guid>
{
    public Task<Guid> Handle(OnboardClientCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.ExternalRef))
            throw new ArgumentException("ExternalRef is required");

        return Task.FromResult(Guid.NewGuid());
    }
}
