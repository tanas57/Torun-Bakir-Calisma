using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Torun.Lang
{
    public static class CurrentLanguage
    {
        private static readonly ILanguage LANG = new TR();

        public static ILanguage Language
        {

            get
            {
                return LANG;
            }
        }
    }
}
