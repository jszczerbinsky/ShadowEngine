using ShadowBuild;
using ShadowBuild.Input;
using ShadowBuild.Input.Axis;
using ShadowBuild.Objects;
using ShadowBuild.Objects.Dimensions;
using ShadowBuild.Objects.Texturing;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace test_app
{
    class Game : ShadowBuildProject
    {
        GameObject testObj2 = new GameObject(new RegularTexture((Bitmap)Bitmap.FromFile("wykres.png")));

        GameObject testObj3;

        public override void OnStart()
        {
            GameObject testObj = new GameObject(new RegularTexture((Bitmap)Bitmap.FromFile("wykres.png")));
            testObj.setPosition(300, 400);
            testObj2.setPosition(50, -20);
            testObj2.setParent(testObj);
            testObj3 = new GameObject(new GridTexture((Bitmap)Bitmap.FromFile("wykres.png"), 2, 3));
            testObj3.setPosition(0, 0);
            testObj3.setParent(testObj2);
            testObj3.setSize(1, 1);

        }

        public override void OnTick()
        {
            Axis.setup(new KeyboardAxis("test", Keys.A, Keys.D));
            testObj3.move(0, Axis.getValue("test")*Time.delay*20);
        }
    }
}
