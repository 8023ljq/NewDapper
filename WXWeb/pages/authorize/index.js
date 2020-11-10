// pages/authorize/index.js
import http from '../../api/http.js'

Page({
  getPhoneNumber(e) {
    const data = {
      SessionKey: wx.getStorageSync('session_key'),
      IV: e.detail.iv,
      encryptedData: e.detail.encryptedData,
    }
    http.common.getphone(data).then(res=>{
      if(res.data.ResultCode==200){
        console.log(res)
        if (e.detail.errMsg == 'getPhoneNumber:ok') {
          const authorize = {
            phone: res.data.ResultData.phone,
            openid : wx.getStorageSync('openid'),
            unionid : wx.getStorageSync('unionid')
          }
          http.common.authorize(authorize).then(res=>{
            wx.showToast({
              title: res.data.ResultMsgs,
              icon: "none"
            });
            if (res.data.ResultCode == 200) {
              wx.reLaunch({
                url: '/pages/list/index',
              });
            }
          })
        }
      }
    })
  },

  /**
   * 页面的初始数据
   */
  data: {

  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {

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

  }
})