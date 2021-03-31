using Main.Presentation.ViewModel;
using Main.Util;
using UnityEngine;

namespace Main.Presentation.View
{
    public class MainMenu : MonoBehaviour
    {
        private readonly MainMenuVm _vm = new MainMenuVm();

        public void OnClickPlayButton()
        {
            Log.Info(GetType().FullName,nameof(OnClickPlayButton));
            _vm.OnClickPlayButton();
        }

        public void OnClick2PlayerButton()
        {
            Log.Info(GetType().FullName,nameof(OnClick2PlayerButton));
            _vm.OnClick2PlayerButton();
        }

        public void OnClickOnlinePlayButton()
        {
            Log.Info(GetType().FullName,nameof(OnClickOnlinePlayButton));
            _vm.OnClickOnlinePlayButton();
        }
    }
}
