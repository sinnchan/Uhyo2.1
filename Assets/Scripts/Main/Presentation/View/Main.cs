using UnityEngine;

namespace Main.Presentation.View
{
    
    public class Main: MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod]
        public static void Initialization()
        {
            ScenePresenter.Init();
        }
    }
}
