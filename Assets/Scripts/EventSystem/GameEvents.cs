using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKilled : GameEvent
    {
        public EnemyManager.TypeOfEnemy enemyType{ get; private set; }
        
        public EnemyKilled(EnemyManager.TypeOfEnemy EnemyType)
        {
            enemyType = EnemyType;
        }
    }

public class EnemyWaveKilled: GameEvent{}

public class StayFourCorner: GameEvent{}

public class StayEnemyAchieve: GameEvent{}

public class WalkEnemyAchieve : GameEvent{}

public class JumpEnemyAchieve : GameEvent{}

public class WaveAchieve : GameEvent{}

public class StayCornerAchieve : GameEvent{}