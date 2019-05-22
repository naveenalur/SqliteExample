using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLiteSimple.Services
{
    public class MessageDialog : ViewModelBase
    {
        private readonly IDialogService dialogService;

        public MessageDialog(IDialogService dialogService)
        {
            this.dialogService = dialogService;
        }
    }
}
