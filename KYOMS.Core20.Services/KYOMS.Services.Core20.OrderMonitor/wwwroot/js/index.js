var table = function () {
    function formatConvert(value, row, index) {
        return value == 1 ? "是" : "否";
    }
    function DataDbNullFormatConvert(value, row, index) {

        return value.indexOf('0001') > -1 ? "" : value;
    }
    function init() {
        $("#userTable").bootstrapTable('destroy');
        $("#userTable").bootstrapTable({
            //请求方法
            method: 'post',
            url: "/api/orderMonitor/getdata",
            contentType: "application/x-www-form-urlencoded",
            dataType: "json",
            singleSelect: true,
            sidePagination: "server", //服务端处理分页
            clickToSelect: true, //是否启用点击选中行
            //是否显示行间隔色
            striped: false,
            //showRefresh:true,
            //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
            cache: false,
            //是否显示分页（*）
            pagination: true,
            //是否启用排序
            sortable: true,
            //排序方式
            sortOrder: "desc",
            pageSize: 10,
            //可供选择的每页的行数（*）
            pageList: [10, 30, 50],
            showColumns: false,
            showToggle: true,  
            switchable: true,
            showExport: true,
            showColumns:true,
            exportDataType: 'basic',
            exportTypes: ['json', 'xml', 'csv', 'txt', 'sql', 'excel'],
            exportOptions: {
                fileName: '运单状态监控表',  //文件名称设置  
                worksheetName: 'sheet1',  //表格工作区名称  
                tableName: '运单状态监控表',
                excelstyles: ['background-color', 'color', 'font-size', 'font-weight']
            },  
            //这个接口需要处理bootstrap table传递的固定参数,并返回特定格式的json数据
            //默认值为 'limit',传给服务端的参数为：limit, offset, search, sort, order Else
            //queryParamsType:'',
            ////查询参数,每次调用是会带上这个参数，可自定义
            columns: [
                { title: '编号', field: 'ID', width: 40, sortable: true},
                { title: '订单编号', field: 'ORDER_NO', width: 200, sortable: true },
                { title: '外部系统编号', field: 'OUTSYS_ORDERCODE', width: 80, sortable: true },
                { title: '运单号', field: 'MAILNO', width: 200, sortable: true },
                { title: '订单来源', field: 'ORDER_SOURCE', width: 80, sortable: true },
                { title: '动作时间', field: 'ACTION_TIME', width: 200, sortable: true, formatter: DataDbNullFormatConvert },
                { title: '距揽件耗时(H)', field: 'ELAPSED', width: 200, sortable: true },
                { title: '揽件', field: 'GOT', width: 40, sortable: true, formatter: formatConvert},
                { title: '揽件时间', field: 'GOT_TIME', width: 200, sortable: true, formatter: DataDbNullFormatConvert },
                { title: '揽件耗时(H)', field: 'GOT_ELAPSED', width: 200, sortable: true },
                { title: '发件', field: 'DEPARTURE', width: 40, sortable: true, formatter: formatConvert },
                { title: '发件时间', field: 'DEPARTURE_TIME', width: 200, sortable: true, formatter: DataDbNullFormatConvert },
                { title: '到件', field: 'ARRIVAL', width: 40, sortable: true, formatter: formatConvert },
                { title: '到件时间', field: 'ARRIVAL_TIME', width: 200, sortable: true, formatter: DataDbNullFormatConvert },
                { title: '派件', field: 'SENT_SCAN', width: 40, sortable: true, formatter: formatConvert },
                { title: '派件时间', field: 'SENT_SCAN_TIME', width: 200, sortable: true, formatter: DataDbNullFormatConvert },
                { title: '签收', field: 'SIGNED', width: 40, sortable: true, formatter: formatConvert },
                { title: '签收耗时(H)', field: 'SIGNED_TIME', width: 200, sortable: true, formatter: DataDbNullFormatConvert },
                { title: '问题件', field: 'OTHER', width: 40, sortable: true, formatter: formatConvert },
                { title: '问题件时间', field: 'OTHER_TIME', width: 200, sortable: true, formatter: DataDbNullFormatConvert },
                { title: '异常', field: 'FAILED', width: 40, sortable: true, formatter: formatConvert },
                { title: '异常时间', field: 'FAILED_TIME', width: 200, sortable: true, formatter: DataDbNullFormatConvert },
                { title: '订单状态', field: 'ORDER_STATUS', width: 40, sortable: true, formatter: formatConvert },
                { title: '订单创建时间', field: 'ORDER_TIME', width: 200, sortable: true, formatter: DataDbNullFormatConvert },
                { title: '记录时间', field: 'CREATE_TIME', width: 200, sortable: true, formatter: DataDbNullFormatConvert }
            ],
            queryParams: function queryParams(params) {   //设置查询参数
                var param = {
                    PageSize: params.limit,   //页面大小
                    PageIndex: parseInt(parseInt(params.offset / params.limit) + 1),  //页码
                    OrderBy: params.sort == undefined ? '' : params.sort + ' ' + params.order,
                    ORDER_NO: $('#ORDER_NO').val(),
                    START_TIME: $('#START_TIME').val(),
                    END_TIME: $('#END_TIME').val(),
                    GOT: $("#GOT").val(),
                    DEPARTURE: $("#DEPARTURE").val(),
                    ARRIVAL: $("#ARRIVAL").val(),
                    SENT_SCAN: $("#SENT_SCAN").val(),
                    SIGNED: $("#SIGNED").val(),
                    OTHER: $("#OTHER").val(),
                    FAILED: $("#FAILED").val(),
                    ORDER_STATUS: $("#ORDER_STATUS").val()
                };
                return param;
            }
        });
    }
    return {
        Init: function () {
            init();
        }
    }
}();

$(function () {
    table.Init();
    $('#START_TIME').datetimepicker({
        format: 'yyyy-mm-dd', language: 'zh-CN',
        weekStart: 1,
        todayBtn: 1,
        autoclose: 1,
        todayHighlight: 1,
        startView: 2,
        minView: 2,
        forceParse: 0
    });
    $('#END_TIME').datetimepicker({
        format: 'yyyy-mm-dd', language: 'zh-CN',
        weekStart: 1,
        todayBtn: 1,
        autoclose: 1,
        todayHighlight: 1,
        startView: 2,
        minView: 2,
        forceParse: 0
    });
    $('#btn_search').click(function () {
        var sdate = new Date($('#START_TIME').val());
        var edate = new Date($('#END_TIME').val());
        if (sdate != '' && edate != '' && sdate > edate) {
            alert('开始日期不能大于结束日期');
            return false;
        }
        Pace.restart();
        table.Init();
    });
    $('#btn_reset').click(function () {
        location.reload();
    });
    $('#remove_stime').click(function () {
        $('#START_TIME').val('');
    });
    $('#remove_etime').click(function () {
        $('#END_TIME').val('');
    });

})