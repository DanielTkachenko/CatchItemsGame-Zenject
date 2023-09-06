

namespace CatchItemsGame
{
    public class UIGameWindow : UIAnimationWindow
    {
        public override void Show()
        {
            base.Show();
            UIService.Show<HUDWindow>();
        }

        public override void Hide()
        {
            base.Hide();
        }
    }
}