using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.UI.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            var nav = new NavigationService();
            nav.Configure("ClientInformationView", typeof(ClientInfoView));
            nav.Configure("LineInformationView", typeof(LineInfioView));
            //nav.Configure("SimulatorView", typeof(sim));

            SimpleIoc.Default.Register<INavigationService>(() => nav);
            SimpleIoc.Default.Register<ClientInfoModel>();
            SimpleIoc.Default.Register<LineInfoModel>();
        }

        public ClientInfoModel ClientInfo
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ClientInfoModel>();
            }
        }

        public LineInfoModel LineInfo
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LineInfoModel>();
            }
        }
    }
}
