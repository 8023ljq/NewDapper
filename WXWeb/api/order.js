import {config} from '../utils/config.js'
import request from '../utils/http/request.js'

//获取项目列表
export function getclientlist(data){
  return request({
    url: `${config.api_base_url}/user/getclientlist`,
    method: 'post',
    data
  })
}

//获取项目详情
export function getdetails(data){
  return request({
    url: `${config.api_base_url}/user/getdetails`,
    method: 'get',
    data
  })
}

//添加项目
export function addclient(data){
  return request({
    url: `${config.api_base_url}/user/addclient`,
    method: 'post',
    data
  })
}

//添加需求
export function addfollow(data){
  return request({
    url: `${config.api_base_url}/user/addfollow`,
    method: 'post',
    data
  })
}

//修改项目
export function updateclient(data){
  return request({
    url: `${config.api_base_url}/user/updateclient`,
    method: 'post',
    data
  })
}