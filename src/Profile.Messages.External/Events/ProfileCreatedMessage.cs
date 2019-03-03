using System;

namespace Profile.Messages.External
{
    public class ProfileCreatedMessage
    {
        public string ProfileId { get; protected set; }

        public ProfileCreatedMessage(string profileId)
        {
            if (string.IsNullOrWhiteSpace(profileId)) throw new ArgumentNullException(nameof(profileId));
            ProfileId = profileId;
        }
    }
}
