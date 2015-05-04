﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirFighter {
    public class Scene {
        private int Score;
        private PlayerShip Player;
        List<EnemyShip> Enemies;
        List<Bullet> Bullets;

        public Scene() {
            Player = new PlayerShip();
            Enemies = new List<EnemyShip>();
            Bullets = new List<Bullet>();
            Score = 0;

            for (int i = 0; i < 3; i++) {
                EnemyShip es = new EnemyShip(i * 120, 10);
                Enemies.Add(es);
            }

        }

        public void Draw(Graphics g) {
            g.Clear(Color.White);
            Player.Draw(g);
            foreach (EnemyShip es in Enemies) {
                es.Draw(g);
            }

            foreach (Bullet b in Bullets) {
                b.Draw(g);
            }

            Brush brush = new SolidBrush(Color.Black);
            g.DrawString(Score.ToString(), new Font("Arial", 24, FontStyle.Bold), brush, 10, 10);
            g.DrawString(Player.Health.ToString(), new Font("Arial", 24, FontStyle.Bold), brush, 330, 10);
            brush.Dispose();
        }

        public void MovePlayer(bool ToLeft) {
            Player.Move(ToLeft);
        }

        private bool CheckHit(EnemyShip es, Bullet b) {
            bool x = es.Position.Y + 41 > b.Position.Y;
            bool y = (b.Position.X > es.Position.X - 8) && (b.Position.X < es.Position.X + 38);
            return x && y;
        }

        public void Move() {
            foreach (EnemyShip es in Enemies) {
                foreach (Bullet b in Bullets) {
                    if (CheckHit(es, b)) {
                        ++Score;
                        es.RemoveHealth();
                        b.Active = false;
                    }
                }
                es.Move();
            }

            Enemies.RemoveAll(x => x.Health == 0);
            Bullets.RemoveAll(x => x.Active == false);

            foreach (Bullet b in Bullets) {
                b.Move();
            }
        }

        public void AddBullet() {
            Bullet b = new Bullet(Player.Position);
            Bullets.Add(b);
        }
    }
}