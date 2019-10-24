using System;

namespace RomTool.Data
{
    public static class GuidStore
    {
        public static Guid FsFfSv2 = Guid.Parse("8C8CE578-8A3D-4F1C-9935-896185C32DD3");
        public static Guid EfiSetupVariable = Guid.Parse("EC87D643-EBA4-4BB5-A1E5-3F3E36B20DA9");
        public static Guid EfiGlobalVariable = Guid.Parse("8BE4DF61-93CA-11D2-AA0D-00E098032B8C");
        public static Guid EfiSioVariable = Guid.Parse("560BF58A-1E0D-4D7E-953F-2980A261E031");
        public static Guid LzmaSection = Guid.Parse("EE4E5898-3914-4259-9D6E-DC7BD79403CF");
    }
}