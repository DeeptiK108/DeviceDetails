using DeviceManager.Common;
using DeviceManager.Contracts;
using System;
using System.Diagnostics;
using System.Windows;

namespace DeviceManager.ViewModels
{
    internal class EditDeviceViewModel:AbstractViewModel
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

        tblDeviceDetails _deviceObj;
        public DelegateCommand SubmitCommand { get; private set; }
        public DelegateCommand RevertCommand { get; private set; }

        IMainViewModelCallback _mainVMCallback;
        public EditDeviceViewModel(tblDeviceDetails deviceObj , IMainViewModelCallback mainVM)
        {
            _deviceObj = deviceObj;
            _mainVMCallback = mainVM;

            SubmitCommand = new DelegateCommand(OnSubmit, OnCanSubmit);
            RevertCommand = new DelegateCommand(OnRevert, OnCanRevert);

            if (deviceObj != null)
            {
                ID = deviceObj.id;
                Location = deviceObj.location;
                Device_Health = deviceObj.device_health;
                Type = deviceObj.type;
                Price = Convert.ToDouble(deviceObj.price);
                Last_Used = Convert.ToDateTime(deviceObj.last_used).Date;
                Color = deviceObj.color;
            }
            else
            {
                //Implement defaults or fetch current selected item
                
            }

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
                _deviceObj.id = ID; // ideally this has to be read only if it is an Identity column
                _deviceObj.location = Location;
                _deviceObj.type = Type;
                _deviceObj.device_health = Device_Health;
                _deviceObj.last_used = Last_Used.Date;
                _deviceObj.price = Convert.ToDecimal(Price);
                _deviceObj.color = Color;

                using (var db = new DeviceInfoManagerEntities())
                {
                    var entity = db.tblDeviceDetails.Find(_deviceObj.id);
                    if (entity == null)
                    {
                        return;
                    }

                    db.Entry(entity).CurrentValues.SetValues(_deviceObj);
                    db.SaveChanges();
                }

                _mainVMCallback.LoadDeviceDetails();
            }

            catch(Exception ex)
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
