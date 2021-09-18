import React from 'react';
import {TextField} from '@material-ui/core';
import {PrintButton} from 'components/PDF';


const FromToForm = ({print_url, allow_no_start})=>{
  const date = new Date()
  const today = `${date.getFullYear()}-${('0'+(date.getMonth()+1)).slice(-2)}-${('0'+(date.getDate())).slice(-2)}`
  const [form_inputs, setInputs] = React.useState({end_date: today})

  const handleChange = (e)=>{
    setInputs({...form_inputs, [e.target.name]: e.target.value})
  }

  return(
    <div className="card p-2">
      <div className="form-row">
        <div className="col-md-3">
          <TextField
            label="From"
            name="start_date"
            type="date"
            variant="outlined"
            size="small"
            fullWidth
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
        <div className="col-xl-1 col-sm-2">
          <PrintButton
            disabled={!allow_no_start && !form_inputs.start_date}
            url={`${print_url}?start_date=${form_inputs.start_date}&end_date=${form_inputs.end_date}`}
          />
        </div>
      </div>
    </div>
  )
}

export default FromToForm;