using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CustomTimers.Common
{
    public class KeyedElement
    {
        [XmlAttribute]
        public string Key { get; set; }

        [XmlAttribute]
        public string Value { get; set; }

        public KeyedElement()
        {
            
        }

        public KeyedElement(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
