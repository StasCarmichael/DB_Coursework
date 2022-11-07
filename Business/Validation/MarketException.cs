using System;
using System.Runtime.Serialization;

namespace Business.Validation
{
    [Serializable]
    public sealed class MarketException : Exception
    {
        public MarketException()
        { }
        public MarketException(string msg) : base(msg)
        { }



        private MarketException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        { base.GetObjectData(info, context); }

    }
}
