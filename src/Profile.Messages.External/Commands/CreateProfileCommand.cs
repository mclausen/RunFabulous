using System;

namespace Profile.Messages.External.Commands
{
    public class CreateProfileCommand
    {
        public string Name { get; protected set; }

        public CreateProfileCommand(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            Name = name;
        }
    }
}
