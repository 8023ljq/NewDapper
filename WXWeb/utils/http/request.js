import {config } from "../config.js"

//请求本地接口
const request=(url,options)=>{
    return new Promise((resolve,reject)=>{
      wx.request({
        url:`${config.api_base_url}${url}`,
        method:options.method,
        data:options.method=='GET'?options.data:JSON.stringify(options.data),
        header:{
          'Content-Type':'application/json;charset=UTF-8'
        },
        success(request){
            resolve(request)
        },
        fail(err){
          reject(err)
        }
      })
    })
  }
  //请求第三方接口
  const requestThird=(url,options)=>{
      return new Promise((resolve,reject)=>{
        wx.request({
          url:`${url}`,
          method:options.method,
          data:options.method=='GET'?options.data:JSON.stringify(options.data),
          header:{
            'Content-Type':'application/json;charset=UTF-8'
          },
          success(request){
              resolve(request)
          },
          fail(err){
            reject(err)
          }
        })
      })
    }

  const get=(url,options={})=>{
    return request(url,{
      method:'GET',
      data:options
    })
  }
  const post=(url,options)=>{
    return request(url,{
      method:'POST',
      data:options
    })
  }
  const getThird=(url,options={})=>{
      return requestThird(url,{
        method:'GET',
        data:options
      })
    }
    const postThird=(url,options)=>{
      return requestThird(url,{
        method:'POST',
        data:options
      })
    }

  module.exports={
    get,
    post,
    getThird,
    postThird
  }