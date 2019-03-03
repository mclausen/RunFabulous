using System;

namespace Profile.Messages.External
{
    public class ProfileCreatedEvent
    {
        public string ProfileId { get; protected set; }

        public ProfileCreatedEvent(string profileId)
        {
            if (string.IsNullOrWhiteSpace(profileId)) throw new ArgumentNullException(nameof(profileId));
            ProfileId = profileId;
        }
    }
}
