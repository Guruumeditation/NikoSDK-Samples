using System.Runtime.Serialization;

namespace NikoRestAPI
{
    [DataContract]
    public class ExecuteAction
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int Value { get; set; }
    }
}
