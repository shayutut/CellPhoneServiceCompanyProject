using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Simulator.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<CallOrSMSModel>();
        }

        public CallOrSMSModel Simulator
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CallOrSMSModel>();
            }
        }
    }
}
