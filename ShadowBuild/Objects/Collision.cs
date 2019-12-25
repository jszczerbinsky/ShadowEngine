using System.Windows;

namespace ShadowBuild.Objects
{
    public static class Collision
    {
        public static bool Check(GameObject obj1, GameObject obj2)
        {
            Point start1 = obj1.GetStartPosition();
            Point start2 = obj2.GetStartPosition();

            Point tmp;

            tmp = new Point(obj1.ActualTexture.GetSize().X * obj1.Size.X, obj1.ActualTexture.GetSize().Y * obj1.Size.Y);

            Point end1 = new Point(
                obj1.GetStartPosition().X + tmp.X,
                obj1.GetStartPosition().Y + tmp.Y);

            tmp = new Point(obj2.ActualTexture.GetSize().X * obj2.Size.X, obj2.ActualTexture.GetSize().Y * obj2.Size.Y);

            Point end2 = new Point(
                obj2.GetStartPosition().X + tmp.X,
                obj2.GetStartPosition().Y + tmp.Y);

            if (
             (end1.X > start2.X
             && end1.X < end2.X
             && end1.Y > start2.Y
             && end1.Y < end2.Y)
             ||
             (end1.X > start2.X
             && end1.X < end2.X
             && start1.Y > start2.Y
             && start1.Y < end2.Y)
             ||
             (start1.X > start2.X
             && start1.X < end2.X
             && end1.Y > start2.Y
             && end1.Y < end2.Y)
             ||
             (start1.X > start2.X
             && start1.X < end2.X
             && start1.Y > start2.Y
             && start1.Y < end2.Y)
             )
                return true;
            return false;
        }
    }
}
