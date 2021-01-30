﻿using ConsoleGameEngine;
using System.Collections.Generic;
using System.Numerics;

namespace RexMinus1
{
    public abstract class Level
    {
        protected List<Model> models = new List<Model>();

        private Sprite hud;
        private Sprite hud_left;
        private Sprite hud_middle;
        private Sprite hud_right;

        public ModelRenderer ModelRenderer { get; set; }

        public ConsoleEngine Engine { get; set; }

        public SpriteRenderer SpriteRenderer { get; set; }

        public LevelManager LevelManager { get; set; }

        public AnimationRenderer AnimationRenderer { get; set; }

        public void DrawDebug()
        {
            Engine.WriteText(new Point(0, 0), "X: " + ModelRenderer.CameraRotation, 14);
            Engine.WriteText(new Point(0, 3), "D: " + Vector3.Distance(ModelRenderer.CameraPosition, models[0].Position), 14);

            Engine.WriteText(new Point(0, 5), "P: " + ModelRenderer.CameraPosition.X + " " + ModelRenderer.CameraPosition.Y + " " + ModelRenderer.CameraPosition.Z, 14);
            Engine.WriteText(new Point(0, 6), "A: " + CustomMath.SimpleAngleBetweenTwoVectors(ModelRenderer.CameraForward, new Vector3(1, 0, 0)), 14);

            Engine.WriteText(new Point(0, 8), "F: " + ModelRenderer.CameraForward.X + " " + ModelRenderer.CameraForward.Y + " " + ModelRenderer.CameraForward.Z, 14);
            Engine.WriteText(new Point(0, 9), "L: " + ModelRenderer.CameraLeft.X + " " + ModelRenderer.CameraLeft.Y + " " + ModelRenderer.CameraLeft.Z, 14);
            Engine.WriteText(new Point(0, 10), "R: " + ModelRenderer.CameraRight.X + " " + ModelRenderer.CameraRight.Y + " " + ModelRenderer.CameraRight.Z, 14);
        }

        public void DrawHud()
        {
            SpriteRenderer.RenderSingle(new Point(0, 0), hud);
            SpriteRenderer.RenderSingle(new Point(12, 59), hud_left);
            SpriteRenderer.RenderSingle(new Point(47, 59), hud_middle);
            SpriteRenderer.RenderSingle(new Point(83, 59), hud_right);

            DrawBar(new Point(13, 59), 31, PlayerManager.Instance.Shields, 4);
            DrawBar(new Point(48, 59), 31, PlayerManager.Instance.Energy, 4);
            DrawBar(new Point(83, 59), 31, PlayerManager.Instance.Heat, 4);

            Engine.WriteText(new Point(24, 60), "  SHIELD  ", 1);
            Engine.WriteText(new Point(59, 60), "  ENERGY  ", 1);
            Engine.WriteText(new Point(94, 60), "   HEAT   ", 1);
        }

        public void DrawBar(Point origin, int size, float value, int color)
        {
            int end = (int)(size * value);
            Engine.Line(origin, new Point(origin.X + end, origin.Y), color);
        }

        protected void PlayAnimation(Animation anim)
        {
            AnimationRenderer.Add(anim);
        }

        public virtual void Start()
        {
            AnimationRenderer.Clear();
        }

        public virtual void Create()
        {
            ModelRenderer.UpdateCameraRotation(0.0f);

            hud = Sprite.FromFile("Assets/hud.png");
            hud_left = Sprite.FromFile("Assets/hud_bottom_bar_left.png");
            hud_middle = Sprite.FromFile("Assets/hud_bottom_bar_middle.png");
            hud_right = Sprite.FromFile("Assets/hud_bottom_bar_right.png");
        }

        public virtual void Update()
        {
            ModelRenderer.UpdateViewMatrix();
            ModelRenderer.UpdateVisibleFaces(models);
        }

        public virtual void Render()
        {
            ModelRenderer.Render();
            AnimationRenderer.Render();
        }
    }
}