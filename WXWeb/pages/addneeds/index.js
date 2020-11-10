// pages/addneeds/index.js
import http from '../../api/http.js'
Page({

  /**
   * 页面的初始数据
   */
  data: {
    TranceNum: ''
  },

  //添加需求跟进
  formSubmit(e) {
    var that = this
    console.log(that.TranceNum)
    const data = {
      Feedback: e.detail.value.Feedback,
      NewDemand: e.detail.value.Feedback,
      ClientId: that.data.TranceNum,
      OpenId: wx.getStorageSync('openid')
    }
    if (data.Feedback == '' || data.NewDemand == '') {
      wx.showToast({
        title: '无法添加空的数据',
        icon: 'none',
      });
    }
    else {
      http.order.addfollow(data).then(res => {
        wx.showToast({
          title: res.data.ResultMsgs,
          icon: 'none',
        });
        if (res.data.ResultCode == 200) {
          wx.navigateTo({
            url: '/pages/projectDetails/index?TranceNum=' + that.data.TranceNum,
          })
        }
      })
    }
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function () {
    var that = this
    let pages = getCurrentPages();
    let currentPages = pages[pages.length - 1];
    let options = currentPages.options;
    that.setData({
      TranceNum: options.TranceNum
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

  }
})