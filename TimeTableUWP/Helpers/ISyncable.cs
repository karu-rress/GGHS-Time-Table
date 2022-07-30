using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTableUWP;

// Ensures that a class or struct is sync-able with SQL server.
public interface ISyncable
{
    Task SyncAsync();
}
