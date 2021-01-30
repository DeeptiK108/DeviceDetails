using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DeviceManager.ViewModels
{ 
     public abstract class AbstractViewModel : ViewModelBase
    {
        public void NotifyAllPropertiesChanged<T>(T childVM) where T : AbstractViewModel
        {
            var propertyTa = childVM.GetType().GetProperties();
            foreach (var item in propertyTa)
            {
                SendPropertyChanged(item.Name);
            }
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            //You can also use SendPropertyChanged( () => PropertyName )
            SendPropertyChanged(propertyName);
        } 

      

    }
}