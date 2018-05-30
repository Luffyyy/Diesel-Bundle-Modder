using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDBundleModPatcher.CreateMods
{
    public enum ModVariableOptionDisplayType
    {
        None,
        CheckBox,
        TextBox,
        DropDown,
        Slider
    };
    
    class ModVariableOption
    {
        private String variable;
        private String displaytext;
        private ModVariableOptionDisplayType displaytype = ModVariableOptionDisplayType.None;
        private String range_min;
        private String range_max;
        private String range_step;
        private List<String> items = new List<string>();
        private String items_default;

    }
}
