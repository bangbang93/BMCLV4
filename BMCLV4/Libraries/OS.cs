using System.Runtime.Serialization;

namespace BMCLV4.Libraries
{
    [DataContract]
    public class OS
    {
        [DataMember(Order = 0, IsRequired = false)]
// ReSharper disable InconsistentNaming
        public string windows;
        [DataMember(Order = 1, IsRequired = false)]
        public string linux;
        [DataMember(Order = 2, IsRequired = false)]
        public string osx;
        // ReSharper restore InconsistentNaming
    }
}
