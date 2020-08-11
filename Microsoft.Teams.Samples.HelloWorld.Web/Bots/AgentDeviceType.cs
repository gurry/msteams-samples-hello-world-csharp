namespace Microsoft.Teams.Samples.HelloWorld.Web
{
    public static class AgentDeviceType
    {
        public enum Type
        {
            Unknown = 0,
            Server = 1,
            Desktop = 2,
            Laptop = 3,
            Mobile = 4
        }

        public static string AsString(Type code)
        {
            switch (code)
            {
                case Type.Server:
                    return "Server";

                case Type.Desktop:
                    return "Desktop";

                case Type.Laptop:
                    return "Laptop";

                case Type.Mobile:
                    return "Mobile";

                default: // Including Unknown
                    return "Unknown";
            }
        }
    }
}