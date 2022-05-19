using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcShop.Models
{
    public class ComputerComponent : ObservableObject
    {
        private string type;

        public string Type
        {
            get { return type; }
            set { SetProperty(ref type, value); }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        private int price;

        public int Price
        {
            get { return price; }
            set { SetProperty(ref price, value); }
        }

        public ComputerComponent GetCopy()
        {
            return new ComputerComponent()
            {
                Price = this.Price,
                Type = this.Type,
                Name = this.Name
            };
        }
    }
}
