using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Static.Files
{
    public static class LC_Constants
    {
        public static string Images_URL
        {
            get
            {
                return "/Images/";
            }
        }
        public static string Upload_URL
        {
            get
            {
                return "Upload/";
            }
        }
        public static string Session_URL
        {
            get
            {
                return "session/";
            }
        }
        public static string password
        {
            get
            {
                return "P@$$w0rd";
            }
        }
        public static string DefaultStudentImage
        {
            get
            {
                return "/Content/assets/admin/layout2/img/avatar1.jpg";
            }
        }
        public static string DefaultParentImage
        {
            get
            {
                return "/Content/assets/admin/layout2/img/avatar7.jpg";
            }
        }
        public static string DefaultTeacherImage
        {
            get
            {
                return "/Content/assets/admin/layout2/img/avatar4.jpg";
            }
        }

    }
}
