using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using PcShop.Logic;
using PcShop.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PcShop.ViewModels
{
    public class MainWindowViewModel : ObservableRecipient
    {
        IShopLogic logic;
        
        private ComputerComponent selectedFromComponents;

        public ComputerComponent SelectedFromComponents
        {
            get { return selectedFromComponents; }
            set
            {
                SetProperty(ref selectedFromComponents, value);
                (LoadComponentsCommand as RelayCommand).NotifyCanExecuteChanged();
                (DiscountComponentCommand as RelayCommand).NotifyCanExecuteChanged();
                (UndoDiscountComponentCommand as RelayCommand).NotifyCanExecuteChanged();
                (AddComponentCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }
        
        private ComputerComponent selectedFromComputerComponents;

        public ComputerComponent SelectedFromComputerComponents
        {
            get { return selectedFromComputerComponents; }
            set
            {
                SetProperty(ref selectedFromComputerComponents, value);
                (RemoveComponentCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }



        public ObservableCollection<ComputerComponent> Components { get; set; }
        public ObservableCollection<ComputerComponent> ComputerComponents { get; set; }


        public ICommand AddComponentCommand { get; set; }
        public ICommand RemoveComponentCommand { get; set; }
        public ICommand DiscountComponentCommand { get; set; }
        public ICommand UndoDiscountComponentCommand { get; set; }

        public ICommand LoadComponentsCommand { get; set; }
        public int SumPrice { get { return logic.SumPrice; } }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public MainWindowViewModel() : this(IsInDesignMode?null:Ioc.Default.GetService<IShopLogic>())
        {

        }

        public MainWindowViewModel(IShopLogic logic)
        {
            this.logic=logic;
            Components = new ObservableCollection<ComputerComponent>();
            ComputerComponents = new ObservableCollection<ComputerComponent>();

            LoadComponentsCommand = new RelayCommand(
                () => logic.LoadComponents(Components, ComputerComponents),
                () => Components.Count == 0 && SelectedFromComponents == null
                );

            AddComponentCommand = new RelayCommand(
                () => logic.AddComponent(SelectedFromComponents),
                () => SelectedFromComponents != null && logic.MaxLimit(SelectedFromComponents)
                );

            RemoveComponentCommand = new RelayCommand(
                () => logic.RemoveComponent(SelectedFromComputerComponents),
                () => SelectedFromComputerComponents!=null
                );

            DiscountComponentCommand = new RelayCommand(
                () => logic.DiscountComponent(SelectedFromComponents),
                () => SelectedFromComponents != null
                );

            UndoDiscountComponentCommand = new RelayCommand(
                () => logic.UndoDiscountComponent(SelectedFromComponents),
                () => SelectedFromComponents != null
                );

            Messenger.Register<MainWindowViewModel, string, string>(this, "ShopInfo", (recipient, msg) =>
            {
                OnPropertyChanged(nameof(SumPrice));
            });
        }
    }
}
