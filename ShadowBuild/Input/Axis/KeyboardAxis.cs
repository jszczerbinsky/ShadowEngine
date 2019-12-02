using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShadowBuild.Input.Axis
{
    public class KeyboardAxis : Axis
    {
        internal Keys minusValue;
        internal Keys plusValue;

        public KeyboardAxis(string name, Keys minusValue, Keys plusValue)
        {
            this.minusValue = minusValue;
            this.plusValue = plusValue;
            this.name = name;
        }
    }
}
