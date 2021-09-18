import axios from '../../util/Api';
import showError from '../../util/showError';


export const balanceSheet = (date)=>{                  // balance sheet
  return new Promise((resolve, reject)=>{
    axios.get(`/get/balance_sheet?end_date=${date}`)
    .then(resp => resolve(resp.data))
    .catch(err=>{
			showError(err)
			reject()
    })
  })
}