import React from 'react';
import {TextField, Button} from '@material-ui/core';
import SupplierSearch from 'components/input/SupplierSearch';


export default React.memo(({form_inputs, handleInputs, require_fields, confirmed, handleSuplierConfirmed})=>{
  const [supplier_info, setSupplierInfo] = React.useState(null)

  React.useEffect(()=>{
    setSupplierInfo(null)
  }, [])

  
  return(
    <div className="card p-2">
      <div className="form-row d-flex justify-content-center">
        <div className="col-md-4 col-sm-7">
          <SupplierSearch
            name="supplier_code"
            label="Supplier"
            margin="dense"
            required
            disabled={confirmed}
            error={require_fields.supplier_code}
            handleInputs={handleInputs}
            setSupplierInfo={setSupplierInfo}
          />
        </div>
        {supplier_info &&
          <div className="col-xl-3 col-md-4 col sm-5">
            <div className="d-flex flex-column align-items-center justify-content-center">
              <span><strong>Company : </strong>&nbsp;{supplier_info.company}</span>
              <hr className="w-50 p-0 m-0"/>
              <span><strong>Mobile : </strong>&nbsp;{supplier_info.mobile}</span>
            </div>
          </div>
        }  
        <div className="col-md-2 col-sm-3">
          <TextField                                     // entry date
            label="Entry Date"
            name="entry_date"
            type="date"
            fullWidth
            required
            variant="outlined"
            size='small'
            margin="dense"
            defaultValue={form_inputs.entry_date}
            onChange={handleInputs}
            InputLabelProps={{shrink: true}}
          />
        </div>
        {supplier_info &&
          <div className="d-flex align-items-center">
            <Button variant="contained" size="small" color="primary" onClick={handleSuplierConfirmed}>
              Confirm
            </Button>
          </div>
        }
      </div>
    </div>
  )
})