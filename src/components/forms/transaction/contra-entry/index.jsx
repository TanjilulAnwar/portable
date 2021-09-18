import React from 'react';
import {useHistory, Link} from 'react-router-dom';
import {
  TextField,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  Button,
  Fab
} from '@material-ui/core';
import SendIcon from '@material-ui/icons/Send';
import HistoryRoundedIcon from '@material-ui/icons/HistoryRounded';
import { DataSaving } from 'components/loading/DataSaving';
import checkValidation from 'util/checkValidation';
import {transactionMedia} from 'pages/configuration/server_action';
import {postContra, checkAvailable} from 'pages/transaction/server_action';
import cogoToast from 'cogo-toast';


const ContraForm = ()=>{
  const date = new Date()
  const today = `${date.getFullYear()}-${('0'+(date.getMonth()+1)).slice(-2)}-${('0'+(date.getDate())).slice(-2)}`
  const [form_inputs, setFormInputs] = React.useState({entry_date: today})
  const [required, setRequired] = React.useState({ac_head_dr: false, ac_head_cr: false, amount: false})
  const [available_balance, setAvailable] = React.useState({})
  const [head_list, setAcHeadList] = React.useState([])
  const [loading, setLoading] = React.useState(false)
  const history = useHistory()

  React.useEffect(()=>{
    transactionMedia()
      .then(resp => setAcHeadList(resp.query))
  }, [])

  const handleChange = React.useCallback((e)=>{
    if(e.target.name==='ac_head_cr' || e.target.name==='ac_head_dr'){
      checkAvailable(e.target.value)
        .then(resp => resp.success && setAvailable({...available_balance, [e.target.name]: resp.message.available_balance}))
    }
    setFormInputs({...form_inputs, [e.target.name]: e.target.name==='amount'?parseInt(e.target.value):e.target.value})
  }, [form_inputs, available_balance])

  const handleSave = ()=>{
    const {require_fields, isFormValid} = checkValidation(form_inputs, required)
    setRequired({...require_fields})
    if(isFormValid){
      setLoading(true)
      postContra(form_inputs)
        .then(resp => {
          if(resp.success){
            cogoToast.success('Transection Successful')
            history.replace(history.location.pathname)
          } else cogoToast.error(resp.message)
        })
        .finally(()=>setLoading(false))
    }
  }

  return(
    <div className="card px-3 py-2">
      <div className="form-row">
        <div className="col-md-3 col-sm-6 col-12">
          <TextField                                    // Transfer Date
            name="entry_date"
            required
            label="Transfer Date"
            type="date"
            variant="outlined"
            size="small"
            margin="dense"
            fullWidth
            value={form_inputs.entry_date}
            onChange={handleChange}
            InputLabelProps={{shrink: true}}
          />
        </div>
        <div className="col-md-3 col-sm-6 col-12">
          <FormControl variant="outlined" margin="dense" size="small" fullWidth required error={required.ac_head_cr}>
            <InputLabel id="transfer_from_label">Transfer From</InputLabel>
            <Select                                   // Transfer from
              labelId="transfer_from_label"
              name="ac_head_cr"
              label="Transfer From"
              value={form_inputs.ac_head_cr}
              onChange={handleChange}
            >
              {head_list.map(val => (
                <MenuItem key={val.ac_head_id} value={val.ac_head_id}>{val.ac_head_name}</MenuItem>
              ))}
            </Select>
          </FormControl>
          {!!available_balance.ac_head_cr &&
            <em className="text-muted">Available Balance: <strong>{available_balance.ac_head_cr}</strong> BDT</em>
          }
        </div>
        <div className="col-md-3 col-sm-6 col-12">
          <FormControl variant="outlined" margin="dense" size="small" fullWidth required error={required.ac_head_dr}>
            <InputLabel id="transfer_to_label">Transfer To</InputLabel>
            <Select                                   // Transfer To
              labelId="transfer_to_label"
              name="ac_head_dr"
              label="Transfer To"
              value={form_inputs.ac_head_dr}
              onChange={handleChange}
            >
              {head_list.map(val => (
                <MenuItem key={val.ac_head_id} value={val.ac_head_id}>{val.ac_head_name}</MenuItem>
              ))}
            </Select>
          </FormControl>
          {!!available_balance.ac_head_dr &&
            <em className="text-muted">Available Balance: <strong>{available_balance.ac_head_dr}</strong> BDT</em>
          }
        </div>
        <div className="col-md-3 col-sm-6 col-12">
          <TextField                                    // amount
            label="Amount"
            name="amount"
            required
            type="number"
            variant="outlined"
            size="small"
            margin="dense"
            fullWidth
            error={required.amount}
            value={form_inputs.amount}
            onChange={handleChange}
          />
        </div>
      </div>
      <div className="form-row">
        <div className="col-md-5 col-12">
          <TextField                                    // Reference Number
            label="Reference Number"
            placeholder="Enter Reference Number"
            name="reference"
            variant="outlined"
            size="small"
            margin="dense"
            fullWidth
            value={form_inputs.reference}
            onChange={handleChange}
          />
        </div>
        <div className="col-md-7 col-12">
          <TextField                                    // Description
            label="Description"
            placeholder="Write your contra description"
            name="description"
            variant="outlined"
            size="small"
            margin="dense"
            multiline
            fullWidth
            value={form_inputs.description}
            onChange={handleChange}
          />
        </div>
      </div>

      <div className="form-row">
        <div className="col"/>
        <div className="col">
          <div className="d-flex justify-content-center mt-3">
            <Button size="small" color="primary" variant="contained" onClick={handleSave} disabled={loading}>
              Submit &nbsp; {loading?<DataSaving/>:<SendIcon fontSize="small"/>}
            </Button>
          </div>
        </div>
        <div className="col d-flex justify-content-end align-items-center">
          <Link to="/transaction/contra-entry/c-history">
            <Fab size="small">
              <HistoryRoundedIcon color="primary" />
            </Fab>
          </Link>
        </div>
      </div>
    </div>
  )
}

export default React.memo(ContraForm);