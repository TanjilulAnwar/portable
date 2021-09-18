import React from 'react';
import {TextField} from '@material-ui/core';
import {PrintButton} from 'components/PDF';
import SupplierSearch from 'components/input/SupplierSearch';


const FromToForm = ({print_url})=>{
  const date = new Date()
  const today = `${date.getFullYear()}-${('0'+(date.getMonth()+1)).slice(-2)}-${('0'+(date.getDate())).slice(-2)}`
  const [form_inputs, setInputs] = React.useState({end_date: today})

  const handleChange = (e)=>{
    setInputs({...form_inputs, [e.target.name]: e.target.value})
  }


  return(
    <div className="card p-3">
      <div className="form-row">
        <div className="col-md-4">
          <SupplierSearch
            name="supplier_code"
            label="Supplier"
            required
            handleInputs={handleChange}
          />
        </div>
        <div className="col-md-3">
          <TextField
            label="From"
            name="start_date"
            type="date"
            variant="outlined"
            size="small"
            fullWidth
            required
            onChange={handleChange}
            defaultValue={form_inputs.start_date}
            InputLabelProps={{shrink: true}}
          />
        </div>
        <div className="col-md-3">
          <TextField
            label="To"
            name="end_date"
            type="date"
            variant="outlined"
            size="small"
            fullWidth
            onChange={handleChange}
            defaultValue={form_inputs.end_date}
            InputLabelProps={{shrink: true}}
          />
        </div>
        <div className="col">
          <PrintButton
            disabled={!form_inputs.start_date || !form_inputs.supplier_code}
            url={`${print_url}?start_date=${form_inputs.start_date}&end_date=${form_inputs.end_date}&supplier_code=${form_inputs.supplier_code}`}
          />
        </div>
      </div>
    </div>
  )
}

export default FromToForm;