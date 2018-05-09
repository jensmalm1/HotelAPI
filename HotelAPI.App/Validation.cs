using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelAPI.App
{
    public class Validation
    {

        public static bool ValidateId(string id)
        {
            if (!String.IsNullOrWhiteSpace(id))
            {
                int num;

                if (int.TryParse(id, out num))

                    return true;
            }

            return false;

        }

        public static bool ValidateName(string name)
        {
            if (!String.IsNullOrWhiteSpace(name))

                return true;

            return false;
        }

    }
}
