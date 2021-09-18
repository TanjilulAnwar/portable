import React from 'react';
import {
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  TextField,
  IconButton,
  Button
} from '@material-ui/core';
import { AnimateOnChange } from 'react-animation';
import DeleteForeverTwoToneIcon from '@material-ui/icons/DeleteForeverTwoTone';
import AddRoundedIcon from '@material-ui/icons/AddRounded';
import {accountHeadAll} from 'pages/configuration/server_action';     // Should delete 


const AmountFields = React.memo(({type, index, head_list, values, deleteRow, handleAmountChange, require_fields})=>{
  return(
    <div className="row">
      <div className="col-md-6 col-sm-8">
        <FormControl variant="outlined" margin="dense" size="small" fullWidth required error={require_fields?require_fields.ac_head_id:false}>
          <InputLabel id="ac_name_label">Account Name</InputLabel>
          <Select                                   // Account Name
            labelId="ac_name_label"
            name="ac_head_id"
            label="Account Name"
            value={values['ac_head_id']?values['ac_head_id']:''}
            onChange={(e)=>handleAmountChange(type, index, e)}
          >
            {head_list && head_list.map(val=>(
              <MenuItem key={val.ac_head_id} value={val.ac_head_id}>
                {val.ac_head_name}
              </MenuItem>
            ))}
          </Select>
        </FormControl>
      </div>
      <div className="col-md-4 col-sm-4">
        <TextField                                    // amount
          name="amount"
          label="Amount"
          type="number"
          variant="outlined"
          size="small"
          margin="dense"
          fullWidth
          required
          error={require_fields?require_fields.amount:false}
          value={values['amount']?values['amount']:''}
          onChange={e=>handleAmountChange(type, index, e)}
        />
      </div>
      {index !==0 &&
        <div className="col d-flex aligh-items-center">
          <IconButton onClick={()=>deleteRow(index, type)}>
            <DeleteForeverTwoToneIcon color="error"/>
          </IconButton>
        </div>
      }
    </div>
  )
})



const AmountSection = ({setAmountList, require_fields})=>{
  const [head_list, setHeadList] = React.useState([])
  const [debit_rows, setDebitRow] = React.useState([{}])
  const [credit_rows, setCreditRow] = React.useState([{}])

  React.useEffect(()=>{
    accountHeadAll()
      .then(resp => resp.success && setHeadList(resp.message))
  }, [])

  const handleAmountChange = (type, index, event)=>{
    let rows = type === 'DEBIT' ? debit_rows : credit_rows
    let setRows = type === 'DEBIT' ? setDebitRow : setCreditRow
    rows[index][event.target.name] = event.target.name==='amount'?parseInt(event.target.value):event.target.value
    setAmountList(type, [...rows])
    setRows([...rows])
  }

  const deleteRow = (index, type)=>{
    let rows = type === 'DEBIT' ? debit_rows : credit_rows
    let setRows = type === 'DEBIT' ? setDebitRow : setCreditRow
    rows.splice(index, 1)
    setAmountList(type, [...rows])
    setRows([...rows])
  }


  return(
    <div className="row">
      <din className="col border-right">
        <div className="d-flex justify-content-between">
          <em><h5 className="text-primary font-weight-bold">Debit</h5></em>
          <Button color="primary" size="small" variant="contained" onClick={()=>setDebitRow([...debit_rows, {}])}>
            <AddRoundedIcon fontSize="small"/> Add
          </Button>
        </div>
        {debit_rows.map((values, index)=>(
          <AmountFields
            key={index}
            index={index}
            values={values}
            type="DEBIT"
            head_list={head_list}
            deleteRow={deleteRow}
            handleAmountChange={handleAmountChange}
            require_fields={require_fields['journal_debit'][index]}
          />
        ))}
        <Total amount_list={debit_rows}/>
      </din>
      <din className="col">
        <div className="d-flex justify-content-between">
          <em><h5 className="text-primary font-weight-bold">Credit</h5></em>
          <Button color="primary" size="small" variant="contained" onClick={()=>setCreditRow([...credit_rows, {}])}>
            <AddRoundedIcon fontSize="small"/> Add
          </Button>
        </div>
        {credit_rows.map((values, index)=>(
          <AmountFields
            key={index}
            index={index}
            values={values}
            type="CREDIT"
            head_list={head_list}
            deleteRow={deleteRow}
            handleAmountChange={handleAmountChange}
            require_fields={require_fields['journal_credit'][index]}
          />
        ))}
        <Total amount_list={credit_rows}/>
      </din>
    </div>
  )
}

export default React.memo(AmountSection);


const calculateTotal = (list)=>{
  let total = 0
  list.forEach((val) => total+=val.amount?val.amount:0)
  return total
}

const Total = React.memo(({amount_list})=>{
  const total = calculateTotal(amount_list)
  return(
    <React.Fragment>
      <hr className="mb-0 mt-1"/>
      <div className="row my-2">
        <div className="col-md-6 d-flex align-items-center justify-content-end">
          <h6 className="m-0">Total :</h6>
        </div>
        <div className="col-md-4">
          <span className="text-secondary text-monospace lead h3">
            <AnimateOnChange animationIn="bounceIn" animationOut="bounceOut" durationOut={300}>
              {total ? total.toLocaleString() : 0}
            </AnimateOnChange>
          </span>
        </div>
      </div>
    </React.Fragment>
  )
})