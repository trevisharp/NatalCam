using OpenCvSharp;

namespace NatalCam
{
    public class SilhouetteExtractor
    {
        private Mat background = null;
        private Mat mbbackground = null;
        private Mat rmat = null;

        public Mat Background
        {
            get => background;
            set
            {
                background = value;
                if (mbbackground == null)
                {
                    mbbackground = new Mat(new int[]
                    {
                        background.Width, background.Height
                    }, MatType.CV_8U);
                }

                Cv2.MedianBlur(background, mbbackground, 15);
            }
        }

        public Mat Extract(Mat mat)
        {
            rmat ??=
                rmat = new Mat(new int[]
                {
                    mat.Width, mat.Height
                }, MatType.CV_8U);

            Cv2.MedianBlur(mat, rmat, 15);

            return rmat;
        }
    }
}