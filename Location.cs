using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeDesktop
{
    class Location
    {
        public Point Position;
        public Rectangle Bounds;

        public Location(Point loc, Rectangle bounds)
        {
            Position = loc;
            Bounds = bounds;
        }
    }
}
