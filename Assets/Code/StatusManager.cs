using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    /// <summary>
    /// 自分が食らう、相性によるダメージ倍率
    /// </summary>
    public struct CongenialityRate
    {
        public float fire;
        public float water;
        public float wind;
        public float earth;
    }

    /// <summary>
    /// ステータス
    /// </summary>
    public struct Status
    {
        public int HP;
        public int power;
        public int defense;
        public CongenialityRate congenialityRate;   // 相性倍率
        public float criticalRate;  // 会心率
        public float maxSP;  // 最大SP
        public int speed;   // 移動速度
        public float luck;  // 運（仮）
    }

    /// <summary>
    /// 相性倍率 0 のデータ
    /// </summary>
    static readonly CongenialityRate cong_zero = new() { fire = 0, water = 0, wind = 0, earth = 0 };
    //--------------------------------
    //              セットアップ処理
    //--------------------------------



    public static CongenialityRate CreateCongenialityRate
    (
        float fire  = 1f,
        float water = 1f,
        float wind  = 1f,
        float earth = 1f
    )
    {
        var newCongenialityRate = new CongenialityRate();

        // 各倍率を設定
        newCongenialityRate.fire  = fire;
        newCongenialityRate.water = water;
        newCongenialityRate.wind  = wind;
        newCongenialityRate.earth = earth;

        return newCongenialityRate;

    }


    /// <summary>
    /// 新たなステータスデータを作成
    /// </summary>
    /// <param name="HP">HP</param>
    /// <param name="power">攻撃力</param>
    /// <param name="defense">防御力</param>
    /// <param name="congeniality">相性倍率</param>
    /// <param name="criticalRate">会心率</param>
    /// <param name="maxSP">最大SP</param>
    /// <param name="speed">移動速度</param>
    /// <param name="luck">運</param>
    /// <returns>ステータス構造体</returns>
    public static Status CreateStatus
    (
        int HP = 0,
        int power = 0,
        int defense = 0,
        CongenialityRate congenialityRate = new(),
        float criticalRate = 0,
        float maxSP = 0,
        int speed = 0,
        float luck = 0
    )
    {
        var newStatus = new Status();

        // 各ステータスを設定
        newStatus.HP = HP;
        newStatus.power = power;
        newStatus.defense = defense;
        newStatus.criticalRate = criticalRate;
        newStatus.maxSP = maxSP;
        newStatus.speed = speed;
        newStatus.luck = luck;

        // 相性倍率が未設定なら初期化
        if (Equals(congenialityRate, new CongenialityRate()))
            newStatus.congenialityRate = CreateCongenialityRate();
        else
            newStatus.congenialityRate = congenialityRate;


        return newStatus;
    }


    //--------------------------------
    //              演算
    //--------------------------------


    public static CongenialityRate SumCongenialityRate(params CongenialityRate[] congArray)
    {
        var sumCong = new CongenialityRate();   // 各倍率の合算値

        // 各倍率を合算
        foreach(var cong in congArray )
        {
            sumCong.fire += cong.fire;
            sumCong.water += cong.water;
            sumCong.wind += cong.wind;
            sumCong.earth += cong.earth;
        }

        return sumCong;
    }


    /// <summary>
    /// ステータス値を合算
    /// </summary>
    /// <param name="statusArray">ステータスリスト　※params指定のためStatusの列挙も可</param>
    /// <returns></returns>
    public static Status SumStatus(params Status[] statusArray)
    {
        var sumStatus = CreateStatus();     // 各ステータスの合算値
        sumStatus.congenialityRate = cong_zero;

        // 各ステータスを合算
        foreach (var status in statusArray)
        {
            sumStatus.HP += status.HP;
            sumStatus.power += status.power;
            sumStatus.defense += status.defense;
            sumStatus.criticalRate += status.criticalRate;
            sumStatus.maxSP += status.maxSP;
            sumStatus.speed += status.speed;
            sumStatus.luck += status.luck;
            sumStatus.congenialityRate = SumCongenialityRate(sumStatus.congenialityRate, status.congenialityRate);
        }

        return sumStatus;
    }

    //--------------------------------
    //              表示
    //--------------------------------

    public static void ShowCongenialityRate(CongenialityRate cong)
    {
        var showText = $"---倍率---";

        showText += $"\n火 : {cong.fire}";
        showText += $"\n水 : {cong.water}";
        showText += $"\n風 : {cong.wind}";
        showText += $"\n土 : {cong.earth}";

        Debug.Log(showText);
    }

    public static void ShowStatus(Status status)
    {
        var showText = $"---ステータス---";

        showText += $"\nHP : {status.HP}";
        showText += $"\n攻撃力 : {status.power}";
        showText += $"\n防御力 : {status.defense}";
        showText += $"\n会心率 : {status.criticalRate}";
        showText += $"\n最大SP : {status.maxSP}";
        showText += $"\n移動速度 : {status.speed}";
        showText += $"\n運 : {status.luck}";
        showText += $"\n倍率 : 下ログを参照";

        Debug.Log(showText);

        ShowCongenialityRate(status.congenialityRate);
    }


}



