import axios from '../../util/Api';
import showError from '../../util/showError';


export const passwordChange = (data)=>{                  // Change user password
  return new Promise((resolve, reject)=>{
    axios.post("/Pos/PasswordChange", data)
      .then(resp => resolve(resp.data))
      .catch(err=>{
        showError(err)
        reject()
      })
  })
}