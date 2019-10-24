namespace RomTool.Data
{
    public enum BlockType
    {
        NA = 0x00,
        RawFile = 0x01,
        GuidDefined = 0x02,    // GuidDefined section or Freeform file
        SecCore = 0x03,
        PeiCore = 0x04,
        DxeCore = 0x05,
        PeiModule = 0x06,
        DxeDriver = 0x07,
        Application = 0x09,
        SmmModule = 0x0a,
        SmmCore = 0x0d,
        Pe32Image = 0x10,
        DxeDependency = 0x13,
        Version = 0x14,
        Ui = 0x15,
        FreeformGuid = 0x18,
        RawSection = 0x19,
        PeiDependency = 0x1b,
        MmDependency = 0x1c,
        Pad = 0xf0
    }
}