﻿using OpenTK.Mathematics;
using RaceGame.Api.Common.GameObjects;
using System;

namespace RaceGame.Api.Services.LevelService
{
    public class LevelService : ILevelService
    {
        private GameObject[] _levelObjects;
        private string[] _levelsRightSequence;

        public LevelService()
        {
            CreateLevel();
        }

        public void CreateLevel()
        {
            // создаём карту
            Vector2[] positions =
            {
                new Vector2(0,0),
                new Vector2(0,100),
                new Vector2(0,200),
                new Vector2(0,300),
                new Vector2(0,400),
                new Vector2(0,500),
                new Vector2(0,600),
                new Vector2(100,0),
                new Vector2(200,0),
                new Vector2(300,0),
                new Vector2(400,0),
                new Vector2(500,0),
                new Vector2(600,0),
                new Vector2(700,0),
                new Vector2(800,0),
                new Vector2(900,0),
                new Vector2(1000,0),
                new Vector2(1100,0),
                new Vector2(1100,100),
                new Vector2(1100,200),
                new Vector2(1100,300),
                new Vector2(1100,400),
                new Vector2(1100,500),
                new Vector2(1100,600),
                new Vector2(100,600),
                new Vector2(200,600),
                new Vector2(300,600),
                new Vector2(400,600),
                new Vector2(500,600),
                new Vector2(600,600),
                new Vector2(700,600),
                new Vector2(800,600),
                new Vector2(900,600),
                new Vector2(1000,600),
                new Vector2(1100,600),

                new Vector2(100,100),
                new Vector2(200,100),
                new Vector2(600,100),

                new Vector2(400,200),
                new Vector2(800,200),
                new Vector2(900,200),

                new Vector2(200,300),
                new Vector2(300,300),
                new Vector2(400,300),
                new Vector2(500,300),
                new Vector2(600,300),
                new Vector2(700,300),
                new Vector2(800,300),
                new Vector2(900,300),

                new Vector2(200,400),
                new Vector2(300,400),
                new Vector2(400,400),
                new Vector2(500,400),
                new Vector2(600,400),
                new Vector2(700,400),
                new Vector2(800,400),
                new Vector2(900,400),
            };

            _levelObjects = new GameObject[positions.Length];
            _levelsRightSequence = new string[positions.Length];
            for (int i = 0; i < positions.Length; i++)
            {
                _levelObjects[i] = new GameObject()
                {
                    Id = Guid.NewGuid().ToString(),
                    PositionX = positions[i].X,
                    PositionY = positions[i].Y,
                    SizeX = 100, //sizes[i].X,
                    SizeY = 100 //sizes[i].Y
                };

                _levelsRightSequence[i] = _levelObjects[i].Id;
            }
        }

        public GameObject[] GetLevel()
        {
            return _levelObjects;
        }
    }
}
