var EquipReinforcement = {
    text:'装备强化',
    Items: {
        0: {
            key: 'Arm', text: '武器'
        },
        1: {
            key: 'Necklace', text: '项链'
        },
        2: {
            key: 'Ring', text: '戒指'
        },
        3: {
            key: 'Dress', text: '饰品'
        },
        4: {
            key: 'Helmet', text: '头盔'
        },
        5: {
            key: 'Cuirass', text: '胸甲'
        },
        6: {
            key: 'Guard', text: '护腿'
        },
        7: {
            key: 'Shous', text: '鞋子'
        },
        8: {
            key: 'Pants', text: '裤子'
        },
        9: {
            key: 'Earing', text: '耳环'
        },
        10: {
            key: 'End', text: '装备强化总数量'
        }
    },
    OP_BAK: {
        Param1:'装备枚举号',
        Param2:'强化后等级',
        Param3:'强化后的保底值',
        Param4:'强化装备道具的唯一标识',
        Param5:'强化消耗的绑定金币数量',
        Param6:'结束，强化消耗的道具和数量'
    }
};
var EquipUpdateStar = {
    text: '装备升星',
    OP_BAK: {
        Param1:'精炼前等级',
        Param2: '精炼后等级',
        Param3:'精炼结果 1:精炼成功 0:精炼失败',
        Param4:'精炼装备道具的唯一标识',
        Param5:'精炼消耗的绑定金币数量',
        Param6:'结束，精炼消耗的道具和数量'
    }
};
var EquipmentEnchantment = {
    text: '装备附魔',
    Items: {
        0: { key: 'Hole', text: '开孔' },
        1: { key: 'Enchant', text: '附魔' },
        2: { key: 'GetExp', text: '附魔长成' }
    }
};
var EquipJewel = {
    text: '装备宝石',
    Items: {
        0: { key: 'Hole', text: '开孔' },
        1: { key: 'Set_Store', text: '镶嵌' },
        2: { key: 'UnSet_Stone', text: '摘除' }
    }
};
var EquipWash = {
    text: '装备洗练',
    Items: {
        0: { key: 'Randeff', text: '洗练' },
        1: { key: 'Save_Attr', text: '保存属性' }
    }
};
var gameOpidConfig = {
    50118: {
        data: EquipReinforcement
    },
    50120: {
        data: EquipmentEnchantment
    },
    50121: {
        data: EquipJewel
    },
    50122: {
        data: EquipWash
    },
    50124: {
        data: {
            text: '拍卖行物品上架'
        }
    },
    50125: {
        data: {
            text: '拍卖行物品下架'
        }
    },
    50126: {
        data: {
            text: '拍卖行物品购买'
        }
    }
};
/*
gameOpidConfig  数据读取形式

for (var opid in gameOpidConfig) {
    console.log(opid);//读取opid
    console.log(gameOpidConfig[opid]);//读取每一项的内容
}
*/