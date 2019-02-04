using System;
using System.Collections.Generic;
using System.Text;

namespace CrossWord.ViewModel
{


    public class SharedClasses
    {
        #region Singleton Setup
        static SharedClasses _instance = null;

        private SharedClasses()
        {

        }

        public static SharedClasses Instance()
        {
            if (_instance == null)
            {
                _instance = new SharedClasses();
            }

            return _instance;
        }

        #endregion

        public App AppClass;

    }
}
