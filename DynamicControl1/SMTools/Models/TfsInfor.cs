using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.Models
{
    public class TfsInfor
    {
        public string UserName
        {
            get;
            set;
        }
        public string Password
        {
            get;
            set;
        }
        public string ServerUrl
        {
            get;
            set;
        }
        public string Domain
        {
            get;
            set;
        }
        public bool NeedAuthenticate
        {
            get;
            set;
        }
        public string WorkspaceMapping
        {
            get;
            set;
        }
    }
}
