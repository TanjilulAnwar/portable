import axios from '../../util/Api';
import showError from '../../util/showError';

export const searchByDate = (date, sales)=>{                            // search by date
  const url = sales?`/sale/history?customer_code=${date.customer_code}&start_date=${date.start_date}&end_date=${date.end_date}`:`/purchase/history?supplier_code=${date.supplier_code}&start_date=${date.start_date}&end_date=${date.end_date}`
  return new Promise((resolve, reject)=>{
    axios.get(url)
    .then(resp => resolve(resp.data))
    .catch(err=>{
			showError(err)
      reject()
    })
  })
}

export const searchByInvoice = (invoice, sales)=>{                      // search by invoice
  const url = sales?`/sale/history/single?invoice=${invoice}`:`/purchase/history/single?invoice=${invoice}`
  return new Promise((resolve, reject)=>{
    axios.get(url)
    .then(resp => resolve(resp.data))
    .catch(err=>{
			showError(err)
      reject()
    })
  })
}