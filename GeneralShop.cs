using System.Collections.Generic;

public class GeneralShop : Shop
{
    // 存哪些塔可以建造
    public TowerCollection standardTower;
    public TowerCollection rangeTower;
    public TowerCollection laserTower;
    public TowerCollection sunflower;


    private static GeneralShop instance;
    private void Awake()
    {
        instance = this;
    }

    public static GeneralShop GetInstance()
    {
        return instance;
    }
    public Dictionary<UIType,string> GetGeneralPrice()
    {
        Dictionary<UIType, string> dic = new Dictionary<UIType, string>();
        dic.Add( UIType.standard, standardTower.towers[0].cost.ToString());
        dic.Add(UIType.range, rangeTower.towers[0].cost.ToString());
        dic.Add(UIType.laser, laserTower.towers[0].cost.ToString());
        dic.Add(UIType.sleep, sunflower.towers[0].cost.ToString());
        return dic;
    }

    // 提供具体建造方法，供button选用
    public void BuildStandardTower()
    {
        base.BuildTower(standardTower);
    }

    public void BuildRangeTower()
    {
        base.BuildTower(rangeTower);
    }

    public void BuildSunflower()
    {
        base.BuildTower(sunflower);
    }

    public void BuildLaserTower()
    {
        base.BuildTower(laserTower);
    }
}
