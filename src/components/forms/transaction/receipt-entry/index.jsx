import React from 'react';
import { Link, useHistory } from 'react-router-dom';
import {
  TextField,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  Button,
  IconButton,
  Fab
} from '@material-ui/core';
import CustomerSearch from "components/input/CustomerSearch";
import AddRoundedIcon from '@material-ui/icons/AddRounded';
import ClearRoundedIcon from '@material-ui/icons/ClearRounded';
import HistoryRoundedIcon from '@material-ui/icons/HistoryRounded';
import { AnimateOnChange } from 'react-animation';
import {PrintButton} from 'components/PDF';
import {DataSaving} from 'components/loading/DataSaving';
import {accountsHeadList, postTransaction} from 'pages/transaction/server_action';
import cogoToast from 'cogo-toast';
import {url} from 'util/Api';
import {transactionMedia} from 'pages/configuration/server_action';



const ReceiptFor = React.memo((props)=>{
  const {index, receipt_number, handleChange, handleAddOne, handleRemoveOne, require_fields, form_inputs, receipt_for} = props
  const [ac_name_list, setAcList] = React.useState([])
  const [ac_name, setAcName] = React.useState('')

  React.useEffect(()=>{
    setAcName(form_inputs[index].receipt_name_id)
    if(!!form_inputs[index].receipt_head){
      const name_list = receipt_for.find(val => val.ac_head_id === form_inputs[index].receipt_head)
      setAcList(name_list.ac_name_list)
    }
  // eslint-disable-next-line
  }, [form_inputs[index].receipt_head, receipt_for, index])

  const handleAccountName = React.useCallback((e)=>{
    setAcName(e.target.value)
    handleChange(e, index)
  }, [handleChange, index])

  const manageCustomerInfo = React.useCallback((info)=>{
    let e_cus = {target: {name: 'customer_info', value: info}}
    let e = {target: {name: 'amount', value: info?info.due:null}}
    handleChange(e, index)
    handleChange(e_cus, index)
  }, [index, handleChange])


  return(
    <div className="form-row">
      <div className="col-md-3 col-sm-6">
        <FormControl variant="outlined" margin="dense" size="small" fullWidth required error={require_fields[index].receipt_head}>
          <InputLabel id="receipt_for_label">Receipt Head</InputLabel>
          <Select                                   // Receipt For
            labelId="receipt_for_label"
            name="receipt_head"
            label="Receipt Head"
            value={form_inputs[index]['receipt_head']?form_inputs[index]['receipt_head']:''}
            onChange={(e)=>handleChange(e, index)}
          >
            {receipt_for && receipt_for.map(val=>(
              <MenuItem key={val.ac_head_id} value={val.ac_head_id}>
              {/* <MenuItem key={val.ac_head_id} value={val.ac_head_id} disabled={form_inputs.map(inpt => inpt.receipt_head).indexOf(val.ac_head_id) !== -1 ? true : false}> */}
                {val.ac_head_name}
              </MenuItem>
            ))}
          </Select>
        </FormControl>
      </div>
      <div className="col-md-3 col-sm-6">
        <FormControl variant="outlined" margin="dense" size="small" fullWidth required error={require_fields[index].receipt_name_id}>
          <InputLabel id="ac_name_label">Account Name</InputLabel>
          <Select                                              // Account Name
            labelId="ac_name_label"
            name="receipt_name_id"
            label="Account Name"
            value={ac_name?ac_name:''}
            onChange={handleAccountName}
          >
            {ac_name_list && ac_name_list.map(val=>(
              <MenuItem key={val.ac_head_id} value={val.ac_head_id}>
                {val.ac_head_name}
              </MenuItem>
            ))}
          </Select>
        </FormControl>
      </div>
      {form_inputs[index]['receipt_head'] === "11010000" &&
        <div className="col-md-3 col-sm-4">
          <CustomerSearch                                    // Customer search
            label="Customer"
            name="customer_code"
            margin="dense"
            required
            defaultValue={form_inputs[index]['customer_info']}
            setCustomerInfo={manageCustomerInfo}
            handleInputs={e=>handleChange(e, index)}
          />
          {form_inputs[index]['customer_info'] &&
            <div className="text-center">
              <em><strong className="text-muted">{form_inputs[index]['customer_info'].name}</strong></em>
              <hr className="m-0"/>
              <strong className="text-muted">{form_inputs[index]['customer_info'].mobile}</strong>
            </div>
          }
        </div>
      }
      <div className="col-md-2 col-sm-4">
        <TextField                                    // amount
          label="Amount"
          name="amount"
          type="number"
          variant="outlined"
          size="small"
          margin="dense"
          fullWidth
          required
          error={require_fields[index].amount}
          value={form_inputs[index]['amount']?form_inputs[index]['amount']:''}
          onChange={e=>handleChange(e, index)}
        />
      </div>
      {form_inputs[index]['receipt_head'] !== "11010000" &&
        <div className="col-md-3 col-sm-4">
          <TextField                                    // Description
            name="description"
            label="Description"
            variant="outlined"
            size="small"
            margin="dense"
            fullWidth
            value={form_inputs[index]['description']?form_inputs[index]['description']:''}
            onChange={e=>handleChange(e, index)}
          />
        </div>
      }
      <div className="col-1 d-flex justify-content-center align-items-center">
        {index !==0 &&
          <IconButton className="p-1" onClick={()=>handleRemoveOne(index)}>
            <ClearRoundedIcon color="error"/>
          </IconButton>
        }
        {receipt_number === index+1 &&
          <IconButton className="p-1 _heartbeat" onClick={handleAddOne}>
            <AddRoundedIcon fontSize="large" color="primary"/>
          </IconButton>
        }
      </div>
    </div>
  )
})


const checkValidation = (form_inputs, require_fields)=>{
  let is_valid = false
  require_fields['account_head_id'] =  form_inputs.account_head_id ? false : true
  form_inputs['pay_for'].forEach((pay, i) => {
    require_fields['pay_for'][i]['receipt_head'] = pay.receipt_head ? false: true
    require_fields['pay_for'][i]['amount'] = pay.amount ? false: true
  })
  is_valid = !require_fields['account_head_id']
  if(!is_valid) return {is_valid, requires: require_fields}
  require_fields['pay_for'].forEach((_, i) => {
    is_valid = !require_fields['pay_for'][i]['receipt_head'] && !require_fields['pay_for'][i]['amount']
    if(!is_valid) return {is_valid, requires: require_fields}
  })
  return {is_valid, requires: require_fields}
}


export default React.memo(() =>{
  const date = new Date()
  const today = `${date.getFullYear()}-${('0'+(date.getMonth()+1)).slice(-2)}-${('0'+(date.getDate())).slice(-2)}`
  const [form_inputs, setFormInputs] = React.useState({receipt_date: today, pay_for: [{}]})
  const [receipt_number, setReceiptNumber] = React.useState(1)
  const [receipt_for_list, setReceiptForList] = React.useState([])
  const [require_fields, setRequireFields] = React.useState({account_head_id: false, pay_for: [{}]})
  const [head_list, setHeadList] = React.useState([])
  const [total_amount, setTotalAmount] = React.useState(0)
  const [saving, setSaving] = React.useState(false)
  const [saved, setSaved] = React.useState(false)
  const [voucher, setVoucher] = React.useState('')
  const history = useHistory()

  React.useEffect(()=>{
    accountsHeadList('I')
      .then(resp => setReceiptForList(resp.query))
    transactionMedia()
      .then(resp => setHeadList(resp.query))
  }, [])

  const handleChange = (e, index)=>{
    if(e.target.name==='receipt_head' || e.target.name==='receipt_name_id' || e.target.name==='amount'|| e.target.name==='customer_code' || e.target.name==='customer_info' || e.target.name==='description'){
      let old = [...form_inputs.pay_for]
      old[index][e.target.name] = e.target.name === 'amount' ? parseFloat(e.target.value) : e.target.value
      if(e.target.name==='receipt_head'){
        old[index]['receipt_name_id'] = receipt_for_list.find(val => val.ac_head_id === e.target.value).ac_name_list[0]['ac_head_id']
      }
      setFormInputs({...form_inputs, pay_for: old})
      if(e.target.name === 'amount'){
        setTotalAmount(calculate_total(old))
      }
    } else setFormInputs({...form_inputs, [e.target.name]: e.target.value})
  }

  const handleAddOne = React.useCallback(()=>{                  // add new entry
    setReceiptNumber(receipt_number+1)
    let new_field = [...form_inputs.pay_for]
    let new_require = [...require_fields.pay_for]
    new_field.push({})
    new_require.push({})
    setRequireFields({...require_fields, pay_for: new_require})
    setFormInputs({...form_inputs, pay_for: new_field})
  }, [receipt_number, form_inputs, require_fields])

  const handleRemoveOne = React.useCallback((index)=>{            // remove a entry
    setReceiptNumber(receipt_number-1)
    let remove_field = [...form_inputs.pay_for]
    let remove_require = [...require_fields.pay_for]
    remove_field.splice(index, 1)
    remove_require.splice(index, 1)
    setFormInputs({...form_inputs, pay_for: remove_field})
    setRequireFields({...require_fields, pay_for: remove_require})
    setTotalAmount(calculate_total(remove_field))
  }, [form_inputs, require_fields, receipt_number])

  const handleSave = ()=>{                                          // save
    const {is_valid, requires} = checkValidation(form_inputs, require_fields)
    setRequireFields({...requires})
    if(is_valid){
      setSaving(true)
      postTransaction('I', form_inputs)
        .then(resp => {
          if(resp.success){
            cogoToast.success('Receipt Entry Successful')
            setVoucher(resp.voucher_id)
            setSaved(true)
          } else cogoToast.warn(resp.message)
        })
        .finally(()=>setSaving(false))
    } else cogoToast.error('Please fillup required fields')
  }

  const handleReset = ()=> history.replace(history.location.pathname)

  return(
    <div className="card p-3">
      <div className="form-row">
        <div className="col-xl-2 col-md-3 col-sm-4 col-12">
          <TextField                                    // Receipt Date
            name="receipt_date"
            required
            label="Receipt Date"
            type="date"
            variant="outlined"
            size="small"
            margin="dense"
            fullWidth
            defaultValue={form_inputs.receipt_date}
            onChange={handleChange}
            InputLabelProps={{shrink: true}}
          />
        </div>
        <div className="col-md-3 col-sm-6 col-12">
          <FormControl variant="outlined" margin="dense" size="small" fullWidth required error={require_fields.account_head_id}>
            <InputLabel id="receipt_by_label">Receipt By</InputLabel>
            <Select                                   // Receipt By
              labelId="receipt_by_label"
              name="account_head_id"
              label="Receipt By"
              defaultValue={form_inputs.account_head_id}
              onChange={handleChange}
            >
              {head_list.map(val => (
                <MenuItem key={val.ac_head_id} value={val.ac_head_id}>{val.ac_head_name}</MenuItem>
              ))}
            </Select>
          </FormControl>
        </div>
        <div className="col-xl-2 col-md-3 col-sm-4 col-12">
          <TextField                                    // Referance no
            name="ref_no"
            label="Referance No."
            variant="outlined"
            size="small"
            margin="dense"
            fullWidth
            onChange={handleChange}
          />
        </div>

        <div className="col text-right d-none d-md-block">
          <Link to="/transaction/receipts-entry/p-history">
            <Fab size="small" data-toggle="tooltip" data-placement="top" title="Receipt History">
              <HistoryRoundedIcon color="primary" />
            </Fab>
          </Link>
        </div>
      </div>

      <div className="row">
        <div className="col-lg-9 com-md-7">
          {[...Array(receipt_number).keys()].map(number => (
            <ReceiptFor
              key={number}
              index={number}
              receipt_number={receipt_number}
              handleChange={handleChange}
              handleAddOne={handleAddOne}
              handleRemoveOne={handleRemoveOne}
              require_fields={require_fields['pay_for']}
              form_inputs={form_inputs.pay_for}
              receipt_for={receipt_for_list}
            />
          ))}
        </div>
        <div className="col-lg-3 d-flex justify-content-center align-items-center">
          <Total amount={total_amount}/>
        </div>
      </div>

      <div className="text-right">
        <Link to="/transaction/receipts-entry/r-history" className="d-sm-block d-md-none mx-2">
          <IconButton className="p-2">
            <HistoryRoundedIcon color="primary" />
          </IconButton>
        </Link>
        {saved &&
          <PrintButton url={`${url}/print/receiptVoucher?voucher_id=${voucher}`} />
        }
        <Button className="ml-3" variant="contained" color="primary" size="small" onClick={saved?handleReset:handleSave} disabled={saving}>
          <AnimateOnChange animationIn="bounceIn" animationOut="bounceOut" durationOut={300}>{saved?'Reset':'Save'}</AnimateOnChange> {saving && <DataSaving/>}
        </Button>
      </div>

    </div>
  )}
)


const calculate_total = (amount_list)=>{
  let total = 0
  amount_list.map(val => (total += val.amount? val.amount : 0))
  return total
}

const Total = React.memo(({amount})=>{
  return(
    <div className="text-center m-3">
      <h4 className="font-weight-bold">Total Amount</h4>
      <span className="text-secondary text-monospace lead" style={{fontSize: 35}}>
        <AnimateOnChange animationIn="bounceIn" animationOut="bounceOut" durationOut={300}>
          {amount ? amount.toLocaleString() : 0}
        </AnimateOnChange>
      </span>
    </div>
  )
})