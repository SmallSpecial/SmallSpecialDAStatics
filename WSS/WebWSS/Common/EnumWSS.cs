using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WebWSS.Common
{
    public class EnumWSS
    {
        //GM日志类型
        public enum ENUM_GMCMD_LOG_TYPE
        {

            //指令格式 : help [<命令名>]
            //提供命令的参数说明。
            [Description("提供命令的参数说明")]
            E_TYPE_GM__CMD_HELP = 0,

            //指令格式 : go <方向> )
            //让你往指定的方向移动。
            [Description("让你往指定的方向移动")]
            E_TYPE_GM_CMD_GO = 1,

            //指令格式: look [<物品>|<人物>|<地点>]
            //你可以查看你所在的环境、物品、人物的信息。
            //如果没有参数，则是直接查看你所在的场景。
            //并且提供该场景的所有可能的出口。为行走指令（go）提供线索。
            [Description("查看你所在的环境、物品、人物的信息")]
            E_TYPE_GM_CMD_LOOK = 2,

            //用户列表, 统计在线总人数
            //列出当前所有用户的用户名（别名），名字等有关信息。
            [Description("列出当前所有用户名（别名），名字等信息")]
            E_TYPE_GM_CMD_USERLIST = 3,

            //指令格式: get <物品>|all [from <容器>] [to <某人>]
            //从地上捡起某物品。从某容器里拿出某物品。
            [Description("从地上捡起某物品。从某容器里拿出某物品")]
            E_TYPE_GM_CMD_GET = 4,


            //指令格式: drop <物品>|all [from <某人>] [to <容器>]
            //把身上的物品扔到地上。
            [Description("把身上的物品扔到地上")]
            E_TYPE_GM_CMD_DROP = 5,

            //
            //指令格式: give <物品> to <某人>
            //把你身上的东西给某人。
            [Description("把你身上的东西给某人")]
            E_TYPE_GM_CMD_GIVE = 6,
            //
            //指令格式: inventory [<某人>]
            //查看自己身上带了哪些物品。
            [Description("查看自己身上带了哪些物品")]
            E_TYPE_GM_CMD_INVENTORY = 7,
            //
            //指令格式: hp [<某人>|<物品>]
            //查看自己的属性。
            [Description("查看自己的属性")]
            E_TYPE_GM_CMD_HP = 8,

            //指令格式: score
            //查看自己的经验。
            [Description("查看自己的经验")]
            E_TYPE_GM_CMD_SCORE = 9,

            //指令格式: open <宝箱>
            //打开某个宝箱，该宝箱内的物品将掉到地上。
            [Description("打开某个宝箱，该宝箱内物品掉到地上")]
            E_TYPE_GM_CMD_OPEN = 10,

            //关闭某样东西。
            [Description("关闭某样东西")]
            E_TYPE_GM_CMD_CLOSE = 11,

            //打开相同别名的所有宝箱。
            [Description("打开相同别名的所有宝箱")]
            E_TYPE_GM_CMD_OPENALL = 12,

            //指令格式: list <某人|物品>。与inventory命令功能完全相同
            //列出该人携带或者该物品内部的所有物品。
            //这个指令主要用于查看商店的商品。
            [Description("列出该人携带或该物品内部所有物品")]
            E_TYPE_GM_CMD_LIST = 13,

            //指令格式: use <物品> [to <某人>]
            //对自己或者某人使用物品。
            [Description("对自己或者某人使用物品")]
            E_TYPE_GM_CMD_USE = 14,

            //指令格式: kill <某人|all> [死亡方式]
            //杀死某个NPC或者用户。
            [Description("杀死某个NPC或者用户")]
            E_TYPE_GM_CMD_KILL = 15,

            //指令格式: summon <某人>
            //把某人抓到你所在的地点。
            //只能抓用户或者本场景内的NPC（图形版）。
            [Description("把某人抓到你所在的地点")]
            E_TYPE_GM_CMD_SUMMON = 16,

            //指令格式: where [<某人>]
            //查看自己或某人所在的位置。
            [Description("查看自己或某人所在的位置")]
            E_TYPE_GM_CMD_WHERE = 17,

            //指令格式: rights [激活码]
            //查看或者修改自己的权限
            [Description("查看或者修改自己的权限")]
            E_TYPE_GM_CMD_RIGHTS = 18,

            //指令格式: who <用户>
            //查看用户的所有属性信息。
            [Description("查看用户的所有属性信息")]
            E_TYPE_GM_CMD_WHO = 19,

            //指令格式: attribute <物品>
            //查看物体的详细信息。
            [Description("查看物体的详细信息")]
            E_TYPE_GM_CMD_ATTRIBUTE = 20,

            //指令格式: after <某人>
            //来到某人的身边。此人必须是用户或者NPC。
            //图形专用，必须在同场景内
            [Description("来到某人的身边")]
            E_TYPE_GM_CMD_AFTER = 21,

            //指令格式: forget
            //忘记所有的事情，清空所有的标志位。
            [Description("清空所有的标志位")]
            E_TYPE_GM_CMD_FORGET = 22,

            //指令格式: flag [<标志位名>] [数值]
            //查看或更改自己的标志位。
            //如果没有标志位名则为全部标志位，如果没有数值则为查看。
            [Description("查看或更改自己的标志位")]
            E_TYPE_GM_CMD_FLAG = 23,

            //指令格式：experience [<某人>] <数量>
            //给指定的人增加指定数量的经验值
            [Description("给指定的人增加指定数量的经验值")]
            E_TYPE_GM_CMD_EXP = 24,

            //显示所有的出口
            [Description("显示所有的出口")]
            E_TYPE_GM_CMD_DIR = 25,

            //系统公告
            [Description("系统公告")]
            E_TYPE_GM_CMD_SYSTEMSAY = 26,

            //用户列表
            //列出当前所有用户的用户名（别名），名字等有关信息。
            [Description("用户列表")]
            E_TYPE_GM_CMD_LIST_CMD = 27,

            //指令格式 : birthitem <道具ExcelID><数量><耐久度> <是否出现效果0><是否出现效果1><是否出现效果2><是否出现效果3><是否出现效果4><是否出现效果5><是否出现效果6><是否出现效果7>                                                创建道具
            [Description("创建道具")]
            E_TYPE_GM_CMD_BIRTH_ITEM = 28,

            //指令格式 : gotoscene <Scene ID>                                       让你移动到指定的SceneID的地图
            [Description("让你移动到指定的SceneID的地图")]
            E_TYPE_GM_CMD_GOTO_SCENE = 29,

            //指令格式: team [<某人>]                                              使该人成为自己的队员。或者查看队伍信息。
            [Description("使该人成为自己的队员。或查看队伍信息")]
            E_TYPE_GM_CMD_TEAM = 30,

            //指令格式: dismiss <某人>|all                                           解散该人，解除与之的组队关系。
            [Description("解散该人，解除与之的组队关系")]
            E_TYPE_GM_CMD_DISMISS = 31,

            //指令格式: follow <某人>|none                                         加入或者离开该人所在的队伍中。
            [Description("加入或者离开该人所在的队伍中")]
            E_TYPE_GM_CMD_FOLLOW = 32,

            //指令格式：attrchange [<某人>] <力量> <智慧> <敏捷> <体质>                 给指定的人修改四项基本属性
            [Description("给指定的人修改四项基本属性")]
            E_TYPE_GM_CMD_ATT_EDIT = 33,

            //指令格式 : setpos <X Pos><Z Pos>                                       让你跳转到指定的位置
            [Description("让你跳转到指定的位置")]
            E_TYPE_GM_CMD_SETPOS = 34,

            //指令格式 : subeffect [<某人>]                                           显示指定人物身上的subeffect
            [Description("显示指定人物身上的subeffect")]
            E_TYPE_GM_CMD_SUBEFFECT = 35,

            //指令格式 : skilllevel [index][level]                                     修改技能的级别
            [Description("修改技能的级别")]
            E_TYPE_GM_CMD_SKILL_LEVEL = 36,

            //指令格式 : skillflag <flag> [0: off][1: on]                           打开或者关闭技能的调试flag， flag: [cooldown] [cost] [study] [point]
            [Description("打开或关闭技能的调试flag")]
            E_TYPE_GM_CMD_SKILL_FLAG = 37,

            //指令格式 : subeffectflag <flag> [0: off][1: on]                         打开或者关闭附加效果的调试flag， flag: [rcu]
            [Description("打开或关闭附加效果的调试flag")]
            E_TYPE_GM_CMD_EFFECT_FLAG = 38,

            //"指令格式1 : task list
            //列出身上带的所有任务
            //指令格式2 : task del <ID>
            //删除指定ID的任务
            [Description("列出身上带的所有任务,删除任务")]
            E_TYPE_GM_CMD_TASK = 39,

            //"指令格式 :  levelup <等级>
            //升级
            [Description("升级")]
            E_TYPE_GM_CMD_LEVELUP = 40,

            //指令格式2 :  change modi <序号> <属性> <数值>
            //改变的人物属性
            //显示可改变的人物属性
            [Description("改变的人物属性")]
            E_TYPE_GM_CMD_CHANGE = 41,

            //指令格式 : rumor <讯息>      
            //散布谣言，没有人知道是你说的这些话。
            [Description("散布谣言，没有人知道是你说的这些话")]
            E_TYPE_GM_CMD_RUMOR = 42,

            //指令格式: chat <讯息>   
            //闲聊，没有人知道是你说的这些话
            [Description("闲聊，没有人知道是你说的这些话")]
            E_TYPE_GM_CMD_CHAT = 43,

            //指令格式: shout <讯息>
            //大声叫喊，所有正在游戏中的人(包括服务器)都会听见你的话。
            [Description("大声叫喊，正在游戏中的人都会听见你的话")]
            E_TYPE_GM_CMD_SHOUT = 44,

            //指令格式: tell <讯息>
            //私聊，对方可以不在附近，只要在游戏中即可对话。
            [Description("私聊，对方可以不在附近，在游戏中即可对话")]
            E_TYPE_GM_CMD_TELL = 45,

            //指令格式：money [<某人>] <数量>
            //给指定的人增加指定数量的钱
            [Description("给指定的人增加指定数量的钱")]
            E_TYPE_GM_CMD_MONEY = 46,

            //指令格式：save [<某人>]
            //强制角色存盘
            [Description("强制角色存盘")]
            E_TYPE_GM_CMD_SAVE = 47,

            //指令格式: register <密码>
            //注册用户，使你的权限从访问者升为普通用户。
            //密码是用户购买到的月费卡中提供的注册密码。
            [Description("注册用户，权限从访问者升为普通用户")]
            E_TYPE_GM_CMD_REGISTER = 48,

            //指令格式 :  wear
            //设置、显示道具耐久度
            [Description("设置、显示道具耐久度")]
            E_TYPE_GM_CMD_WEARITEM = 49,

            //指令格式 :  faction
            //设置帮派
            [Description("设置帮派")]
            E_TYPE_GM_CMD_FACTION = 50,

            //指令格式 :  cmdtime <cmdID(-1表示所有)>
            //观察服务器对消息的处理时间
            [Description("观察服务器对消息的处理时间")]
            E_TYPE_GM_CMD_TIME_TEST = 51,

            //指令格式 :  logic
            //设定服务器的快速逻辑和慢速逻辑 fps 限制
            [Description("设定服务器的快速逻辑和慢速逻辑")]
            E_TYPE_GM_CMD_SERVER_LOGIC = 52,

            //指令格式 : ghost list 或者 ghost flush [sb.]
            E_TYPE_GM_CMD_GHOSTOPT = 53,

            //指令格式 : reloadcfg s 或者 reloadcfg f
            E_TYPE_GM_CMD_RELOAD_CFG_FILE = 54,

            //指令格式 : loadghost ghost_type ghost_number
            E_TYPE_GM_CMD_LOADGHOST = 55,

            //指令格式 :	tasksort task_id from_position list_size
            //				tasksort task_id list_size
            //				tasksort task_id
            E_TYPE_GM_CMD_TASKSORT = 56,

            //指令格式：award [<国籍>] <相关奖品>
            //给指定国籍的人发奖，奖品可能有钱和物品
            [Description("给指定国籍的人发奖，钱和物品")]
            E_TYPE_GM_CMD_AWARD = 57,

            //指令格式：award [<国籍>] <-i> <物品id> <数量>
            //给指定国籍的人发奖，奖品为物品
            [Description("给指定国籍的人发奖，物品")]
            E_TYPE_GM_CMD_AWARD_ITEM = 58,

            //指令格式：award [<国籍>] <-i> <金钱数量>
            //给指定国籍的人发奖，奖品为钱
            [Description("给指定国籍的人发奖，钱")]
            E_TYPE_GM_CMD_AWARD_MONEY = 59,

            //指令格式：changescene
            //强制所有的人切换场景
            [Description("强制所有的人切换场景")]
            E_TYPE_GM_CMD_AWARD_CHANGESCENE = 60,

            //指令格式：特殊
            //GMCMD -> LGC
            E_TYPE_GM_CMD_TO_LGCOPT = 61,

            //指令格式：activity
            //活动相关的一些指令
            [Description("活动相关的一些指令")]
            E_TYPE_GM_CMD_ACTIVITY = 62,

            //指令格式 overwhelming ([0,1])
            //关闭/开启无敌状态
            [Description("关闭/开启无敌状态")]
            E_TYPE_GM_CMD_OVERWHELMING = 63,

            //指令格式: whois GlobalID
            //查询某个玩家的一些属性(哪个地方, 什么名字);
            [Description("查询某个玩家的一些属性")]
            E_TYPE_GM_CMD_WHOIS = 64,

            //指令格式: SendServerPos ([0,1])
            //开启发送服务器位置信息
            [Description("开启发送服务器位置信息")]
            E_TYPE_GM_CMD_SENDSERVERPOS = 65,

            //Added by lsk 2006.3.3
            // 下面的不是巫师指令的log，先借放在这里。
            E_TYPE_GM_CMD_OTHER_BEGIN = 2000,

            // 玩家升级时间过短
            [Description("玩家升级时间过短")]
            E_TYPE_GM_CMD_OTHER_LEVEL_UP_SHORT = 2010,

            // 玩家一次升级超过一级
            [Description("玩家一次升级超过一级")]
            E_TYPE_GM_CMD_OTHER_LEVEL_UP_MORE = 2002,

            // 角色身上有两个GUID相同的道具
            [Description("角色身上有两个GUID相同的道具")]
            E_TYPE_GM_CMD_OTHER_GUID_SAME = 2003,

            // 设置道具灵性
            [Description("设置道具灵性")]
            E_TYPE_GM_CMD_LOAD_GEM = 2004,

            //设置定点飞行状态---起飞/降落
            [Description("设置定点飞行状态---起飞/降落")]
            E_TYPE_GM_CMD_TO_FLYUP = 2005,


        };

        enum ENUM_OPERA_LOG_TYPE
        {
            [Description("")]
            OPERA_LOG_TYPE_BEGIN = 0,
            // ...
            OPERA_LOG_ITEM_BEGIN = 10000,
            //-------道具记录---------//

            // 从NPC那里买道具
            // pLogPara 参数
            // D0	:	角色的UserID
            // D1	:	角色的GlobleID
            // N2	:	道具的数量
            // N3	:	钱
            // D4	:	道具的ID
            [Description("从NPC那里买道具")]
            OPERA_LOG_BUY_ITEM_FROM_NPC = 10000,

            // 向NPC那里卖道具
            // pLogPara 参数
            // D0	:	角色的UserID
            // D1	:	角色的GlobleID
            // N2	:	道具的数量
            // N3	:	钱
            // D4	:	道具的ID
            [Description("向NPC那里卖道具")]
            OPERA_LOG_SELL_ITEM_TO_NPC = 10001,

            // 玩家间的道具交易
            // szDes : 道具的GUID\t道具的EXCELID
            [Description("玩家间的道具交易")]
            OPERA_LOG_PLAYER_TO_PLAYER_TRADE = 10002,

            // 捡道具
            // pLogPara 参数
            // D0	:	角色的UserID
            // D1	:	角色的GlobleID
            // D2	:	道具的ID
            [Description("捡道具")]
            OPERA_LOG_PICKUP_ITEM = 10003,

            // 仍道具
            // pLogPara 参数
            // D0	:	角色的UserID
            // D1	:	角色的GlobleID
            // D2	:	道具的ID
            [Description("扔道具")]
            OPERA_LOG_DROP_ITEM = 10004,

            // 使用道具
            // pLogPara 参数
            // D0	:	角色的UserID
            // D1	:	角色的GlobleID
            // D2	:	道具的ID
            [Description("使用道具")]
            OPERA_LOG_USE_ITEM = 10005,

            // 怪死亡时凋落的道具
            // FIELD1 : 怪ExclID
            // FIELD2 : 无效
            // FIELD3 : 掉落道具的ExelID 
            // FIELD4 : 场景号
            // OP_BAK : 道具GUID
            [Description("怪死亡时凋落的道具")]
            OPERA_LOG_DEATH_DROP_ITEM = 10006,

            // 宝石合成
            // pLogPara 参数
            // D0	:	角色的UserID
            // D1	:	角色的GlobleID
            // D2	:	合成道具的ID
            // N3	:	原材料的数量
            // D4	:	原材料的ID （循环）
            [Description("宝石合成")]
            OPERA_LOG_COMPOSE_ITEM = 10007,

            // 镶嵌道具
            // pLogPara 参数
            // D0	:	角色的UserID
            // D1	:	角色的GlobleID
            // D2	:	镶嵌道具的ID
            // D3	:	宝石的ID
            [Description("镶嵌道具")]
            OPERA_LOG_BESET_ITEM = 10008,

            // 道具入库
            // pLogPara 参数
            // D0	:	角色的UserID
            // D1	:	角色的GlobleID
            // D2	:	道具的ID
            [Description("道具入库")]
            OPERA_LOG_ITEM_IN_DEPOT = 10009,

            // 道具出库
            // pLogPara 参数
            // D0	:	角色的UserID
            // D1	:	角色的GlobleID
            // D2	:	道具的ID
            [Description("道具出库")]
            OPERA_LOG_ITEM_OUT_DEPOT = 10010,

            // 存钱
            // pLogPara 参数
            // D0	:	角色的UserID
            // D1	:	角色的GlobleID
            // N2	:	钱数
            //
            [Description("存钱")]
            OPERA_LOG_MONEY_IN_DEPOT = 10011,

            // 取钱
            // pLogPara 参数
            // D0	:	角色的UserID
            // D1	:	角色的GlobleID
            // N2	:	钱数
            //
            [Description("取钱")]
            OPERA_LOG_MONEY_OUT_DEPOT = 10012,

            //道具法宝融化消耗掉
            // pLogPara 参数
            // D0	:	角色的UserID
            // D1	:	角色的GlobleID
            // D2	:	道具的ID
            [Description("道具法宝融化消耗掉")]
            OPERA_LOG_FABAO_RONGHUA_ITEM = 10012,

            // 摆摊
            // pLogPara 参数
            // D0	:	摆摊者的UserID
            // D1	:	摆摊者的GlobleID
            // D2	:	顾客的UserID
            // D3	:	顾客的GlobleID
            // N4	:	顾客拿出钱的数量
            // D5	:	摆摊者拿出道具的ID
            [Description("摆摊")]
            OPERA_LOG_STALL_TRADE = 10013,

            // 修理道具
            // pLogPara 参数
            // D0	:	角色的UserID
            // D1	:	角色的GlobleID
            // N2	:	道具的数量
            // N3	:	钱
            // D4	:	道具的ID
            [Description("修理道具")]
            OPERA_LOG_MEND_ITEM = 10014,

            // 道具在地上自然消失
            // pLogPara 参数
            // D0	:	道具的ID
            // D1	:	场景ID
            [Description("道具在地上自然消失")]
            OPERA_LOG_DESTROY_ITEM = 10015,

            // 道具损坏
            // pLogPara 参数
            // D0	:	角色的UserID
            // D1	:	角色的GlobleID
            // D2	:	道具的ID
            [Description("道具损坏")]
            OPERA_LOG_WASTE_ITEM = 10016,

            //子道具损坏
            // pLogPara 参数
            // D0	:	角色的UserID
            // D1	:	角色的GlobleID
            // D2	:	镶嵌道具的ID
            // D3	:	宝石的ID
            [Description("子道具损坏")]
            OPERA_LOG_WASTE_SON_ITEM = 10017,


            //幻化道具
            // pLogPara 参数
            // D0	:	角色的UserID
            // D1	:	角色的GlobleID
            // D2	:	道具的ID
            [Description("幻化道具")]
            OPERA_LOG_COMPOUND_ITEM = 10018,

            //精炼道具
            // pLogPara 参数
            // D0	:	角色的UserID
            // D1	:	角色的GlobleID
            // D2	:	道具的ID
            [Description("精炼道具")]
            OPERA_LOG_UPDATE_ITEM = 10019,

            // 装备分解
            // pLogPara 参数
            // D0	:	角色的UserID
            // D1	:	角色的GlobleID
            // D2	:	道具的ID
            [Description("装备分解")]
            OPERA_LOG_RESOLVE_ITEM = 10020,

            //道具叠放删除
            // pLogPara 参数
            // D0	:	角色的UserID
            // D1	:	角色的GlobleID
            // D2	:	道具的ID
            [Description("道具叠放删除")]
            OPERA_LOG_SUPERPOSITION_ITEM = 10021,

            //道具法宝融合消耗掉
            // pLogPara 参数
            // D0	:	角色的UserID
            // D1	:	角色的GlobleID
            // D2	:	道具的ID
            [Description("道具法宝融合消耗掉")]
            OPERA_LOG_FABAO_RONGHE_ITEM = 10022,


            // 洪荒卷轴刷新星级消耗掉
            // pLogPara 参数
            // D0	:	角色的UserID
            // D1	:	角色的GlobleID
            // D2	:	道具的ID
            [Description("洪荒卷轴刷新星级消耗掉")]
            OPERA_LOG_DAILYTASK_LV_REFRE = 10023,

            //通过任务获得道具
            // pLogPara 参数
            // D0	:	角色的UserID
            // D1	:	角色的GlobleID
            // D2	:	道具的ID
            [Description("通过任务获得道具")]
            OPERA_LOG_TASK_GET_ITEM = 10024,

            //通过任务获得道具道具栏满扔在地上
            // pLogPara 参数
            // D0	:	角色的UserID
            // D1	:	角色的GlobleID
            // D2	:	道具的ID
            [Description("通过任务获得道具道具栏满扔在地上")]
            OPERA_LOG_TASK_GET_ITEM_TOGROUND = 10025,

            // 直接删除玩家道具
            // pLogPara 参数
            // D0	:	角色的UserID
            // D1	:	角色的GlobleID
            // D2	:	道具的ID
            // d3	:	数量
            [Description("直接删除玩家道具")]
            OPERA_LOG_DELETE_PLAYER_ITEM = 10026,

            // 添加任务记录
            // FIELD1 : 玩家UserID
            // FIELD2 : 玩家GlobalID
            // FIELD3 : 任务或活动ID
            // FIELD4 : 类型 ENUM_TASK_LOG_TYPE
            // OP_BAK : 任务当前值
            [Description("添加任务记录")]
            OPERA_LOG_ACCEPT_MISSION = 10027,
            // 完成任务记录 参数同 [Description("道具记录")]OPERA_LOG_ACCEPT_MISSION
            [Description("完成任务记录")]
            OPERA_LOG_COMPLETE_MISSION = 10028,
            // 删除任务记录 参数同 [Description("道具记录")]OPERA_LOG_ACCEPT_MISSION
            [Description("删除任务记录")]
            OPERA_LOG_DELETE_MISSION = 10029,
            // 放弃任务记录 参数同 [Description("道具记录")]OPERA_LOG_ACCEPT_MISSION
            [Description("放弃任务记录")]
            OPERA_LOG_DISCARD_MISSION = 10030,

            // 杀怪战斗结果
            // FIELD1 : 玩家UserID   或 怪的ExcelID
            // FIELD2 : 玩家GlobalID 或 无效
            // FIELD3 : 攻击者玩家UserID   或 攻击者怪的ExcelID
            // FIELD4 : 攻击者玩家GlobalID 或 无效
            // OP_BAK : 场景ID
            [Description("杀怪战斗结果")]
            OPERA_LOG_FIGHT_RESULT = 10031,

            // 法宝融合成功
            // FIELD1 : 玩家UserID   或 怪的ExcelID
            // FIELD2 : 玩家GlobalID 或 无效
            // FIELD3 : 攻击者玩家UserID   或 攻击者怪的ExcelID
            // FIELD4 : 攻击者玩家GlobalID 或 无效
            // OP_BAK : 场景ID
            [Description("法宝融合成功")]
            OPERA_LOG_FABAO_RONGHE_SUC = 10032,

            // 法宝融合升星
            // FIELD1 : 玩家UserID   或 怪的ExcelID
            // FIELD2 : 玩家GlobalID 或 无效
            // FIELD3 : 
            // FIELD4 : 
            // OP_BAK : 场景ID
            [Description("法宝融合升星")]
            OPERA_LOG_FABAO_STAR_LVUP = 10033,

            // 法宝激活成功
            // FIELD1 : 玩家UserID   或 怪的ExcelID
            // FIELD2 : 玩家GlobalID 或 无效
            // FIELD3 : 
            // FIELD4 : 
            // OP_BAK : 场景ID
            [Description("法宝激活成功")]
            OPERA_LOG_FABAO_ACTIVE_SUC = 10034,

            // 法宝经验的操作
            // FIELD1 : 玩家UserID   或 怪的ExcelID
            // FIELD2 : 玩家GlobalID 或 无效
            // FIELD3 : 
            // FIELD4 : 
            // OP_BAK : 场景ID
            [Description("法宝经验的操作")]
            OPERA_LOG_FABAO_EXP_OP = 10035,


            //从奇货居买东西
            // pLogPara 参数
            // D0	:	角色的UserID
            // D1	:	角色的GlobleID
            // D2	:	道具的ID
            [Description("从奇货居买东西")]
            OPERA_LOG_BUY_FROM_SYS_ITEM = 10036,

            //	获得金钱
            //	D0	:	获得类型	N1获得金钱数		D2			D3
            //	仓库操作								-			-
            //	丢弃或拾取								-			-
            //	任务奖励								任务类型	任务ID	[0-普通;1-排行;2-悬赏;3-跑环]
            //	创建帮派								-			-
            //	切换场景								当前场景	目标场景
            //	GM修改									GM GlobalID	-
            //	系统后台发奖等							奖励ID
            //	玩家间交易中的准备						对方GlobalID
            //	玩家间交易								对方GlobalID
            //	NPC交易									道具物品ID
            //	修理装备花费							道具ID
            [Description("获得金钱")]
            OPERA_LOG_CHANGE_MONEY = 10037,

            //	获得绑定金钱
            //	D0	:	获得类型	N1获得金钱数		D2			D3
            //	仓库操作								-			-
            //	丢弃或拾取								-			-
            //	任务奖励								任务类型	任务ID	[0-普通;1-排行;2-悬赏;3-跑环]
            //	创建帮派								-			-
            //	切换场景								当前场景	目标场景
            //	GM修改									GM GlobalID	-
            //	系统后台发奖等							奖励ID
            //	NPC交易									道具物品ID
            //	修理装备花费							道具ID
            [Description("获得绑定金钱")]
            OPERA_LOG_CHANGE_BIND_MONEY = 10038,

            //	金钱的流通
            //  只负责金钱的流通 详细信息 查看ENUM_CHANGE_MONEY_TYPE
            [Description("金钱的流通")]
            OPERA_LOG_CHANGE_MONEY_CIRCU = 10039,

            //	绑定金钱的流通
            //  只负责金钱的流通 详细信息 查看ENUM_CHANGE_MONEY_TYPE
            [Description("道具记录")]
            OPERA_LOG_CHANGE_BIND_MONEY_CIRCU = 10040,

            // 命魂经验日志
            // 信息见：ENUM_SOULPOINT_LOG_TYPE
            [Description("绑定金钱的流通")]
            OPERA_LOG_CHANGE_SOULPOINT = 10041,


            //　作任务记录ｉＤ  统一到 [Description("道具记录")]OPERA_LOG_GETEXP
            // pLogPara 参数
            // D0	:	角色的UserID
            // D1	:	角色的GlobleID
            // D2	:	经验
            // D3	:	任务ｉｄ
            //[Description("作任务记录")]OPERA_LOG_EXP_MISSION_ID			= 10050,

            //　作任务NPC ｉＤ ---废弃统一用 [Description("道具记录")]OPERA_LOG_GETEXP
            // pLogPara 参数
            // D0	:	角色的UserID
            // D1	:	角色的GlobleID
            // D2	:	经验
            // D3	:	任务ｉｄ
            //[Description("道具记录")]OPERA_LOG_EXP_NPC_ID				= 10051,


            // 龙珠数量的改变
            //	D0	:	获得类型	N1获得金钱数		D2			D3
            //	开宝箱所得								-			-
            [Description("龙珠数量的改变")]
            OPERA_LOG_TAKE_CHANGE_LONGPEARL = 10052,

            // 威望数量的改变
            //	D0	:	获得类型	N1获得数量	
            [Description("威望数量的改变")]
            OPERA_LOG_TAKE_CHANGE_PRESTIGE = 10053,

            // 道具的改变
            //	D0	:	获得类型	N1获得数量	
            [Description("道具的改变")]
            OPERA_LOG_TAKE_CHANGE_ITEM = 10054,

            // add by cyh
            // 通宝的改变 ENUM_CHANGE_GOLD_TYPE
            // ENUM_CHANGE_GOLD_TYPE	
            [Description("通宝的改变")]
            OPERA_LOG_CHANGE_GOLD = 10055,

            //绑定通宝的改变
            [Description("绑定通宝的改变")]
            OPERA_LOG_CHANGE_BINDGOLD = 10056,

            //战场积分的改变
            [Description("战场积分的改变")]
            OPERA_LOG_CHANGE_BATTLE_SCORE = 10057,

            // 添加悬赏任务记录
            // FIELD1 : 玩家UserID
            // FIELD2 : 玩家GlobalID
            // FIELD3 : 任务或活动ID
            // FIELD4 : 类型 ENUM_TASK_LOG_TYPE
            // OP_BAK : dwPara1悬赏任务类型 dwPara2消耗精力值
            [Description("添加悬赏任务记录")]
            OPERA_LOG_ACCEPT_DALIY_TASK = 10058,
            // 完成悬赏任务记录 参数同 [Description("道具记录")]OPERA_LOG_ACCEPT_DALIY_TASK
            [Description("完成悬赏任务记录")]
            OPERA_LOG_COMPLETE_DALIY_TASK = 10059,
            // 删除悬赏任务记录 参数同 [Description("道具记录")]OPERA_LOG_ACCEPT_DALIY_TASK
            [Description("删除悬赏任务记录")]
            OPERA_LOG_DELETE_DALIY_TASK = 10060,
            // 放弃悬赏任务记录 参数同 [Description("道具记录")]OPERA_LOG_ACCEPT_DALIY_TASK
            [Description("放弃悬赏任务记录")]
            OPERA_LOG_DISCARD_DALIY_TASK = 10061,
            // 直接完成悬赏任务记录 参数同 [Description("道具记录")]OPERA_LOG_ACCEPT_DALIY_TASK
            [Description("直接完成悬赏任务记录")]
            OPERA_LOG_IMM_COM_DALIY_TASK = 10062,

            // 悬赏任务刷新星级消耗记录 
            // FIELD1 : 玩家UserID
            // FIELD2 : 玩家GlobalID
            // FIELD3 : 任务或活动ID
            // FIELD4 : 消耗的道具的ExcelID
            // OP_BAK : dwPara1前一个星级 dwPara2后一个星级
            [Description("悬赏任务刷新星级消耗记录")]
            OPERA_LOG_REFRESH_LV_DALIY_TASK = 10063,

            // 法宝段位升级操作
            // FIELD1 : 玩家UserID   或 怪的ExcelID
            // FIELD2 : 玩家GlobalID 或 无效
            // FIELD3 : 
            // FIELD4 : 
            // OP_BAK : 场景ID
            [Description("法宝段位升级操作")]
            OPERA_LOG_FABAO_LEVEL_UP = 10064,

            // 灵力转换即装备融合
            // FIELD1 : 玩家UserID   或 怪的ExcelID
            // FIELD2 : 玩家GlobalID 或 无效
            // FIELD3 : 
            // FIELD4 : 
            // OP_BAK : 场景ID
            [Description("灵力转换即装备融合")]
            OPERA_LOG_EQUIP_RONGHE = 10065,

            // 镖车升级
            // FIELD1 : 玩家UserID   或 怪的ExcelID
            // FIELD2 : 玩家GlobalID 或 无效
            // FIELD3 : 
            // FIELD4 : 
            // OP_BAK : 场景ID
            [Description("镖车升级")]
            OPERA_LOG_CAMION_LVUP = 10066,

            // 接受镖车任务成功
            // FIELD1 : 玩家UserID   或 怪的ExcelID
            // FIELD2 : 玩家GlobalID 或 无效
            // FIELD3 : 
            // FIELD4 : 
            // OP_BAK : 场景ID
            [Description("接受镖车任务成功")]
            OPERA_LOG_CAMION_ACCETP_SUC = 10067,

            // 接受镖车任务失败
            // FIELD1 : 玩家UserID   或 怪的ExcelID
            // FIELD2 : 玩家GlobalID 或 无效
            // FIELD3 : 
            // FIELD4 : 
            // OP_BAK : 场景ID
            [Description("接受镖车任务失败")]
            OPERA_LOG_CAMION_ACCETP_FAIL = 10068,

            // 镖车结束
            // FIELD1 : 玩家UserID   或 怪的ExcelID
            // FIELD2 : 玩家GlobalID 或 无效
            // FIELD3 : 
            // FIELD4 : 
            // OP_BAK : 场景ID
            [Description("镖车结束")]
            OPERA_LOG_CAMION_END = 10069,

            // 玩家间的金钱，通宝交易
            // szDes : 金钱\t通宝
            [Description("玩家间的金钱，通宝交易")]
            OPERA_LOG_PLAYER_TO_PLAYER_MONEY_TRADE = 10070,

            // 邮件操作
            // 参数：见ENUM_MAIL_LOG
            [Description("邮件操作")]
            OPERA_LOG_MAIL_OP = 10071,

            // 系统奖励
            // FIELD1 : 玩家UserID   或 怪的ExcelID
            // FIELD2 : 玩家GlobalID 或 无效
            // FIELD3 : 类型
            // FIELD4 : 道具ExlID
            // OP_BAK : 
            [Description("系统奖励")]
            OPERA_LOG_SYS_GIFT = 10072,


            //-------角色信息---------//
            // 角色升级
            // pLogPara 参数
            // D0	:	角色的UserID
            // D1	:	角色的GlobleID
            // D2	:	原级数
            // D3	:	现级数
            [Description("角色升级")]
            OPERA_LOG_PLAYER_LEVEL_UP = 20000,

            // 角色死亡
            // pLogPara 参数
            // D0	:	角色的UserID
            // D1	:	角色的GlobleID
            // D2	:	死亡场景ID
            // N3	:	死亡方式
            // N4	:	=1 (被玩家杀)			:	=2 (被NPC杀)	:	=-1 (其他)	
            // D5	:	凶手角色的UserID		:	NPC的ID			:	-1
            // D6	:	凶手角色的GlobleID		:	-1				:	-1
            [Description("角色死亡")]
            OPERA_LOG_PLAYER_DEATH = 20001,

            // 角色转职
            // pLogPara 参数
            // D0	:	角色的UserID
            // D1	:	角色的GlobleID
            // D2	:	源职业
            // D3	:	转后的职业
            [Description("角色转职")]
            OPERA_LOG_PLAYER_CHANGE_PRO = 20002,

            //// 角色建立队伍
            //// pLogPara 参数
            //// D0	:	角色的UserID
            //// D1	:	角色的GlobleID
            //// D2	:	队伍ID
            //
            [Description("角色建立队伍")]
            OPERA_LOG_PLAYER_CREATE_TEAM = 20002,

            // 角色加入队伍
            // pLogPara 参数
            // D0	:	角色的UserID
            // D1	:	角色的GlobleID
            // D2	:	队伍ID
            //
            [Description("角色加入队伍")]
            OPERA_LOG_PLAYER_ADD_IN_TEAM = 20003,

            // 角色离开队伍
            // pLogPara 参数
            // D0	:	角色的UserID
            // D1	:	角色的GlobleID
            // D2	:	队伍ID
            //
            [Description("角色离开队伍")]
            OPERA_LOG_PLAYER_LEAVE_TEAM = 20004,

            // 角色解散队伍
            // pLogPara 参数
            // D0	:	角色的UserID
            // D1	:	角色的GlobleID
            // D2	:	队伍ID
            //
            [Description("角色解散队伍")]
            OPERA_LOG_PLAYER_DESTROY_TEAM = 20005,

            // 角色建立国家
            // pLogPara 参数
            // D0	:	角色的UserID
            // D1	:	角色的GlobleID
            // D2	:	国家ID
            //
            [Description("角色建立国家")]
            OPERA_LOG_PLAYER_CREATE_COUNTRY = 20006,

            // 角色加入国家
            // pLogPara 参数
            // D0	:	角色的UserID
            // D1	:	角色的GlobleID
            // D2	:	国家ID
            //
            [Description("角色加入国家")]
            OPERA_LOG_PLAYER_ADD_IN_COUNTRY = 20007,

            // 角色离开国家
            // pLogPara 参数
            // D0	:	角色的UserID
            // D1	:	角色的GlobleID
            // D2	:	国家ID
            //
            [Description("角色离开国家")]
            OPERA_LOG_PLAYER_LEAVE_COUNTRY = 20008,

            // 角色解散国家
            // pLogPara 参数
            // D0	:	角色的UserID
            // D1	:	角色的GlobleID
            // D2	:	国家ID
            //
            [Description("角色解散国家")]
            OPERA_LOG_PLAYER_DESTROY_COUNTRY = 20009,

            // 交友
            // pLogPara 参数
            // D0	:	角色的UserID
            // D1	:	角色的GlobleID
            // D2	:	好友的UserID
            // D3	:	好友的GlobleID
            [Description("交友")]
            OPERA_LOG_PLAYER_ADD_FRIEND = 20010,

            // 取消交友
            // pLogPara 参数
            // D0	:	角色的UserID
            // D1	:	角色的GlobleID
            // D2	:	好友的UserID
            // D3	:	好友的GlobleID
            [Description("取消交友")]
            OPERA_LOG_PLAYER_DEL_FRIEND = 20011,

            // 宠物升级
            // pLogPara 参数
            // D0	:	宠物的的ExcelID
            // D1	:	主角的GlobleID
            // D2	:	原级数
            // D3	:	现级数
            [Description("宠物升级")]
            OPERA_LOG_SUMMON_LEVEL_UP = 20012,

            // 添加仇人
            [Description("添加仇人")]
            OPERA_LOG_PLAYER_ADD_ENEMY = 20013,

            // 宠物操作
            // 分两次记录，第一次为
            // 角色userid,globalid,宠物excelid,宠物uuid
            // 第二次记录宠物的类型

            // 孵化
            [Description("孵化")]
            OPERA_LOG_PET_BIRTH = 20014,

            // 领养
            [Description("领养")]
            OPERA_LOG_PET_ADOPT = 20015,

            // 丢弃
            [Description("丢弃")]
            OPERA_LOG_PET_DISCARD = 20016,

            // 删除
            [Description("删除")]
            OPERA_LOG_PET_DEL = 20017,

            // 观看
            [Description("观看")]
            OPERA_LOG_PET_VIEW = 20018,

            // 合体
            [Description("合体")]
            OPERA_LOG_PET_COMPOUND = 20019,

            // 解体
            [Description("解体")]
            OPERA_LOG_PET_BREAKUP = 20020,

            // 幻化
            [Description("幻化")]
            OPERA_LOG_PET_HUANHUA = 20021,

            // 训练
            [Description("训练")]
            OPERA_LOG_PET_TRAINNING = 20022,

            // 结束训练
            [Description("结束训练")]
            OPERA_LOG_PET_ENDTRAIN = 20023,
            // 宠物结束

            //创建帮派
            [Description("创建帮派")]
            OPERA_LOG_FACTION_CREATE = 30001,
            //解散帮派
            [Description("解散帮派")]
            OPERA_LOG_FACTION_DELETE = 30002,
            //申请加入帮派
            [Description("申请加入帮派")]
            OPERA_LOG_FACTION_APPLY_ADD = 30003,
            //批准加入帮派
            [Description("批准加入帮派")]
            OPERA_LOG_FACTION_CONFIRM_MEMBER = 30004,
            //删除成员
            [Description("删除成员")]
            OPERA_LOG_FACTION_DELETE_MEMBER = 30005,
            //离开帮派
            [Description("离开帮派")]
            OPERA_LOG_FACTION_LEAVE = 30006,
            //因为队员离开或删除队员，帮派被迫解散
            [Description("因为队员离开或删除队员，帮派被迫解散")]
            OPERA_LOG_FACTION_LEAVE_DELETE = 30007,

            //帮会更改荣誉值
            [Description("帮会更改荣誉值")]
            OPERA_LOG_FACTION_HOROR = 30008,


            //
            OPERA_LOG_FACTION_APPOINT_RANK = 30008,


            //特殊日志及重大日志
            //某人被杀
            [Description("某人被杀")]
            OPERA_LOG_PERSON_KILL = 40001,

            //获取金钱
            [Description("获取金钱")]
            OPERA_LOG_GET_REMNANT_MONEY = 40002,
            //获取金钱返回
            [Description("获取金钱返回")]
            OPERA_LOG_GET_REMNANT_MONEY_R = 40003,
            //从系统买东西
            [Description("从系统买东西")]
            OPERA_LOG_BUY_GOODS_FROM_SYS = 40004,
            //从系统买东西返回
            [Description("从系统买东西返回")]
            OPERA_LOG_BUY_GOODS_FROM_SYS_R = 40005,

            //玩家完成任务获得经验    统一到 [Description("道具记录")]OPERA_LOG_GETEXP
            //
            [Description("玩家完成任务获得经验")]
            OPERA_LOG_COMPLETEMISION_GETEXP = 40011,
            //杀死boss获得经验小类型0表示没有组队1组队2好友 
            [Description("杀死boss获得经验")]
            OPERA_LOG_KILLBOSS_GETEXP = 40012,



            //角色操作
            //升级
            //
            [Description("升级")]OPERA_LOG_UPDATE_GRADE			= 40051,
            //登陆
            //
            [Description("登陆")]OPERA_LOG_PLAYER_LOGIN			= 40052,
            ////退出
            //
            [Description("退出")]OPERA_LOG_PLAYER_LOGOUT			= 40053,

            //点卡交易
            //锁定点卡
            [Description("锁定点卡")]
            OPERA_LOG_LOCK_CARD_POINT = 40101,
            //取消锁定
            [Description("取消锁定")]
            OPERA_LOG_UNLOCK_CARD_POINT = 40102,

            //锁定点卡成功
            [Description("锁定点卡成功")]
            OPERA_LOG_LOCK_CARD_POINT_OK = 40103,
            //锁定定点卡失败
            [Description("锁定定点卡失败")]
            OPERA_LOG_LOCK_CARD_POINT_FAILD = 40104,

            //划点成功
            [Description("划点成功")]
            OPERA_LOG_DEDUCT_CARD_POINT_OK = 40105,
            //划点失败
            [Description("划点失败")]
            OPERA_LOG_DEDUCT_CARD_POINT_FAILD = 40106,

            //奖品
            //获得奖品
            [Description("获得奖品")]
            OPERA_LOG_GET_AWARD = 402101,
            //接受奖品
            [Description("接受奖品")]
            OPERA_LOG_ACCEPT_AWARD = 40202,

            //拒绝奖品
            [Description("拒绝奖品")]
            OPERA_LOG_REFUSE_AWARD = 40203,
            //获得奖品成功
            [Description("获得奖品成功")]
            OPERA_LOG_GET_AWARD_OK = 40204,

            //获得奖品失败
            [Description("获得奖品失败")]
            OPERA_LOG_GET_AWARD_FAILD = 40205,
            //中奖
            [Description("中奖")]
            OPERA_LOG_WIN_AWARD = 40206,


            //身上钱过多
            [Description("身上钱过多")]
            OPERA_LOG_MAX_MONEY = 50000,

            //身上钱改变太多
            [Description("身上钱改变太多")]
            OPERA_LOG_CHANGE_MAX_MONEY = 50001,

            // 家族相关
            // 创建家族成功
            // pLogPara 参数
            // D0	:	角色的GlobleID
            [Description("创建家族成功")]
            OPERA_LOG_CREATE_KINDRED = 50050,

            // 创建家族旗帜
            // pLogPara 参数
            // D0	:	角色的GlobleID
            [Description("创建家族旗帜")]
            OPERA_LOG_CREATE_ICON = 50051,

            // 修改家族旗帜
            // pLogPara 参数
            // D0	:	角色的GlobleID
            [Description("修改家族旗帜")]
            OPERA_LOG_MODIFY_ICON = 50052,

            // 删除家族成员
            // pLogPara 参数
            // D0	:	操作者GlobleID
            // D1   :   删除成员的GlobleID
            [Description("删除家族成员")]
            OPERA_LOG_DEL_MEMBER = 50053,

            // 添加家族成员
            // pLogPara 参数
            // D0	:	操作者的GlobleID
            // D1   :   添加成员的GlobleID
            [Description("添加家族成员")]
            OPERA_LOG_ADD_MEMBER = 50054,

            // 退出家族
            // pLogPara 参数
            // D0	:	操作者的GlobleID
            [Description("退出家族")]
            OPERA_LOG_QUIT_KINDRED = 50055,

            // 解散家族
            // pLogPara 参数
            // D0	:	操作者的GlobleID
            [Description("解散家族")]
            OPERA_LOG_DISCARD_KINDRED = 50056,

            // 创建家族失败,合理的
            // pLogPara 参数
            // D0	:	退出者的GlobleID
            // D1   :   从前台提交到返回前台的时间(秒)
            // D2   :   后台数据库操作的时间(秒)
            [Description("创建家族失败")]
            OPERA_LOG_CREATE_KINDRED_FAILED = 50057,

            // 创建家族失败,并且玩家掉线了,这个会导致多扣了玩家的费用
            // pLogPara 参数
            // D0	:	退出者的GlobleID
            // D1   :   从前台提交到返回前台的时间(秒)
            // D2   :   后台数据库操作的时间(秒)
            [Description("创建家族失败")]
            OPERA_LOG_CREATE_KINDRED_ERRO_FAILED = 50058,

            // 家族给活跃度记录
            // pLogPara 参数
            // D0	:	操作者GlobleID
            // N1   :   数量(是有符号整数)
            // N2   :   类型 NUMM_METHOD_GET_ACTIVITY
            // D3   :   成功还是失败(1成功,0失败)
            [Description("家族给活跃度记录")]
            OPERA_LOG_ADD_KINDRED_ACTIVITY = 50058,

            // 家族给威望记录
            // pLogPara 参数
            // D0	:	操作者GlobleID
            // N1   :   数量(是有符号整数)
            // N2   :   类型 NUMM_METHOD_GET_CACHET
            // D3   :   成功还是失败(1成功,0失败)
            [Description("家族给威望记录")]
            OPERA_LOG_ADD_KINDRED_CACHET = 50059,

            // 侍卫合成记录
            // pLogPara 参数
            // D0	:	操作者GlobleID
            // D1   :   主侍卫的ExcelID
            // D2   :   副侍卫的ExcelID
            // D3   :   成功/失败 (1成功,0失败)
            [Description("侍卫合成记录")]
            OPERA_LOG_HOUSECARL_OPTIMIZE = 50060,

            // 大喊聊天积分记录
            // pLogPara 参数
            // D0	:	操作者GlobleID
            // N1   :   改变的数量
            // D2   :   改变的类型(原因)
            // D3   :   原来的积分(LOW 32位)
            // D4   :   原来的积分(HIG 32位)

            [Description("大喊聊天积分记录")]
            OPERA_LOG_CHATSCORE_CHANGE = 50061,

            // 打开宝箱记录
            // pLogPara 参数
            // D0	:	操作者GlobleID
            // D1   :   宝箱ID
            // N2   :   第几次开箱
            [Description("打开宝箱记录")]
            OPERA_LOG_OPEN_BOX = 50062,

            // 领取开启宝箱的奖品记录
            // pLogPara 参数
            // D0   :   操作者GlobleID
            // D1   :   宝箱ID
            // N2   :   第几次开箱
            // D3   :   奖品ID
            // N4   :   特殊道具(经验 金钱 龙珠)时数量
            // N5   :   领取成功(1)或失败(0)
            [Description("领取开启宝箱的奖品记录")]
            OPERA_LOG_TAKE_BOX_GIFT = 50063,

            //向数据库请求操作绑定点数
            // D0   :   操作者GlobleID
            // D1   :   现有的或者要操作的点数
            [Description("向数据库请求操作绑定点数")]
            OPERA_LOG_BINDPOINT = 50064,

            //记录超过某个值给经验
            // D0   :   操作者GlobleID
            // D1   :   要增加的经验数
            // D2   :   得经验的类型
            [Description("记录超过某个值给经验")]
            OPERA_LOG_GETEXP = 50065,
            //对进入过副本的玩家记录
            // D0   :   操作者GlobleID
            // D1   :   要进入的副本id
            [Description("对进入过副本的玩家记录")]
            OPERA_LOG_ENTER_FB = 50066,

            // 侍卫升星记录
            // pLogPara 参数
            // D0	:	操作者GlobleID
            // D1   :   侍卫的ExcelID
            // N2   :   侍卫升星后等级
            // N3   :   成功/失败 (1成功,0失败)
            [Description("侍卫升星记录")]
            OPERA_LOG_HOUSECARL_LVUP = 50067,

            // 玩家添加扩展仓库
            // D0   :   操作者GlobleID
            // D1   :   要增加第几个扩展仓库
            [Description("玩家添加扩展仓库")]
            OPERA_LOG_ADD_DEPOT_BAG = 50068,

            // 法宝技能鉴定
            // FIELD1 : 
            // FIELD2 :
            // D0   :   
            // D1   :   要增加第几个扩展仓库
            [Description("法宝技能鉴定")]
            OPERA_LOG_FABAO_SKILL_ACTIVE = 50069,

            // 角色死亡时凋落的道具
            // FIELD1 : 玩家UserID
            // FIELD2 : 玩家GlobalID
            // FIELD3 : 掉落道具的ExelID 
            // FIELD4 : 场景号
            // OP_BAK : 道具GUID
            // 同上玩家掉落
            [Description("角色死亡时凋落的道具")]
            OPERA_LOG_PLAYER_DROP_ITEM = 50070,

            // 从NPC那里回购道具
            // pLogPara 参数
            // D0	:	角色的UserID
            // D1	:	角色的GlobleID
            // N2	:	道具的数量
            // N3	:	钱
            // D4	:	道具的ID
            [Description("从NPC那里回购道具")]
            OPERA_LOG_BUY_BACK_FROM_NPC = 50071,

            // pLogPara 参数
            // D0	:	角色的UserID
            // D1	:	角色的GlobleID
            // D2	:	道具的ID
            [Description("")]
            OPERA_LOG_RECOMPOUND_ITEM = 50072,

            // 脚本主动创建并扔出的道具
            // FIELD1 : 玩家UserID
            // FIELD2 : 玩家GlobalID
            // FIELD3 : 掉落道具的ExelID 
            // FIELD4 : 场景号
            // OP_BAK : 道具GUID
            // 同上玩家掉落
            [Description("脚本主动创建并扔出的道具")]
            OPERA_LOG_SCRIPT_DROP_ITEM = 50073,

            // 角色聊天的LOG记录
            // FIELD1 : 玩家UserID
            // FIELD2 : 玩家GlobalID
            // FIELD3 : 聊天类型  - CHAT_CHANNEL_ID
            // FIELD4 : 目标人
            // OP_BAK : 聊天内容
            [Description("角色聊天的LOG记录")]
            OP_LOG_PLAYER_CHAT = 50074,

            // 临时发奖系统
            [Description("临时发奖系统")]
            OP_LOG_SYS_GIVE_PRIZE = 50075,

            //封印等级解封玩家升级
            [Description("封印等级解封玩家升级")]
            OP_UNLOCK_LIMIT_LEVEL = 50076,

            OPERA_LOG_TYPE_END
        };

        public enum ENUM_PROFESSION_ID
        {
            // 花灵的四个角色的ID
            [Description("花灵")]
            ID_FEMALE_HUALING_1 = 0,
            [Description("花灵")]
            ID_FEMALE_HUALING_2 = 1,
            [Description("花灵")]
            ID_MALE_HUALING_1 = 2,
            [Description("花灵")]
            ID_MALE_HUALING_2 = 3,

            // 天师的四个角色的ID
            ID_MALE_TIANSHI_1 = 4,
            ID_MALE_TIANSHI_2 = 5,
            ID_FEMALE_TIANSHI_1 = 6,
            ID_FEMALE_TIANSHI_2 = 7,

            // 龙战士的四个角色
            ID_MALE_LONGZHANSHI_1 = 8,
            ID_MALE_LONGZHANSHI_2 = 9,
            ID_FEMALE_LONGZHANSHI_1 = 10,
            ID_FEMALE_LONGZHANSHI_2 = 11,

            // 射手的四个角色
            ID_MALE_SHESHOU_1 = 12,
            ID_MALE_SHESHOU_2 = 13,
            ID_FEMALE_SHESHOU_1 = 14,
            ID_FEMALE_SHESHOU_2 = 15,

            // 浪人的四个角色
            ID_MALE_LANGREN_1 = 16,
            ID_MALE_LANGREN_2 = 17,
            ID_FEMALE_LANGREN_1 = 18,
            ID_FEMALE_LANGREN_2 = 19,

            // 潜行者的四个角色
            ID_MALE_QIANXINGZHE_1 = 20,
            ID_MALE_QIANXINGZHE_2 = 21,
            ID_FEMALE_QIANXINGZHE_1 = 22,
            ID_FEMALE_QIANXINGZHE_2 = 23,

            // 祭祀的四个角色
            ID_MALE_JISI_1 = 24,
            ID_MALE_JISI_2 = 25,
            ID_FEMALE_JISI_1 = 26,
            ID_FEMALE_JISI_2 = 27,

            // 僧侣的四个角色
            ID_MALE_SENGLV_1 = 28,
            ID_MALE_SENGLV_2 = 29,
            ID_FEMALE_SENGLV_1 = 30,
            ID_FEMALE_SENGLV_2 = 31,

            ID_PROFESSION_END = ID_FEMALE_SENGLV_2,
        };

        public enum ENUM_PROFESSION_ID_L
        {
            [Description("花灵")]
            ID_FEMALE_HUALING_1 = 0,
            [Description("天师")]
            ID_MALE_TIANSHI_1 = 1,
            [Description("浪人")]
            ID_MALE_LONGZHANSHI_1 = 2,
            [Description("龙胆")]
            ID_MALE_SHESHOU_1 = 3,
            [Description("巧工")]
            ID_MALE_LANGREN_1 = 4,
            [Description("虎贲")]
            ID_MALE_QIANXINGZHE_1 = 5,
            [Description("行者")]
            ID_MALE_JISI_1 = 6,
            [Description("斗仙")]
            ID_MALE_SENGLV_1 = 7,
        };

        //得到枚举列表
        public static List<EnumItem> GetEnumList(Type enumType, bool withAll)
        {
            List<EnumItem> list = new List<EnumItem>();
            if (!enumType.IsEnum)
            {
                throw new InvalidOperationException();
            }
            if (withAll)
            {
                list.Add(new EnumItem("全部选项", null));
            }
            Type typDescription = typeof(DescriptionAttribute);
            System.Reflection.FieldInfo[] fields = enumType.GetFields();
            foreach (System.Reflection.FieldInfo field in fields)
            {
                if (!field.FieldType.IsEnum)
                {
                    continue;
                }
                //通过字段名得到枚举值
                int value = (int)enumType.InvokeMember(field.Name, System.Reflection.BindingFlags.GetField, null, null, null);
                string text = string.Empty;
                //获取字段特性
                object[] arr = field.GetCustomAttributes(typDescription, true);
                if (arr.Length > 0)
                {
                    DescriptionAttribute da = (DescriptionAttribute)arr[0];
                    text = da.Description;
                }
                else
                {
                    text = field.Name;
                }
                list.Add(new EnumItem(text, value));
            }

            return list;
        }

        //得到枚举名字
        public static string GetEnumName(Type enumType, string enumValue)
        {
            List<EnumItem> list = new List<EnumItem>();
            if (!enumType.IsEnum)
            {
                throw new InvalidOperationException();
            }

            Type typDescription = typeof(DescriptionAttribute);
            System.Reflection.FieldInfo[] fields = enumType.GetFields();
            System.Reflection.FieldInfo field = enumType.GetField(enumValue);
            int value = (int)enumType.InvokeMember(field.Name, System.Reflection.BindingFlags.GetField, null, null, null);
            string text = string.Empty;
            //获取字段特性
            object[] arr = field.GetCustomAttributes(typDescription, true);
            if (arr.Length > 0)
            {
                DescriptionAttribute da = (DescriptionAttribute)arr[0];
                text = da.Description;
            }
            else
            {
                text = field.Name;
            }

            return text;

        }

        public static string GetCType(object cid)
        {
            try
            {
                return Common.EnumWSS.GetEnumName(typeof(Common.EnumWSS.ENUM_GMCMD_LOG_TYPE), ((Common.EnumWSS.ENUM_GMCMD_LOG_TYPE)Convert.ToInt32(cid)).ToString());
            }
            catch (System.Exception ex)
            {
                return cid.ToString();
            }
        }

        public static string GetOPName(object cid)
        {
            try
            {
               return Common.EnumWSS.GetEnumName(typeof(Common.EnumWSS.ENUM_OPERA_LOG_TYPE), ((Common.EnumWSS.ENUM_OPERA_LOG_TYPE)Convert.ToInt32(cid)).ToString());
            }
            catch (System.Exception ex)
            {
                return cid.ToString();
            }
        }

        public static string GetProNameL(object cid)
        {
            try
            {
                return Common.EnumWSS.GetEnumName(typeof(Common.EnumWSS.ENUM_PROFESSION_ID_L), ((Common.EnumWSS.ENUM_PROFESSION_ID_L)Convert.ToInt32(cid)).ToString());
            }
            catch (System.Exception ex)
            {
                return cid.ToString();
            }
        }
    }

    //枚举名字和值
    public class EnumItem
    {
        private string _Ename;

        public string Ename
        {
            get { return _Ename; }
            set { _Ename = value; }
        }
        private int? _Evalue;

        public int? Evalue
        {
            get { return _Evalue; }
            set { _Evalue = value; }
        }
        public EnumItem(string name, int? value)
        {
            _Ename = name;
            _Evalue = value;
        }

    }
}
