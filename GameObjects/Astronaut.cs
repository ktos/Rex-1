﻿using ConsoleGameEngine;
using System;
using System.Numerics;

namespace RexMinus1.GameObjects
{
    internal class Astronaut : Model, ICollision
    {
        public bool IsCollected { get; set; }

        public float CollisionAttack { get; set; } = 0;
        public float CollisionRange { get; set; } = 5;
        public float DetectionRange { get; set; } = 50;
        public float IdentificationRange { get; set; } = 10;
        public bool IsVisible { get; set; } = true;
        public bool IsIdentified { get; set; } = false;
        public bool IsDetected { get; set; } = false;

        public float Collision(Vector3 player)
        {
            var d = Math.Abs(Vector3.Dot(player, this.Position));

            if (d < CollisionRange)
            {
                AudioPlaybackEngine.Instance.PlayCachedSound("beep_3");
                IsCollected = true;
            }

            return d;
        }
    }
}