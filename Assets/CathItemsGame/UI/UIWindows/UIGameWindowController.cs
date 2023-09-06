

namespace CatchItemsGame
{
    public class UIGameWindowController
    {
        private readonly UIService _uiService;
        
        private UIGameWindow _GameWindow;

        public UIGameWindowController(UIService uiService)
        {
            _uiService = uiService;
            _GameWindow = uiService.Get<UIGameWindow>();
        }

        public void ShowEndMenuWindow()
        {
            _uiService.Hide<UIGameWindow>();
            _uiService.Hide<HUDWindow>();
            _uiService.Show<UIEndGameWindow>();
        }
    }
}