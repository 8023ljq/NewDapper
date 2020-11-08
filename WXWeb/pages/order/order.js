//order.js
//获取应用实例
const app = getApp()

//引入代码
var call = require("../../utils/http/request.js")

Page({
  data: {
    OrderList: [
      {
        "name":"标题一",
        "date":"阿萨德股份的观点个人股阿萨德股份的观点个人股阿萨德股份的观点个人股阿萨德股份的观点个人股阿萨德股份的观点个人股阿萨德股份的观点个人股阿萨德股份的观点个人股阿萨德股份的观点个人股",
      },
      {
        "name":"标题二",
        "date":"阿萨德股份的观点个人股",
      },
      {
        "name":"标题三",
        "date":"阿飞洒发顺啊实打实的撒3丰撒打算",
      },
      {
        "name":"标题四",
        "date":"阿飞洒发顺丰撒打算",
      }
    ],
    IsShow: false,
    activeName: '1',
  },
  onChange(event) {
    console.log(event)
    this.setData({
      activeName: event.detail,
    });
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