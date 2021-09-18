import React, {useCallback} from 'react';
import {useHistory} from 'react-router-dom';
import {TextField, Button, FormControl, InputLabel, Select, MenuItem, OutlinedInput, InputAdornment, IconButton} from '@material-ui/core';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPercent, faDollarSign } from '@fortawesome/free-solid-svg-icons'
import { DataSaving } from 'components/loading/DataSaving';
import { AnimateOnChange } from 'react-animation';
import {PrintButton} from 'components/PDF';
import SyncRoundedIcon from '@material-ui/icons/SyncRounded';
import {transactionMedia} from 'pages/configuration/server_action';


let RESET = 1

export default React.memo(({form_inputs, handleInputs, handleSave, saving, saved, invoice})=>{
  const history = useHistory()
  const [head_list, setHeadList] = React.useState([])

  React.useEffect(()=>{
    transactionMedia()
      .then(resp => setHeadList(resp.query))
  }, [])

  const handleReset = useCallback(()=>{
    history.replace(history.location.pathname, {reset: RESET++})
  }, [history])

  const handleDiscountType = useCallback(()=>{
    let event = {target: {name: 'percent', value: !form_inputs.percent}}
    handleInputs(event)
  }, [form_inputs.percent, handleInputs])

  return(
    <div className="card py-2 px-3">
      <div className="form-row d-flex justify-content-center">
        <div className="col-md-2 col-sm-3">
          <FormControl variant="outlined" size="small" margin="dense">
            <InputLabel htmlFor="discount">Discount</InputLabel>
            <OutlinedInput
              id="discount"
              name="discount"
              type="number"
              onChange={handleInputs}
              endAdornment={
                <InputAdornment position="end">
                  <IconButton
                    onClick={handleDiscountType}
                    edge="end"
                    color="secondary"
                  >
                    {form_inputs.percent ? <FontAwesomeIcon className="_heartbeat" icon={faPercent}/> : <FontAwesomeIcon className="_heartbeat" icon={faDollarSign}/>}
                  </IconButton>
                </InputAdornment>
              }
              labelWidth={70}
            />
          </FormControl>
        </div>
        <div className="col-md-2 col-sm-3">
          <TextField                                     // Payment
            label="Payment"
            name="payment"
            type="number"
            fullWidth
            variant="outlined"
            size='small'
            margin="dense"
            value={form_inputs.payment?form_inputs.payment:''}
            onChange={handleInputs}
            // InputLabelProps={{shrink: true}}
          />
        </div>
        <div className="col-md-2 col-sm-3">
          <FormControl fullWidth size="small" margin="dense" variant="outlined" required>
            <InputLabel id="account_head_id-label">Receive By</InputLabel>
            <Select
              labelId="account_head_id-label"
              name="account_head_id"
              label="Receive By"
              defaultValue={form_inputs.account_head_id}
              onChange={handleInputs}
            >
              {head_list.map(val => (
                <MenuItem key={val.ac_head_id} value={val.ac_head_id}>{val.ac_head_name}</MenuItem>
              ))}
            </Select>
          </FormControl>
        </div>
      </div>
      <div className="row my-3">
        <div className="col text-right">
          <span>Grand Total: </span>
          <strong className="text-success h4 text-monospace">
            <AnimateOnChange animationIn="bounceIn" animationOut="bounceOut" durationOut={300}>
              {form_inputs.grand_total ? form_inputs.grand_total.toLocaleString() : 0}
            </AnimateOnChange>
          </strong>
        </div>
        <div className="col">
          <span>Due: </span>
          <strong className="text-danger h4 text-monospace">
            <AnimateOnChange animationIn="bounceIn" animationOut="bounceOut" durationOut={300}>
              {form_inputs.grand_total ? (form_inputs.grand_total - (form_inputs.payment?form_inputs.payment:0)).toLocaleString() : 0}
            </AnimateOnChange>
          </strong>
        </div>
      </div>
      <div className="text-center">
        {!saved
          ? <Button className="w-25" variant="contained" size="small" color="primary" onClick={handleSave} disabled={saving}>
              Submit {saving && <DataSaving/>}
            </Button>
          : <div>
              <PrintButton url={`/print/receipt?invoice=${invoice}`}/>
              <Button className="ml-2" variant="contained" size="small" color="primary" onClick={handleReset} disabled={saving}>
                Reset <SyncRoundedIcon fontSize="small"/>
              </Button>
            </div>
        }
      </div>
    </div>
  )
})