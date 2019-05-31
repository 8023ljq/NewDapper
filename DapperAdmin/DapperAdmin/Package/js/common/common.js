layui.use(['form', 'layedit', 'laydate'], function () {
    var form = layui.form
        , layer = layui.layer;
    //监听提交
    form.on('submit(demo1)', function (data) {
        var obj = $(this);
        obj.text("登录中...").attr("disabled", "disabled").addClass("layui-disabled");
        $.ajax({
            type: 'POST',
            url: '/Home/IndexAct/',
            data: data.field,
            dataType: "json",
            success: function (res) {//res为相应体,function为回调函数
                $.LayerMsg(res.ResultMsg, res.ResultCode);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                layer.alert('操作失败！！！' + XMLHttpRequest.status + "|" + XMLHttpRequest.readyState + "|" + textStatus, { icon: 5 });
            },
            complete: function () {
                obj.text("登录").removeAttr("disabled").removeClass("layui-disabled");

            }
        });
        return false;
    });
})

//弹窗提示 ResultMsg:提示内容 ResultType:提示方式
$.LayerMsg = function (ResultMsg, ResultType) {
    if (ResultType >= 0) {
        var icon = "";
        var str = "";
        switch (ResultType) {
            case 0://正常
                icon = "fa fa-check-circle";
                str = "success";
                break;
            case 1://错误
                icon = "fa fa-times-circle";
                str = "error";
                break;
            case 2://警告
                icon = "fa fa-exclamation-circle";
                str = "warning";
                break;
            default:
                icon = "fa fa-times-circle";
                str = "error";
                break;
        }
    }
    top.layer.msg(ResultMsg, { icon: 0, time: 1000, shift: 1, shade: [0.3, '#000'] });
    top.$(".layui-layer-msg").css("border-radius", "5px")
    top.$(".layui-layer-msg").children("div").addClass('layui-layer-msg-' + str);
    top.$(".layui-layer-msg").children("div").children("i").removeClass('layui-layer-ico layui-layer-ico').addClass(icon);
}