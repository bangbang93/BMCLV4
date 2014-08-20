using System.Runtime.Serialization;

namespace BMCLV4.Libraries
{
    [DataContract]
    public class Libraryies
    {
        [DataMember(Order = 0, IsRequired = true)]
// ReSharper disable InconsistentNaming
        public string name;
        [DataMember(Order = 1, IsRequired = false)]
        public OS natives;
        [DataMember(Order = 2, IsRequired = false)]
        public Extract extract;
        [DataMember(IsRequired = false)]
        public string url;
        [DataMember(Order = 4, IsRequired = false)]
        public rules[] rules;

        // ReSharper restore InconsistentNaming
    }
}
