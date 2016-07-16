using SMTools.FileCopier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMDeployment.UIModels.FileCopier
{
    public class DestinationFolderUI : DestinationFolder
    {
        public bool Selected { get; set; }
        public DestinationFolderUI()
        {
            Selected = true;
        }
    }
}
