//home.js
//获取应用实例
const app = getApp()

//引入请求代码
var Request = require("../../utils/http/request.js")
// 引入SDK核心类
var QQMapWX = require('../../utils/qqmap-wx-jssdk.min.js');

// 实例化API核心类
var demo = new QQMapWX({
  key: 'P4VBZ-6S7LU-RYYVV-4Q3HI-WHPMF-T6BXA' // 位置服务key必填
});

Page({
  data: {
    latitude:'',//纬度
    longitude:'',//经度
    localCity:'',//城市
    district:'',
    address:'',//详细地址
    systemunfo: {},
    accuracy: {}, //微信获取位置信息
  },
  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    var that = this
    wx.getLocation({
      type: 'wgs84', // 默认为 wgs84 返回 gps 坐标，gcj02 返回可用于 wx.openLocation 的坐标
      success: function (res) {
        //赋值经纬度
        that.setData({
          latitude: res.latitude,
          longitude: res.longitude,
        })
        demo.reverseGeocoder({
          location: {
            latitude: that.data.latitude,
            longitude: that.data.longitude
          },
          success: function (res) {
            console.log(res);
            let province = res.result.address_component.province;//省份
            let city = res.result.address_component.city;//城市
            let district=res.result.address_component.district;//城市
            that.setData({
              localCity: city,
              district:district,
              address:res.result.formatted_addresses.recommend,//城市
            })
          },
          fail: function (res) {
            console.log(res);
          }
        })
        console.log(res);
      }
    })
  },
  /**
   * 生命周期函数--监听页面初次渲染完成
   */
  onReady: function () {
  },
  
  /**
   * 生命周期函数--监听页面显示
   */
  onShow: function () {
  },

  /**
   * 生命周期函数--监听页面隐藏
   */
  onHide: function () {
  },

  /**
   * 生命周期函数--监听页面卸载
   */
  onUnload: function () {
  },

  /**
   * 页面相关事件处理函数--监听用户下拉动作
   */
  onPullDownRefresh: function () {

  },

  /**
   * 页面上拉触底事件的处理函数
   */
  onReachBottom: function () {

  },

  /**
   * 用户点击右上角分享
   */
  onShareAppMessage: function () {

  },
  getQuery:function(){//获取天气数据
    let queryDate={
      city:'上海',
      key:'18c17c84096147d980cb621905327ec5',
    };
    Request.getThird("http://apis.juhe.cn/simpleWeather/query",queryDate).then(res=>{
      console.log(res);

    })
  },
});