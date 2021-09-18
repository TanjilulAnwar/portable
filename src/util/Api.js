import axios from 'axios';

// const url = 'http://192.168.0.102:44319'
const url = 'http://116.193.218.147:44319'

export default axios.create({
  baseURL: url,
  headers: {
    accept: 'application/json'
  }
});

export { url }
