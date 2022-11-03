using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using PSample.Views;
using System;

namespace PSample.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IDialogService _dialogService;

        private string _title = "PSamples";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel(IRegionManager regionManager, IDialogService dialogService)
        {
            _regionManager = regionManager;
            _dialogService = dialogService;
            SystemDateUpdateButton = new DelegateCommand(SystemDateUpdateButtonExecute);
            ShowViewButton = new DelegateCommand(ShowViewButtonExecute);
            ShowViewBButton = new DelegateCommand(ShowViewBButtonExecute);
            ShowViewCButton = new DelegateCommand(ShowViewCButtonExecute);
        }

        private string _systemDateLabel = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        public string SystemDateLabel
        {
            get { return _systemDateLabel; }
            set { SetProperty(ref _systemDateLabel, value); }
        }

        public DelegateCommand SystemDateUpdateButton { get; }
        public DelegateCommand ShowViewButton { get; }
        public DelegateCommand ShowViewBButton { get; }
        public DelegateCommand ShowViewCButton { get; }

        private void SystemDateUpdateButtonExecute()
        {
            SystemDateLabel = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        }
        private void ShowViewButtonExecute()
        {
            _regionManager.RequestNavigate("ContentRegion", nameof(ViewA));
        }

        private void ShowViewBButtonExecute()
        {
            var p = new NavigationParameters();
            p.Add(nameof(ViewBViewModel.MyLabel), SystemDateLabel);
            _regionManager.RequestNavigate("ContentRegion", nameof(ViewB), p);
        }

        private void ShowViewCButtonExecute()
        {
            var p = new DialogParameters();
            p.Add(nameof(ViewCViewModel.ViewCTextBox), SystemDateLabel);
            _dialogService.ShowDialog(nameof(ViewC), p, ViewCClose);

        }
        private void ViewCClose(IDialogResult dialogResult)
        {
            if (dialogResult.Result == ButtonResult.OK)
            {
                SystemDateLabel = dialogResult.Parameters.GetValue<string>(nameof(ViewCViewModel.ViewCTextBox));            }
        }
    }
}
