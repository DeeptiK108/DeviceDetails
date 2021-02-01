using DeviceManager.Common;
using DeviceManager.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

namespace DeviceManager.ViewModels
{
    /// <summary>
    /// Class to add new Device details
    /// </summary>
    internal class AddDeviceViewModel : AbstractViewModel, IDataErrorInfo
    {
        #region properties
        private List<string> ErrorList;

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


        //private string _message;
        //public string Message
        //{
        //    get
        //    {
        //        return _message;
        //    }
        //    set
        //    {
        //        _message = value;
        //        NotifyPropertyChanged("Message");

        //    }
        //}

        #endregion properties

        public DelegateCommand SubmitCommand { get; private set; }
        public DelegateCommand RevertCommand { get; private set; }

        IMainViewModelCallback _mainVMCallback;

        #region constructor

        public AddDeviceViewModel(IMainViewModelCallback mainVM)
        {
            _mainVMCallback = mainVM;
            
            SubmitCommand = new DelegateCommand(OnSubmit, OnCanSubmit);
            RevertCommand = new DelegateCommand(OnRevert, OnCanRevert);

        }

        #endregion

        #region commands
        [DebuggerStepThrough]
        private bool OnCanSubmit(object parameter)
        {
            // All necessary conditions can be included here       
            //validation can be proofed   
            return ((ErrorList.Count > 0) ? false : true);
            

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

                MessageBox.Show("Device details successfully added!");
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

        #endregion commands


        #region validation
        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        string IDataErrorInfo.this[string fieldName]
        {        
        
            get
            {
                ErrorList = new List<string>();
              string  message = "";
                if (fieldName == "Price")
                {
                    double doubleVal;
                    if (double.TryParse(this.Price.ToString(), out doubleVal))
                    {
                        //Any range related validations
                    }
                    else
                    {
                        //Data type or format related validation
                        message = "Price has a Format error.\n";
                        ErrorList.Add(message);
                    }
                }

               else if (fieldName == "ID")
                {
                    int val;
                    if (int.TryParse(this.ID.ToString(), out val))
                    {
                       if(ID == 0)

                        {
                            message = "ID cannot be 0.\n";
                            ErrorList.Add(message);
                        }
                    }
                    else
                    {
                        //Data type or format related validation
                       message = "ID has a Format error";
                        ErrorList.Add(message);
                    }
                }

              
                return message;             
            }
        }

        #endregion


    }
}
