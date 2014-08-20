using System.Runtime.Serialization;

namespace BMCLV4.Libraries
{
    [DataContract]
    public class Extract
    {
        [DataMember(Order = 0, IsRequired = false)]
// ReSharper disable once InconsistentNaming
        public string[] exclude;
    }
}
