
const request=(options)=>{
  return new Promise((resolve,reject)=>{
    const {data,method}=options
    if(data&&method!=='get'){
      options.data=JSON.stringify(data)
    }
    wx.showLoading({
      title: '加载中',
      mask: true
    });
    wx.request({
      header:{
        'Content-Type':'application/json',
        'openid':wx.getStorageSync('openid')
        // 'unionid':'123456789'
      },
      ...options,
      success:function(res){
        wx.hideLoading({
          success: (res) => {},
          fail: (err) => {},
        });
          if(res.data.ResultCode==700){
            console.log(res)
            wx.showToast({
              title: res.data.ResultMsgs,
              mask:true,
              icon:'none',
              duration:2000,
              success:function(){
                wx.redirectTo({
                  url: '/pages/authorize/index',
                })
                reject()
              }
            })
          }else{
            resolve(res)
          }
      },
      fail:function(res){
        wx.hideLoading({
          success: (res) => {},
          fail: (err) => {},
        });
        reject(res)
      }
    })
  })
}
export default request