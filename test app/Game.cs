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
            Camera.defaultCameraMode = DefaultCameraMode.STATIC_SIZE;
            GameObject testObj = new GameObject(new RegularTexture((Bitmap)Bitmap.FromFile("wykres.png")));
            testObj.setPosition(300, 400);
            testObj2.setPosition(50, -20);
            testObj2.setParent(child);
            testObj2.setSize(0.3, 0.3);
            child.setParent(testObj1);
            child.setSize(0.2, 0.2);
            child.setPosition(200, 200);
        }

        public override void OnTick()
        {
            Axis.setup(new KeyboardAxis("test", Keys.A, Keys.D));
            Axis.setup(new KeyboardAxis("test1", Keys.W, Keys.S));
            child.move(Axis.getValue("test")*Loop.delay*70, Axis.getValue("test1")*Loop.delay*70);
        }
    }
}
