using ShadowBuild;
using ShadowBuild.Input;
using ShadowBuild.Input.Axis;
using ShadowBuild.Objects;
using ShadowBuild.Objects.Dimensions;
using ShadowBuild.Objects.Texturing;
using ShadowBuild.Rendering;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace test_app
{
    class Game : ShadowBuildProject
    {
        GameObject testObj2 = new GameObject(new RegularTexture((Bitmap)Bitmap.FromFile("wykres.png")));
        GameObject child = new GameObject(new RegularTexture((Bitmap)Bitmap.FromFile("wykres.png")));

        GameObject testObj1 = new GameObject(new RegularTexture((Bitmap)Bitmap.FromFile("wykres.png")));
        public override void OnStart()
        {
            Camera.defaultCameraMode = DefaultCameraMode.RESIZE_WITH_WINDOW;
            GameObject testObj = new GameObject(new RegularTexture((Bitmap)Bitmap.FromFile("wykres.png")));
            testObj.SetPosition(300, 400);
            testObj2.SetPosition(50, -20);
            testObj2.SetParent(child);
            testObj2.SetSize(0.3, 0.3);
            child.SetParent(testObj1);
            child.SetSize(0.2, 0.2);
            child.SetPosition(200, 200);
        }

        public override void OnTick()
        {
            Axis.Setup(new KeyboardAxis("test", Keys.A, Keys.D));
            Axis.Setup(new KeyboardAxis("test1", Keys.W, Keys.S));
            child.Move(Axis.GetValue("test")*Loop.delay*70, Axis.GetValue("test1")*Loop.delay*70);
        }
    }
}
