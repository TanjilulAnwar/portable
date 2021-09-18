import axios from '../../util/Api';
import showError from '../../util/showError';


export const willBeExpired = ()=>{                  // will be expired notice
  return new Promise((resolve, reject)=>{
    axios.get('/home/expirednotice')
    .then(resp => resolve(resp.data))
    .catch(err=>{
			showError(err)
			reject()
    })
  })
}

export const expired = ()=>{                    // already expired notice
  return new Promise((resolve, reject)=>{
    axios.get('/home/expired')
    .then(resp => resolve(resp.data))
    .catch(err=>{
			showError(err)
			reject()
    })
  })
}
