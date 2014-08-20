using System.Runtime.Serialization;

namespace BMCLV4.Libraries
{
    [DataContract]
    public class Ros
    {
        [DataMember(Order = 0, IsRequired = true)]
// ReSharper disable InconsistentNaming
        public string name;
        [DataMember(Order = 1, IsRequired = false)]
        public string version;
        // ReSharper restore InconsistentNaming
    }
}
