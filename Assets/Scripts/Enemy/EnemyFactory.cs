using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public abstract class EnemyFactory
    {
        public abstract IEnemy SpawnEnemy(Vector3 position);
    }
}
