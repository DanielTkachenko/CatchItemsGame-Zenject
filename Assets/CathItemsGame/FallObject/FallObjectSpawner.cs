using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CatchItemsGame
{
    public class FallObjectSpawner
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

        public FallObjectSpawner(
            PlayerScoreCounter scoreCounter,
            FallObjectController fallObjectController)
        {
            var spawnerConfig = Resources.Load<FallObjectSpawnConfig>(ResourcesConst.FallObjectSpawnConfig);
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
            _spawnPeriod = 6.5f;
            TickableManager.UpdateNotify += Update;
        }

        public void StopSpawn()
        {
            TickableManager.UpdateNotify -= Update;
            _fallObjectController.DestroyAll();
        }

        private void Update()
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