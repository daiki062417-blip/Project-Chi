using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{

/*
・基本計算
キャラクターステータスは、既に武器分のステータス加算を受けている    

純粋ダメージ = （キャラ攻撃力＋武器攻撃力） × 技係数
ダメージ＝純粋ダメージ - 防御力＊定数
会心ダメージ＝ダメージ＊（会心補正（会心率によって増加）＋武器会心補正　(会心失敗時は1)
属性ダメージ = 会心ダメージ＊属性補正
最終ダメージ = 属性ダメージ＊乱数(｛運による値)｝%)
※運：81~121ぐらい？
・特殊な計算がある場合はそちらを優先。
 */
    int damage(StatusManager.Status mystatus, StatusManager.Status enemystatus)
    {
        //仮で技係数をここで用意しているが、後から引数で参照されるようにする
        float wazakeisu = 0.8f;
        float defence_constant = 0.8f;

        int d = (int)(mystatus.power * wazakeisu);
        d -= (int)defence_constant * enemystatus.defense;
        //補正無しで会心ダメージ倍率は1.5倍で、会心率100％で倍率2.0倍（修正してよい）
        d = (int)(d * (1.5 + mystatus.criticalRate * 0.05));


        return d;
    }

    //属性相性による倍率計算。攻撃側で倍率をさらに増やせるようにするべき？
    int CongenialityRateDamage
    (int before_damage, Parameter.element MyElement, Parameter.element EnemyElement)
    {
        int d = before_damage;
        //自分属性と相手属性を元にダメージを乗算する
        if(MyElement == Parameter.element.normal || EnemyElement ==Parameter.element.normal)
        {
            return d;
        }
        //未完成だよ！

        return d;
    }
}
