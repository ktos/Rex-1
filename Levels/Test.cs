﻿using ConsoleGameEngine;
using RexMinus1.GameObjects;
using System;
using System.Linq;
using System.Numerics;

namespace RexMinus1.Levels
{
    internal class Test : Level
    {
        private Animations.Laser laser;

        public override void Start()
        {
            if (PlayerManager.Instance.IsMusicEnabled)
                MusicPlaybackEngine.Instance.PlayMusic("Assets/song_main.ogg");

            AudioPlaybackEngine.Instance.PlayCachedSound("startgame");

            speed = 0;

            base.Start();
        }

        public override void Create()
        {
            models.Add(new Mine
            {
                Position = new Vector3(0, 0, 23),
                RotationX = 1.241f
            });

            models.Add(new Astronaut
            {
                Mesh = Mesh.LoadFromObj("Assets/obj_astro.obj"),
                Position = new Vector3(30, 0, 3),
            });

            laser = new Animations.Laser();

            base.Create();
        }

        public override void Update()
        {
            // zmiana pozycji gracza
            if (Engine.GetKeyDown(ConsoleKey.LeftArrow) || Engine.GetKeyDown(ConsoleKey.A))
            {
                ModelRenderer.UpdateCameraRotation(-0.05f);
            }

            if (Engine.GetKeyDown(ConsoleKey.RightArrow) || Engine.GetKeyDown(ConsoleKey.D))
            {
                ModelRenderer.UpdateCameraRotation(0.05f);
            }

            if (Engine.GetKeyDown(ConsoleKey.UpArrow) || Engine.GetKeyDown(ConsoleKey.W))
            {
                speed += 0.05f;
                PlayerManager.Instance.Energy -= 0.01f;
                PlayerManager.Instance.Heat += 0.1f;
            }

            if (Engine.GetKeyDown(ConsoleKey.DownArrow) || Engine.GetKeyDown(ConsoleKey.S))
            {
                speed -= 0.04f;
                PlayerManager.Instance.Energy -= 0.01f;
                PlayerManager.Instance.Heat += 0.1f;
            }

            if (Engine.GetKeyDown(ConsoleKey.Q))
            {
                ModelRenderer.UpdateCameraMovement(0.0f, -0.1f);
            }

            if (Engine.GetKeyDown(ConsoleKey.E))
            {
                ModelRenderer.UpdateCameraMovement(0.0f, 0.1f);
            }

            if (Engine.GetKeyDown(ConsoleKey.R))
            {
                ModelRenderer.UpdateFOV(0.1f);
            }

            if (Engine.GetKeyDown(ConsoleKey.F))
            {
                ModelRenderer.UpdateFOV(-0.1f);
            }

            if (Engine.GetKeyDown(ConsoleKey.F2))
            {
                LevelManager.GoTo(0);
            }

            // aktualizacja pozycji
            ModelRenderer.UpdateCameraMovement(speed, 0.0f);

            // strzał
            if (Engine.GetKeyDown(ConsoleKey.U))
            {
                // animacja lasera
                laser.Reset();
                laser.IsPaused = false;
                PlayAnimation(laser);
                AudioPlaybackEngine.Instance.PlayCachedSound("shoot_laser");

                // aktualizacja energii
                PlayerManager.Instance.Energy -= 0.1f;
                PlayerManager.Instance.Heat += 0.1f;

                // sprawdzenie warunków trafienia
                foreach (var enemy in models.OfType<Enemy>())
                {
                    enemy.Hit(ModelRenderer.CameraPosition);
                }
            }

            // sprawdzenie warunków zwycięstwa i przegranej
            //if (base.CheckWin())
            //    LevelManager.GoTo(LevelManager.GameOverWin);

            if (base.CheckLose())
                LevelManager.GoTo(LevelManager.GameOverLose);

            // kolizje, poruszanie obiektami
            base.Update();
        }

        public override void Render()
        {
            base.Render();

            DrawCompassBar();
            DrawHud();
        }
    }
}