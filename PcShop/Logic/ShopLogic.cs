using Microsoft.Toolkit.Mvvm.Messaging;
using PcShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcShop.Logic
{
    public class ShopLogic : IShopLogic
    {
        IList<ComputerComponent> components;
        IList<ComputerComponent> computercomponents;
        IMessenger messenger;

        public ShopLogic(IMessenger messenger)
        {
            this.messenger = messenger;
        }

        public void LoadComponents(IList<ComputerComponent> components, IList<ComputerComponent> computercomponents)
        {
            this.components = components;
            this.computercomponents = computercomponents;

            string[] lines = File.ReadAllLines("input.txt");
            foreach (string line in lines)
            {
                string[] lineComponents = line.Split(",");
                components.Add(new ComputerComponent()
                {
                    Price = int.Parse(lineComponents[0]),
                    Type = lineComponents[1],
                    Name = lineComponents[2]
                });
            }

            messenger.Send("Component list done", "ShopInfo");

        }

        public void AddComponent(ComputerComponent component)
        {
            computercomponents.Add(component.GetCopy());
            messenger.Send("Component added", "ShopInfo");

        }

        public void DiscountComponent(ComputerComponent component)
        {
            double a = Convert.ToDouble(component.GetCopy().Price);
            a = Convert.ToDouble(component.Price * 0.9);

            messenger.Send("Component discounted", "ShopInfo");
        }

        public void RemoveComponent(ComputerComponent component)
        {
            components.Remove(component);
            messenger.Send("Component removed", "ShopInfo");
        }

        public int SumPrice
        {
            get
            {
                return computercomponents.Count == 0 ? 0 : computercomponents.Sum(t => t.Price);
            }
        }
    }
}
