using System.ComponentModel;

namespace Common.Enum
{
    public enum SystemMessage
    {
        [Description("has been connected to the room")]
        Greeting,
        [Description("left room")]
        Farewell,
    }
}