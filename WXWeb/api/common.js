import {config} from '../utils/config.js'
import request from '../utils/http/request.js'

//获取用户信息
export function wechatuserinfo(data){
  return request({
    url: `${config.api_base_url}/user/wechatuserinfo`,
    method: 'get',
    data
  })
}

//获取当前微信手机号
export function getphone(data){
  return request({
    url: `${config.api_base_url}/user/getphone`,
    method: 'post',
    data
  })
}

//手机号授权操作
export function authorize(data){
  return request({
    url: `${config.api_base_url}/user/authorize`,
    method: 'post',
    data
  })
}

//获取员工列表
export function getuserlist(data){
  return request({
    url: `${config.api_base_url}/user/getuserlist`,
    method: 'get',
    data
  })
}
