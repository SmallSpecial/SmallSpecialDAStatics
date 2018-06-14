/// <reference path="D:\TFS_WORKING\XXCQ-Dev\CodeAndDoc\GameServicePlat\code\WSS\WebWSS\Stats/UserManage/UserEmail.html" />
var moneyStatics = {
    soliverSum: [
        { text: '邮件银币总数', param: 2 },
        { text: '玩家拾取总数', param: 4 },
        { text: '任务奖励总计', param: 5 },
        { text: '悬赏任务总计', param: 29 },
        { text: '灵数猎手总计', param: 48 },
        { text: '帮会创建总计', param: 6 },
        { text: '修理装备总计', param: 11 },
        { text: '法宝升星总计', param: 14 },
        { text: '装备精炼总计', param: 18 },
        { text: '悬赏任务扣除总计', param: 22 },
        { text: '玩家邮资总计', param: 28 },
        { text: '拍卖行手续费总计', param: 31 },
        { text: '直接完成洪荒任务总计', param: 32 },
        { text: '刷新洪荒任务总计', param: 33 },
        { text: '刷新洪荒星级总计', param: 34 },
        { text: '帮会捐献总计', param: 41 },
        { text: '精炼转移总计', param: 44 },
        { text: '法宝技能刷新总计', param: 45 },
        { text: '装备铭刻总计', param: 52 },
        { text: '修改转换联盟总计', param: 57 },
        { text: '坐骑繁殖消耗总计', param: 58 },
        { text: '坐骑刷星消耗总计', param: 59 },
        { text: '坐骑刷新资质总计', param: 60 },
        { text: '坐骑先天悟道总计', param: 61 },
         { text: '坐骑后天悟道总计', param:62 },
        { text: '交易行卖银币总计', param: 49 },
        { text: '元宝购买手续费总计', param: 51 }
    ],
    soliverRank: [
       { text: '玩家拾取排行', param: 4 },
       { text: '任务奖励排行', param: 5 },
       { text: '悬赏任务排行', param: 29 },
       { text: '灵数猎手排行', param: 48 },
       { text: '拍卖行购物排行', param: 3 },
       { text: '修理装备排行', param: 11 },
       { text: '法宝升星排行', param: 14 },
       { text: '装备精炼排行', param: 18 },
       { text: '悬赏任务扣除排行', param: 22 },
       { text: '玩家邮资排行', param: 28 },
       { text: '拍卖行手续费排行', param: 31 },
       { text: '直接完成洪荒任务排行', param: 32 },
       { text: '刷新洪荒任务排行', param: 33 },
       { text: '刷新洪荒星级排行', param: 34 },
       { text: '帮会捐献排行', param: 41 },
       { text: '精炼转移总计', param: 44 },
       { text: '法宝技能刷新排行', param: 45 },
       { text: '装备铭刻排行', param: 52 },
       { text: '修改转换联盟排行', param: 57 },
       { text: '坐骑繁殖消耗排行', param: 58 },
       { text: '坐骑刷星消耗排行', param: 59 },
       { text: '坐骑刷新资质排行', param: 60 },
       { text: '坐骑先天悟道排行', param: 61 },
       { text: '坐骑后天悟道排行', param: 62 },
       { text: '交易行卖银币排行', param: 49 },
       { text: '元宝购买手续费排行', param: 51 }
       //{ text: '银币交易获取次数排行', param: 1 },
       //{ text: '银币交易失去金额排行', param: 1, extendParam: '&order=desc' },
       //{ text: '银币交易获取金额排行', param: 51, extendParam: '&order=asc' },
       //{ text: '邮件收发银币次数排行', param: 51 },
       //{ text: '邮件获取银币次数排行', param: 51 },
       //{ text: '邮件获取银币金额排行', param: 51 },
       //{ text: '邮件发送银币金额排行', param: 51 },
       //{ text: '仓库存取银币次数排行', param: 51 },
       //{ text: '仓库取出银币金额排行', param: 51 },
       //{ text: '仓库存进银币金额排行', param: 51 }
   ]
};

var menu = [
    { id: '4', text: '用户', hidden: true },
    { id: '3', text: '角色', hidden: true },
    { id: '1', text: '道具', hidden: true },
    { id: '2', text: '金钱', hidden: true },
    { id: '5', text: '聊天', hidden: true },
    { id: '6', text: '任务', hidden: true },
    { id: '7', text: '商城', hidden: true },
    { id: '8', text: '推广', hidden: true },
    { id: '9', text: '问卷', hidden: true },
    { id: '10', text: '服务器', hidden: true },
    { id: '14', text: '详细信息', hidden: true },
    { id: '16', text: '游戏概览', hidden: false },
    { id: '17', text: '游戏收入', hidden: false },
    { id: '18', text: '虚拟币', hidden: false },
];




var tool = [
    { id: '11', text: '贵重物品掉落', hidden: false },
    { id: '12', text: '拍卖行统计' },
    { id: '13', text: '物品统计' },
    { id: '15', text: '拍卖行统计-New' }
];
var tool0menus = [
    { id: '1101',param: "Stats/ItemDropDay.aspx", text: '装备历史掉落' },
    { id: '1102',param: "Stats/ItemDropDayNow.aspx?isnow=1", text: '装备历史掉落(当天)' },
    { id: '1103',param: "Stats/StoneDropDay.aspx", text: '精炼石(宝石)' },
    { id: '1104',param: "Stats/ItemDropQuery.aspx", text: '掉落日志查询' },
    { id: '1105',param: "stats/ItemAttriAttack.aspx", text: '极品属性统计(攻击)' },
    { id: '1106',param: "stats/ItemAttriDefence.aspx", text: '极品属性统计(防御)' },
    { id: '1107',param: "Stats/ItemQueryRank.aspx", text: '道具次数统计*' },
    { id: '1108',param: "Stats/ItemQuery.aspx", text: '道具日志查询' },
    { id: '1109',param: "Stats/TradeQuery.aspx", text: '交易日志查询' },
    { id: '1110',param: "Stats/OtherQuery.aspx", text: '其它日志查询' }
];
var tool1menus = [
    { id: '1201', param: 'Stats/PublicSale.aspx', text: '拍卖行整体统计' },
    { id: '1202', param: 'stats/PublicSaleStar.aspx', text: '道具星级统计' },
    { id: '1203', param: 'stats/PublicSaleLevel.aspx', text: '道具等级统计' },
    { id: '1204', param: 'stats/PublicSaleJinglian.aspx', text: '道具精炼统计' },
    { id: '1205', param: 'stats/PublicSaleHuanhua.aspx', text: '道具幻化统计' },
    { id: '1206', param: 'stats/PublicSaleRankFight.aspx', text: '道具战斗力排行' },
    { id: '1207', param: 'Admin_NoPage.Aspx', text: '道具一口价排行',hidden:true },
    { id: '1208', param: 'Admin_NoPage.Aspx', text: '玩家售卖排行', hidden: true },
    { id: '1209', param: 'Stats/UserManage/StaticTemplate.aspx?grid=AuctionRoom&category=AuctionRoom&pre=<', text: '拍卖行详情统计[ing]', hidden: false },
];
var tool2menus = [
    { id: '1301', param: "Stats/Item/TypeTotal.aspx?opid=10047&p=15&order=asc&t={t}", text: 'NPC购买获取总计' },
    { id: '1302', param: "Stats/Item/TypeTotal.aspx?opid=10047&p=27&order=asc&t={t}", text: '主线任务获取总计' },
    { id: '1303', param: "Stats/Item/TypeTotal.aspx?opid=10047&p=2&order=asc&t={t}", text: '拾取物品获取总计' },
    { id: '1304', param: "Stats/Item/TypeTotal.aspx?opid=10047&p=34&order=asc&t={t}", text: '邮件邮寄获取总计' },
    { id: '1305', param: "Stats/Item/TypeTotal.aspx?opid=10048&p=20051&order=asc&t={t}", text: '角色死亡掉落总计' },
    { id: '1306', param: "Stats/Item/RoleRank.aspx?opid=10047&p=29&order=desc&t={t}", text: '商城购买获取排行' },
    { id: '1307', param: "Stats/Item/RoleRank.aspx?opid=10047&p=3&order=desc&t={t}", text: '镇神关奖励获取排行' },
    { id: '1308', param: "Stats/Item/RoleRank.aspx?opid=10047&p=6&order=desc&t={t}", text: '帮会领地奖励获取排行' },
    { id: '1309', param: "Stats/Item/RoleRank.aspx?opid=10047&p=7&order=desc&t={t}", text: '累计奖励获取排行' },
    { id: '1310', param: "Stats/Item/RoleRank.aspx?opid=10047&p=10&order=desc&t={t}", text: '英雄榜奖励获取排行' },
    { id: '1311', param: "Stats/Item/RoleRank.aspx?opid=10047&p=16&order=desc&t={t}", text: '活跃值奖励获取排行' },
    { id: '1312', param: "Stats/Item/RoleRank.aspx?opid=10047&p=17&order=desc&t={t}", text: '命魂盒奖励获取排行' },
    { id: '1313', param: "Stats/Item/RoleRank.aspx?opid=10047&p=23&order=desc&t={t}", text: '兑换奖励获取排行' },
    { id: '1314', param: "Stats/Item/RoleRank.aspx?opid=10047&p=26&order=desc&t={t}", text: '签到奖励获取排行' },
    { id: '1315', param: "Stats/Item/RoleRank.aspx?opid=10047&p=27&order=desc&t={t}", text: '任务奖励获取排行' },
    { id: '1316', param: "Stats/Item/RoleRank.aspx?opid=10047&p=28&order=desc&t={t}", text: '补偿奖励获取排行' },
    { id: '1317', param: "Stats/Item/RoleRank.aspx?opid=10048&p=36&order=desc&t={t}", text: '离线活动奖励获取排行' },
    { id: '1318', param: "Stats/Item/RoleRank_Trade_InCount.aspx?opid=10047&p2=31&order=desc&t={t}", text: '角色道具交易获取排行' },
    { id: '1319', param: "Stats/Item/RoleRank_In_TradeCount.aspx?opid=10047&p=34&order=desc&t={t}", text: '角色道具邮件获取排行' },
    { id: '1320', param: "Stats/Item/RoleRank.aspx?opid=10048&p=20032&order=desc&p2=433001,433003,433005,433007,433009,433011,433013,433015&t={t}", text: '精炼石消耗排行' },
    { id: '1321', param: "Stats/Item/RoleRank.aspx?opid=10048&p=20032&order=desc&p2=433001&t={t}", text: '1品精炼石消耗排行' },
    { id: '1322', param: "Stats/Item/RoleRank.aspx?opid=10048&p=20032&order=desc&p2=433003&t={t}", text: '2品精炼石消耗排行' },
    { id: '1323', param: "Stats/Item/RoleRank.aspx?opid=10048&p=20032&order=desc&p2=433005&t={t}", text: '3品精炼石消耗排行' },
    { id: '1324', param: "Stats/Item/RoleRank.aspx?opid=10048&p=20032&order=desc&p2=433007&t={t}", text: '4品精炼石消耗排行' },
    { id: '1325', param: "Stats/Item/RoleRank.aspx?opid=10048&p=20032&order=desc&p2=433009&t={t}", text: '5品精炼石消耗排行' },
    { id: '1326', param: "Stats/Item/RoleRank.aspx?opid=10048&p=20032&order=desc&p2=433011&t={t}", text: '6品精炼石消耗排行' },
    { id: '1327', param: "Stats/Item/RoleRank.aspx?opid=10048&p=20032&order=desc&p2=433013&t={t}", text: '7品精炼石消耗排行' },
    { id: '1328', param: "Stats/Item/RoleRank.aspx?opid=10048&p=20032&order=desc&p2=433015&t={t}", text: '8品精炼石消耗排行' },
    { id: '1329', param: "Stats/Item/RoleRank.aspx?opid=10048&p=20053&order=asc&t={t}", text: '卖给NPC物品排行' },
    { id: '1330', param: "Stats/Item/PickUpItemRank.aspx?opid=10047&p=2&order=desc&p2=433001&t={t}", text: '1品精炼石拾取排行' },
    { id: '1331', param: "Stats/Item/PickUpItemRank.aspx?opid=10047&p=2&order=desc&p2=433003&t={t}", text: '2品精炼石拾取排行' },
    { id: '1332', param: "Stats/Item/PickUpItemRank.aspx?opid=10047&p=2&order=desc&p2=433005&t={t}", text: '3品精炼石拾取排行' },
    { id: '1333', param: "Stats/Item/PickUpItemRank.aspx?opid=10047&p=2&order=desc&p2=433007&t={t}", text: '4品精炼石拾取排行' },
    { id: '1334', param: "Stats/Item/PickUpItemRank.aspx?opid=10047&p=2&order=desc&p2=433009&t={t}", text: '5品精炼石拾取排行' },
    { id: '1335', param: "Stats/Item/PickUpItemRank.aspx?opid=10047&p=2&order=desc&p2=433011&t={t}", text: '6品精炼石拾取排行' },
    { id: '1336', param: "Stats/Item/PickUpItemRank.aspx?opid=10047&p=2&order=desc&p2=433013&t={t}", text: '7品精炼石拾取排行' },
    { id: '1337', param: "Stats/Item/PickUpItemRank.aspx?opid=10047&p=2&order=desc&p2=433015&t={t}", text: '8品精炼石拾取排行' },
    { id: '1338', param: "Stats/Item/PickUpItemRank.aspx?opid=10047&p=2&order=desc&p2=581200&t={t}", text: '四元精金拾取排行' },
    { id: '1339', param: "Stats/Item/PickUpItemRank.aspx?opid=10047&p=2&order=desc&p2=581054&t={t}", text: '佛骨菩提子拾取排行' },
    { id: '1340', param: "Stats/Item/PickUpItemRank.aspx?opid=10047&p=2&order=desc&p2=580117&t={t}", text: '铭刻石拾取排行' },
    { id: '1341', param: "Stats/Trade/RoleRank.aspx?opid=10002&order=desc&t={t}", text: '角色道具交易排行' },
    { id: '1342', param: "Stats/Item/ItemBusinessRank.aspx", text: '道具交易排行' },
    { id: '1343', param: "Stats/Other/RoleRank_Proficiency.aspx?opid=10019&order=asc&t={t}", text: '装备精炼排行' }
];
var tool3menus = [
    { id: '1501', param: "ModelPage/AuctionShop/SearchLog.aspx", text: '拍卖行搜索日志' },
    { id: '1502', param: "ModelPage/AuctionShop/PutawayLog.aspx?isnow=1", text: '拍卖行上架日志' },
    { id: '1503', param: "ModelPage/AuctionShop/SoldOutLog.aspx", text: '拍卖行下架日志' },
    { id: '1504', param: "ModelPage/AuctionShop/PurchaseLog.aspx", text: '拍卖行购买日志' }
];
var moneyCategory = {
    silver: { key: 10038, value: '银币' },
    gold: { key: 10037, value: '金币' }
};
var money = [{
    id: '210', text: '金钱统计'
}, {
    id: '220', text: '银币统计'
}, {
    id: '230', text: '银票统计'
}, {
    id: '240', text: '财富榜'
}];
var money0menus = [
    { id: '2101', text: '金币产出消耗',  param: "Stats/MoneyGoldOutIn.aspx" },
    { id: '2102', text: '银币产出消耗',  param: "Stats/MoneySilverOutIn.aspx" },
    { id: '2103', text: '金币分类消耗',  param: "Stats/MoneyGoldType.aspx" },
    { id: '2104', text: '金币分类产出',  param: "Stats/MoneyGoldTypeOut.aspx" },
    { id: '2106', text: '银币分类产消',  param: "Stats/MoneySilverType.aspx" },
    { id: '2105', text: '银币存有量',    param: "Stats/MoneySilverExist.aspx" },
    { id: '2107', text: '玩家产消排行-金币',param: "Stats/MoneyGoldRole.aspx" },
    { id: '2108', text: '玩家流通排行-金币[OPID = 10039]', param: "Stats/MoneyGoldRoleStream.aspx",hidden:true },
    { id: '2109', text: '任务银币排行[opid=10037 PARA_1 in(5,29)]', param: "Stats/MoneyRankTask.aspx", hidden: true },
    { id: '2110', text: '任务银币排行( 银票)[stop]', param: "Stats/MoneySilverRankTask.aspx", hidden: true }
];
var money2menus = [
    { id: '2301', param: "Stats/Money/TypeTotal.aspx?opid={gold}&p=3&order=asc&t={t}", text: '主线任务获取总计' },
    { id: '2302', param: "Stats/Money/TypeTotal.aspx?opid={gold}&p=14&order=asc&t={t}", text: '活动奖励获取总计' },
    { id: '2303', param: "Stats/Money/TypeTotal.aspx?opid={gold}&p=12&order=asc&t={t}", text: '修理装备消耗总计' },
    { id: '2304', param: "Stats/Money/TypeTotal.aspx?opid={gold}&p=6&order=asc&t={t}", text: '装备精炼消耗总计' },
    { id: '2305', param: "Stats/Money/TypeTotal.aspx?opid={gold}&p=19&order=asc&t={t}", text: '装备铭刻消耗总计' },
    { id: '2306', param: "Stats/Money/TypeTotal.aspx?opid={gold}&p=10&order=asc&t={t}", text: '法宝升星消耗总计' },
    { id: '2307', param: "Stats/Money/TypeTotal.aspx?opid={gold}&p=15&order=asc&t={t}", text: '精炼转移消耗总计' },
    { id: '2308', param: "Stats/Money/RoleRank.aspx?opid={gold}&p=5&order=desc&t={t}", text: 'NPC交易排行' },
    { id: '2309', param: "Stats/Money/RoleRank.aspx?opid={gold}&p=0&order=desc&t={t}", text: '玩家拾取排行' },
    { id: '2310', param: "Stats/Money/RoleRank.aspx?opid={gold}&p=12&order=asc&t={t}", text: '装备修理消耗排行' },
    { id: '2311', param: "Stats/Money/RoleRank.aspx?opid={gold}&p=6&order=asc&t={t}", text: '装备精练消耗排行' },
    { id: '2312', param: "Stats/Money/RoleRank.aspx?opid={gold}&p=19&order=asc&t={t}", text: '装备铭刻消耗排行' },
    { id: '2313', param: "Stats/Money/RoleRank.aspx?opid={gold}&p=10&order=asc&t={t}", text: '法宝升星消耗排行' },
    { id: '2314', param: "Stats/Money/RoleRank.aspx?opid={gold}&p=15&order=asc&t={t}", text: '精练转移消耗排行' }
];
var money3menus = [
    { id: '2401', param: "Stats/HonorMoneySRoleRank.aspx", text: '银币角色排行' },
    { id: '2402', param: "Admin_NoPage.Aspx", text: '金币周消耗排行', hidden: true },
    { id: '2403', param: "Admin_NoPage.Aspx", text: '金币总消耗排行', hidden: true }
];

var roleOnline = [
    { id: '30',  text: '在线统计' },
    { id: '31',  text: '角色统计' },
    { id: '32',  text: '流失统计' },
    { id: '33',  text: '公会统计' }
];
var roleOnline0menus = [
    { id: '3001', param: "Stats/RoleOnlineFlow_Zone.Aspx", text: '(服)每小时' },
    { id: '3002', param: "Stats/RoleOnlineFlowM_zone.aspx", text: '(服)每15分钟' },
    { id: '3003', param: "Stats/RoleOnlineFlow_ALL.Aspx", text: '(总)每小时' },
    { id: '3004', param: "Stats/RoleOnlineFlowM_All.aspx", text: '(总)每15分钟' }
];

var roleOnline1menus = [
    { id: '3101',  param: "Stats/RoleVocation.aspx", text: '角色总量统计' },
    { id: '3102',  param: "Stats/RoleVocationGrow.aspx", text: '角色增量统计' },
    { id: '3103',  param: "Stats/RoleLoginZone.aspx", text: '角色登陆总数统计' },
    { id: '3104',  param: "Stats/RoleOnlineLoginRank_Two.aspx", text: '角色登录次数排行' },
    { id: '3105',  param: "Stats/RoleOnlineLoginOutRank_Two.aspx", text: '角色登录出数排行' },
    { id: '3106',  param: "Stats/RoleVocationLevel.aspx", text: '职业等级统计' },
    { id: '3107',  param: "Stats/RoleVocationLevelGrow.aspx", text: '(职业等级统计)增量' },
    { id: '3108',  param: "Stats/RoleVocationLevelPara.aspx", text: '等级分段统计' },
    { id: '3109',  param: "Stats/RoleVocationLevelParaGrow.aspx", text: '(等级分段统计)增量' },
    { id: '3110',  param: "stats/RoleLevelTime.aspx", text: '等级耗时统计' },
    { id: '3111',  param: "Stats/RoleVocationLevelRank.aspx", text: '等级排行统计' },
    { id: '3112', param: "Stats/PlayerBattleRank.aspx", text: '攻击力排行统计' },
    { id: '3113',  param: "Stats/RoleVocationDefenceRank.aspx", text: '防御力排行统计' },
    { id: '3114',  param: "stats/RoleVocationPKHonorRank.aspx", text: 'PK荣誉排行统计' },
    { id: '3115',  param: "stats/Fight/RoleRank.aspx?opid=10031&p=0&order=desc&t={t}", text: '角色死亡次数排行' },
    { id: '3116',  param: "stats/Fight/RoleRank.aspx?opid=10031&p=1&order=desc&t={t}", text: '被玩家杀死次数排行' },
    { id: '3117', param: "stats/Fight/RoleRank.aspx?opid=10031&p=2&order=desc&t={t}", text: '被NPC杀死次数排行' },
    { id: '3118', param: "stats/GodRank.aspx", text: '神之试炼排行榜' },
    { id: '3119', param: "stats/WeekKillRank.aspx", text: '每周击杀排行榜' },
    { id: '3120', param: "stats/WeekExploitRank.aspx", text: '每周功勋排行榜' },
    { id: '3121', param: "stats/CampExploitRank.aspx", text: '阵营功勋排行榜' },
    { id: '3122', param: "stats/CorpsbattleRank.aspx", text: '公会战力排行榜' },
];
var roleOnline2menus = [
    { id: '3201',param: "stats/RoleLoseTimeFirst.aspx", text: '在线时长流失统计' },
    { id: '3202', param: "stats/RoleLoseTimeAll.aspx", text: '累计时长流失统计' },
    { id: '3203', param: "Stats/RoleLoseLevelPara.aspx", text: '等级段流失统计' },
    { id: '3204', param: "Stats/RoleLoseLevel.aspx", text: '每等级流失统计' },
    { id: '3205', param: "Stats/RoleLoseLevelCreate.aspx", text: '每等级创建流失统计' }
];
var roleOnline3menus = [
    { id: '3301', param: "stats/CorpsRankHonor.aspx", text: '公会繁荣度排行' },
    { id: '3302', param: "stats/CorpsRankLevel.aspx", text: '公会等级排行' },
    { id: '3303', param: "stats/CorpsRankUCount.aspx", text: '公会人数排行' }
];
var user = [
    { id: '41', text: ' 用户统计' },
    { id: '42', text: 'GM统计', hidden: false },
    { id: '43', text: 'Releact Data', hidden: false }
];
var  user0menus = [
    { id: '4101',param: "Stats/UserLoginZone.aspx", text: '用户登陆情况统计' },
    { id: '4102',param: "Stats/UserLoginArea.aspx", text: '登陆区域分布统计' },
    { id: '4103',param: "Stats/UserStateActive.aspx", text: '用户活跃统计' },
    { id: '4104',param: "Stats/UserStateLose.aspx", text: '用户流失统计' },
    { id: '4105',param: "Stats/UserStateBack.aspx", text: '用户回归统计' },
    { id: '4106',param: "Stats/UserLoginTimePara.aspx", text: '在线时长分布统计' },
    { id: '4107',param: "Stats/RoleOnlineLoginRank.aspx", text: '用户登录次数排行' },
    { id: '4108', param: "Stats/RoleOnlineIPRank.aspx", text: 'IP登录次数排行' },
    { id: '4109', param: "Stats/UserRemain.aspx", text: '用户留存统计' },
    { id: '4110', param: "Stats/RoleRemain.aspx", text: '角色留存统计' },
];
var user1menus = [
    { id: '4201', param: "Admin_NoPage.Aspx", text: '用户统计', hidden: true },
    { id: '4202', param: "Stats/GMQuery.aspx", text: 'GM统计' },
    { id: '4203', param: 'Stats/UserManage/StaticTemplate.aspx?grid=GMUser&category=GMUser&hiddenTimeSpanTool=true', text: 'GM授权用户', hidden: false }//新功能 GM授权用户查询
];
var userEmail = [
    { id: '4301', param: "stats/UserManage/UserEmail.aspx", text: 'Emai', hidden: false },
    { id: '4302', param: "stats/UserManage/UserActiveMountStatics.aspx", text: '坐骑激活统计', hidden: false },
    { id: '4303', param: "stats/UserManage/UserMountLevelStatic.aspx", text: '激活坐骑等级统计', hidden: false },
    { id: '4304', param: "stats/UserManage/UserSocialContactStatics.aspx?grid=UserAddFriendLog", text: '用户好友', hidden: false }, //删除的好友与添加好友在同一页面统计
    { id: '4305', param: "stats/UserManage/UserSocialContactStatics.aspx?grid=Ememy", text: '黑名单', hidden: false },//黑名单
    { id: '4306', param: "stats/EmailLog.aspx", text: 'Email信息统计', hidden: false }
];
var chat = [
   { id: '51', text: ' 聊天相关'}
];
var chat0menus = [
    { id: '5101',  param: "stats/ChatTypeNumD.aspx", text: '聊天数量统计' },
    { id: '5102',  param: "stats/ChatRoleNumRank.aspx", text: '聊天数量排行(世界)' },
    { id: '5103',  param: "stats/ChatRoleRpeatRank.aspx", text: '重复聊天统计(世界)' },
    { id: '5104',  param: "stats/ChatQuery_Hours.aspx", text: '一小时聊天内容排行' },
    { id: '5105',  param: "Admin_NoPage.Aspx", text: '可疑聊天统计', hidden:true },
    { id: '5106',  param: "stats/ChatQuery.Aspx", text: '聊天内容查询' }
];
var task = [
    { id: '61', text: '任务统计', icon: 'icon-cog', url: '' }
];
var task0menus = [
    { id: '6101', param: "stats/TaskAcceptRank.aspx", text: '任务接受排行' },
    { id: '6102', param: "stats/TaskAcceptRankRT.aspx", text: '重复接受任务排行' },
    { id: '6103', param: "stats/TaskFinishRank.aspx", text: '任务完成排行' },
    { id: '6104', param: "stats/TaskFinishRankRT.aspx", text: '重复完成任务排行' },
    { id: '6105', param: "stats/TaskAcceptFinish.aspx", text: '任务完成比统计' },
    { id: '6106', param: "stats/Other/TypeTotal.aspx?opid=50066&order=desc&t={t}", text: '活动完成情况统计' },
    { id: '6107', param: "stats/TaskBugList.aspx", text: '问题任务列表' },
    { id: '6108', param: "stats/TaskQuery.aspx", text: '任务日志查询' }
];
var mark = [
    { id: '71', text: '充值统计' },
    { id: '72', text: '商城统计' },
    { id: '73', text: '元宝统计' }
];
var mark0menus = [
    { id: '7101', param: "stats/UserAccountDepositRank_Money.aspx", text: '充值钱数排行*' },
    { id: '7102', param: "stats/UserAccountDepositRank_Time.aspx", text: '充值次数排行*' },
    { id: '7103', param: "stats/UserAccountDepositRank_All.aspx", text: '整体充值统计*' },
    { id: '7104', param: "stats/UserAccountInterzone.aspx", text: '充值区间统计' },
    { id: '7105', param: "stats/UserAccountQuery.aspx", text: '充值用户查询' },
    { id: '7106', param: "stats/UserAccountLogQuery.aspx", text: '充值记录查询' },
    { id: '7107', param: "Stats/RechargeLog.aspx", text: '充值信息统计' },
    { id: '7108', param: "Stats/RechargeByTimeLog.aspx", text: '充值信息按时间段统计' },
    { id: '7109', param: "Stats/RechargeRoleSumLog.aspx", text: '角色充值信息统计' },
    { id: '7110', param: "Stats/RechargeSumLog.aspx", text: '充值信息汇总统计' },
    { id: '7111', param: "Stats/DiamondLog.aspx", text: '货币信息统计' },
    { id: '7112', param: "Stats/MoneyLog.aspx", text: '道具信息统计' },
    { id: '7113', param: "Stats/GoldLog.aspx", text: '金币银币信息统计' },
    { id: '7114', param: "ModelPage/ShoppingMallLog/RedDiamondGiftShop.aspx", text: '红钻礼包商城信息统计' },
    { id: '7115', param: "ModelPage/ShoppingMallLog/GrowthFund.aspx", text: '成长基金信息统计' },
    { id: '7116', param: "ModelPage/ShoppingMallLog/ShopLog.aspx", text: '商店信息统计' },
];
var mark1menus = [
    { id: '7201', param: "stats/ShopSale.aspx", text: '商城销售统计(总)(服)' },
    { id: '7202', param: "stats/ShopSale_DayALL.aspx", text: '商城销售统计(总)(天)' },
    { id: '7203', param: "stats/ShopSale_Day.aspx", text: '商城销售统计(天)' },
    { id: '7204', param: "stats/ShopSale_Day_01.aspx", text: '商城交易笔数统计(天)' },
    { id: '7205', param: "stats/ShopSale_Day_02.aspx", text: '商城消费帐号数统计(天)' },
    { id: '7206', param: "stats/ShopSale_Day_03.aspx", text: '商城消费元宝数统计(天)' },
    { id: '7207', param: "stats/ShopSaleItem_Goods.aspx", text: '商品销售统计' },
    { id: '7208', param: "stats/ShopSaleItem_Goods_Day_GoodsCount.aspx", text: '商品销售数量统计(天)' },
    { id: '7209', param: "stats/ShopSaleItem_Goods_Day_MoneyCount.aspx", text: '商品销售元宝统计(天)' },
    { id: '7210', param: "stats/ShopSaleItem_Goods_Day_TradeCount.aspx", text: '商品销售笔数统计(天)' },
    { id: '7211', param: "stats/ShopSaleItem_Items.aspx", text: '道具销售统计' },
    { id: '7212', param: "stats/ShopSaleRoleLevel.aspx", text: '玩家购买统计' },
    { id: '7213', param: "stats/ShopSaleItem_ExcelLevel.aspx?shoptype=0&title={t}", text: '物品等级消耗统计' },
    { id: '7214', param: "stats/ShopSaleItem_RoleIDCount.aspx?shoptype=0&title={t}", text: '商品购买排行' },
    { id: '7215', param: "stats/ShopSaleCount.aspx?shoptype=0&title={t}", text: '购买次数排行[lose table]' },
    { id: '7216', param: "stats/ShopSaleB.aspx", text: '商城销售统计 绑.总.服' },
    { id: '7217', param: "stats/ShopSaleB_DayALL.aspx", text: '商城销售统计 绑.总.天' },
    { id: '7218', param: "stats/ShopSaleB_Day.aspx", text: '商城销售统计(绑)(天)' },
    { id: '7219', param: "stats/ShopSaleItem_GoodsB.aspx", text: '商品销售统计(绑)' },
    { id: '7220', param: "stats/ShopSaleItem_ItemsB.aspx", text: '道具销售统计(绑)' },
    { id: '7221', param: "stats/ShopSaleRoleLevelB.aspx", text: '玩家购买统计(绑)' },
    { id: '7222', param: "stats/ShopSaleItem_ExcelLevel.aspx?shoptype=1&title={t}", text: '物品等级消耗统计(绑)' },
    { id: '7223', param: "stats/ShopSaleItem_RoleIDCount.aspx?shoptype=1&title={t}", text: '商品购买排行(绑)' },
    { id: '7224', param: "stats/ShopSaleCount.aspx?shoptype=1&title={t}", text: '购买次数排行(绑)[lose table]' },
    { id: '7225', param: "stats/ShopQuery.aspx", text: '商城日志查询' },
    { id: '7226', param: "stats/ShopSaleItem_ExcelIDCount.aspx?shoptype=0&title={t}", text: '商城物品销售日志' },
    { id: '7227', param: "stats/ShopSaleItem_ExcelIDCount.aspx?shoptype=1&title={t}", text: '商城物品销售日志(绑)' },
    { id: '7228', param: "stats/AccountInterzoneShopSaleItems.aspx?shoptype=1&title={t}", text: '玩家消费习惯统计' },
    { id: '7229', param: "stats/AccountInterzoneShopSaleItemsB.aspx?shoptype=1&title={t}", text: '玩家消费习惯统计(绑定)' }
];

var mark2menus = [
    { id: '7301', param: "stats/UserAccountTradeAll.aspx", text: '整体提取统计*' },
    { id: '7302', param: "stats/Gold/RoleRank.aspx?opid=10055&p=0&order=asc&t={t}", text: '角色提取排行' },
    { id: '7303', param: "stats/Gold/RoleRank.aspx?opid=10055&p=fuzhi&order=asc&t={t}", text: '角色消耗排行' },
    { id: '7304', param: "stats/Gold/RoleRank.aspx?opid=10055&p=2&order=asc&t={t}", text: '角色购物排行' },
    { id: '7305', param: "stats/Gold/RoleRank.aspx?opid=10056&p=zhengzhi&order=asc&t={t}", text: '角色获取排行(绑定)' },
    { id: '7306', param: "stats/Gold/RoleRank.aspx?opid=10056&p=fuzhi&order=asc&t={t}", text: '角色消耗排行(绑定)' },
    { id: '7307', param: "stats/Gold/RoleRank.aspx?opid=10056&p=2&order=asc&t={t}", text: '角色购物排行(绑定)' },
    { id: '7308', param: "stats/GoldQuery.aspx", text: '元宝日志查询' },
    { id: '7309', param: "stats/PhysicalPowerQuery.aspx", text: '体力日志查询' }
];
var guess = [
    { id: '81', text: '推广统计' }
];
var guess0menus = [
    { id: '8101',  param: "stats/Count.aspx", text: '安装\卸载统计' },
    { id: '8102',  param: "stats/CountItem.aspx", text: '操作系统统计' },
    { id: '8103', param: "stats/CountItem.aspx?item=cast(F_ScreenWidth%20as%20varchar(10))%2B*%2Bcast(F_ScreenHeight%20as%20varchar(10))&title={t}", text: '系统分辨率统计' },
    { id: '8104',  param: "stats/CountItem.aspx?item=F_Area&title={t}", text: '所在地区统计' },
    { id: '8105', param: "stats/CountItem.aspx?item=F_Language&title={t}", text: '系统语言统计' },
    { id: '8106', param: "stats/CountItem.aspx?item=F_WinBit&title={t}", text: '系统架构统计' },
    { id: '8107',  param: "stats/MediaBaiduIndex.aspx", text: '百度指数' },
    { id: '8108',  param: "stats/MediaWeiboZone.aspx", text: '微博转发统计' },
    { id: '8109',  param: "stats/MediaWeiboQuery.aspx", text: '微博转发查询' }
];
var questionService = [
    { id: '91', text: '问卷调查' }
];
var questionService0menus = [
    { id: '9101', param: "/Admin_IFrame.aspx?src=stats/QuestSex.aspx?itemid=1", text: '男女比例' },
    { id: '9102', param: "/Admin_IFrame.aspx?src=stats/QuestSex.aspx?itemid=2", text: '年龄分布' },
    { id: '9103', param: "/Admin_IFrame.aspx?src=stats/QuestSex.aspx?itemid=3", text: '用户来源' },
    { id: '9104', param: "/Admin_IFrame.aspx?src=stats/QuestSex.aspx?itemid=4", text: '游戏类型' },
    { id: '9105', param: "/Admin_IFrame.aspx?src=stats/QuestSex.aspx?itemid=5", text: '上网游戏时间' },
    { id: '9106', param: "/Admin_IFrame.aspx?src=stats/QuestSex.aspx?itemid=6", text: '累计游戏时间' },
    { id: '9107', param: "/Admin_IFrame.aspx?src=stats/QuestSex.aspx?itemid=7", text: '游戏地址' },
    { id: '9108', param: "/Admin_IFrame.aspx?src=stats/QuestSex.aspx?itemid=8", text: '意愿消费' }
];
var service = [{ id: '101', text: '服务器统计' }];
var service0menus = [{ id: '10101', icon: 'icon-glass', url: "/Admin_IFrame.aspx?src=stats/ZoneInfo.aspx", text: '服务器基本状态统计' }];

var detailedInfo = [{ id: '141', text: '详细信息统计' }];
var detailedInfo0menus = [
    { id: '14101', param: "Stats/CampLog.aspx", text: '阵营信息统计' },
    { id: '14102', param: "Stats/CorpsLog.aspx", text: '公会信息统计' },
    { id: '14103', param: "Stats/ModelClothesLog.aspx", text: '时装信息统计' },

    { id: '14104', param: "ModelPage/PlayerInfoLog/EquipmentInfoLog.aspx", text: '装备信息统计' },
    { id: '14105', param: "ModelPage/PlayerInfoLog/MountInfoLog.aspx", text: '坐骑信息统计' },
    { id: '14106', param: "ModelPage/PlayerInfoLog/PetInfoLog.aspx", text: '宠物信息统计' },
    { id: '14107', param: "ModelPage/PlayerInfoLog/ShoppingMallLog.aspx", text: '商城信息统计' },
    { id: '14108', param: "ModelPage/PlayerInfoLog/ShopInfoLog.aspx", text: '商店信息统计' },
    { id: '14109', param: "ModelPage/PlayerInfoLog/ActivityInfoLog.aspx", text: '活动信息统计' },
    { id: '14110', param: "ModelPage/PlayerInfoLog/FriendGiveGift.aspx", text: '好友赠送礼物信息统计' },
    { id: '14111', param: "ModelPage/PlayerInfoLog/UserOnlineTimeAvg.aspx", text: '用户平均在线时长统计' },
    { id: '14112', param: "ModelPage/PlayerInfoLog/TenEvenDraw.aspx", text: '十连抽奖信息统计' },
];

//游戏概览
var gameOverView = [{ id: '161', text: '游戏概览统计' }];
var gameOverViewMenus = [
    { id: '16101', param: "ModelPage/GameOverView/GamePandect.aspx", text: '总览' },
    { id: '16102', text: '实时在线' },
    { id: '16103', param: "ModelPage/GameOverView/RealTimeNew.aspx", text: '实时新增' },
    { id: '16104', param: "ModelPage/GameOverView/RealTimeNew_Channel.aspx", text: '实时新增（渠道）' },
    { id: '16105', param: "ModelPage/GameOverView/RealTimeNew_Zone_Channel.aspx", text: '实时新增（区服/渠道）' },
    { id: '16106', param: "ModelPage/GameOverView/RealTimeRemain_Channel.aspx", text: '实时留存（渠道）' },
    { id: '16107', param: "ModelPage/GameOverView/RealTimeRemain_Zone_Channel.aspx", text: '实时留存（区服/渠道）' },
    { id: '16108', param: "ModelPage/GameOverView/Building.aspx", text: '实时留存（付费分类）' },
    { id: '16109', param: "ModelPage/GameOverView/RealTimeLogin.aspx", text: '实时登录' },
    { id: '16110', param: "ModelPage/GameOverView/RealTimeInCome.aspx", text: '实时收入' },
    { id: '16111', param: "ModelPage/GameOverView/RealTimeLoginNewPlayer.aspx", text: '实时登录（新玩家）' },
    { id: '16112', param: "ModelPage/GameOverView/Building.aspx", text: '数据汇总' },
];
//实时在线
var realTimeOnlineMenus = [
    { id: '1610201', param: "Stats/RoleOnlineFlow_Zone.Aspx", text: '(服)每小时' },
    { id: '1610202', param: "Stats/RoleOnlineFlowM_zone.aspx", text: '(服)每15分钟' },
    { id: '1610203', param: "Stats/RoleOnlineFlow_ALL.Aspx", text: '(总)每小时' },
    { id: '1610204', param: "Stats/RoleOnlineFlowM_All.aspx", text: '(总)每15分钟' }
];
//游戏收入
var gameInCome = [{ id: '171', text: '游戏收入统计' }];
var gameInComeMenus = [
    { id: '17101', param: "ModelPage/GameInCome/RechargeData.aspx", text: '充值数据' },
    { id: '17102', param: "ModelPage/GameInCome/RechargeRankList.aspx", text: '充值排行榜' },
    { id: '17103', param: "ModelPage/GameInCome/RechargeLevel.aspx", text: '付费等级' },
    { id: '17104', param: "ModelPage/GameOverView/Building.aspx", text: 'LTV' },
    { id: '17105', param: "ModelPage/GameInCome/RechargeRegion.aspx", text: '充值区间' },
    { id: '17106', param: "ModelPage/GameOverView/Building.aspx", text: '持续付费' },
    { id: '17107', param: "ModelPage/GameInCome/FirstChargeAnalyze.aspx", text: '首充分析' },
    { id: '17108', param: "ModelPage/GameInCome/RechargeRecord.aspx", text: '玩家充值记录' },
    { id: '17109', param: "ModelPage/GameOverView/Building.aspx", text: '新老用户充值（账号）' },
    { id: '17110', param: "ModelPage/GameOverView/Building.aspx", text: '新老用户充值（区服/渠道）' },
    { id: '17111', param: "ModelPage/GameInCome/RechargeFrequency.aspx", text: '充值频次' },
    { id: '17112', param: "ModelPage/GameOverView/Building.aspx", text: '历史充值情况' },
    { id: '17113', param: "ModelPage/GameOverView/Building.aspx", text: '用户付费率' },
];
//虚拟币
var gameMoney = [{ id: '181', text: '虚拟币统计' }];
var gameMoneyMenus = [
    { id: '18101', param: "ModelPage/GameOverView/Building.aspx", text: '商城购买' },
    { id: '18102', param: "ModelPage/GameOverView/Building.aspx", text: '钻石产生和消耗' },
    { id: '18103', param: "ModelPage/GameOverView/Building.aspx", text: '额外钻石产销途径' },
    { id: '18104', param: "ModelPage/GameOverView/Building.aspx", text: '虚拟币产销数量' },
    { id: '18105', param: "ModelPage/GameOverView/Building.aspx", text: '虚拟币产销途径' },
    { id: '18106', param: "ModelPage/GameOverView/Building.aspx", text: '绑钻产销途径' },
    { id: '18107', param: "ModelPage/GameOverView/Building.aspx", text: '商城购买（所有货币）' },
];

