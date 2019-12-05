﻿using ShadowBuild;
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

        GameObject testObj1 = new GameObject(new RegularTexture((Bitmap)Bitmap.FromFile("wykres.png")));
        public override void OnStart()
        {
            GameObject testObj = new GameObject(new RegularTexture((Bitmap)Bitmap.FromFile("wykres.png")));
            testObj.setPosition(300, 400);
            testObj2.setPosition(50, -20);
            testObj2.setParent(testObj);

        }

        public override void OnTick()
        {
            Axis.setup(new KeyboardAxis("test", Keys.A, Keys.D));
            Axis.setup(new KeyboardAxis("test1", Keys.W, Keys.S));
            testObj1.move(Axis.getValue("test")*Time.delay*70, Axis.getValue("test1")*Time.delay*70);
            if (Collision.check(testObj1, testObj2) || Collision.check(testObj2, testObj1)) Log.say("eee");
            else Log.say("UUUUUUUUUUU");
        }
    }
}
