using System.Diagnostics.CodeAnalysis;
using MediatR;
using Persistence;

namespace Application.Activities
{
  public class Delete
  {
    public record Command(Guid Id) : IRequest<bool>;

    public class Handler : IRequestHandler<Command, bool>
    {
      public readonly DataContext _context;
      public Handler(DataContext context)
      {
        _context = context;
      }

      public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
      {
        var activity = await _context.Activities.FindAsync(request.Id);

        if (activity == null)
        {
          return false;
        }
        _context.Activities.Remove(activity);
        await _context.SaveChangesAsync();
        return true;
      }
    }

  }
}