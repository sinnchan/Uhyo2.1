using Main.Domain.Entities;
using Main.Presentation.ViewModel;
using Main.Util;
using UnityEngine;

namespace Main.Presentation.View
{
    public class PlaySetting : MonoBehaviour
    {
        private readonly PlaySettingVm _vm = new PlaySettingVm();

        /// <summary>
        ///     1 -> Black
        ///     2 -> White
        /// </summary>
        /// <param name="turnNum">turn number</param>
        public void SelectTurn(int turnNum)
        {
            Log.Info(GetType().FullName, $"{nameof(SelectTurn)} -> {turnNum}");
            switch (turnNum)
            {
                case 1:
                    _vm.OnSelectTurn(Turn.Black);
                    break;
                case 2:
                    _vm.OnSelectTurn(Turn.White);
                    break;
                default:
                    Log.Warning(GetType().FullName,$"Invalid setting value -> {nameof(turnNum)}:{turnNum}");
                    break;
            }
        }

        /// <summary>
        ///     cpu strength 1~5
        /// </summary>
        /// <param name="cpuStrength"></param>
        public void SelectCpuStrength(int cpuStrength)
        {
            Log.Info(GetType().FullName,$"{nameof(SelectCpuStrength)} -> {cpuStrength}");
            if (1 <= cpuStrength && cpuStrength <= 5)
                _vm.OnSelectCpuStrength(cpuStrength);
            else
                Log.Warning(GetType().FullName,$"Invalid setting value -> {nameof(SelectCpuStrength)}:{cpuStrength}");
        }

        public void OnClickStartButton()
        {
            _vm.OnGameStart();
        }

        public void OnClickBackButton()
        {
            _vm.OnBack();
        }
    }
}
