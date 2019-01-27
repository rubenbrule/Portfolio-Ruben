using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_Overview_Application
{
    interface IData
    {
        int getTotalVisitors();
        int getTotalVisitorsAtEvent();
      //  List<string> getListofVisitorsReservedSpot(int spot_id);

    }
}
