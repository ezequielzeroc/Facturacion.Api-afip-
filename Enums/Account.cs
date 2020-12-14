using System;
using System.Collections.Generic;
using System.Text;

namespace Enums
{
    public class Account
    {
        public enum Login
        {
            Success = 1,
            User_Password_Error = 2
        }
        public enum Create
        {
            Created = 1,
            Not_Created = 2,
            Not_Created_Already_Exists = 3,
            Not_Created_Rol_Not_Exists = 4,
            Internal_Error = 5,
            Unknown_Error = 6,
            Account_Exists = 7
        }

        public enum Delete
        {
            Deleted = 1,
            Not_Deleted = 2,
            Error_Deleting = 3,
            Unknown_error = 4,
            User_Not_Found = 5
               
        }

        public enum ChangePassword
        {
            Changed = 1,
            Not_Changed = 2,
            Password_Verification_Failure = 3,
            Password_Not_Linked = 4,
            Password_Dont_Match = 5,
            Unknown_Error = 100
        }
    }
}
