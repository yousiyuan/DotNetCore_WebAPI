using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KYOMS.Core20.DE.Enum
{
    public enum ENUM_ORDER_SOURCE_TYPE
    {
        /// <summary>
        /// 手工新建
        /// </summary>
        MANUAL = 1,

        /// <summary>
        /// 第三方接口（2，3，4）
        /// </summary>
        INTERFACE = 2,

        /// <summary>
        /// 批量导入
        /// </summary>
        BULKIMPORT = 5,

        /// <summary>
        /// 其他
        /// </summary>
        OTHER = 100,
    }

    public enum ENUM_ORDER_SOURCE
    {

        #region [手工新建]
        /// <summary>
        /// OMS后台
        /// </summary>
        MANUAL = 100,

        /// <summary>
        /// OMS后台
        /// </summary>
        OMS = 101,
        #endregion

        #region [第三方接口]
        /// <summary>
        /// 标准接口
        /// </summary>
        INTERFACE = 200,

        /// <summary>
        /// 阿里
        /// </summary>
        ALI = 201,

        /// <summary>
        /// 慧聪
        /// </summary>
        HUICHONG = 202,

        /// <summary>
        /// 物流宝
        /// </summary>
        WLB = 203,
        //WULIUBAO = 203,

        /// <summary>
        /// 淘宝
        /// </summary>
        TAOBAO = 204,

        /// <summary>
        /// 远成APP下单
        /// </summary>
        YCAPP = 401,

        /// <summary>
        /// 远程官网
        /// </summary>
        YCWEBSITE = 402,

        /// <summary>
        /// 电子面单
        /// </summary>
        EBILL =403,

        /// <summary>
        /// UD订单
        /// </summary>
        UD = 404,

        /// <summary>
        /// 快递鸟标准订单
        /// </summary>
        KDNIAO =405,

        /// <summary>
        /// 快递鸟电子面单
        /// </summary>
        KDNIAOEBILL = 406,
        /// <summary>
        /// 运东西
        /// </summary>
        YDX= 407,

        /// <summary>
        /// 东箭标准订单
        /// </summary>
        DJ = 408,

        /// <summary>
        /// 快递在线下单
        /// </summary>
        TAOBAO_ONLINE = 409,

        /// <summary>
        /// 自联订单
        /// </summary>
        ZL=410,
        #endregion

        #region [批量导入]
        /// <summary>
        /// 批量导入
        /// </summary>
        BULKIMPORT = 500,
        #endregion

        #region[其他]
        /// <summary>
        /// 其他
        /// </summary>
        OTHER = 10000,
        #endregion
    }
}
