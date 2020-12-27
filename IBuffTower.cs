using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuffTower
{
    void Register(AttackableTower attackableTower);

    void Delete(AttackableTower attackableTower);

    //void Update();

    void DoBuff(AttackableTower attackableTower);

    void Start();
}
