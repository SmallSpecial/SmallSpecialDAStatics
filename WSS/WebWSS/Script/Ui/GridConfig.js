var gridConfig = {
    AuctionRoom: {//拍卖行
        Columns: [
            { field: 'F_Date', title: '日期' },
            {field:'num',title:'排序'}
        ]
    },
    UserEmail: {//用户邮件
        Columns: [
            { field: 'RoleId', title: lang.column.roleNo,width:100 },
            { field: 'Title', title: lang.column.title,width:200 },
            { field: 'Text', title: lang.column.text,width:600 },
            {
                field: 'IsRead', title: lang.column.state, width: 100, formatter: function (val) {
                    var str = (val).toString(2);//该字段数据存储规则：十项（二进制）或运算第一位值为1代表已读 如存储481 >111100001 第一位（最低位）值为1表示已读
                    if ((val || 1)==val) {
                        return lang.label.isRead;
                    }
                    return lang.label.unRead;
                }
            },
            {
                field: 'HavaProp', title: lang.column.prop, formatter: function (val) {
                    if (val == true) {
                        return lang.label.hava;
                    }
                    return lang.label.nohava;;
                }
            }
        ]
    },
    UserActiveMountStatics: {
        Columns: [
            { field: 'UserId', title: lang.column.userNo },
            {
                field: 'ActiveMountIds', title: lang.column.num, formatter: function (column,row,index) {
                    return column.length;
                }
            },
            { field: 'ActiveMountIds', title: lang.column.activeMountIds }
        ]
    },
    UserMountLevelStatic: {
        Columns: [
            {
                field: 'mountId', title: lang.column.NO
            },
            {
                field: 'level', title: lang.column.level
            }, {
                field: 'mountNumber',title:lang.column.num
            }
        ]
    },
    UserAddFriendLog: {
        Columns: [
             { field: 'index', title: lang.column.index },
            { field: 'Userid', title: lang.column.userName },
            { field: 'roleid', title: lang.column.roleNo },
            { field: 'Level', title: lang.column.level },
            {
                field: 'ActionType', title: lang.column.step, formatter: function (val) {
                    if (val == 1)
                        return lang.cmd.add;
                    return lang.cmd.del;
                }
            },
            {
                field: 'OpTime', title: lang.label.time, formatter: function (val) {
                    return $.milDateString(val);
                }
            }
        ]
    }
};
var gridList = {
    AuctionRoom: 'AuctionRoom',//拍卖行
    UserEmail: 'UserEmailData',//用户邮件
    GMUser: 'GMUser',//GM授权用户列表
    UserActiveMountStatics: 'UserActiveMountStatics',
    UserMountLevelStatic: 'UserMountLevelStatic',
    UserAddFriendLog: 'UserAddFriendLog'
}
var dataField = {
    pageNumber: 'pageNumber',
    pageSize: 'pageSize',
    total: 'total',
    pageTotal: 'pageTotal'
};
var AppCache = {
    GameZone: 'GameZone',
    PageLimit: 'PageLimit'
}