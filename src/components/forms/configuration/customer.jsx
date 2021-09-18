import React from 'react';
import {
  TextField,
  Button,
  FormControl,
  InputLabel,
  MenuItem,
  Select
} from '@material-ui/core';
import cogoToast from 'cogo-toast';
import {customer_require} from './require';
import checkValidation from '../../../util/checkValidation';
import { DataSaving } from '../../loading/DataSaving';
import { postCustomerInfo } from '../../../pages/configuration/server_action';
var address = require('../../../util/area.json');


// eslint-disable-next-line
export default React.memo(({updateList, update, customer_info, handleClose})=>{
  const [form_inputs, setFormInputs] = React.useState(update?{...customer_info}:{customer_type: true})
  const [requires, setRequireFields] = React.useState(customer_require)
  const division_list = address.message
  const [district_list, setDistrictList] = React.useState([])
  const [thana_list, setThanaList] = React.useState([])
  const [saving, setSaving] = React.useState(false)

  React.useEffect(()=>{
    if(update && customer_info.division){
      const dis_list = division_list.filter(val=>val.name === form_inputs.division)[0].districts
      setDistrictList(dis_list)
      setThanaList(dis_list.filter(val=>val.name === form_inputs.district)[0].thanas)
    }
    // eslint-disable-next-line
  }, [])

  const handleChange = React.useCallback((e)=>{
    if(e.target.name === 'division'){
      setDistrictList(division_list.filter(val=>val.name === e.target.value)[0].districts)
      setFormInputs({...form_inputs, [e.target.name]: e.target.value, thana: null, district: null})
      return
    } else if(e.target.name === 'district') {
      setThanaList(district_list.filter(val=>val.name === e.target.value)[0].thanas)
      setFormInputs({...form_inputs, [e.target.name]: e.target.value, thana: null})
      return
    }
    setFormInputs({...form_inputs, [e.target.name]: e.target.value})
  }, [division_list, district_list, form_inputs])

  const handleSave = React.useCallback(()=>{
    const {isFormValid, require_fields} = checkValidation(form_inputs, requires)
    if(isFormValid){
      setSaving(true)
      postCustomerInfo(form_inputs)
        .then(resp => {
          cogoToast.success(`Customer ${update?'Updated':'Added'} Successfully`)
          updateList(resp, `${update?'UPDATE':'ADD_NEW'}`)
          setFormInputs({customer_type: true})
        })
        .finally(() => setSaving(false))
    } else cogoToast.error('Please, fill in all required fields!')
    setRequireFields({...require_fields})
  }, [form_inputs, requires, update, updateList])


  return(
    <div>
      <div className="form-row">
        <div className="col-md-7 col-12">
          <TextField
            label="Customer Name"
            variant="outlined"
            margin="dense"
            fullWidth
            placeholder="Customer Name"
            name="name"
            required
            autoFocus
            error={requires.name}
            value={form_inputs.name || ''}
            onChange={handleChange}
          />
        </div>
        <div className="col-md-5 col-12">
          <TextField
            label="Patient ID"
            variant="outlined"
            margin="dense"
            fullWidth
            placeholder="Patient ID"
            name="patient_id"
            value={form_inputs.patient_id || ''}
            onChange={handleChange}
          />
        </div>
      </div>

      <div className="form-row">
        <div className="col-lg-4 col-md-4 col-sm-6">
          <FormControl fullWidth size="small" margin="dense" variant="outlined">
            <InputLabel id="division-label">Division</InputLabel>
            <Select
              labelId="division-label"
              name="division"
              label="Division"
              value={form_inputs.division || ''}
              onChange={handleChange}
            >
              {division_list.map(val=>(
                <MenuItem key={val.id} value={val.name}>{val.name}</MenuItem>
              ))}
            </Select>
          </FormControl>
        </div>
        <div className="col-lg-4 col-md-4 col-sm-6">
          <FormControl fullWidth size="small" margin="dense" variant="outlined">
            <InputLabel id="district-label">District</InputLabel>
            <Select
              labelId="district-label"
              name="district"
              label="District"
              value={form_inputs.district || ''}
              onChange={handleChange}
            >
              {district_list.map(val=>(
                <MenuItem key={val.id} value={val.name}>{val.name}</MenuItem>
              ))}
            </Select>
          </FormControl>
        </div>
        <div className="col-lg-4 col-md-4 col-sm-6">
          <FormControl fullWidth size="small" margin="dense" variant="outlined">
            <InputLabel id="thana-label">Thana</InputLabel>
            <Select
              labelId="thana-label"
              name="thana"
              label="Thana"
              value={form_inputs.thana || ''}
              onChange={handleChange}
            >
              {thana_list.map(val=>(
                <MenuItem key={val} value={val}>{val}</MenuItem>
              ))}
            </Select>
          </FormControl>
        </div>
        <div className="col">
          <TextField
            label="Address"
            variant="outlined"
            margin="dense"
            fullWidth
            multiline
            placeholder="Address"
            name="address"
            required
            error={requires.address}
            value={form_inputs.address || ''}
            onChange={handleChange}
          />
        </div>
      </div>
      <div className="form-row">
        <div className="col-md-4 col-sm-4 col-12">
          <TextField
            label="Mobile"
            variant="outlined"
            margin="dense"
            fullWidth
            type="number"
            placeholder="Mobile"
            name="mobile"
            required
            error={requires.mobile}
            value={form_inputs.mobile || ''}
            onChange={handleChange}
          />
        </div>
        <div className="col-md-4 col-sm-4 col-12">
          <FormControl fullWidth size="small" margin="dense" variant="outlined" required>
            <InputLabel id="customer_type-label">Customer Status</InputLabel>
            <Select
              labelId="customer_type-label"
              name="customer_type"
              label="Customer Status"
              value={form_inputs.customer_type || ''}
              onChange={handleChange}
            >
              <MenuItem value={true}>Active</MenuItem>
              <MenuItem value={false}>Inactive</MenuItem>
            </Select>
          </FormControl>
        </div>
        <div className="col-md-8">
          <TextField
            label="Remarks"
            variant="outlined"
            margin="dense"
            fullWidth
            placeholder="Remarks"
            name="remarks"
            value={form_inputs.remarks || ''}
            onChange={handleChange}
          />
        </div>
      </div>

      <div className="text-center mt-3">
        <Button className="text-capitalize mr-2" variant="outlined" color="primary" size="small" onClick={handleClose} disabled={saving}>
          Cancel
        </Button>
        <Button className="text-capitalize" variant="contained" color="primary" size="small" onClick={handleSave} disabled={saving}>
          Save {saving && <DataSaving/>}
        </Button>
      </div>

    </div>
  )
})