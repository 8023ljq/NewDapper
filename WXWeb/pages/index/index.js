//index.js
//获取应用实例
const app = getApp()

Page({
  data: {
    motto: 'Hello World',
    userInfo: {},
    hasUserInfo: false,
    canIUse: wx.canIUse('button.open-type.getUserInfo'),//测试getUserInfo在当前版本是否可用
    latitude: 23.099994,
    longitude: 113.324520,
    background: ['demo-text-1', 'demo-text-2', 'demo-text-3'],
    indicatorDots: true,
    vertical: false,
    autoplay: false,
    interval: 2000,
    duration: 500
  },
  // onShareAppMessage() {
  //   return {
  //     title: 'cover-view',
  //     path: 'page/component/pages/cover-view/cover-view'
  //   }
  // },
  //事件处理函数
  bindViewTap: function() {
    // wx.navigateTo({//页面跳转
    //   url: '../logs/logs'
    // })
  },
    /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function () {
    if (app.globalData.userInfo) {
      this.setData({
        userInfo: app.globalData.userInfo,//将全局用户信息赋值给变量
        hasUserInfo: true//显示引导授权按钮
      })
    } else if (this.data.canIUse){
      // 由于 getUserInfo 是网络请求，可能会在 Page.onLoad 之后才返回
      // 所以此处加入 callback 以防止这种情况
      // app.userInfoReadyCallback = res => 
      //   this.setData({
      //     userInfo: res.userInfo,
      //     hasUserInfo: true
      //   })
      // }
    } else {
      // 在没有 open-type=getUserInfo 版本的兼容处理
      // wx.getUserInfo({
      //   success: res => {
      //     app.globalData.userInfo = res.userInfo
      //     this.setData({
      //       userInfo: res.userInfo,
      //       hasUserInfo: true
      //     })
      //   }
      // })
    }
  },
  Login(){
     //点击取消按钮
     if (e.detail.userInfo == null) {
      console.log("授权失败")
    } 
    else {//点击允许按钮
      this.setData({
        userInfo: e.detail.userInfo,
        hasUserInfo: true
      })
    }
    //全局对象用户信息赋值
    app.globalData.userInfo = e.detail.userInfo
  },
  getUserInfo: function(e) {
    console.log(e)
    app.globalData.userInfo = e.detail.userInfo
    this.setData({
      userInfo: e.detail.userInfo,
      hasUserInfo: true
    })
  }
})
