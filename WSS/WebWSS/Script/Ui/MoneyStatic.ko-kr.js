var moneyStatics = {
    soliverSum: [
        { text: '메일 은화 총수', param: 2 },
        { text: '플레이어 줍다 총수', param: 4 },
        { text: '총 작업 상인', param: 5 },
        { text: '현상 작업을 합계', param: 29 },
        { text: '링 몇 사냥꾼 총', param: 48 },
        { text: '민간비밀 결사 만들기 합계', param: 6 },
        { text: '수리 장비 합계', param: 11 },
        { text: '보물 되 성 합계', param: 14 },
        { text: '총 장비 정련하다', param: 18 },
        { text: '현상 작업을 공제 합계', param: 22 },
        { text: '플레이어 우편 합계', param: 28 },
        { text: '경매점 수속비는 합계', param: 31 },
        { text: '직접 태고 임무를 완성할 합계', param: 32 },
        { text: '새로 고침 태고 임무를 합계', param: 33 },
        { text: '새로 고침 태고 성급 합계', param: 34 },
        { text: '민간비밀 결사 기부 합계', param: 41 },
        { text: '정련 이동 합계', param: 44 },
        { text: '또는 방법 기능 새로 고침 합계', param: 45 },
        { text: '총 장비 사무치다', param: 52 },
        { text: '수정 변환 연맹 합계', param: 57 },
        { text: '승마 번식 소모 합계', param: 58 },
        { text: '승마 솔 별 소모 합계', param: 59 },
        { text: '승마 새로 고침 자질 합계', param: 60 },
        { text: '승마 선천적으로 오도 합계', param: 61 },
         { text: '승마 모레 오도 합계', param: 62 },
        { text: '거래 돼 파는 은화 합계', param: 49 },
        { text: '원보 구매 수속비는 합계', param: 51 }
    ],
    soliverRank: [
       { text: '랭킹 플레이어 줍다', param: 4 },
       { text: '임무를 장려 순위', param: 5 },
       { text: '현상 작업을 순위', param: 29 },
       { text: '링 몇 사냥꾼 순위', param: 48 },
       { text: '경매점 구매 순위', param: 3 },
       { text: '수리 장비 순위', param: 11 },
       { text: '보물 되 성 순위', param: 14 },
       { text: '장비 정련하다 순위', param: 18 },
       { text: '현상 작업을 공제 순위', param: 22 },
       { text: '우편 랭킹 플레이어', param: 28 },
       { text: '경매점 수수료 순위', param: 31 },
       { text: '직접 태고 임무를 완성할 순위', param: 32 },
       { text: '새로 고침 태고 임무를 순위', param: 33 },
       { text: '새로 고침 태고 성급 순위', param: 34 },
       { text: '민간비밀 결사 기부 순위', param: 41 },
       { text: '정련 이동 합계', param: 44 },
       { text: '또는 방법 기능 새로 고침 순위', param: 45 },
       { text: '장비 깊이 순위', param: 52 },
       { text: '변환 연맹 순위 변경', param: 57 },
       { text: '승마 번식 소모 순위', param: 58 },
       { text: '승마 솔 별 소모 순위', param: 59 },
       { text: '승마 새로 고침 자질 순위', param: 60 },
       { text: '승마 선천적으로 오도 순위', param: 61 },
       { text: '승마 모레 오도 순위', param: 62 },
       { text: '거래 돼 파는 은화 순위', param: 49 },
       { text: '원보 구매 수수료 순위', param: 51 }

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
var moneyCategory = {
    silver: { key: 10038, value: '은' },
    gold: { key: 10037, value: '금' }
};
var menu = [
    { id: '4', text: '유저' },
    { id: '3', text: '캐릭터' },
    { id: '1', text: '도구', hidden: false },
    { id: '2', text: '금전', hidden: false },
    { id: '5', text: '채팅' },
    { id: '6', text: '퀘스트' },
    { id: '7', text: '상가', hidden: false },
    { id: '8', text: '보급', hidden: false },
    { id: '9', text: '설문', hidden: false },
    { id: '10', text: '서버' },
    { id: '14', text: '세부' }
];
var tool = [
    { id: '11', text: '귀중품은 떨어졌다'},
    { id: '12', text: '경매점 통계'},
    { id: '13', text: '물품 통계' },
    { id: '15', text: '경매점 통계-New' }
];
var tool0menus = [
    { id: '1101', param: "Stats/ItemDropDay.aspx", text: '장비 역사 떨어졌다' },
    { id: '1102', param: "Stats/ItemDropDayNow.aspx?isnow=1", text: '장비 역사 떨어지다 (당일)' },
    { id: '1103', param: "Stats/StoneDropDay.aspx", text: '정련 돌 (보석)' },
    { id: '1104', param: "Stats/ItemDropQuery.aspx", text: '로그 검색 떨어졌다' },
    { id: '1105', param: "stats/ItemAttriAttack.aspx", text: '상길의 속성 통계 (공격)' },
    { id: '1106', param: "stats/ItemAttriDefence.aspx", text: '상길의 속성 통계 (방어)' },
    { id: '1107', param: "Stats/ItemQueryRank.aspx", text: '도구 횟수 통계 *' },
    { id: '1108', param: "Stats/ItemQuery.aspx", text: '검색 도구 로그' },
    { id: '1109', param: "Stats/TradeQuery.aspx", text: '거래 로그 검색' },
    { id: '1110', param: "Stats/OtherQuery.aspx", text: '다른 로그 검색' }
];
var tool1menus = [
    { id: '1201',param: 'Stats/PublicSale.aspx', text: '경매점 전체 통계' },
    { id: '1202',param: 'stats/PublicSaleStar.aspx', text: '도구 성급 통계' },
    { id: '1203',param: 'stats/PublicSaleLevel.aspx', text: '도구 등급 통계' },
    { id: '1204',param: 'stats/PublicSaleJinglian.aspx', text: '도구 정련하다 통계' },
    { id: '1205',param: 'stats/PublicSaleHuanhua.aspx', text: '도구 죽다 통계' },
    { id: '1206',param: 'stats/PublicSaleRankFight.aspx', text: '도구 전투력을 순위' },
    { id: '1207',param: 'Admin_NoPage.Aspx', text: '도구 고정가는 순위' },
    { id: '1208',param: 'Admin_NoPage.Aspx', text: '플레이어 판매 순위' },
    { id: '1209', param: 'Stats/UserManage/StaticTemplate.aspx?grid=AuctionRoom&category=AuctionRoom', text: '경매점 자세한 통계[ing]', hidden: false },
];
var tool2menus = [
    { id: '1301', param: "Stats/Item/TypeTotal.aspx?opid=10047&p=15&order=asc&t={t}", text: 'NPC 총 구매 가져오기' },
    { id: '1302', param: "Stats/Item/TypeTotal.aspx?opid=10047&p=27&order=asc&t={t}", text: '메인 라인 작업 가져오기 합계' },
    { id: '1303', param: "Stats/Item/TypeTotal.aspx?opid=10047&p=2&order=asc&t={t}", text: '줍다 물건 가져오는 합계' },
    { id: '1304', param: "Stats/Item/TypeTotal.aspx?opid=10047&p=34&order=asc&t={t}", text: '전자 우편으로 가져오기 합계' },
    { id: '1305', param: "Stats/Item/TypeTotal.aspx?opid=10048&p=20051&order=asc&t={t}", text: '캐릭터 사망 총 떨어졌다' },
    { id: '1306', param: "Stats/Item/RoleRank.aspx?opid=10047&p=29&order=desc&t={t}", text: '상가 구매 순위 가져오기' },
    { id: '1307', param: "Stats/Item/RoleRank.aspx?opid=10047&p=3&order=desc&t={t}", text: '진 신 닫다 얻다 순위 상' },
    { id: '1308', param: "Stats/Item/RoleRank.aspx?opid=10047&p=6&order=desc&t={t}", text: '민간비밀 결사 영지 장려 가져오기 순위' },
    { id: '1309', param: "Stats/Item/RoleRank.aspx?opid=10047&p=7&order=desc&t={t}", text: '누계 장려 가져오기 순위' },
    { id: '1310', param: "Stats/Item/RoleRank.aspx?opid=10047&p=10&order=desc&t={t}", text: '영웅 목록 가져오기 순위 상' },
    { id: '1311', param: "Stats/Item/RoleRank.aspx?opid=10047&p=16&order=desc&t={t}", text: '활발하다 값 장려 가져오기 순위' },
    { id: '1312', param: "Stats/Item/RoleRank.aspx?opid=10047&p=17&order=desc&t={t}", text: '목숨을 혼 상자 상 가져오기 순위' },
    { id: '1313', param: "Stats/Item/RoleRank.aspx?opid=10047&p=23&order=desc&t={t}", text: '태환 장려 가져오기 순위' },
    { id: '1314', param: "Stats/Item/RoleRank.aspx?opid=10047&p=26&order=desc&t={t}", text: '로그인 보너스 가져오기 순위' },
    { id: '1315', param: "Stats/Item/RoleRank.aspx?opid=10047&p=27&order=desc&t={t}", text: '작업 가져오기 순위 상' },
    { id: '1316', param: "Stats/Item/RoleRank.aspx?opid=10047&p=28&order=desc&t={t}", text: '보상 장려 가져오기 순위' },
    { id: '1317', param: "Stats/Item/RoleRank.aspx?opid=10048&p=36&order=desc&t={t}", text: '오프라인 행사 가져오기 순위 상' },
    { id: '1318', param: "Stats/Item/RoleRank_Trade_InCount.aspx?opid=10047&p2=31&order=desc&t={t}", text: '캐릭터 아이템 거래 가져오기 순위' },
    { id: '1319', param: "Stats/Item/RoleRank_In_TradeCount.aspx?opid=10047&p=34&order=desc&t={t}", text: '캐릭터 아이템 메일 가져오기 순위' },
    { id: '1320', param: "Stats/Item/RoleRank.aspx?opid=10048&p=20032&order=desc&p2=433001,433003,433005,433007,433009,433011,433013,433015&t={t}", text: '정련 돌 소모 순위' },
    { id: '1321', param: "Stats/Item/RoleRank.aspx?opid=10048&p=20032&order=desc&p2=433001&t={t}", text: '1 품 정련하다 돌 소모 순위' },
    { id: '1322', param: "Stats/Item/RoleRank.aspx?opid=10048&p=20032&order=desc&p2=433003&t={t}", text: '2품 정련하다 돌 소모 순위' },
    { id: '1323', param: "Stats/Item/RoleRank.aspx?opid=10048&p=20032&order=desc&p2=433005&t={t}", text: '3품 정련하다 돌 소모 순위' },
    { id: '1324', param: "Stats/Item/RoleRank.aspx?opid=10048&p=20032&order=desc&p2=433007&t={t}", text: '4품 정련하다 돌 소모 순위' },
    { id: '1325', param: "Stats/Item/RoleRank.aspx?opid=10048&p=20032&order=desc&p2=433009&t={t}", text: '5품 정련하다 돌 소모 순위' },
    { id: '1326', param: "Stats/Item/RoleRank.aspx?opid=10048&p=20032&order=desc&p2=433011&t={t}", text: '6품 정련하다 돌 소모 순위' },
    { id: '1327', param: "Stats/Item/RoleRank.aspx?opid=10048&p=20032&order=desc&p2=433013&t={t}", text: '7품 정련하다 돌 소모 순위' },
    { id: '1328', param: "Stats/Item/RoleRank.aspx?opid=10048&p=20032&order=desc&p2=433015&t={t}", text: '8품 정련하다 돌 소모 순위' },
    { id: '1329', param: "Stats/Item/RoleRank.aspx?opid=10048&p=20053&order=asc&t={t}", text: '물건을 팔아 NPC 순위' },
    { id: '1330', param: "Stats/Item/PickUpItemRank.aspx?opid=10047&p=2&order=desc&p2=433001&t={t}", text: '1 품 정련하다 돌 줍다 순위' },
    { id: '1331', param: "Stats/Item/PickUpItemRank.aspx?opid=10047&p=2&order=desc&p2=433003&t={t}", text: '2품 정련하다 돌 줍다 순위' },
    { id: '1332', param: "Stats/Item/PickUpItemRank.aspx?opid=10047&p=2&order=desc&p2=433005&t={t}", text: '3품 정련하다 돌 줍다 순위' },
    { id: '1333', param: "Stats/Item/PickUpItemRank.aspx?opid=10047&p=2&order=desc&p2=433007&t={t}", text: '4품 정련하다 돌 줍다 순위' },
    { id: '1334', param: "Stats/Item/PickUpItemRank.aspx?opid=10047&p=2&order=desc&p2=433009&t={t}", text: '5품 정련하다 돌 줍다 순위' },
    { id: '1335', param: "Stats/Item/PickUpItemRank.aspx?opid=10047&p=2&order=desc&p2=433011&t={t}", text: '6품 정련하다 돌 줍다 순위' },
    { id: '1336', param: "Stats/Item/PickUpItemRank.aspx?opid=10047&p=2&order=desc&p2=433013&t={t}", text: '7품 정련하다 돌 줍다 순위' },
    { id: '1337', param: "Stats/Item/PickUpItemRank.aspx?opid=10047&p=2&order=desc&p2=433015&t={t}", text: '8품 정련하다 돌 줍다 순위' },
    { id: '1338', param: "Stats/Item/PickUpItemRank.aspx?opid=10047&p=2&order=desc&p2=581200&t={t}", text: '사차원 순금 줍다 순위' },
    { id: '1339', param: "Stats/Item/PickUpItemRank.aspx?opid=10047&p=2&order=desc&p2=581054&t={t}", text: '불사리 무환자나무 줍다 순위' },
    { id: '1340', param: "Stats/Item/PickUpItemRank.aspx?opid=10047&p=2&order=desc&p2=580117&t={t}", text: '문자를 금석에 새기다 돌 줍다 순위' },
    { id: '1341', param: "Stats/Trade/RoleRank.aspx?opid=10002&order=desc&t={t}", text: '캐릭터 아이템 교역 순위' },
    { id: '1342', param: "Stats/Item/ItemBusinessRank.aspx", text: '도구 교역 순위' },
    { id: '1343', param: "Stats/Other/RoleRank_Proficiency.aspx?opid=10019&order=asc&t={t}", text: '장비 정련하다 순위' }
];
var tool3menus = [
    { id: '1501', param: "ModelPage/AuctionShop/SearchLog.aspx", text: '거래소 조회 로그' },
    { id: '1502', param: "ModelPage/AuctionShop/PutawayLog.aspx?isnow=1", text: '거래소 등록 로그' },
    { id: '1503', param: "ModelPage/AuctionShop/SoldOutLog.aspx", text: '거래소 등록 취소 로그' },
    { id: '1504', param: "ModelPage/AuctionShop/PurchaseLog.aspx", text: '거래소 구매 로그' }
];
var money = [{
    id: '210', text: '금전 통계'
}, {
    id: '220', text: '은화 통계'
}, {
    id: '230', text: '어음, 통계'
}, {
    id: '240', text: '재산 리스트'
}];

var money0menus = [
    { id: '2101', text: '생산 소비 금화', param: "Stats/MoneyGoldOutIn.aspx" },
    { id: '2102', text: '생산 소비 은화', param: "Stats/MoneySilverOutIn.aspx" },
    { id: '2103', text: '금화 분류 소모', param: "Stats/MoneyGoldType.aspx" },
    { id: '2104', text: '금화 분류 생산해 내다', param: "Stats/MoneyGoldTypeOut.aspx" },
    { id: '2106', text: '은화 분류 재산을 없애다', param: "Stats/MoneySilverType.aspx" },
    { id: '2105', text: '은화 보유 재다', param: "Stats/MoneySilverExist.aspx" },
    { id: '2107', text: '플레이어 생산 사라지다 순위 - 금화', param: "Stats/MoneyGoldRole.aspx" },
    { id: '2108', text: '플레이어 유통 순위 - 금화[OPID = 10039]', param: "Stats/MoneyGoldRoleStream.aspx" },
    { id: '2109', text: '임무를 은화 순위[opid=10037 PARA_1 in(5,29)]', param: "Stats/MoneyRankTask.aspx" },
    { id: '2110', text: '임무를 은화 순위 (어음)[stop]', param: "Stats/MoneySilverRankTask.aspx" }
];

var money2menus = [
    { id: '2301', param: "Stats/Money/TypeTotal.aspx?opid={gold}&p=3&order=asc&t={t}", text: '메인 라인 작업 가져오기 합계' },
    { id: '2302', param: "Stats/Money/TypeTotal.aspx?opid={gold}&p=14&order=asc&t={t}", text: '총 활동 장려 가져오기' },
    { id: '2303', param: "Stats/Money/TypeTotal.aspx?opid={gold}&p=12&order=asc&t={t}", text: '수리 장비 소모 합계' },
    { id: '2304', param: "Stats/Money/TypeTotal.aspx?opid={gold}&p=6&order=asc&t={t}", text: '장비 정련하다 소모 합계' },
    { id: '2305', param: "Stats/Money/TypeTotal.aspx?opid={gold}&p=19&order=asc&t={t}", text: '장비 깊이 소모 합계' },
    { id: '2306', param: "Stats/Money/TypeTotal.aspx?opid={gold}&p=10&order=asc&t={t}", text: '보물 되 성 소모 합계' },
    { id: '2307', param: "Stats/Money/TypeTotal.aspx?opid={gold}&p=15&order=asc&t={t}", text: '정련 이동 소모 합계' },
    { id: '2308', param: "Stats/Money/RoleRank.aspx?opid={gold}&p=5&order=desc&t={t}", text: 'NPC 교역 순위' },
    { id: '2309', param: "Stats/Money/RoleRank.aspx?opid={gold}&p=0&order=desc&t={t}", text: '랭킹 플레이어 줍다' },
    { id: '2310', param: "Stats/Money/RoleRank.aspx?opid={gold}&p=12&order=asc&t={t}", text: '장비 수리 소모 순위' },
    { id: '2311', param: "Stats/Money/RoleRank.aspx?opid={gold}&p=6&order=asc&t={t}", text: '장비 세련되다 소모 순위' },
    { id: '2312', param: "Stats/Money/RoleRank.aspx?opid={gold}&p=19&order=asc&t={t}", text: '장비 깊이 소모 순위' },
    { id: '2313', param: "Stats/Money/RoleRank.aspx?opid={gold}&p=10&order=asc&t={t}", text: '보물 되 성 소모 순위' },
    { id: '2314', param: "Stats/Money/RoleRank.aspx?opid={gold}&p=15&order=asc&t={t}", text: '세련되다 이동 소모 순위' }
];
var money3menus = [
    { id: '2401', param: "Stats/HonorMoneySRoleRank.aspx", text: '은화 역할 순위' },
    { id: '2402', param: "Admin_NoPage.Aspx", text: '금화 주 소모 순위', hidden: false },
    { id: '2403', param: "Admin_NoPage.Aspx", text: '금화 총 소비 순위', hidden: false }
];
var roleOnline = [
    { id: '30', text: '접속 통계' },
    { id: '31', text: '캐릭터 통계' },
    { id: '32', text: '이탈 통계' },
    { id: '33', text: '레벨별 이탈 통계' }
];
roleOnline0menus = [
    { id: '3001', param: "Stats/RoleOnlineFlow_Zone.Aspx", text: '서버별 매 시간' },
    { id: '3002', param: "Stats/RoleOnlineFlowM_zone.aspx", text: '서버별 매 15분' },
    { id: '3003', param: "Stats/RoleOnlineFlow_ALL.Aspx", text: '합계별 매 시간' },
    { id: '3004', param: "Stats/RoleOnlineFlowM_All.aspx", text: '합계별 매 시간' }
];
var roleOnline1menus = [
    { id: '3101', param: "Stats/RoleVocation.aspx", text: '캐릭터 전체 통계' },
    { id: '3102', param: "Stats/RoleVocationGrow.aspx", text: '캐릭터 전체 통계' },
    { id: '3103', param: "Stats/RoleLoginZone.aspx", text: '캐릭터 총 로그인수 통계' },
    { id: '3104', param: "Stats/RoleOnlineLoginRank_Two.aspx", text: '캐릭터 로그인 횟수 통계' },
    { id: '3105', param: "Stats/RoleOnlineLoginOutRank_Two.aspx", text: '캐릭터 로그 아웃 통계' },
    { id: '3106', param: "Stats/RoleVocationLevel.aspx", text: '직업 레벨 통계' },
    { id: '3107', param: "Stats/RoleVocationLevelGrow.aspx", text: '직업 레벨 증가 통계' },
    { id: '3108', param: "Stats/RoleVocationLevelPara.aspx", text: '레벨 구간 통계' },
    { id: '3109', param: "Stats/RoleVocationLevelParaGrow.aspx", text: '레벨 구간 증가 통계' },
    { id: '3110', param: "stats/RoleLevelTime.aspx", text: '레벨별 소모 시간 통계' },
    { id: '3111', param: "Stats/RoleVocationLevelRank.aspx", text: '레벨 랭킹 통계' },
    { id: '3112', param: "Stats/PlayerBattleRank.aspx", text: '공격력 랭킹 통계' },
    { id: '3113', param: "Stats/RoleVocationDefenceRank.aspx", text: '공격력 랭킹 통계' },
    { id: '3114', param: "stats/RoleVocationPKHonorRank.aspx", text: '공격력 랭킹 통계' },
    { id: '3115', param: "stats/Fight/RoleRank.aspx?opid=10031&p=0&order=desc&t={t}", text: '캐릭터 사망 횟수 랭킹' },
    { id: '3116', param: "stats/Fight/RoleRank.aspx?opid=10031&p=1&order=desc&t={t}", text: '캐릭터 사망 횟수 랭킹' },
    { id: '3117', param: "stats/Fight/RoleRank.aspx?opid=10031&p=2&order=desc&t={t}", text: 'NPC에 의해 킬 당한 횟수 랭킹' },
    { id: '3118', param: "stats/GodRank.aspx", text: '신의 시련 랭킹' },
    { id: '3119', param: "stats/WeekKillRank.aspx", text: '주간 처치 랭킹' },
    { id: '3120', param: "stats/WeekExploitRank.aspx", text: '주간 공훈 랭킹' },
    { id: '3121', param: "stats/CampExploitRank.aspx", text: '진영 공훈 랭킹' },
    { id: '3122', param: "stats/CorpsbattleRank.aspx", text: '길드 전투력 랭킹' },
];

var roleOnline2menus = [
    { id: '3201', param: "stats/RoleLoseTimeFirst.aspx", text: '접속 시간별 이탈 통계' },
    { id: '3202', param: "stats/RoleLoseTimeAll.aspx", text: '누적 시간별 이탈 통계' },
    { id: '3203', param: "Stats/RoleLoseLevelPara.aspx", text: '레벨 구간별 이탈 통계' },
    { id: '3204', param: "Stats/RoleLoseLevel.aspx", text: '레벨별 이탈 통계' },
    { id: '3205', param: "Stats/RoleLoseLevelCreate.aspx", text: '每等级创建流失统计' }
];
var roleOnline3menus = [
    { id: '3301', param: "stats/CorpsRankHonor.aspx", text: '레벨별 이탈 통계' },
    { id: '3302', param: "stats/CorpsRankLevel.aspx", text: '길드 레벨 랭킹' },
    { id: '3303', param: "stats/CorpsRankUCount.aspx", text: '길드 인원수 랭킹' }
];
var user = [
    { id: '41', text: ' 유저' },
    { id: '42', text: 'GM 통계' },
    { id: '43', text: 'Releact Data', hidden: false }
];
var user0menus = [
    { id: '4101', param: "Stats/UserLoginZone.aspx", text: '유저 로그인 현황 통계' },
    { id: '4102', param: "Stats/UserLoginArea.aspx", text: '접속 구간 분포 통계' },
    { id: '4103', param: "Stats/UserStateActive.aspx", text: 'DAU 통계' },
    { id: '4104', param: "Stats/UserStateLose.aspx", text: '이탈 유저 통계' },
    { id: '4105', param: "Stats/UserStateBack.aspx", text: '복귀 유저 통계' },
    { id: '4106', param: "Stats/UserLoginTimePara.aspx", text: '접속 시간 분포 통계' },
    { id: '4107', param: "Stats/RoleOnlineLoginRank.aspx", text: '유저 로그인 횟수 랭킹' },
    { id: '4108', param: "Stats/RoleOnlineIPRank.aspx", text: '유저 로그인 횟수 랭킹' },
    { id: '4109', param: "Stats/UserRemain.aspx", text: '유저 잔존율 통계' },
    { id: '4110', param: "Stats/RoleRemain.aspx", text: '캐릭터 잔존율 통계' },
];
var user1menus = [
    { id: '4201', param: "Admin_NoPage.Aspx", text: '작업 유형 통계', hidden: false },
    { id: '4202', param: "Stats/GMQuery.aspx", text: 'GM로그 검색' },
    { id: '4203', param: 'Stats/UserManage/StaticTemplate.aspx?grid=GMUser&category=GMUser&hiddenTimeSpanTool=true', text: 'GM인증 사용자', hidden: false }//新功能 GM授权用户查询
];
var userEmail = [
    { id: '4301', param: "stats/UserManage/UserEmail.aspx", text: 'Emai', hidden: false },
     { id: '4302', param: "stats/UserManage/UserActiveMountStatics.aspx", text: 'Active mount statics', hidden: false },
     { id: '4303', param: "stats/UserManage/UserMountLevelStatic.aspx", text: 'Active mount level statics', hidden: false },
     { id: '4304', param: "stats/UserManage/UserSocialContactStatics.aspx?grid=UserAddFriendLog", text: 'friend', hidden: false }, //删除的好友与添加好友在同一页面统计
     { id: '4305', param: "stats/UserManage/UserSocialContactStatics.aspx?grid=Ememy", text: 'ememy', hidden: false },//黑名单
     { id: '4306', param: "stats/EmailLog.aspx", text: 'Email信息统计', hidden: false }
];
var chat = [
   { id: '51', text: ' 채팅 관련' }
];
chat0menus = [
    { id: '5101', param: "stats/ChatTypeNumD.aspx", text: '채팅 수량 통계' },
    { id: '5102', param: "stats/ChatRoleNumRank.aspx", text: '월드 채팅 수량 랭킹' },
    { id: '5103', param: "stats/ChatRoleRpeatRank.aspx", text: '월드 중복 채팅 수량 통계' },
    { id: '5104', param: "stats/ChatQuery_Hours.aspx", text: '월드 중복 채팅 수량 통계' },
    { id: '5105', param: "Admin_NoPage.Aspx", text: '의심 채팅 통계', hidden: false },
    { id: '5106', param: "stats/ChatQuery.Aspx", text: '의심 채팅 통계' }
];
var task = [
    { id: '61', text: '퀘스트 통계' }
];
var task0menus = [
    { id: '6101', param: "stats/TaskAcceptRank.aspx", text: '퀘스트 통계' },
    { id: '6102', param: "stats/TaskAcceptRankRT.aspx", text: '퀘스트 중복 수락 랭킹' },
    { id: '6103', param: "stats/TaskFinishRank.aspx", text: '퀘스트 완료 랭킹' },
    { id: '6104', param: "stats/TaskFinishRankRT.aspx", text: '퀘스트 중복 완료 랭킹' },
    { id: '6105', param: "stats/TaskAcceptFinish.aspx", text: '퀘스트 완료 비율 통계' },
    { id: '6106', param: "stats/Other/TypeTotal.aspx?opid=50066&order=desc&t={t}", text: '퀘스트 완료 비율 통계' },
    { id: '6107', param: "stats/TaskBugList.aspx", text: '문제 퀘스트 리스트' },
    { id: '6108', param: "stats/TaskQuery.aspx", text: '퀘스트 로그 조회' }
];
var mark = [
    { id: '71', text: '충전 통계' },
    { id: '72', text: '상가 통계' },
    { id: '73', text: '원보 통계' }
];
var mark0menus = [
    { id: '7101', param: "stats/UserAccountDepositRank_Money.aspx", text: '충전 돈 숫자가 순위*' },
    { id: '7102', param: "stats/UserAccountDepositRank_Time.aspx", text: '충전 횟수 순위*' },
    { id: '7103', param: "stats/UserAccountDepositRank_All.aspx", text: '전체 충전 통계*' },
    { id: '7104', param: "stats/UserAccountInterzone.aspx", text: '충전 구간 통계' },
    { id: '7105', param: "stats/UserAccountQuery.aspx", text: '충전 사용자 검색' },
    { id: '7106', param: "stats/UserAccountLogQuery.aspx", text: '충전 기록 검색' },
    { id: '7106', param: "stats/UserAccountLogQuery.aspx", text: '充值记录查询',hidden:true },
    { id: '7107', param: "Stats/RechargeLog.aspx", text: '결제 정보 통계' },
    { id: '7108', param: "Stats/RechargeByTimeLog.aspx", text: '결제 정보 시간별 통계' },
    { id: '7109', param: "Stats/RechargeRoleSumLog.aspx", text: '캐릭터 결제 정보 조회' },
    { id: '7110', param: "Stats/RechargeSumLog.aspx", text: '결제 정보 종합 통계' },
    { id: '7111', param: "Stats/DiamondLog.aspx", text: '다이아 히스토리' },
    { id: '7112', param: "Stats/MoneyLog.aspx", text: '아이템 정보 통계' },
    { id: '7113', param: "Stats/GoldLog.aspx", text: '골드/실버 정보 통계' },
    { id: '7114', param: "ModelPage/ShoppingMallLog/RedDiamondGiftShop.aspx", text: '랜덤 패키지 상점 통계' },
    { id: '7115', param: "ModelPage/ShoppingMallLog/GrowthFund.aspx", text: '성장 패키지 통계' },
    { id: '7116', param: "ModelPage/ShoppingMallLog/ShopLog.aspx", text: '잡화점 통계' },
];
var mark1menus = [
    { id: '7201', param: "stats/ShopSale.aspx", text: '상가 판매 통계 (늘) (옷)' },
    { id: '7202', param: "stats/ShopSale_DayALL.aspx", text: '상가 판매 통계 (늘) (일)' },
    { id: '7203', param: "stats/ShopSale_Day.aspx", text: '상가 판매 통계 (일)' },
    { id: '7204', param: "stats/ShopSale_Day_01.aspx", text: '상가 거래 펜 몇 통계 (일)' },
    { id: '7205', param: "stats/ShopSale_Day_02.aspx", text: '쇼핑 소비 계좌번호가 몇 통계 (일)' },
    { id: '7206', param: "stats/ShopSale_Day_03.aspx", text: '쇼핑 소비 원보 몇 통계 (일)' },
    { id: '7207', param: "stats/ShopSaleItem_Goods.aspx", text: '상품 판매 통계' },
    { id: '7208', param: "stats/ShopSaleItem_Goods_Day_GoodsCount.aspx", text: '상품 판매 수량 통계 (일)' },
    { id: '7209', param: "stats/ShopSaleItem_Goods_Day_MoneyCount.aspx", text: '상품 판매 원보 통계 (일)' },
    { id: '7210', param: "stats/ShopSaleItem_Goods_Day_TradeCount.aspx", text: '상품 판매 펜 몇 통계 (일)' },
    { id: '7211', param: "stats/ShopSaleItem_Items.aspx", text: '도구 판매 통계' },
    { id: '7212', param: "stats/ShopSaleRoleLevel.aspx", text: '플레이어 구매 통계' },
    { id: '7213', param: "stats/ShopSaleItem_ExcelLevel.aspx?shoptype=0&title={t}", text: '물품 레벨 소비 통계' },
    { id: '7214', param: "stats/ShopSaleItem_RoleIDCount.aspx?shoptype=0&title={t}", text: '상품 구매 순위' },
    { id: '7215', param: "stats/ShopSaleCount.aspx?shoptype=0&title={t}", text: '구매 횟수 순위' },
    { id: '7216', param: "stats/ShopSaleB.aspx", text: '상가 판매 통계 묶어. 자꾸. 웨어' },
    { id: '7217', param: "stats/ShopSaleB_DayALL.aspx", text: '상가 판매 통계 묶어. 자꾸. 날' },
    { id: '7218', param: "stats/ShopSaleB_Day.aspx", text: '상가 판매 통계 (납치) (일)' },
    { id: '7219', param: "stats/ShopSaleItem_GoodsB.aspx", text: '상품 판매 통계 (납치)' },
    { id: '7220', param: "stats/ShopSaleItem_ItemsB.aspx", text: '도구 판매 통계 (납치)' },
    { id: '7221', param: "stats/ShopSaleRoleLevelB.aspx", text: '플레이어 구매 통계 (납치)' },
    { id: '7222', param: "stats/ShopSaleItem_ExcelLevel.aspx?shoptype=1&title={t}", text: '물품 레벨 소비 통계 (납치)' },
    { id: '7223', param: "stats/ShopSaleItem_RoleIDCount.aspx?shoptype=1&title={t}", text: '상품 구매 순위 (납치)' },
    { id: '7224', param: "stats/ShopSaleCount.aspx?shoptype=1&title={t}", text: '구매 횟수 순위 (납치)' },
    { id: '7225', param: "stats/ShopQuery.aspx", text: '상가 로그 검색' },
    { id: '7226', param: "stats/ShopSaleItem_ExcelIDCount.aspx?shoptype=0&title={t}", text: '상가 물건 판매 기록' },
    { id: '7227', param: "stats/ShopSaleItem_ExcelIDCount.aspx?shoptype=1&title={t}", text: '상가 물건 판매 기록 (납치)' },
    { id: '7228', param: "stats/AccountInterzoneShopSaleItems.aspx?shoptype=1&title={t}", text: '플레이어 소비 습관을 통계' },
    { id: '7229', param: "stats/AccountInterzoneShopSaleItemsB.aspx?shoptype=1&title={t}", text: '플레이어 소비 습관을 통계 (바인딩)' }
];
var mark2menus = [
    { id: '7301', param: "stats/UserAccountTradeAll.aspx", text: '전체 추출 통계*' },
    { id: '7302', param: "stats/Gold/RoleRank.aspx?opid=10055&p=0&order=asc&t={t}", text: '캐릭터 추출 순위' },
    { id: '7303', param: "stats/Gold/RoleRank.aspx?opid=10055&p=fuzhi&order=asc&t={t}", text: '캐릭터 소모 순위' },
    { id: '7304', param: "stats/Gold/RoleRank.aspx?opid=10055&p=2&order=asc&t={t}", text: '캐릭터 구매 순위' },
    { id: '7305', param: "stats/Gold/RoleRank.aspx?opid=10056&p=zhengzhi&order=asc&t={t}", text: '캐릭터 삭제 순위 (바인딩)' },
    { id: '7306', param: "stats/Gold/RoleRank.aspx?opid=10056&p=fuzhi&order=asc&t={t}", text: '캐릭터 소모 순위 (바인딩)' },
    { id: '7307', param: "stats/Gold/RoleRank.aspx?opid=10056&p=2&order=asc&t={t}", text: '캐릭터 구매 순위 (바인딩)' },
    { id: '7308', param: "stats/GoldQuery.aspx", text: '원보 로그 검색' }
];
var guess = [
    { id: '81', text: '보급 통계' }
];
var guess0menus = [
    { id: '8101', param: "stats/Count.aspx", text: '설치 마운트 통계' },
    { id: '8102', param: "stats/CountItem.aspx", text: '운영 체제 통계' },
    { id: '8103', param: "stats/CountItem.aspx?item=cast(F_ScreenWidth%20as%20varchar(10))%2B*%2Bcast(F_ScreenHeight%20as%20varchar(10))&title={t}", text: '시스템 해상도 통계' },
    { id: '8104', param: "stats/CountItem.aspx?item=F_Area&title={t}", text: '지역 통계' },
    { id: '8105', param: "stats/CountItem.aspx?item=F_Language&title={t}", text: '시스템 언어 통계' },
    { id: '8106', param: "stats/CountItem.aspx?item=F_WinBit&title={t}", text: '시스템 구조 통계' },
    { id: '8107', param: "stats/MediaBaiduIndex.aspx", text: '바 이두 지수' },
    { id: '8108', param: "stats/MediaWeiboZone.aspx", text: '블 로그 전달 통계' },
    { id: '8109', param: "stats/MediaWeiboQuery.aspx", text: '블 로그 전달 조회' }
];
var questionService = [
    { id: '91', text: '설문 조사' }
];
var questionService0menus = [
    { id: '9101', param: "/Admin_IFrame.aspx?src=stats/QuestSex.aspx?itemid=1", text: '비례' },
    { id: '9102', param: "/Admin_IFrame.aspx?src=stats/QuestSex.aspx?itemid=2", text: '나이 분포' },
    { id: '9103', param: "/Admin_IFrame.aspx?src=stats/QuestSex.aspx?itemid=3", text: '사용자 출처' },
    { id: '9104', param: "/Admin_IFrame.aspx?src=stats/QuestSex.aspx?itemid=4", text: '게임 유형' },
    { id: '9105', param: "/Admin_IFrame.aspx?src=stats/QuestSex.aspx?itemid=5", text: '인터넷 게임 시간' },
    { id: '9106', param: "/Admin_IFrame.aspx?src=stats/QuestSex.aspx?itemid=6", text: '누계 게임 시간' },
    { id: '9107', param: "/Admin_IFrame.aspx?src=stats/QuestSex.aspx?itemid=7", text: '게임 주소' },
    { id: '9108', param: "/Admin_IFrame.aspx?src=stats/QuestSex.aspx?itemid=8", text: '뜻을 소비' }
];
var service = [{ id: '101', text: '서버 통계' }];
var service0menus = [{ id: '10101', param: "stats/ZoneInfo.aspx", text: '서버 기본 상태 통계' }];

var detailedInfo = [{ id: '141', text: '검색어 세부 정보' }];
var detailedInfo0menus = [
    { id: '14101', param: "Stats/CampLog.aspx", text: '진영 정보 조회' },
    { id: '14102', param: "Stats/CorpsLog.aspx", text: '길드 정보 조회' },
    { id: '14103', param: "Stats/ModelClothesLog.aspx", text: '코스튬 정보 통계' }
];