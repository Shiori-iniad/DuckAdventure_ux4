using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// IFaderというインターフェースを定義
// ⇔この機能を持っていることを保証する約束
public interface IFader
{
    // このメソッドを必ず実装しなさいという宣言
    void FadeToScene(string sceneName);
}