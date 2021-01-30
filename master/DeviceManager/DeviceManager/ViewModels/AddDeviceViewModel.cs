using DeviceManager.Common;
using DeviceManager.Contracts;
using System;
using System.Diagnostics;
using System.Windows;

namespace DeviceManager.ViewModels
{
    internal class AddDeviceViewModel : AbstractViewModel
    {
        #region properties
        private string _title;

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                NotifyPropertyChanged("Title");

            }
        }

        private int _id;
        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("ID");

            }
        }

        private string _location;
        public string Location
        {
            get
            {
                return _location;
            }
            set
            {
                _location = value;
                NotifyPropertyChanged("Location");

            }
        }


        private string _type;
        public string Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                NotifyPropertyChanged("Type");

            }
        }


        private string _device_health;
        public string Device_Health
        {
            get
            {
                return _device_health;
            }
            set
            {
                _device_health = value;
                NotifyPropertyChanged("Device_Health");

            }
        }


        private DateTime _last_used;
        public DateTime Last_Used
        {
            get
            {
                return _last_used;
            }
            set
            {
                _last_used = value;
                NotifyPropertyChanged("Last_Used");

            }
        }


        private double _price;
        public double Price
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
                NotifyPropertyChanged("Price");

            }
        }


        private string _color;
        public string Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
                NotifyPropertyChanged("Color");

            }
        }

        #endregion properties

        public DelegateCommand SubmitCommand { get; private set; }
        public DelegateCommand RevertCommand { get; private set; }

        IMainViewModelCallback _mainVMCallback;
        public AddDeviceViewModel(IMainViewModelCallback mainVM)
        {
            _mainVMCallback = mainVM;
            
            SubmitCommand = new DelegateCommand(OnSubmit, OnCanSubmit);
            RevertCommand = new DelegateCommand(OnRevert, OnCanRevert);

        }

        [DebuggerStepThrough]
        private bool OnCanSubmit(object parameter)
        {
            // All necessary conditions can be included here
            return true;
        }

        private void OnSubmit(object parameter)
        {
            try
            {
                var deviceObj = new tblDeviceDetails();
                deviceObj.id = ID;
                deviceObj.location = Location;
                deviceObj.type = Type;
                deviceObj.device_health = Device_Health;
                deviceObj.last_used = Last_Used.Date;
                deviceObj.price = Convert.ToDecimal(Price);
                deviceObj.color = Color;

                using (var db = new DeviceInfoManagerEntities())
                {
                    db.tblDeviceDetails.Add(deviceObj);
                    db.SaveChanges();
                }

                _mainVMCallback.LoadDeviceDetails();

            }

            catch (Exception ex)
            {

                MessageBox.Show("Unexpected error occured. \n" + ex.InnerException);
            }

        }

        [DebuggerStepThrough]
        private bool OnCanRevert(object parameter)
        {
            // All necessary conditions can be included here
            return true;
        }

        private void OnRevert(object parameter)
        {
            // Code to clear all fields can be implemented

        }
    }
}
