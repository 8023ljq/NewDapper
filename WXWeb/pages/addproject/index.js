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
    TranceNum: '', //过渡容器
    dataModel: {
      TranceNum: '',
      ContactPerson: '',
      ContactDetails: '',
      CustomerSource: '',
      SourceMethod: '',
      ProductType: '',
      Participant:'',
      FirstDockingPeople: '',
      ClientLevel: '',
      CompanyAddress: '',
      BasicNeeds: ''
    },
    isadd: true
  },

  //对接人
  PickerChange(e) {
    var that = this
    that.setData({
      pickerindex: e.detail.value
    })
  },

  //项目等级
  LevelChange(e) {
    var that = this
    that.setData({
      levelindex: e.detail.value
    })
    console.log(e.detail.value);
    console.log(that.data.levelindex);
  },

  //添加按钮
  formSubmit(e) {
    var that = this
    that.setData({
      dataModel: e.detail.value
    })

    if (that.data.isadd) {
      http.order.addclient(e.detail.value).then(res => {
        wx.showToast({
          title: res.data.ResultMsgs,
          icon: res.data.ResultType,
          success: (result) => {
            setTimeout(() => {
              if (res.data.ResultCode == 200) {
                wx.reLaunch({
                  url: '/pages/list/index',
                });
              }
            }, 1000);
          },
        });
      })
    } else {
      that.data.dataModel.TranceNum = that.data.TranceNum;
      http.order.updateclient(that.data.dataModel).then(res => {
        wx.showToast({
          title: res.data.ResultMsgs,
          icon: res.data.ResultType,
          success: (result) => {
            setTimeout(() => {
              if (res.data.ResultCode == 200) {
                wx.reLaunch({
                  url: '/pages/list/index',
                });
              }
            }, 1000);
          },
        });
      })
    }
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function () {
    var that = this
    http.common.getuserlist().then(res => {
      const arry = [];
      if (res.data.ResultCode == 200) {
        for (let i = 0; i < res.data.ResultData.selectList.length; i++) {
          arry.push(res.data.ResultData.selectList[i].Value)
        }
        that.setData({
          picker: arry,
        })
      }
    })
    let pages = getCurrentPages();
    let currentPages = pages[pages.length - 1];
    let options = currentPages.options;
    if (options.TranceNum != null) {
      http.order.getdetails({
        Clientid: options.TranceNum
      }).then(res => {
        if (res.data.ResultCode == 200) {
          that.setData({
            dataModel: res.data.ResultData.Model,
            isadd: false,
            pickerindex: null,
            levelindex: null,
          })
          that.data.TranceNum = options.TranceNum
          for (let i = 0; i < that.data.picker.length; i++) {
            if (that.data.picker[i] == res.data.ResultData.Model.FirstDockingPeople) {
              that.setData({
                pickerindex: i,
              })
            }
          }
          for (let k = 0; k < that.data.level.length; k++) {
            if (that.data.level[k] == res.data.ResultData.Model.ClientLevel) {
              that.setData({
                levelindex: k,
              })
            }
          }
        }
        console.log(that.data.dataModel)
      })
    }
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