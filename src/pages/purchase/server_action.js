import axios from '../../util/Api';
import showError from '../../util/showError';


export const supplierSearch = (val)=>{                            // supplier Search
  return new Promise((resolve, reject)=>{
    axios.get(`/Supplier/search?search_string=${val}`)
    .then(resp => resolve(resp.data))
    .catch(err=>{
			showError(err)
      reject()
    })
  })
}


export const purchaseConfirm = (form_inputs)=>{                         // purchase Confirm
  return new Promise((resolve, reject)=>{
    axios.post("/Purchase/confirm", form_inputs)
    .then(resp => resolve(resp.data))
    .catch(err=>{
			showError(err)
      reject()
    })
  })
}
