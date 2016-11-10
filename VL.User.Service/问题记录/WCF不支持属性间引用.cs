using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VL.User.Service
{

    [DataContract]
    [KnownType(typeof(B))]
    public class A
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public List<A> As { set; get; }
        [DataMember]
        public List<B> Bs { set; get; }

        //CARE 不支持调用其他属性的属性!!!
        //[DataMember]
        //public int BsValue
        //{
        //    get
        //    {
        //        return Bs.Sum(c => c.Value);
        //    }
        //}
    }
    [DataContract]
    public class B
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Value { get; set; }
    }
}
