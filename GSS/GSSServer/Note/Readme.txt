一、注意事项
1.对于工单的类型使用由于要配套过国家语言支持，因此是将工单类型的关键字存储在GSSDB库下字典表中，而工单实际上类型描述文本需要通过该关键字来查找对应的资源项
2.目前对于一些数据时直接在服务端直接进行了文本的转换，如工单的类型以及工单的状态，如果多个国家共用同一套gss服务端，则不能实现客户端这类信息的国家语言化支持。【这里可以后期修改】
3.对于工单数据显示的列信息，首先追踪服务端中对于列转换是否成功【在服务端中会对工单类进行关键字读取，然后查看是否存在对应的文本】。【建议此处后期进行调整，直接在数据库层次一步处理】
4.目前登录奖励以及活动物品掉落配置功能都没有安装工单流程进行，需要调整工单流程，【创建>审核>工单同步到MySQL】
【备注】2017年8月18日09:36:32  现将资源项同一存储在GSS\LanguageItems 类库下，实现客户端和服务器端，接口共用同一套文本 ！！！
5.【2017-08-24】登录奖励内容设置，将工单和奖励内容一起以实体属性形式传递给服务端，在服务端中会将Serializable序列化的实体进行匹配转换（Serializable序列化的实体将会增加相应的属性字符串）、以接收占用数据库的内存空间，
后期如果要修改奖励属性，请一并在LoginAwardLogicData属性中增加相应的属性。
登录奖励新增字段：金钱，蓝钻
6.【2017-08-29】隐藏bug修复
在GSSDB库中存储角色权限信息T_Roles是将权限ID列表存储到[F_Power]字段中，但是目前该字段限定了长度为300个字符实际上授权是绑定页面控件的ID，导致存储权限不足，对于该字段进行修改设置长度为max
7.[2017-08-31] 业务改动
由于韩方使用gss不使用审核流程，运营人员使用gss创建工单数据时直接使用，因此需要调整目前的工单逻辑，
在创建工单页面中增加函数 GetWorkOrderType（GSS\GSSClient\FormTaskAdd.cs）来控制工单直接生效操作。
新增存储过程SP_AddLogicDataAfterTask，在服务端创建工单完毕之后同时使用该存储过程将逻辑数据进行存储
注意：对于账号和角色同时封停，在只解封角色时不修改账号的封停信息
二、bug 修复:2017-08-21
1.修复gss发送邮件时在没有提供装备数据时写入数据到MySQL失败
三、支持:
设置是否可以支持gss多启动，以及安装多个gss
四、备注
1.登录奖励UI控制 [gspara_Db库playerloadgameawardinfo]
列				数据类型	可空 最大长度 列名
F_MIN_LEVEL		smallint	NO	NULL  最低等级
F_MAX_LEVEL		smallint	NO	NULL  最大等级
F_Order			int			NO	NULL  工单号
F_AWARDID		int			NO	NULL  奖励提供的道具ID
F_GOLD			int			NO	NULL		【new】
F_Mail_Title	char		NO	40	  邮件标题
F_Mail_Content	varchar		NO	200	  邮件内容
F_Sender_Name	char		NO	20	  邮件发送人
F_BEGINTIME		datetime	NO	NULL  登录奖励生效时间
F_INVALIDTIME	datetime	NO	NULL  登录奖励失效时间
F_ZONEID		smallint	NO	NULL  【不使用】
F_NOTE			varchar		YES	100   备注
F_Bind_GOLD		int					  蓝钻【new】
五、隐藏的问题
1、在gss客户端中【查询角色】会使用到战区名称作为检索条件，但是此处的战区名称是固定在客户端配置文件中，有可能与数据库中存储的战区名不一致。
2、gss的主任务页检索条件。在检索角色时只限制角色名和角色ID（）
3、目前在查询数据时韩服上使用的数据库字符集为 SQL_Latin1_General_CP1_CI_AS，但是数据库中存储的内容可以是韩文也可以是中文，所以对于查询时使用的参数需要进行编码转换。
将参数值字符前加N（将结果转换为   Unicode，就是双字节字符）
如：F_Name=N'大区'
六、待进行
1.战力排行榜的 需要以下几个信息
排名、角色名、职业、等级、战力、韩方账号名、设备ID、阵营(unionid  1 联盟   2 部落 0 没有)

排行榜数据来源gsdata_db库下ranktable 存储各个排行榜的详细排行,gspara_Db库下 rankconfigtable存储的是排行榜分类信息，
