using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace World
{
    public class EnemyGenerator
    {
        private List<Vector3Int> ground;
        private List<Vector3Int> air;
        private List<Vector3Int> wall;
        private int enemyCount = 10;
        public List<GameObject> enemies;
        IList<GameObject> o;
        public EnemyGenerator(List<Vector3Int> groundPositions, List<Vector3Int> airPositions, List<Vector3Int> wallPositions)
        {
            ground = groundPositions;
            air = airPositions;
            wall = wallPositions;
            enemies = new List<GameObject>();
            Addressables.LoadAssetsAsync<GameObject>("air", obj =>
            {
                enemies.Add(obj);
            }).Completed += GenerateAirEnemies;
            Addressables.LoadAssetsAsync<GameObject>("ground", obj =>
            {
                enemies.Add(obj);
            }).Completed += GenerateGroundEnemies;
        }
        private void GenerateGroundEnemies(AsyncOperationHandle<IList<GameObject>> obj)
        {
            try { o = obj.Result; }
            catch { }
            while (enemyCount > 0)
            {
                foreach (Vector3Int position in ground)
                {
                    if (enemyCount > 0 && !wall.Contains(position))
                    {
                        var enemyPosition = new Vector3Int(position.x, position.y + 1, 0);
                        if (position.y > 10 && !ground.Contains(enemyPosition) && Random.Range(0, 10) == 1)
                        {
                            var offsetPosition = new Vector2(enemyPosition.x + .5f, enemyPosition.y + .2f);
                            WorldGenerator.Instantiate(o[Random.Range(0, o.Count)], offsetPosition, Quaternion.identity);
                            enemyCount--;
                        }
                    }
                }
            }
        }
        private void GenerateAirEnemies(AsyncOperationHandle<IList<GameObject>> obj)
        {
            try { o = obj.Result; }
            catch { }
            foreach (Vector3Int position in air)
            {
                if (enemyCount > 0 && position.y > 10 && Random.Range(0, 8) == 1)
                {
                    var offsetPosition = new Vector2(position.x + .5f, position.y + .5f);
                    WorldGenerator.Instantiate(o[Random.Range(0, o.Count)], position: offsetPosition, Quaternion.identity);
                    enemyCount--;
                }
            }
        }

    }
}
