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
        private List<EnemyShip> Enemies;
        private List<Bullet> Bullets;
        private Random Randomizer;
        private int Counter;
        private int EnemySpeed;
        private Form1 ReferenceToForm;

        /// <summary>
        /// Konstruktor na scenata.
        /// </summary>
        public Scene(Form1 f) {
            ReferenceToForm = f;
            Player = new PlayerShip();
            Enemies = new List<EnemyShip>();
            Bullets = new List<Bullet>();
            Score = 0;
            Counter = 0;
            EnemySpeed = 2;
            Randomizer = new Random();
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

        /// <summary>
        /// Metoda koja ja povikuva metodata "Move" kaj igrachot.
        /// </summary>
        /// <param name="Side">Na koja strana da se dvizhi igrachot.</param>
        /// <param name="p">Ako se dvizhime so gluvcheto, so "p" ja prenosime pozicijata.</param>
        public void MovePlayer(int Side, Point p) {
            Player.Move(Side, p);
        }

        /// <summary>
        /// Metoda koja proveruva dali daden neprijatel (es) e vo dopir so metak (b).
        /// </summary>
        /// <param name="es">Neprijatel (EnemyShip)</param>
        /// <param name="b">Metak (Bullet)</param>
        /// <returns>Rezultatot e "true" ako dvata objekti se vo dopir.</returns>
        private bool CheckHit(EnemyShip es, Bullet b) {
            if (b.Position.Y < -180) {
                b.Active = false;
                return false;
            }
            bool x = es.Position.Y + 41 > b.Position.Y;
            bool y = (b.Position.X > es.Position.X - 8) && (b.Position.X < es.Position.X + 38);
            return x && y;
        }

        /// <summary>
        /// Gi brishe avionite shto se nadvor od vidliviot del.
        /// </summary>
        /// <param name="es">EnemyShip</param>
        /// <returns>Vrakaj "true" ako avionot e pod opredelenata granica.</returns>
        private bool CheckEnemy(EnemyShip es) {
            if (es.Position.Y > 520) {
                Player.RemoveHealth();
                if (Player.Health == 0)
                    ReferenceToForm.EndGame();
                es.RemoveHealth();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Pridvizhuvanje na site objekti na scenata.
        /// </summary>
        public void Move() {
            foreach (EnemyShip es in Enemies) {
                if (CheckEnemy(es))
                    continue;
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

        /// <summary>
        /// Metoda za dodavanje na metak.
        /// </summary>
        public void AddBullet() {
            Bullet b = new Bullet(Player.Position);
            Bullets.Add(b);
        }

        /// <summary>
        /// Funkcija koja gi zabrzuva neprijatelite.
        /// </summary>
        public void SpeedUpEnemies() {
            Counter = ++Counter % 5;
            if (Counter == 4) {
                ++EnemySpeed;
                Console.WriteLine("Speed: " + EnemySpeed);
            }
        }

        /// <summary>
        /// Funkcija za generiranje na neprijateli.
        /// Prvo se generira kolku neprijateli.
        /// Potoa se stavat na random mesto i se dodavaat vo listata na neprijateli.
        /// </summary>
        public void GenerateEnemies() {
            int c = Randomizer.Next(1, 4);
            int from = 0;
            if (c != 0) from = 300 / c;
            for (int i = 0; i < c; i++) {
                EnemyShip es = new EnemyShip(Randomizer.Next(i * from, (i + 1) * from), -10 * i, EnemySpeed);
                Enemies.Add(es);
            }
        }
    }
}
