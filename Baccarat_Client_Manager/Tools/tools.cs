using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baccarat_Client_Manager.Tools
{
    public class tools
    {
        public static string moneyBuilder(string innerMoney)
        {
            int index = innerMoney.IndexOf('.');
            StringBuilder builder = new StringBuilder(innerMoney);
            string s2;
            if (index != -1)
            {
                s2 = innerMoney.Split('.')[0];
                builder.Remove(index, innerMoney.Length - index);
            }
            else
            {
                s2 = innerMoney;
            }
            if (builder.Length > 3)
            {
                for (int q = 1; q < s2.Length; q++)
                {
                    if (q % 3 == 0)
                    {
                        builder.Insert(s2.Length - q, ',');
                    }
                }
                if (index != -1)
                {
                    for (int w = index; w < innerMoney.Length; w++)
                    {
                        builder.Append(innerMoney[w]);
                    }
                }
                return builder.ToString();
            }
            else
            {
                return innerMoney;
            }
        }
    }
}
