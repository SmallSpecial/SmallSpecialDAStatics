using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSSModel.Request
{
    /// <summary>
    /// 运营活动-掉落活动
    /// </summary>
    [Serializable]
    public class ActiveFallGoods : GameZone
    {
        public string ActiveName { get; set; }
       
        /// <summary>
        /// 限制的角色最低等级
        /// </summary>
        public int MinRoleLevel { get; set; }
        /// <summary>
        /// 限制的角色的最高等级
        /// </summary>
        public int MaxRoleLevel { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 打怪掉物品的场景ID集合，运行多个场景
        /// </summary>
        public int[] SceneIds{ get; set; }
        /// <summary>
        /// 物品掉落数目
        /// </summary>
        public int FallGoodNum { get; set; }
        /// <summary>
        /// 掉落的物品ID
        /// </summary>
        public int GoodId { get; set; }
        /// <summary>
        /// 掉落概率的分子
        /// </summary>
        public int GoodsFallPRNumerator { get; set; }
        /// <summary>
        /// 掉落概率的分母
        /// </summary>
        public int GoodsFallPRDenominator { get; set; }
        /// <summary>
        /// 物品掉落概率【如果该属性赋值了则忽略GoodsFallPRNumerator，GoodsFallPRDenominator】
        /// </summary>
        public double? GoodFallPR { get; set; }
        /// <summary>
        /// 掉落类型
        /// </summary>
        public int FallType { get; set; }
        /// <summary>
        /// 掉落的道具ExcelID
        /// </summary>
        public int EquipNo { get; set; }
        
    }
    [Serializable]
    public class ActiveFallGoodsData : ActiveFallGoods
    {
        /// <summary>
        /// 创建人
        /// </summary>
        public int CreateBy { get; set; }
        public string BigZoneName { get; set; }
        public string ZoneName { get; set; }
        public int AppId { get; set; }
        /// <summary>
        /// 同步配置到MySQL
        /// </summary>
        public bool SyncConfig { get; set; }
    }

    [Serializable]
    public class GameZone 
    {
        public int BigZoneID { get; set; }
        public int ZoneID { get; set; }
        public int TaskTypeID { get; set; }
    }
    public class ActiveFallGoodLogicData
    {//活动掉落数据的逻辑数据【将存储到server中，由于序列表的数据会在json解析时增加字符k__BackingField导致多占用内存不直接使用ActiveFallGoods类， 实际上就是ActiveFallGoods只是为了减少内存占用】
        public string ActiveName { get; set; }
        public int MinRoleLevel { get; set; }
        public int MaxRoleLevel { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int[] SceneIds { get; set; }
        public int FallGoodNum { get; set; }
        public int GoodId { get; set; }
        public int GoodsFallPRNumerator { get; set; }
        public int GoodsFallPRDenominator { get; set; }
        public double? GoodFallPR { get; set; }
        public int FallType { get; set; }
        public int EquipNo { get; set; }
        public int BigZoneID { get; set; }
        public int ZoneID { get; set; }
        public int TaskTypeID { get; set; }
    }
}
