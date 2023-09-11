using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace CatchItemsGame
{
    public class FallObjectSpawner : ITickable
    {
        private readonly PlayerScoreCounter _scoreCounter;
        private readonly FallObjectController _fallObjectController;
        private readonly float _spawnPeriodMin;
        private readonly float _spawnPeriodMax;
        private readonly float _minPositionX;
        private readonly float _maxPositionX;
        private readonly float _positionY;
        private readonly float _delayStartSpawn;
        private Vector3 _spawnPosition;
        private float _spawnPeriod;
        private float _timer;
        private int _typesCount;

        private bool isActive;

        public FallObjectSpawner(
            PlayerScoreCounter scoreCounter,
            FallObjectController fallObjectController,
            FallObjectSpawnConfig fallObjectSpawnConfig)
        {
            var spawnerConfig = fallObjectSpawnConfig;
            _positionY = spawnerConfig.PositionY;
            _minPositionX = spawnerConfig.MinPositionX;
            _maxPositionX = spawnerConfig.MaxPositionX;
            _spawnPeriodMin = spawnerConfig.SpawnPeriodMin;
            _spawnPeriodMax = spawnerConfig.SpawnPeriodMax;
            _delayStartSpawn = spawnerConfig.DelayStartSpawn;
            _spawnPosition = new Vector2(Random.Range(_minPositionX, _maxPositionX), _positionY);

            _fallObjectController = fallObjectController;
            _spawnPeriod = Random.Range(_spawnPeriodMin, _spawnPeriodMax);
            _typesCount = Enum.GetValues(typeof(FallObjectType)).Length;
        }

        public void StartSpawn()
        {
            isActive = true;
            _spawnPeriod = 6.5f;
            _fallObjectController.StartGame();
        }

        public void StopSpawn()
        {
            isActive = false;
            _fallObjectController.StopGame();
        }

        public void Tick()
        {
            if (isActive)
            {
                _spawnPeriod -= Time.deltaTime;
                _timer += Time.deltaTime;
            
                if (_timer > _delayStartSpawn)
                {
                    if (_spawnPeriod <= 0)
                    {
                        SpawnNewObject();
                        _spawnPeriod = Random.Range(_spawnPeriodMin, _spawnPeriodMax);
                    }
                }
            }
        }

        private void SpawnNewObject()
        {
            var type = Random.Range(0, _typesCount);
            _spawnPosition.x = Random.Range(_minPositionX, _maxPositionX);
            if (_fallObjectController != null)
            {
                _fallObjectController.Create(_spawnPosition, (FallObjectType)type);
            }
        }
    }
}