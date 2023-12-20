using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ca_csIS
{
    interface iCRUDS
    {
        void Create();
        void Read();
        void Update();
        void Delete();
        void Search();
    }
}
