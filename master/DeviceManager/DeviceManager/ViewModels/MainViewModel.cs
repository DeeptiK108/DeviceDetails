using DeviceManager.Common;
using DeviceManager.Contracts;
using DeviceManager.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace DeviceManager.ViewModels
{
    public class MainViewModel : AbstractViewModel, IMainViewModelCallback
    {
     
        public DelegateCommand AddDeviceDetailsCommand { get; private set; }
        public DelegateCommand EditDeviceDetailsCommand { get; private set; }
        public DelegateCommand DeleteDeviceDetailsCommand { get; private set; }


        public  MainViewModel()
        {
            AddDeviceDetailsCommand = new DelegateCommand(OnAddDeviceDetails, OnCanAddDeviceDetails);
            EditDeviceDetailsCommand = new DelegateCommand(OnEditDeviceDetails, OnCanEditDeviceDetails);
            DeleteDeviceDetailsCommand = new DelegateCommand(OnDeleteDeviceDetails, OnCanDeleteDeviceDetails);

            LoadDeviceDetails();
        }

        public void LoadDeviceDetails()
        {
            //Fetch device details from DB
            using (var ctx = new DeviceInfoManagerEntities())
            {
                var q = (from a in ctx.tblDeviceDetails
                         select a).ToList();
                this.Devices = new ObservableCollection<tblDeviceDetails>(q);
            }      

        }

        #region properties
        private ObservableCollection<tblDeviceDetails> _devices;
        public ObservableCollection<tblDeviceDetails> Devices
        {
            get
            {
                return _devices;
            }
            set
            {
                _devices = value;
                NotifyPropertyChanged("Devices");
            }
        }

        private tblDeviceDetails _selectedDevice;
        public tblDeviceDetails SelectedDevice
        {
            get
            {
                return _selectedDevice;
            }
            set
            {
                _selectedDevice = value;
                NotifyPropertyChanged("SelectedDevice");
              
            }
        }

        #endregion properties

        #region Commands

        [DebuggerStepThrough]
        private bool OnCanAddDeviceDetails(object parameter)
        {
            return true;
        }
        private void OnAddDeviceDetails(object parameter)
        {
            var vm = new AddDeviceViewModel(this)
            {
                //Set properties if any
               // Name = "[new Device Details]"
            };
            var dlg = new AddDeviceDialog()
            {
                DataContext = vm
            };
           
            if (dlg.ShowDialog() != true) return;

            //Reload Items
            LoadDeviceDetails();

        }

        [DebuggerStepThrough]
        private bool OnCanEditDeviceDetails(object parameter)
        {
            return true;
        }
        private void OnEditDeviceDetails(object parameter)
        {
            var vm = new EditDeviceViewModel(SelectedDevice, this)
            {
                //Set properties if any
                // Name = "[new Device Details]"
            };
            var dlg = new AddDeviceDialog()
            {
                DataContext = vm
            };

            if (dlg.ShowDialog() != true) return;
     
        }

        [DebuggerStepThrough]
        private bool OnCanDeleteDeviceDetails(object parameter)
        {
            return true;
        }
        private void OnDeleteDeviceDetails(object parameter)
        {          

            if (MessageBox.Show("Are you sure you really want to delete this entry?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
              
                using (var db = new DeviceInfoManagerEntities())
                {
                    var entity = db.tblDeviceDetails.Find(SelectedDevice.id);
                    if (entity == null)
                    {
                        return;
                    }

                    db.tblDeviceDetails.Attach(entity);
                    db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }          
            }
        }


        #endregion Commands

    }
}
