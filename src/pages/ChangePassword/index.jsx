import React from 'react';
import {TextField, Button} from '@material-ui/core';
import SettingsIcon from '@material-ui/icons/Settings';
import './style.css';
import {passwordChange} from './server_action';
import cogoToast from 'cogo-toast';
import { useUserDispatch, signOut } from "../../context/UserContext";
import { DataSaving } from 'components/loading/DataSaving';


const ResetPassword = (props)=>{
  const [passwords, setPasswords] = React.useState({password2: ''})
  const [pass1error, setPass1Error] = React.useState(false)
  const [loading, setLoading] = React.useState(false)
  let dispatch = useUserDispatch();

  // eslint-disable-next-line
  var strongRegex = new RegExp("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})");
  // var mediumRegex = new RegExp("^(((?=.*[a-z])(?=.*[A-Z]))|((?=.*[a-z])(?=.*[0-9]))|((?=.*[A-Z])(?=.*[0-9])))(?=.{6,})");

  const handleChange = (e)=>{
    if(e.target.name === 'password1'){
      setPass1Error(!strongRegex.test(e.target.value))
    }
    setPasswords({...passwords, [e.target.name]: e.target.value})
  }

  const handleSave = ()=>{
    setLoading(true)
    passwordChange(passwords)
      .then(resp => {
        if(resp.success){
          cogoToast.success('Password has been changed!')
          signOut(dispatch, props.history)
        } else cogoToast.error(resp.message)
      })
      .finally(()=>setLoading(false))
  }


  const pass2error = passwords.password2.length>0 && passwords.password1 && passwords.password1!==passwords.password2
  const btnDis = !passwords.old_password || !passwords.password1 || !passwords.password2 || pass2error || pass1error
  return(
    <div className="app-wrapper">
      <h5 className="text-center font-weight-bold mb-3">Reset Password</h5>
      <div className="row">
        <div className="col"/>
        <div className="col">
          <dic className="reset-card p-4">
            <TextField
              label="Current Password"
              name="old_password"
              type="password"
              variant="outlined"
              size="small"
              margin="normal"
              fullWidth
              required
              onChange={handleChange}
            />
            <TextField
              label="New Password"
              name="password1"
              type="password"
              variant="outlined"
              size="small"
              margin="normal"
              fullWidth
              required
              onChange={handleChange}
              error={pass1error}
            />
            <TextField
              label="Confirm Password"
              name="password2"
              type="password"
              variant="outlined"
              size="small"
              margin="normal"
              fullWidth
              required
              onChange={handleChange}
              error={pass2error}
              onKeyPress={(e)=>e.key==='Enter' && !btnDis && handleSave()}
            />

            <Button 
              variant="contained"
              size="small"
              color="primary"
              className="mt-3"
              disabled={btnDis || loading}
              onClick={handleSave}
            >
              Change &nbsp; {loading?<DataSaving/>:<SettingsIcon fontSize="small" />}
            </Button>
          </dic>
        </div>
        <div className="col">
          <strong>The password must contain</strong>
          <ul>
            <li>at least 1 lowercase alphabetical character [a-z]</li>
            <li>at least 1 uppercase alphabetical character [A-Z]</li>
            <li>at least 1 numeric character [0-9]</li>
            <li>at least one special character</li>
            <li>must be eight characters or longer</li>
          </ul>
        </div>
      </div>
    </div>
  )
}

export default React.memo(ResetPassword);