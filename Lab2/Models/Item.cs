using GameInventory.State; 

namespace GameInventory.Models
{
    public abstract class Item //позже будет наследоваться в броне, оружии и тд
    {
        public string Name { get; set; }
        public IItemState State { get; set; }
        public bool IsEquippable { get; set; }
        public bool IsUsable { get; set; }

        public Item(string name)
        {
            Name = name;
            State = new NormalState();
            IsEquippable = false;
            IsUsable = false;
        }

        public void ChangeState(IItemState state)
        {
            State = state;
        }

        public virtual string GetInfo()
        {
            return $"{Name} (Состояние: {State.GetStateName()})";
        }
    }
}