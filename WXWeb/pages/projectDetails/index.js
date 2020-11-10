//order.js
import http from '../../api/http.js'
import Dialog from '../../vantweapp/dialog/dialog';

//引入代码
var call = require("../../utils/http/request.js")

Page({
  data: {
    OrderDetail: {},
    DemandList: [],
    IsShow: false,
    activeName: '1',
    show:false
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
  },

  // 获取项目详情
  getDetails(TranceNum) {
    http.order.getdetails({
      Clientid: TranceNum
    }).then(res => {
      if (res.data.ResultCode == 200) {
        console.log(res);
        this.setData({
          OrderDetail: res.data.ResultData.Model,
          DemandList: res.data.ResultData.DemandList,
        })
      }
    })
  },

  //弹窗显示跟进反馈
  showDialog(event){
    console.log(event)
    Dialog.alert({
      message: event.currentTarget.dataset.id,
    }).then(() => {
      Dialog.close();
    });
  },

  //需求跟进
  handelfollow(event){
    var id = event.currentTarget.dataset.id;
    wx.navigateTo({
      url: '/pages/addneeds/index?TranceNum='+id,
    })
  },
  
  //修改项目
  modifyclient(){
    wx.navigateTo({
      url: '/pages/editproject/index',
    })
  },

  navDetails(event){
    console.log(event)
  },
  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function () {
    let pages = getCurrentPages();
    let currentPages = pages[pages.length - 1];
    let options = currentPages.options;
    this.getDetails(options.TranceNum)
  },

  /**
   * 生命周期函数--监听页面显示
   */
  onShow: function (e) {
   
  },
})