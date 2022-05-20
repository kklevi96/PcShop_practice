using PcShop.Models;
using System.Collections.Generic;

namespace PcShop.Logic
{
    public interface IShopLogic
    {
        int SumPrice { get; }
        void AddComponent(ComputerComponent component);
        void DiscountComponent(ComputerComponent component);
        void UndoDiscountComponent(ComputerComponent component);
        void LoadComponents(IList<ComputerComponent> components, IList<ComputerComponent> computercomponents);
        void RemoveComponent(ComputerComponent component);
    }
}