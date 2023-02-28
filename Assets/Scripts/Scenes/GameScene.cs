using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        //SceneType = Define.Scene.Game;
        GameObject player = Managers.Game.Spawn("dog");
        GameObject bone = Managers.Game.Spawn("bone");
    }
    public override void Clear()
    {

    }
}
