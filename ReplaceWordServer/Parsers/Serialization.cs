using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplaceWordServer.Parsers
{
    public class Serialization
    {
        public static T DesserializeFromJson<T>(String json)
        {
            T desserialized = JsonConvert.DeserializeObject<T>(json);
            return desserialized;
        }

        public static String SerializeToJson<T>(T obj)
        {
            String json = JsonConvert.SerializeObject(obj);
            return json;
        }
    }
}
