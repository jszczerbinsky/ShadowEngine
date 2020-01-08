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

            tmp = new Point(
                obj1.ActualTexture.GetSize().Width * obj1.Size.Width,
                obj1.ActualTexture.GetSize().Height * obj1.Size.Height);

            Point end1 = new Point(
                obj1.GetStartPosition().X + tmp.X,
                obj1.GetStartPosition().Y + tmp.Y);

            tmp = new Point(
                obj2.ActualTexture.GetSize().Width * obj2.Size.Width,
                obj2.ActualTexture.GetSize().Height * obj2.Size.Height);

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
