using System.Runtime.Serialization;

namespace BMCLV4.Libraries
{
    [DataContract]
    public class rules
    {
        [DataMember(Order = 0, IsRequired = true)]
// ReSharper disable InconsistentNaming
        public string action;
        [DataMember(Order = 1, IsRequired = false)]
        public Ros os;
        [DataMember(Order = 2, IsRequired = false)]
        public string version;
        // ReSharper restore InconsistentNaming
    }
}
