import axios from '../../util/Api';
import showError from '../../util/showError';

export const returnProduct = (data, supp)=>{                     // return product information
  const url = supp ? '/purchase/return' : '/sales/return'
  return new Promise((resolve, reject)=>{
    axios.post(url, data)
    .then(resp => resolve(resp.data))
    .catch(err=>{
			showError(err)
      reject()
    })
  })
}
