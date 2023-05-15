using System;
using System.Collections.Generic;
using System.Text;

namespace SignatureEmailParser.BusinessLogic.Helpers
{
    public class KeyValHelper<Key, Val>
    {
        public Key Id { get; set; }
        public Val Value { get; set; }

        public KeyValHelper() { }

        public KeyValHelper(Key key, Val val)
        {
            this.Id = key;
            this.Value = val;
        }
    }
}
