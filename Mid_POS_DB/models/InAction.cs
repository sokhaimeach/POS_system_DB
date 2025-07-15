using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mid_POS_DB.models
{
    internal interface InAction
    {
        void Create();
        void UpdateById(DataGridView dg);
        void DeleteById(DataGridView dg);
        void Search(DataGridView dg);
        void GetData(DataGridView dg);
    }
}
