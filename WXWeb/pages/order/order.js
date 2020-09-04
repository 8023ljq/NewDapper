//order.js
//获取应用实例
const app = getApp()

//引入代码
var call = require("../../utils/http/request.js")

Page({
  data: {
    OrderList: [],
    IsShow: false
  },
  LoginAct() {
    var that = this;
    var asd = call.getRequest("text/getuserlist", this.shuffleSuc, this.fail);
    console.log(asd);
  },
  shuffleSuc: function (data) {
    var that = this;
    that.setData({
      OrderList: data.ResultData.data,
    });
    if (that.data.OrderList.length > 0) {
      that.setData({
        IsShow: true,
      });
    }
  }
})