using System;

namespace BMCLV4.Util
{
    class MathHelper
    {
        public static int ParseIntWithDefault(String par0Str, int par1)
        {
            int j = par1;

            try
            {
                j = int.Parse(par0Str);
            }
            catch
            {
                j = 0;
            }

            return j;
        }
    }
}
