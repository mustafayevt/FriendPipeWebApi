using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendPipeApi.Helpers
{
    public class EmailHelper
    {
       public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
