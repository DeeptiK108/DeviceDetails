using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceManager.Contracts
{
  internal  interface IMainViewModelCallback
    {

        tblDeviceDetails SelectedDevice { get; }

       void LoadDeviceDetails();

    }
}
