// pages/addproject/index.js
import http from '../../api/http.js'
Page({

  /**
   * 页面的初始数据
   */
  data: {
    pickerindex: null,
    picker: [],
    level: ['A', 'B', 'C', 'D', 'E'],
    levelindex: null,
    dataModel: {
      ContactPerson: '',
      ContactDetails: '',
      CustomerSource: '',
      SourceMethod: '',
      ProductType: '',
      FirstDockingPeople: '',
      ClientLevel: '',
      CompanyAddress: '',
      BasicNeeds: ''
    }
  },

  //对接人
  PickerChange(e) {
    this.setData({
      pickerindex: e.detail.value
    })
    console.log(e.detail.value);
  },

  //项目等级
  LevelChange(e) {
    this.setData({
      levelindex: e.detail.value
    })
    console.log(e.detail.value);
  },

  //添加按钮
  formSubmit(e) {
    console.log(e.detail.value)
    var that = this
    that.setData({
      dataModel: e.detail.value
    })
    console.log(that.dataModel)
    http.order.addclient(e.detail.value).then(res => {
      if (res.data.ResultCode == 200) {
        wx.reLaunch({
          url: '/pages/list/index',
        });
      }
    })
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    http.common.getuserlist().then(res => {
      console.log(res)
     const arry=[];
      if (res.data.ResultCode == 200) {
        console.log(res.data.ResultData.selectList)
        for (let i = 0; i < res.data.ResultData.selectList.length; i++) {
          arry.push(res.data.ResultData.selectList[i].Value)
        }
       this.setData({
         picker:arry
       })
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

  }
})