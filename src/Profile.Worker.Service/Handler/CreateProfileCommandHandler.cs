using Profile.Messages.External;
using Profile.Messages.External.Commands;
using Rebus.Bus;
using Rebus.Handlers;
using System.Threading.Tasks;

namespace Profile.Worker.Service.Handler
{
    public class CreateProfileCommandHandler : IHandleMessages<CreateProfileCommand>
    {
        private readonly IBus bus;

        public CreateProfileCommandHandler(IBus bus)
        {
            this.bus = bus;
        }

        public async Task Handle(CreateProfileCommand message)
        {
            // Create Profile

            // Publish ProfileCreated Event
            await bus.Publish(new ProfileCreatedEvent(string.Empty));
        }
    }
}
