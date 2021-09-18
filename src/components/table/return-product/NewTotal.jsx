import React from 'react';
import {
  Button,
  FormControl,
  Select,
  MenuItem,
  InputLabel
} from '@material-ui/core';
import { AnimateOnChange } from 'react-animation';
import DiscountField from 'components/input/Discount';
import { DataSaving } from 'components/loading/DataSaving';
import {transactionMedia} from 'pages/configuration/server_action';


const NewTotal = ({percent, total, return_by, handleDiscountType, handleChange, handleSubmit, saving, saved})=>{
  const [head_list, setHeadList] = React.useState([])

  React.useEffect(()=>{
    transactionMedia()
      .then(resp => setHeadList(resp.query))
  }, [])

  return(
    <div className="card p-3">
      <h6 className="text-center text-primary"><strong>Return Information</strong></h6>
      <hr className="w-50 m-auto"/>
      <div className="row mt-3">
        <div className="col text-right"><b>Return Price :</b></div>
        <div className="col">
          <h5 className="m-0 text-secondary">
            <AnimateOnChange animationIn="bounceIn" animationOut="bounceOut" durationOut={300}>
              {total?total.toLocaleString():0}
            </AnimateOnChange>
          </h5>
        </div>
      </div>
      <div className="row mt-3">
        <div className="col-md-6">
          <div className="d-flex justify-content-center">
            <DiscountField
              percent={percent}
              handleDiscountType={handleDiscountType}
              handleInputs={handleChange}
            />
          </div>
        </div>
        <div className="col-md-6">
          <div className="col d-flex justify-content-end">
            <FormControl size="small" fullWidth variant="outlined" margin="dense" required>
              <InputLabel id="return-by-label">Return By</InputLabel>
              <Select
                labelId="return-by-label"
                label="Return By"
                name="return_by"
                value={return_by}
                onChange={handleChange}
              >
                {head_list.map(val => (
                  <MenuItem key={val.ac_head_id} value={val.ac_head_id}>{val.ac_head_name}</MenuItem>
                ))}
              </Select>
            </FormControl>
          </div>
        </div>
      </div>
      <div className="d-flex justify-content-center mt-3">
        {!saved &&
          <Button className="w-25" variant="contained" size="small" color="primary" onClick={handleSubmit} disabled={saving || !return_by || total===0}>
            Submit {saving && <DataSaving/>}
          </Button>
        }
      </div>
    </div>
  )
}

export default NewTotal;