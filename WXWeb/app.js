//app.js
import http from './api/http.js'
//var http =require('./api/http')

App({
  onLaunch: function () {
    // 展示本地存储能力
    var logs = wx.getStorageSync('logs') || []
    logs.unshift(Date.now())
    wx.setStorageSync('logs', logs)
    wx.login({
      success:function(res){
        if(res.code){
          http.common.wechatuserinfo({UserCode: res.code}).then(res=>{
            if(res.data.ResultCode==200){
              wx.setStorageSync('unionid', res.data.ResultData.result.unionid)
              wx.setStorageSync('session_key', res.data.ResultData.result.session_key)
              wx.setStorageSync('openid', res.data.ResultData.result.openid)
              wx.setStorageSync('isstaff', res.data.ResultData.isstaff)
              if(res.data.ResultData.isstaff){
                wx.reLaunch({
                  url: '/pages/list/index',
                });
              }
              else{
                wx.navigateTo({
                  url: '/pages/authorize/index',
                })
              }
            }
          })
        }
      }
    })
    // 获取用户信息
    wx.getSetting({
      success: res => {
        if (res.authSetting['scope.userInfo']) {
          // 已经授权，可以直接调用 getUserInfo 获取头像昵称，不会弹框
          wx.getUserInfo({
            success: res => {
              // 可以将 res 发送给后台解码出 unionId
              this.globalData.userInfo = res.userInfo

              // 由于 getUserInfo 是网络请求，可能会在 Page.onLoad 之后才返回
              // 所以此处加入 callback 以防止这种情况
              if (this.userInfoReadyCallback) {
                this.userInfoReadyCallback(res)
              }
            }
          })
        }
      }
    })
  },
  onShow:function(){},
  globalData: {
    userInfo: null,
    // 头部的自定义的高度
    statusBarHeight: wx.getSystemInfoSync()['statusBarHeight'],
    headerBtnPosi : wx.getMenuButtonBoundingClientRect()
  }
})