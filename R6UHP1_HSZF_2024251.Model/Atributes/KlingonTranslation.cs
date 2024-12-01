using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R6UHP1_HSZF_2024251.Model.Atributes
{
    using System;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
    public class KlingonTranslationAttribute : Attribute
    {
        public string Translation { get; }

        public KlingonTranslationAttribute(string translation)
        {
            Translation = translation;
        }
    }
}
