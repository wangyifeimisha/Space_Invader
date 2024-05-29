

namespace SpaceInvaders
{
    class ImageNode : SLink
    {
        public ImageNode(Image image)
            : base()
        {
            this.pImage = image;
        }

        // Data
        public Image pImage;
    }
}
