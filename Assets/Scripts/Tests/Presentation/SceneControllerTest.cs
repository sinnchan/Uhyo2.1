using System.Collections.Generic;
using Main.Domain.Entities;
using Main.Presentation;
using NUnit.Framework;
using UniRx;

namespace Tests.Presentation
{
    public class SceneControllerTest
    {
        [SetUp]
        public void SetUp()
        {
            var controller = SceneController.Instance;
            controller.ResetBackStack();
            controller.Init(Scene.MainMenu);
        }

        [Test]
        public void BackTest()
        {
            var controller = SceneController.Instance;
            var actualBackStack = new List<Scene> {Scene.OfflinePlaySetting, Scene.MainMenu};

            controller.Show(Scene.OfflinePlaySetting);
            controller.Show(Scene.CpuPlayGameView);

            Assert.That(controller.GetBackStackList(), Is.EqualTo(actualBackStack));
        }
    }
}
