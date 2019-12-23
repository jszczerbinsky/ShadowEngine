using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Windows.Forms;

namespace ShadowBuild.Input.Axis
{
    [Serializable]
    public class KeyboardAxis : Axis
    {
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("-")]
        internal Keys minusValue;
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("+")]
        internal Keys plusValue;

        public KeyboardAxis(string name, Keys minusValue, Keys plusValue)
        {
            this.minusValue = minusValue;
            this.plusValue = plusValue;
            this.name = name;
        }
    }
}
