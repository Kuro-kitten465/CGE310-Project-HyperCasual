using UnityEngine;

namespace KuroNeko.Utilities.DesignPattern
{
    public interface ICommand
    {
        void Execute();
    }

    public class ThrowKnifeCommand : ICommand
    {
        private KnifeController knife;

        public ThrowKnifeCommand(KnifeController knife)
        {
            this.knife = knife;
        }

        public void Execute()
        {
            knife.Throw();
        }
    }
}
