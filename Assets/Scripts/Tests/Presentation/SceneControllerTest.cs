using System.Collections.Generic;
using Main.Domain.Entities;
using Main.Presentation;
using NUnit.Framework;
using UniRx;

namespace Tests.Presentation
{
    public class SceneControllerTest
    {
        [TestCase(Scene.MainMenu)]
        public void ShowTest(Scene scene)
        {
            var controller = SceneController.Instance;
            controller.GetScenesStream().Subscribe(
                scenes => { Assert.AreEqual(scene, scenes); }
            );
            controller.Show(scene);
        }

        [Test]
        public void BackTest()
        {
            var controller = SceneController.Instance;
            var index = 0;

            var actual = new List<Scene>();
            actual.Add(Scene.MainMenu);
            actual.Add(Scene.OfflinePlaySetting);
            actual.Add(Scene.CpuPlayGameView);
            actual.Add(Scene.OfflinePlaySetting);
            actual.Add(Scene.MainMenu);

            controller.GetScenesStream().Subscribe(
                scenes =>
                {
                    Assert.AreEqual(scenes, actual[index]);
                    index++;
                });
        }
    }
}
