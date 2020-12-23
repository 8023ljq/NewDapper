// pages/list/list.js
import http from '../../api/http.js'
Page({

  /**
   * 页面的初始数据
   */
  data: {
    current: 'tab1',
    IsSelectShow: false,
    OrderList: [],
    SelectModel: {
      Status: 1,
      pageSize: 10,
      curPage: 1,
      count: null,
      Keyword: ''
    },
    Count: 0,
    CountPage: 0,//总页码
    scrollTop: false
  },

  //切换tab
  onClick(event) {
    var that = this
    that.data.SelectModel.Status = event.detail.name + 1;
    that.setData({
      OrderList: []
    })
    that.data.SelectModel.curPage = 1
    this.getclientlist()
  },

  onSearch() { //键盘搜索触发
    wx.showToast({
      title: `点击搜索`,
      icon: 'none',
    });
  },

  onSelect() { //点击搜索按钮
    wx.showToast({
      title: `点击搜索按钮`,
      icon: 'none',
    });
    http.order.getaddress().then(res => {
     console.log(res)
    })
  },

  //获取详情
  navDetails(event) {
    var id = event.currentTarget.dataset.id;
    wx.navigateTo({
      url: '/pages/projectDetails/index?TranceNum=' + id,
    })
  },

  //获取项目列表
  getclientlist() {
    var that = this
    http.order.getclientlist(that.data.SelectModel).then(res => {
      if (res.data.ResultCode == 200) {
        const ordersList = that.data.OrderList.concat(res.data.ResultData.List)
        that.setData({
          OrderList: ordersList,
          Count: res.data.ResultCount
        })
        let countpage = Math.ceil(that.data.Count / that.data.SelectModel.pageSize)
        that.setData({
          CountPage: countpage
        })
      } else {
        wx.showToast({
          title: '暂无数据',
          icon: "none"
        });
      }
    })
    //关闭下拉刷新的窗口
    wx.stopPullDownRefresh()
    // 隐藏导航栏加载框  
    //wx.hideNavigationBarLoading();  
  },
  
  // 监听页面是否滑动，如果滑动距离超过100，回到顶部按钮显示
  onPageScroll(e) {
    // console.log(e);
    if (e.scrollTop > 100) {
      this.setData({
        scrollTop: true
      })
    } else {
      this.setData({
        scrollTop: false
      })
    }
  },
  // 点击回到顶部图标
  goTop() {
    if (wx.pageScrollTo) {
      wx.pageScrollTo({
        scrollTop: 0,
      });
    }
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    this.getclientlist()
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
    var that = this
    that.setData({
      OrderList: []
    })
    that.data.SelectModel.curPage = 1
    // 显示顶部刷新图标  
    //wx.showNavigationBarLoading();  
    this.getclientlist()
  },

  /**
   * 页面上拉触底事件的处理函数
   */
  onReachBottom: function () {
    var that = this
    let newPage = that.data.SelectModel.curPage++;
    console.log("新的页码" + newPage)
    //判断是否还有下一页数据
    if (newPage >= that.data.CountPage) {
      //没有下一页数据了
      wx.showToast({
        title: '没有数据了',
        icon:"none",
      });
    } else {
      //还有数据
      // console.log("还是有数据");
      console.log("请求的页码" + that.data.SelectModel.curPage)
      this.getclientlist()
    }

    //console.log(that.data.Count)
  },

  /**
   * 用户点击右上角分享
   */
  onShareAppMessage: function () {

  }
})