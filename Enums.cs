namespace RomTool
{
    public enum Type
    {
        Image,
        Region,
        Padding,
        Volume,
        File,
        Section,
        Nvar,
        Free
    }
    
    public enum SectionType
    {
        GuidDefined = 0x02,    // header 0x18
        Pe32Image = 0x10,    // header 0x04
        DxeDependency = 0x13,    // header 0x04
        Version = 0x14,    // header 0x06
        Ui = 0x15,    // header 0x04
        FreeformGuid = 0x18,    // header 0x14
        Raw = 0x19,    // header 0x04
        PeiDependency = 0x1b,    // header 0x04
        MmDependency = 0x1c    // header 0x04
    }

    public enum FileType
    {
        NA = 0x00,
        Raw = 0x01,
        Freeform = 0x02,
        SecCore = 0x03,
        PeiCore = 0x04,
        DxeCore = 0x05,
        PeiModule = 0x06,
        DxeDriver = 0x07,
        Application = 0x09,
        SmmModule = 0x0a,
        SmmCore = 0x0d,
        Pad = 0xf0
    }
}