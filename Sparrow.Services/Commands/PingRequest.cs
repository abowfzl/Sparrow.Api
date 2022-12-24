using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Sparrow.Services.Commands;

public class PongResponse
{
    public string? Message { get; set; }
}
public class PingCommand : IRequest<PongResponse>
{
    [Required]
    public string? UserInput { get; set; }
}

public class PingCommandHandler : IRequestHandler<PingCommand, PongResponse>
{

    public PingCommandHandler()
    {
    }

    public Task<PongResponse> Handle(PingCommand request, CancellationToken cancellationToken)
    {
        //do something!
        return Task.FromResult(new PongResponse { Message = "success!" });
    }
}
