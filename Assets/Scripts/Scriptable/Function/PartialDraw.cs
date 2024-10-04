using System;
using System.Collections.Generic;
//using symbol
namespace JsonClass
{
    public partial class Draw
    {
//class symbol
    }

    public partial class Type
    {
//class symbol
    }

    public partial class Shops
    {
//class symbol
        public int MaxProbability
        {
            get
            {
                int value = 0;

                foreach (var item in probabilities)
                {
                    value += item.value;
                }

                return value;
            }
        }
    }

    public partial class Probabilities
    {
//class symbol
    }

}
