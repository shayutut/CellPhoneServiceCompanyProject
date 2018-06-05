using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.ViewModel
{
    public class ViewModelLocator
    {
        CallOrSMSModel _vm;
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<CallOrSMSModel>();

            _vm = ServiceLocator.Current.GetInstance<CallOrSMSModel>();
        }

        public CallOrSMSModel Sim
        {
            get
            {
                return _vm;
            }
        }
    }
}
