import React from 'react';
import {
  TextField,
  Button,
  Chip
} from '@material-ui/core';
import Autocomplete from '@material-ui/lab/Autocomplete';
import cogoToast from 'cogo-toast';
import {category_require} from './require';
import checkValidation from '../../../util/checkValidation';
import { DataSaving } from '../../loading/DataSaving';
import { postCategoryInfo } from '../../../pages/product-configuration/server_action';


export default React.memo(({updateList, update, category_info, handleClose})=>{
  const [form_inputs, setFormInputs] = React.useState(update?{...category_info}:{})
  const [requires, setRequireFields] = React.useState(category_require)
  const [saving, setSaving] = React.useState(false)


  const handleChange = React.useCallback((e, val)=>{
    let inputs = form_inputs
    if(!e.target.name){
      inputs['subcategories'] = val
    } else {
      inputs[e.target.name] = e.target.value
    }
    setFormInputs({...inputs})
  }, [form_inputs])


  const handleSave = React.useCallback(()=>{
    const {isFormValid, require_fields} = checkValidation(form_inputs, requires)
    if(isFormValid){
      setSaving(true)
      postCategoryInfo(form_inputs)
        .then(resp => {
          cogoToast.success(`Generic ${update?'Updated':'Added'} Successfully`)
          updateList(resp, `${update?'UPDATE':'ADD_NEW'}`)
          setFormInputs({})
        })
        .finally(() => setSaving(false))
    } else cogoToast.error('Please, fill in all required fields!')
    setRequireFields({...require_fields})
  }, [form_inputs, requires, update, updateList])


  return(
    <React.Fragment>
      <div className="form-row">
        <div className="col-12">
          <TextField
            label="Generic Name"
            variant="outlined"
            margin="dense"
            fullWidth
            placeholder="Generic Name"
            name="name"
            required
            autoFocus
            error={requires.name}
            value={form_inputs.name || ''}
            onChange={handleChange}
          />
        </div>
      </div>

      <Autocomplete
        options={[]}
        multiple
        freeSolo
        style={{minWidth: 350}}
        defaultValue={form_inputs.subcategories}
        onChange={handleChange}
        renderTags={(value, getTagProps) =>
          value.map((option, index) => (
            <Chip variant="default" size="small" label={option} {...getTagProps({ index })} />
          ))
        }
        renderInput={(params) => (
          <TextField
            {...params}
            size="small"
            margin="dense"
            variant="outlined"
            label="Subcategories"
            placeholder="Hit 'Enter' to set Subcategory" />
        )}
      />

      <div className="form-row">
        <div className="col">
          <TextField
            label="Description"
            variant="outlined"
            margin="dense"
            fullWidth
            multiline
            placeholder="Description"
            name="description"
            value={form_inputs.description || ''}
            onChange={handleChange}
          />
        </div>
      </div>

      <div className="text-center mt-3">
        <Button className="mr-2" variant="outlined" color="primary" size="small" onClick={handleClose} disabled={saving}>
          Cancel
        </Button>
        <Button variant="contained" color="primary" size="small" onClick={handleSave} disabled={saving}>
          Save {saving && <DataSaving/>}
        </Button>
      </div>

    </React.Fragment>
  )
})
