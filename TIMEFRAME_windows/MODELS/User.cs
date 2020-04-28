using System;
using System.Collections.Generic;
using System.Text;

namespace TIMEFRAME_windows.MODELS
{
    // Class to store authenticated user's information (claims and/or tokens)
    public class User
    {
        // FIELDS
        private string _Name;
        private string _Email;
        private string _PicturePath;
        private bool _EmailVerified;


        // CONSTRUCTOR
        public User()
        {
            Name = "";
            Email = "";
            PicturePath = "";
            EmailVerified = false;
        }

        public User(string inp_Name, string inp_Email, string inp_PicturePath, bool inp_EmailVerified)
        {
            Name = inp_Name;
            Email = inp_Email;
            PicturePath = inp_PicturePath;
            EmailVerified = inp_EmailVerified;
        }


        // PROPERTIES
        public string Name
        {
            get { return _Name; }
            set { if (value != _Name) { _Name = value; } }
        }

        public string Email
        {
            get { return _Email; }
            set { if (value != _Email) { _Email = value; } }
        }

        public string PicturePath
        {
            get { return _PicturePath; }
            set { if (value != _PicturePath) { _PicturePath = value; } }
        }

        public bool EmailVerified
        {
            get { return _EmailVerified; }
            set { if (value != _EmailVerified) { _EmailVerified = value; } }
        }
    }
}
