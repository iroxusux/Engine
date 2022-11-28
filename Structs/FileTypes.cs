namespace Engine.Structs
{
    public struct FileTypes
    {
        public const string BIN = ".bin";
        public const string XML = ".xml";
        public const string ELK = ".elk";
        public const string EPT = ".ept";
        public static string GetFilter(string sFileType)
        {
            switch (sFileType)
            {
                case BIN:
                    return "Binary File|*.bin";
                case XML:
                    return "Xml File|*.xml";
                case ELK:
                    return "Eplan Project File|*.elk";
                case EPT:
                    return "Eplan Template Project|*.ept";
            }
            return string.Empty;
        }
    }
}
