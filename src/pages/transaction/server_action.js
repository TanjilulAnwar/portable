import axios from 'util/Api';
import showError from 'util/showError';


export const accountsHeadList = (type)=>{                  // get account head list
  return new Promise((resolve, reject)=>{
    axios.get(`/accountshead/entry_point?type=${type}`)
      .then(resp => resolve(resp.data))
      .catch(err=>{
        showError(err)
        reject()
      })
  })
}

export const postTransaction = (type, form_inputs)=>{                  // post transaction form
  const url = type==='E' ? '/accounts/payment/submit' : '/accounts/receipt/submit'
  return new Promise((resolve, reject)=>{
    axios.post(url, form_inputs)
      .then(resp => resolve(resp.data))
      .catch(err=>{
        showError(err)
        reject()
      })
  })
}

export const transactionHistory = (type)=>{                       // payment and receipt transaction history
  const url = type==='E' ? '/accounts/payment/list' : '/accounts/receipt/list'
  return new Promise((resolve, reject)=>{
    axios.get(url)
      .then(resp => resolve(resp.data))
      .catch(err=>{
        showError(err)
        reject()
      })
  })
}

export const postContra = (form_inputs)=>{                      // contra entry
  const url = '/Accounts/Contra'
  return new Promise((resolve, reject)=>{
    axios.post(url, form_inputs)
      .then(resp => resolve(resp.data))
      .catch(err=>{
        showError(err)
        reject()
      })
  })
}

export const contraHistory = ()=>{                           // contra history
  const url = '/accounts/contralist'
  return new Promise((resolve, reject)=>{
    axios.get(url)
      .then(resp => resolve(resp.data))
      .catch(err=>{
        showError(err)
        reject()
      })
  })
}

export const checkAvailable = (id)=>{                           // check available balance
  const url = `/Accountshead/AvailableBalance?account_head_id=${id}`
  return new Promise((resolve, reject)=>{
    axios.get(url)
      .then(resp => resolve(resp.data))
      .catch(err=>{
        showError(err)
        reject()
      })
  })
}

export const postJournal = (form_inputs)=>{                      // Journal entry
  const url = '/accounts/journal'
  return new Promise((resolve, reject)=>{
    axios.post(url, form_inputs)
      .then(resp => resolve(resp.data))
      .catch(err=>{
        showError(err)
        reject()
      })
  })
}

export const journalMonth = (month)=>{                           // journal history
  const url = `/accounts/journal/history?history_month=${month}`
  return new Promise((resolve, reject)=>{
    axios.get(url)
      .then(resp => resolve(resp.data))
      .catch(err=>{
        showError(err)
        reject()
      })
  })
}