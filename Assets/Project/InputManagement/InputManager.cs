using DenZ.DevelopmentTools.InputSystem;

namespace HoaR.InputManagement
{
    public class InputManager : PlainInputManager<Inputs>
    {
        public IStartCancelAction PointerDownUp { get; }

        public InputManager() : base()
        {
            PointerDownUp = new PointerDownUpAction(Inputs.Game.PointerDownUp);

            Inputs.Game.Enable();
        }
    }
}