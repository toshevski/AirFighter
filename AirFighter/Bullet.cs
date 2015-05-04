﻿using AirFighter.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirFighter {
    public class Bullet {
        private Image BulletImage;
        public Point Position;
        public bool Active;

        public Bullet(Point P) {
            BulletImage = Resources.smallBullet;
            Position = new Point(P.X + 23, P.Y - 5);
            Active = true;
        }

        public void Move() {
            Position = new Point(Position.X, Position.Y - 10);
        }

        public void Draw(Graphics g) {
            g.DrawImage(BulletImage, Position);
        }
    }
}